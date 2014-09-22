using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllThingsConsidered
{

    class Program
    {

        //private static InMemorySessionFactoryProvider instance;
        //public static InMemorySessionFactoryProvider Instance
        //{
        //    get { return instance ?? (instance = new InMemorySessionFactoryProvider()); }
        //}

        private static ISessionFactory sessionFactory;
        private static Configuration configuration;

        static void Main(string[] args)
        {
            Initialize();
            Console.WriteLine("done");
            Console.ReadKey();
        }

        public static void Initialize()
        {
            var sessionFactory = CreateSessionFactory();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
            .Database(SQLiteConfiguration.Standard.UsingFile("test.sq").ShowSql())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Test>())
            .ExposeConfiguration(cfg => configuration = cfg)
            .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            ISession session = sessionFactory.OpenSession();

            var export = new SchemaExport(configuration);
            export.Execute(true, true, false, session.Connection, null);

            return session;
        }
    }
}
