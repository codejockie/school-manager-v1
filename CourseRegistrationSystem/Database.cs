using CourseRegistrationSystem.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System.Web;

namespace CourseRegistrationSystem
{
    public static class Database
    {
        private static ISessionFactory _sessionFactory;
        private const string SessionKey = "CourseRegistrationSystem.Database.SessionKey";

        public static ISession Session
        {
            get { return (ISession) HttpContext.Current.Items[SessionKey]; }
        }

        public static void Configure()
        {
            var config = new Configuration();

            // configure the connection string
            config.Configure();

            // add our mappings
            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();
            mapper.AddMapping<CourseMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            // create session factory
            _sessionFactory = config.BuildSessionFactory();
        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;

            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}
