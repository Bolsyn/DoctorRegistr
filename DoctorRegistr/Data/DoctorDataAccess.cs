using DoctorRegistr.Blank;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DoctorRegistr.Data
{
    public class DoctorDataAccess : DBDataAccess<Doctor>
    {
        public override void Delete(Doctor entity)
        {
            string deleteSqlScript = $"Delete From Doctors where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Doctor entity)
        {
            string insertSqlScript = "Insert into Doctors values (@FullName, @SpecialId, @ScheduleId)";

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

                    var specialParameter = factory.CreateParameter();
                    specialParameter.DbType = System.Data.DbType.Guid;
                    specialParameter.Value = entity.SpecialId;
                    specialParameter.ParameterName = "SpecialId";

                    command.Parameters.Add(specialParameter);

                    var scheduleParameter = factory.CreateParameter();
                    scheduleParameter.DbType = System.Data.DbType.Guid;
                    scheduleParameter.Value = entity.ScheduleId;
                    scheduleParameter.ParameterName = "ScheduleId";

                    command.Parameters.Add(scheduleParameter);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }

        public override ICollection<Doctor> Select()
        {
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = "select * from Doctors";

            var dataReader = command.ExecuteReader();

            var doctor = new List<Doctor>();

            while (dataReader.Read())
            {
                doctor.Add(new Doctor
                {
                    Id = Guid.Parse(dataReader["Id"].ToString()),
                    FullName = dataReader["FullName"].ToString(),
                    SpecialId = Guid.Parse(dataReader["SpecialId"].ToString()),
                    ScheduleId = Guid.Parse(dataReader["ScheduleId"].ToString())
                });
            }

            dataReader.Close();
            command.Dispose();
            return doctor;
        }

        public override void Update(Doctor entity)
        {
            string updateSqlScript = $"update Doctors Set FullName = @FullName Set SpecialId = @SpecialId Set ScheduleId = @ScheduleId where Id = {entity.Id}";

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

                    var specialParameter = factory.CreateParameter();
                    specialParameter.DbType = System.Data.DbType.Guid;
                    specialParameter.Value = entity.SpecialId;
                    specialParameter.ParameterName = "SpecialId";

                    command.Parameters.Add(specialParameter);

                    var scheduleParameter = factory.CreateParameter();
                    scheduleParameter.DbType = System.Data.DbType.Guid;
                    scheduleParameter.Value = entity.ScheduleId;
                    scheduleParameter.ParameterName = "ScheduleId";

                    command.Parameters.Add(scheduleParameter);

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
