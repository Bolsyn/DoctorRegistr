using DoctorRegistr.Blank;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DoctorRegistr.Data
{
    public class ScheduleDataAccess : DBDataAccess<Schedule>
    {
        public override void Delete(Schedule entity)
        {
            string deleteSqlScript = $"Delete From Schedules where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Schedule entity)
        {
            string insertSqlScript = "Insert into Schedules values (@TimesOfVisitsId)";

            using (var transaction = connection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = insertSqlScript;

                try
                {
                    command.Transaction = transaction;

                    var nameParameter = factory.CreateParameter();
                    nameParameter.DbType = System.Data.DbType.Guid;
                    nameParameter.Value = entity.TimesOfVisitsId;
                    nameParameter.ParameterName = "TimesOfVisitsId";

                    command.Parameters.Add(nameParameter);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }

        public override ICollection<Schedule> Select()
        {
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = "select * from Schedules";

            var dataReader = command.ExecuteReader();

            var schedule = new List<Schedule>();

            while (dataReader.Read())
            {
                var tempId = new List<Guid>();
                tempId.Add(Guid.Parse(dataReader["TimesOfVisitsId"].ToString()));

                schedule.Add(new Schedule
                {
                    Id = Guid.Parse(dataReader["Id"].ToString()),
                    TimesOfVisitsId = tempId
                });
            }

            dataReader.Close();
            command.Dispose();
            return schedule;
        }

        public override void Update(Schedule entity)
        {
            string updateSqlScript = $"update Schedules Set TimesOfVisitsId = @TimesOfVisitsId where Id = {entity.Id}";

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
                    visitParameter.Value = entity.TimesOfVisitsId;
                    visitParameter.ParameterName = "TimesOfVisitsId";

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
