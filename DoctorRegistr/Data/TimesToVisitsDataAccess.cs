using DoctorRegistr.Blank;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DoctorRegistr.Data
{
    public class TimesToVisitsDataAccess : DBDataAccess<TimesToVisits>
    {
        public override void Delete(TimesToVisits entity)
        {
            string deleteSqlScript = $"Delete From TimesToVisits where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(TimesToVisits entity)
        {
            string insertSqlScript = "Insert into TimesToVisits values (@TimeOfVisit,)";

            using (var transaction = connection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = insertSqlScript;

                try
                {
                    command.Transaction = transaction;

                    var visitParameter = factory.CreateParameter();
                    visitParameter.DbType = System.Data.DbType.String;
                    visitParameter.Value = entity.TimeOfVisit;
                    visitParameter.ParameterName = "TimeOfVisit";

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

        public override ICollection<TimesToVisits> Select()
        {
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = "select * from TimesToVisits";

            var dataReader = command.ExecuteReader();

            var timeOfVisit = new List<TimesToVisits>();

            while (dataReader.Read())
            {
                timeOfVisit.Add(new TimesToVisits
                {
                    Id = Guid.Parse(dataReader["Id"].ToString()),
                    TimeOfVisit = DateTime.Parse(dataReader["TimeOfVisit"].ToString())
                });
            }

            dataReader.Close();
            command.Dispose();
            return timeOfVisit;
        }

        public override void Update(TimesToVisits entity)
        {
            string updateSqlScript = $"update TimesToVisits Set TimeOfVisit = @TimeOfVisit where Id = {entity.Id}";

            using (var transaction = connection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = updateSqlScript;

                try
                {
                    command.Transaction = transaction;

                    var visitParameter = factory.CreateParameter();
                    visitParameter.DbType = System.Data.DbType.String;
                    visitParameter.Value = entity.TimeOfVisit;
                    visitParameter.ParameterName = "TimeOfVisit";

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
