// FILE: BookingDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class BookingDB : BaseDB
    {
        public BookingList SelectAll()
        {
            command.CommandText = "SELECT * FROM Bookings";
            return new BookingList(base.Select());
        }

        public static Booking SelectById(int id)
        {
            BookingDB db = new BookingDB();
            db.command.CommandText = "SELECT * FROM Bookings WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            BookingList list = new BookingList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Booking b = entity as Booking ?? new Booking();

            if (reader["UserID"] != DBNull.Value) b.User = UserDB.SelectById(Convert.ToInt32(reader["UserID"]));
            if (reader["RoomID"] != DBNull.Value) b.Room = RoomDB.SelectById(Convert.ToInt32(reader["RoomID"]));
            if (reader["CreatedAt"] != DBNull.Value) b.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            if (reader["StartDate"] != DBNull.Value) b.StartDate = Convert.ToDateTime(reader["StartDate"]);
            if (reader["EndDate"] != DBNull.Value) b.EndDate = Convert.ToDateTime(reader["EndDate"]);
            if (reader["AdultCount"] != DBNull.Value) b.AdultCount = Convert.ToInt32(reader["AdultCount"]);
            if (reader["ChildCount"] != DBNull.Value) b.ChildCount = Convert.ToInt32(reader["ChildCount"]);
            b.Status = reader["Status"].ToString();

            base.CreateModel(b);
            return b;
        }

        public override BaseEntity NewEntity() => new Booking();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Booking b) return;
            cmd.CommandText = "DELETE FROM Bookings WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", b.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Booking b) return;

            cmd.CommandText =
                "INSERT INTO Bookings (UserID, RoomID, CreatedAt, StartDate, EndDate, AdultCount, ChildCount, Status) " +
                "VALUES (?,?,?,?,?,?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@userId", DbVal(b.User?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(b.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@created", b.CreatedAt));
            cmd.Parameters.Add(new OleDbParameter("@start", b.StartDate));
            cmd.Parameters.Add(new OleDbParameter("@end", b.EndDate));
            cmd.Parameters.Add(new OleDbParameter("@adults", b.AdultCount));
            cmd.Parameters.Add(new OleDbParameter("@kids", b.ChildCount));
            cmd.Parameters.Add(new OleDbParameter("@status", b.Status));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Booking b) return;

            cmd.CommandText =
                "UPDATE Bookings SET UserID=?, RoomID=?, CreatedAt=?, StartDate=?, EndDate=?, AdultCount=?, ChildCount=?, Status=? " +
                "WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@userId", DbVal(b.User?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@roomId", DbVal(b.Room?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@created", b.CreatedAt));
            cmd.Parameters.Add(new OleDbParameter("@start", b.StartDate));
            cmd.Parameters.Add(new OleDbParameter("@end", b.EndDate));
            cmd.Parameters.Add(new OleDbParameter("@adults", b.AdultCount));
            cmd.Parameters.Add(new OleDbParameter("@kids", b.ChildCount));
            cmd.Parameters.Add(new OleDbParameter("@status", b.Status));
            cmd.Parameters.Add(new OleDbParameter("@id", b.Id));
        }
    }
}
