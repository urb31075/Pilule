// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateForm.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   Defines the NHibernateForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NHibernateORM
{
    //https://stackoverflow.com/questions/10336816/creating-an-sqlite-database-with-nhibernate-but-just-once
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;
    using System.Windows.Forms;

    using NHibernate;
    using NHibernate.Cfg;

    /// <summary>
    /// The n hibernate form.
    /// </summary>
    public partial class NHibernateForm : Form
    {
        public ISession Sess;
        public Configuration Cfg;
        public SQLiteConnection Connection;
        private const string ConnectionString = @"Data Source=d:\config.sqlite;Pooling=true;FailIfMissing=false; BinaryGUID=false;New=false;Compress=true;Version=3";
        public NHibernateForm()
        {
            this.InitializeComponent();
        }

        private void InitButtonClick(object sender, EventArgs e)
        {
            this.Init();
            this.BuildSchema();
            this.Insert();
            this.Retrieve();
            this.Sess.Close();
            this.Sess = null;
        }

        public void Init()
        {
            // Initialize NHibernate
            this.Cfg = new Configuration();
            this.Cfg.Configure();
            IDictionary<string, string> props = new Dictionary<string, string>();
            props.Add("Connection.connection_string", ConnectionString);
            props.Add("Connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            props.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            props.Add("proxyfactory.factory_class", "NHibernate.Bytecode.DefaultProxyFactoryFactory, NHibernate"); //    
            props.Add("query.substitutions", "true=1;false=0");
            props.Add("show_sql", "false");
            this.Cfg.SetProperties(props);
            this.Cfg.AddAssembly(typeof(Catalog).Assembly);
            this.Connection = new SQLiteConnection(ConnectionString);
            this.Connection.Open();

            var sessions = this.Cfg.BuildSessionFactory(); 
            this.Sess = sessions.OpenSession();
        }

        private void BuildSchema()
        {
            //NHibernate.Tool.hbm2ddl.SchemaExport schemaExport = new NHibernate.Tool.hbm2ddl.SchemaExport(Cfg);
            //schemaExport.Execute(false, true, false, Connection, null);

            NHibernate.Tool.hbm2ddl.SchemaUpdate schemaUpdate = new NHibernate.Tool.hbm2ddl.SchemaUpdate(this.Cfg);
            schemaUpdate.Execute(false, true);

        }

        public void Insert()
        {
            // Create a Person...
            var person = new Catalog
            {
                author = "Almudena",
                book = "Pamplinas"
            };

            // And save it to the database
            this.Sess.Save(person);
            this.Sess.Flush();
        }

        public void Retrieve()
        {
            var q = this.Sess.CreateQuery("from Catalog");
            foreach (var p in q.List().Cast<Catalog>())
            {
               this.InfoListBox.Items.Add($"{p.author}, {p.book}, {p.id}");
            }
        }
    }
}