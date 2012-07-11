using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Core.Domain.Persistence;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Shouldly;

namespace Core.TestHelper.Persistence
{
    public abstract class base_when_generating_database_schema
    {
        protected string DatabaseSchemaFileName;
        protected Assembly[] AssembliesToMap;
        protected IEnumerable<Type> IncludeBaseTypes;
        protected Type[] DiscriminatedTypes;
        protected Assembly AssemblyWithConventions;


        protected abstract void SetUp();

        [TestFixtureSetUp]
        public void Context()
        {
            SetUp();
            File.Delete(DatabaseSchemaFileName);
            var nHibernateConfigurator = new NHibernateConfigurator(AssembliesToMap, IncludeBaseTypes, DiscriminatedTypes, AssemblyWithConventions);
            var schemaExport = new SchemaExport(nHibernateConfigurator.GetConfiguration());
            schemaExport.SetOutputFile(DatabaseSchemaFileName);
            schemaExport.Create(true, false);
        }

        [Test]
        public void database_schema_generated()
        {
            File.Exists(DatabaseSchemaFileName).ShouldBe(true);
            (new FileInfo(DatabaseSchemaFileName).Length).ShouldBeGreaterThan(0);
        }
    }
}