// FILE: RoomAvailabilityDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class RoomAvailabilityDB : BaseDB
    {
        public RoomAvailabilityList SelectAll()
        {
            command.CommandText = "SELECT * FROM RoomAvailability";
            return new RoomAvailabilityList(base.Select());
        }

        public static RoomAvailability SelectById(int id)
        {
            RoomAvailabilityDB db = new RoomAvailabilityDB();
            db.command.CommandText = "SELECT * FROM RoomAvailability WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            RoomAvailabilityList list = new RoomAvailabilityList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            RoomAvailability ra = entity as RoomAvailability ?? new RoomAvailability();

            if (reader["RoomID"] != DBNull.Value) ra.Room = RoomDB.SelectById(Convert.ToInt32(reader["RoomID"]));
            if (reader["StartDate"] != DBNull.Value) ra.StartDate = Convert.ToDateTime(reader["StartDate"]);
            if (reader["EndDate"] != DBNull.Value) ra.EndDate = Convert.ToDateTime(reader["EndDate"]);

            base.CreateModel(ra);
            return ra;
        }

        public override BaseEntity NewEntity() => new RoomAvailability();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not RoomAvailability ra) return;
            cmd.CommandText = "DELETE FROM RoomAvailability WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", ra.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not RoomAvailability ra) return;

            cmd.CommandText =
                "INSERT INTO RoomAvailability (RoomID, StartDate, EndDate) VALUES (?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(ra.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@start", ra.StartDate));
            cmd.Parameters.Add(new OleDbParameter("@end", ra.EndDate));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not RoomAvailability ra) return;

            cmd.CommandText =
                "UPDATE RoomAvailability SET RoomID=?, StartDate=?, EndDate=? WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(ra.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@start", ra.StartDate));
            cmd.Parameters.Add(new OleDbParameter("@end", ra.EndDate));
            cmd.Parameters.Add(new OleDbParameter("@id", ra.Id));
        }
    }
}
