// FILE: RoomAvailabilityDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class RoomAvailabilityDB : BaseDB
    {
        // =========================
        // SELECT ALL
        // =========================
        public RoomAvailabilityList SelectAll()
        {
            command.CommandText = "SELECT * FROM RoomAvailability";
            command.Parameters.Clear();
            return new RoomAvailabilityList(base.Select());
        }

        // =========================
        // SELECT BY ID
        // =========================
        public static RoomAvailability SelectById(int id)
        {
            RoomAvailabilityDB db = new RoomAvailabilityDB();
            db.command.CommandText = "SELECT * FROM RoomAvailability WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add("@id", OleDbType.Integer).Value = id;

            RoomAvailabilityList list = new RoomAvailabilityList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        // =========================
        // CREATE MODEL
        // =========================
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            RoomAvailability ra = entity as RoomAvailability ?? new RoomAvailability();

            ra.Id = Convert.ToInt32(reader["ID"]);

            if (reader["RoomID"] != DBNull.Value)
                ra.RoomID = Convert.ToInt32(reader["RoomID"]);

            if (reader["StartDate"] != DBNull.Value)
                ra.StartDate = Convert.ToDateTime(reader["StartDate"]);

            if (reader["EndDate"] != DBNull.Value)
                ra.EndDate = Convert.ToDateTime(reader["EndDate"]);

            return ra;
        }

        public override BaseEntity NewEntity() => new RoomAvailability();

        // =========================
        // INSERT
        // =========================
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            RoomAvailability ra = (RoomAvailability)entity;

            cmd.CommandText =
                "INSERT INTO RoomAvailability (RoomID, StartDate, EndDate) VALUES (?,?,?)";

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@roomId", OleDbType.Integer).Value = ra.RoomID;
            cmd.Parameters.Add("@start", OleDbType.Date).Value = ra.StartDate;
            cmd.Parameters.Add("@end", OleDbType.Date).Value = ra.EndDate;
        }

        // =========================
        // UPDATE
        // =========================
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            RoomAvailability ra = (RoomAvailability)entity;

            cmd.CommandText =
                "UPDATE RoomAvailability SET RoomID=?, StartDate=?, EndDate=? WHERE ID=?";

            cmd.Parameters.Clear();
            cmd.Parameters.Add("@roomId", OleDbType.Integer).Value = ra.RoomID;
            cmd.Parameters.Add("@start", OleDbType.Date).Value = ra.StartDate;
            cmd.Parameters.Add("@end", OleDbType.Date).Value = ra.EndDate;
            cmd.Parameters.Add("@id", OleDbType.Integer).Value = ra.Id;
        }

        // =========================
        // DELETE
        // =========================
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            RoomAvailability ra = (RoomAvailability)entity;

            cmd.CommandText = "DELETE FROM RoomAvailability WHERE ID=?";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", OleDbType.Integer).Value = ra.Id;
        }
    }
}