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
            db.command.Parameters.Add(new OleDbParameter("@id", id));

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
            b.RoomID = Convert.ToInt32(reader["RoomID"]);
            b.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            b.StartDate = Convert.ToDateTime(reader["StartDate"]);
            b.EndDate = Convert.ToDateTime(reader["EndDate"]);
            b.AdultCount = Convert.ToInt32(reader["AdultCount"]);
            b.ChildCount = Convert.ToInt32(reader["ChildCount"]);
            b.Status = reader["Status"].ToString();

            base.CreateModel(b);
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
                "(RoomID, CreatedAt, StartDate, EndDate, AdultCount, ChildCount, Status) " +
                "VALUES (?,?,?,?,?,?,?)";

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new OleDbParameter("@room", b.RoomID));
            cmd.Parameters.Add(new OleDbParameter("@created", b.CreatedAt));
            cmd.Parameters.Add(new OleDbParameter("@start", b.StartDate));
            cmd.Parameters.Add(new OleDbParameter("@end", b.EndDate));
            cmd.Parameters.Add(new OleDbParameter("@adult", b.AdultCount));
            cmd.Parameters.Add(new OleDbParameter("@child", b.ChildCount));
            cmd.Parameters.Add(new OleDbParameter("@status", b.Status));
        }

        // =========================
        // UPDATE  ✅ זה מה שאתה צריך
        // =========================
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Booking b = (Booking)entity;

            cmd.CommandText =
                "UPDATE Bookings SET AdultCount=?, ChildCount=?, Status=? WHERE ID=?";

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new OleDbParameter("@adult", b.AdultCount));
            cmd.Parameters.Add(new OleDbParameter("@child", b.ChildCount));
            cmd.Parameters.Add(new OleDbParameter("@status", b.Status));
            cmd.Parameters.Add(new OleDbParameter("@id", b.Id));
        }

        // =========================
        // DELETE
        // =========================
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            Booking b = (Booking)entity;

            cmd.CommandText = "DELETE FROM Bookings WHERE ID=?";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new OleDbParameter("@id", b.Id));
        }

        public BookingList SelectByHotel(int hotelId)
        {
            command.CommandText =
                "SELECT B.* FROM Bookings B " +
                "INNER JOIN Rooms R ON B.RoomID = R.ID " +
                "WHERE R.HotelID = ?";

            command.Parameters.Clear();
            command.Parameters.Add(new OleDbParameter("@hotelId", hotelId));

            return new BookingList(base.Select());
        }

    }
}
