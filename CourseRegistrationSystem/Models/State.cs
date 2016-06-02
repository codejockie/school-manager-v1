using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistrationSystem.Models
{
    public class State
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class StateMap : ClassMapping<State>
    {
        public StateMap()
        {
            Table("states");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Name, x => x.NotNullable(true));
        }
    }
}
