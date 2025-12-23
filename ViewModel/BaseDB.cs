using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public abstract class BaseDB
    {
        // ---- Connection ----
        protected static readonly string connectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            System.IO.Path.GetFullPath(
                System.Reflection.Assembly.GetExecutingAssembly().Location +
                "/../../../../../ViewModel/sherio.accdb");

        protected static OleDbConnection connection;
        protected OleDbCommand command;
        protected OleDbDataReader reader;

        // ✅ כלי עזר ל-NULL → DBNull
        protected static object DbVal(object value) => value ?? DBNull.Value;

        public BaseDB()
        {
            connection ??= new OleDbConnection(connectionString);
            command = new OleDbCommand { Connection = connection };
        }

        // ישות חדשה לכל DB יורש
        public abstract BaseEntity NewEntity();

        // ---- SELECT (sync) ----
        protected List<BaseEntity> Select()
        {
            var list = new List<BaseEntity>();
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BaseEntity entity = NewEntity();
                    list.Add(CreateModel(entity));
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                reader?.Close();
                // אם אתה רוצה לסגור חיבור בכל פעם, בטל הערה בשורה הבאה:
                // if (connection.State == ConnectionState.Open) connection.Close();
            }
            return list;
        }

        // ---- SELECT (async) ----
        protected async Task<List<BaseEntity>> SelectAsync(string sqlStr)
        {
            var list = new List<BaseEntity>();

            // נשתמש בחיבור חדש מקומי (לא לשתף חיבור אחד בין Threads)
            using (var localConn = new OleDbConnection(connectionString))
            using (var localCmd = new OleDbCommand(sqlStr, localConn))
            {
                try
                {
                    await localConn.OpenAsync();
                    using (var r = await localCmd.ExecuteReaderAsync())
                    {
                        while (await r.ReadAsync())
                        {
                            // נרצה להשתמש ב-reader הקיים של המחלקה ל-CreateModel
                            this.reader = (OleDbDataReader)r;
                            BaseEntity entity = NewEntity();
                            list.Add(CreateModel(entity));
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message + "\nSQL:" + sqlStr);
                }
                finally
                {
                    this.reader = null;
                }
            }
            return list;
        }

        // יצירת מודל מה-Reader
        protected virtual BaseEntity CreateModel(BaseEntity entity)
        {
            entity.Id = Convert.ToInt32(reader["id"]);
            return entity;
        }

        // ---- Change tracking ----
        protected abstract void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd);
        public static List<ChangeEntity> deleted = new List<ChangeEntity>();

        public virtual void Delete(BaseEntity entity)
        {
            BaseEntity reqEntity = NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
                deleted.Add(new ChangeEntity(CreateDeletedSQL, entity));
        }

        protected abstract void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd); // שימור שם המקור
        public static List<ChangeEntity> inserted = new List<ChangeEntity>();

        public virtual void Insert(BaseEntity entity)
        {
            BaseEntity reqEntity = NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
                inserted.Add(new ChangeEntity(CreateInsertdSQL, entity));
        }

        protected abstract void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd);
        public static List<ChangeEntity> updated = new List<ChangeEntity>();

        public virtual void Update(BaseEntity entity)
        {
            BaseEntity reqEntity = NewEntity();
            if (entity != null && entity.GetType() == reqEntity.GetType())
                updated.Add(new ChangeEntity(CreateUpdatedSQL, entity));
        }

        // ---- SAVE CHANGES ----
        public int SaveChanges()
        {
            OleDbTransaction trans = null;
            int recordsAffected = 0;

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                trans = connection.BeginTransaction();
                command.Transaction = trans;

                // INSERT
                foreach (var ce in inserted)
                {
                    command.Parameters.Clear();
                    ce.CreateSql(ce.Entity, command);
                    recordsAffected += command.ExecuteNonQuery();

                    // השבת Identity
                    command.Parameters.Clear();
                    command.CommandText = "SELECT @@IDENTITY";
                    ce.Entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }

                // UPDATE
                foreach (var ce in updated)
                {
                    command.Parameters.Clear();
                    ce.CreateSql(ce.Entity, command);
                    recordsAffected += command.ExecuteNonQuery();
                }

                // DELETE
                foreach (var ce in deleted)
                {
                    command.Parameters.Clear();
                    ce.CreateSql(ce.Entity, command);
                    recordsAffected += command.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                try { trans?.Rollback(); } catch { /* ignore */ }
                System.Diagnostics.Debug.WriteLine(ex.Message + "\nSQL:" + command.CommandText);
            }
            finally
            {
                inserted.Clear();
                updated.Clear();
                deleted.Clear();

                // אם צריך: סגור את החיבור כאן
                // if (connection.State == ConnectionState.Open) connection.Close();
            }

            return recordsAffected;
        }

        // ---- ChangeEntity ----
        public class ChangeEntity
        {
            public BaseEntity Entity { get; set; }
            public CreateSql CreateSql { get; set; }

            public ChangeEntity(CreateSql createSql, BaseEntity entity)
            {
                CreateSql = createSql;
                Entity = entity;
            }
        }

        public delegate void CreateSql(BaseEntity entity, OleDbCommand command);
    }
}
