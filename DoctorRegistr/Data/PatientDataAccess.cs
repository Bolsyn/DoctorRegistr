using DoctorRegistr.Blank;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DoctorRegistr.Data
{
    public class PatientDataAccess : DBDataAccess<Patient>
    {
        public override void Delete(Patient entity)
        {
            string deleteSqlScript = $"Delete From Patients where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Patient entity)
        {
            string insertSqlScript = "Insert into Patients values (@FullName, @TimeOfVisitId)";

            using (var transaction = connection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = insertSqlScript;

                try
                {
                    command.Transaction = transaction;

                    var nameParameter = factory.CreateParameter();
                    nameParameter.DbType = System.Data.DbType.String;
                    nameParameter.Value = entity.FullName;
                    nameParameter.ParameterName = "FullName";

                    command.Parameters.Add(nameParameter);

                    var visitParameter = factory.CreateParameter();
                    visitParameter.DbType = System.Data.DbType.Guid;
                    visitParameter.Value = entity.TimeOfVisitId;
                    visitParameter.ParameterName = "TimeToVisitId";

                    command.Parameters.Add(visitParameter);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }

        public override ICollection<Patient> Select()
        {
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = "select * from Patients";

            var dataReader = command.ExecuteReader();

            var pacient = new List<Patient>();

            while (dataReader.Read())
            {
                pacient.Add(new Patient
                {
                    Id = Guid.Parse(dataReader["Id"].ToString()),
                    FullName = dataReader["FullName"].ToString(),
                    TimeOfVisitId = Guid.Parse(dataReader["TimeOfVisitId"].ToString()),
                });
            }

            dataReader.Close();
            command.Dispose();
            return pacient;
        }

        public override void Update(Patient entity)
        {
            string updateSqlScript = $"update Patients Set FullName = @FullName Set TimeOfVisitId = @TimeOfVisitId  where Id = {entity.Id}";

            using (var transaction = connection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = updateSqlScript;

                try
                {
                    command.Transaction = transaction;

                    var nameParameter = factory.CreateParameter();
                    nameParameter.DbType = System.Data.DbType.String;
                    nameParameter.Value = entity.FullName;
                    nameParameter.ParameterName = "FullName";

                    command.Parameters.Add(nameParameter);

                    var visitParameter = factory.CreateParameter();
                    visitParameter.DbType = System.Data.DbType.Guid;
                    visitParameter.Value = entity.TimeOfVisitId;
                    visitParameter.ParameterName = "TimeOfVisitId";

                    command.Parameters.Add(visitParameter);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }
    }
}
