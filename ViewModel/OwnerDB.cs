// FILE: OwnerDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class OwnerDB : BaseDB
    {
        public OwnerList SelectAll()
        {
            command.CommandText = @"
        SELECT 
            o.ID, 
            o.IsActive,
            u.FullName,
            u.Email,
            u.Phone,
            u.PassHash
        FROM Owners AS o
        INNER JOIN Users AS u ON o.ID = u.ID
    ";

            return new OwnerList(base.Select());
        }

        public static Owner SelectById(int id)
        {
            OwnerDB db = new OwnerDB();
            db.command.CommandText = "SELECT * FROM Owners WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            OwnerList list = new OwnerList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Owner o = entity as Owner ?? new Owner();

            // שדות מ-Owner
            o.IsActive = reader["IsActive"] != DBNull.Value && Convert.ToBoolean(reader["IsActive"]);

            // שדות מ-User
            o.Id = Convert.ToInt32(reader["ID"]);
            o.FullName = reader["FullName"].ToString();
            o.Email = reader["Email"].ToString();
            o.Phone = reader["Phone"].ToString();
            o.PassHash = reader["PassHash"]?.ToString();

            return o;
        }

        public override BaseEntity NewEntity() => new Owner();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Owner o) return;
            cmd.CommandText = "DELETE FROM Owners WHERE ID=@id";
            cmd.Parameters.Add(new OleDbParameter("@id", o.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Owner o) return;
            cmd.CommandText = "INSERT INTO Owners (IsActive) VALUES (@isActive)";
            cmd.Parameters.Add(new OleDbParameter("@isActive", o.IsActive));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Owner o) return;
            cmd.CommandText = "UPDATE Owners SET IsActive=? WHERE ID=@id";
            cmd.Parameters.Add(new OleDbParameter("@isActive", o.IsActive));
            cmd.Parameters.Add(new OleDbParameter("@id", o.Id));
        }
    }
}
