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
            command.Parameters.Clear();
            return new BookingList(base.Select());
        }
        public static Booking SelectById(int id)
        {
            BookingDB db = new BookingDB();
            db.command.CommandText = "SELECT * FROM Bookings WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add("@id", OleDbType.Integer).Value = id;

            BookingList list = new BookingList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }
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

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Booking b = (Booking)entity;

            cmd.CommandText = "DELETE FROM Bookings WHERE ID=?";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", OleDbType.Integer).Value = b.Id;
        }

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
        public bool IsRoomAvailable(int roomId, DateTime start, DateTime end)
        {
            command.Parameters.Clear();

            // 1. כמות יחידות
            command.CommandText = "SELECT TotalUnits FROM Rooms WHERE ID=?";
            command.Parameters.Add("@p1", OleDbType.Integer).Value = roomId;

            object result = command.ExecuteScalar();

            if (result == null || result == DBNull.Value)
                return false;

            int totalUnits = Convert.ToInt32(result);

            command.Parameters.Clear();

            // 2. ספירת חפיפות (בלי Status בכלל כרגע כדי לבדוק יציבות)
            command.CommandText =
                "SELECT COUNT(*) FROM Bookings " +
                "WHERE RoomID=? AND (StartDate < ? AND EndDate > ?)";

            command.Parameters.Add("@p1", OleDbType.Integer).Value = roomId;
            command.Parameters.Add("@p2", OleDbType.Date).Value = end;
            command.Parameters.Add("@p3", OleDbType.Date).Value = start;

            int bookedCount = Convert.ToInt32(command.ExecuteScalar());

            return bookedCount < totalUnits;
        }
    }
}