using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CourseRegistrationSystem.Infrastructure
{
    public static class HtmlDropDownExtensions
    {
        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };
        public static MvcHtmlString CSEnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                from value in values
                select new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = (value.Equals(selectedValue))
                };

            return htmlHelper.DropDownList(name, items);
        }

        public static MvcHtmlString CSEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            TypeConverter converter = TypeDescriptor.GetConverter(enumType);

            IEnumerable<SelectListItem> items =
                values.Select(value => new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                });

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items);
            }

            return htmlHelper.DropDownListFor(expression, items);
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;
            Type underlyingType = Nullable.GetUnderlyingType(realModelType);

            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }

            return realModelType;
        }
    }

    public class PascalCaseWordSplittingEnumConverter : EnumConverter

    {

        public PascalCaseWordSplittingEnumConverter(Type type)

            : base(type)

        {

        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)

        {

            if (destinationType == typeof(string))

            {

                string stringValue = (string)base.ConvertTo(context, culture, value, destinationType);

                stringValue = SplitString(stringValue);

                return stringValue;

            }

            return base.ConvertTo(context, culture, value, destinationType);

        }

        public string SplitString(string stringValue)

        {

            StringBuilder buf = new StringBuilder(stringValue);

            // assume first letter is upper!

            bool lastWasUpper = true;

            int lastSpaceIndex = -1;

            for (int i = 1; i < buf.Length; i++)

            {
                bool isUpper = char.IsUpper(buf[i]);

                if (isUpper & !lastWasUpper)
                {
                    buf.Insert(i, ' ');
                    lastSpaceIndex = i;
                }

                if (!isUpper && lastWasUpper)
                {
                    if (lastSpaceIndex != i - 2)
                    {
                        buf.Insert(i - 1, ' ');
                        lastSpaceIndex = i - 1;
                    }
                }

                lastWasUpper = isUpper;
            }

            return buf.ToString();
        }

    }
}