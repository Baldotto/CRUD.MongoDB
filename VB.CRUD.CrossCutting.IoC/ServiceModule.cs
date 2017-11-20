using System;
using Autofac;
using VB.CRUD.Infra.Data.MongoDB;
using VB.CRUD.Infra.Data.MongoDB.Repositories;
using VB.CRUD.Domain.Interfaces;

namespace VB.CRUD.CrossCutting.IoC
{
    public class ServiceModule : Module
    {

        public readonly string _connectionString;
        public readonly string _database;

        public ServiceModule(string connectionString, string database)
        {
            _connectionString = connectionString;
            _database = database;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new MongoContext(_connectionString, _database))
                .As<MongoContext>().SingleInstance();

            builder.RegisterType<PostRepository>()
                .As<IPostRepository>()
                .InstancePerLifetimeScope();
        }

    }
}
