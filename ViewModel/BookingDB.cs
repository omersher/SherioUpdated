using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class BookingDB : BaseDB
    {
        // =========================
        // SELECT ALL
        // =========================
        public BookingList SelectAll()
        {
            command.CommandText = "SELECT * FROM Bookings";
            command.Parameters.Clear();
            return new BookingList(base.Select());
        }

        // =========================
        // SELECT BY ID
        // =========================
        public static Booking SelectById(int id)
        {
            BookingDB db = new BookingDB();
            db.command.CommandText = "SELECT * FROM Bookings WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add("@id", OleDbType.Integer).Value = id;

            BookingList list = new BookingList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        // =========================
        // CREATE MODEL
        // =========================
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Booking b = entity as Booking ?? new Booking();

            b.Id = Convert.ToInt32(reader["ID"]);
            b.UserID = Convert.ToInt32(reader["UserID"]);
            b.RoomID = Convert.ToInt32(reader["RoomID"]);
            b.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            b.StartDate = Convert.ToDateTime(reader["StartDate"]);
            b.EndDate = Convert.ToDateTime(reader["EndDate"]);
            b.AdultCount = Convert.ToInt32(reader["AdultCount"]);
            b.ChildCount = Convert.ToInt32(reader["ChildCount"]);

            if (reader["Status"] != DBNull.Value)
            {
                var s = reader["Status"].ToString();

                // If stored as number "0"
                if (int.TryParse(s, out int enumNum))
                    b.Status = (BookingStatus)enumNum;
                else if (Enum.TryParse<BookingStatus>(s, true, out var enumVal))
                    b.Status = enumVal;
                else
                    b.Status = BookingStatus.Pending;
            }
            else
            {
                b.Status = BookingStatus.Pending;
            }

            return b;
        }

        public override BaseEntity NewEntity() => new Booking();

        // =========================
        // INSERT
        // =========================
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Booking b = (Booking)entity;

            cmd.CommandText =
                "INSERT INTO Bookings " +
                "(UserID, RoomID, CreatedAt, StartDate, EndDate, AdultCount, ChildCount, Status) " +
                "VALUES (?,?,?,?,?,?,?,?)";

            cmd.Parameters.Clear();

            cmd.Parameters.Add("@user", OleDbType.Integer).Value = b.UserID;
            cmd.Parameters.Add("@room", OleDbType.Integer).Value = b.RoomID;
            cmd.Parameters.Add("@created", OleDbType.Date).Value = b.CreatedAt;
            cmd.Parameters.Add("@start", OleDbType.Date).Value = b.StartDate;
            cmd.Parameters.Add("@end", OleDbType.Date).Value = b.EndDate;
            cmd.Parameters.Add("@adult", OleDbType.Integer).Value = b.AdultCount;
            cmd.Parameters.Add("@child", OleDbType.Integer).Value = b.ChildCount;
            cmd.Parameters.Add("@status", OleDbType.VarChar).Value = b.Status.ToString();
        }

        // =========================
        // UPDATE
        // =========================
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Booking b = (Booking)entity;

            cmd.CommandText =
                "UPDATE Bookings SET AdultCount=?, ChildCount=?, Status=? WHERE ID=?";

            cmd.Parameters.Clear();

            cmd.Parameters.Add("@adult", OleDbType.Integer).Value = b.AdultCount;
            cmd.Parameters.Add("@child", OleDbType.Integer).Value = b.ChildCount;
            cmd.Parameters.Add("@status", OleDbType.VarChar).Value = b.Status.ToString();
            cmd.Parameters.Add("@id", OleDbType.Integer).Value = b.Id;
        }

        // =========================
        // DELETE
        // =========================
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Booking b = (Booking)entity;

            cmd.CommandText = "DELETE FROM Bookings WHERE ID=?";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", OleDbType.Integer).Value = b.Id;
        }

        // =========================
        // SELECT BY HOTEL
        // =========================
        public BookingList SelectByHotel(int hotelId)
        {
            command.CommandText =
                "SELECT B.* FROM Bookings B " +
                "INNER JOIN Rooms R ON B.RoomID = R.ID " +
                "WHERE R.HotelID=?";

            command.Parameters.Clear();
            command.Parameters.Add("@hotelId", OleDbType.Integer).Value = hotelId;

            return new BookingList(base.Select());
        }

        // =========================
        // CHECK ROOM AVAILABILITY 🔥
        // =========================
        public bool IsRoomAvailable(int roomId, DateTime start, DateTime end)
        {
            command.CommandText =
                "SELECT COUNT(*) FROM Bookings " +
                "WHERE RoomID=? AND NOT (EndDate<=? OR StartDate>=?)";

            command.Parameters.Clear();
            command.Parameters.Add("@room", OleDbType.Integer).Value = roomId;
            command.Parameters.Add("@start", OleDbType.Date).Value = start;
            command.Parameters.Add("@end", OleDbType.Date).Value = end;

            // DO NOT open/close connection manually
            int count = Convert.ToInt32(command.ExecuteScalar());

            return count == 0;
        }
    }
}