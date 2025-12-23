// FILE: ReviewDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class ReviewDB : BaseDB
    {
        public ReviewList SelectAll()
        {
            command.CommandText = "SELECT * FROM Reviews";
            return new ReviewList(base.Select());
        }

        public static Review SelectById(int id)
        {
            ReviewDB db = new ReviewDB();
            db.command.CommandText = "SELECT * FROM Reviews WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            ReviewList list = new ReviewList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Review r = entity as Review ?? new Review();

            if (reader["UserID"] != DBNull.Value) r.User = UserDB.SelectById(Convert.ToInt32(reader["UserID"]));
            if (reader["RoomID"] != DBNull.Value) r.Room = RoomDB.SelectById(Convert.ToInt32(reader["RoomID"]));
            if (reader["Rating"] != DBNull.Value) r.Rating = Convert.ToInt32(reader["Rating"]);
            r.Comment = reader["Comment"].ToString();
            if (reader["CreatedAt"] != DBNull.Value) r.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

            base.CreateModel(r);
            return r;
        }

        public override BaseEntity NewEntity() => new Review();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Review r) return;
            cmd.CommandText = "DELETE FROM Reviews WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", r.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Review r) return;

            cmd.CommandText =
                "INSERT INTO Reviews (UserID, RoomID, Rating, Comment, CreatedAt) " +
                "VALUES (?,?,?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@userId", DbVal(r.User?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(r.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@rating", r.Rating));
            cmd.Parameters.Add(new OleDbParameter("@comment", r.Comment));
            cmd.Parameters.Add(new OleDbParameter("@created", r.CreatedAt));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Review r) return;

            cmd.CommandText =
                "UPDATE Reviews SET UserID=?, RoomID=?, Rating=?, Comment=?, CreatedAt=? WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@userId", DbVal(r.User?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(r.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@rating", r.Rating));
            cmd.Parameters.Add(new OleDbParameter("@comment", r.Comment));
            cmd.Parameters.Add(new OleDbParameter("@created", r.CreatedAt));
            cmd.Parameters.Add(new OleDbParameter("@id", r.Id));
        }
    }
}
