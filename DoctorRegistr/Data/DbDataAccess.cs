using DoctorRegistr.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DoctorRegistr.Data
{
    public abstract class DBDataAccess<T> : IDisposable
    {
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;


        public DBDataAccess()
        {
            factory = DbProviderFactories.GetFactory("DoctorRegistrProvider");

            connection = factory.CreateConnection();

            connection.ConnectionString = ConfigurationService.Configuration["DataAccessConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public abstract void Insert(T entity);

        public abstract void Update(T entity);

        public abstract void Delete(T entity);

        public abstract ICollection<T> Select();

        public void ExecuteTranaction(params DbCommand[] commands)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var command in commands)
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
