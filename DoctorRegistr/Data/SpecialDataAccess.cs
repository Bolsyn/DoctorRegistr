using DoctorRegistr.Blank;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DoctorRegistr.Data
{
    public class SpecialDataAccess : DBDataAccess<Special>
    {
        public override void Delete(Special entity)
        {
            string deleteSqlScript = $"Delete From Specials where Id = {entity.Id}";
            using (var command = factory.CreateCommand())
            {
                command.Connection = connection;
                command.CommandText = deleteSqlScript;
                command.ExecuteNonQuery();
            }
        }

        public override void Insert(Special entity)
        {
            string insertSqlScript = "Insert into Specials values (@Name)";

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
                    nameParameter.Value = entity.Name;
                    nameParameter.ParameterName = "Name";

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

        public override ICollection<Special> Select()
        {
            var command = factory.CreateCommand();
            command.Connection = connection;
            command.CommandText = "select * from Specials";

            var dataReader = command.ExecuteReader();

            var special = new List<Special>();

            while (dataReader.Read())
            {
                special.Add(new Special
                {
                    Id = Guid.Parse(dataReader["Id"].ToString()),
                    Name = dataReader["Name"].ToString(),
                });
            }

            dataReader.Close();
            command.Dispose();
            return special;
        }

        public override void Update(Special entity)
        {
            string updateSqlScript = $"update Specials Set Name = @Name where Id = {entity.Id}";

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
                    nameParameter.Value = entity.Name;
                    nameParameter.ParameterName = "Name";

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
    }
}
