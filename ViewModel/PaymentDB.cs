// FILE: PaymentDB.cs
using Model;
using System;
using System.Data.OleDb;

namespace ViewModel
{
    public class PaymentDB : BaseDB
    {
        public PaymentList SelectAll()
        {
            command.CommandText = "SELECT * FROM Payments";
            return new PaymentList(base.Select());
        }

        public static Payment SelectById(int id)
        {
            PaymentDB db = new PaymentDB();
            db.command.CommandText = "SELECT * FROM Payments WHERE ID=?";
            db.command.Parameters.Clear();
            db.command.Parameters.Add(new OleDbParameter("@id", id));
            PaymentList list = new PaymentList(db.Select());
            return list.Count > 0 ? list[0] : null;
        }

        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Payment p = entity as Payment ?? new Payment();

            if (reader["UserID"] != DBNull.Value) p.User = UserDB.SelectById(Convert.ToInt32(reader["UserID"]));
            if (reader["BookingID"] != DBNull.Value) p.Booking = BookingDB.SelectById(Convert.ToInt32(reader["BookingID"]));
            if (reader["Amount"] != DBNull.Value) p.Amount = Convert.ToDecimal(reader["Amount"]);
            p.PayMethod = reader["PayMethod"].ToString();
            if (reader["CreatedAt"] != DBNull.Value) p.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

            base.CreateModel(p);
            return p;
        }

        public override BaseEntity NewEntity() => new Payment();

        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Payment p) return;
            cmd.CommandText = "DELETE FROM Payments WHERE ID=?";
            cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
        }

        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Payment p) return;

            cmd.CommandText =
                "INSERT INTO Payments (UserID, BookingID, Amount, PayMethod, CreatedAt) " +
                "VALUES (?,?,?,?,?)";

            cmd.Parameters.Add(new OleDbParameter("@userId", DbVal(p.User?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@bookingId", DbVal(p.Booking?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@amount", p.Amount));
            cmd.Parameters.Add(new OleDbParameter("@method", p.PayMethod));
            cmd.Parameters.Add(new OleDbParameter("@created", p.CreatedAt));
        }

        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Payment p) return;

            cmd.CommandText =
                "UPDATE Payments SET UserID=?, BookingID=?, Amount=?, PayMethod=?, CreatedAt=? WHERE ID=?";

            cmd.Parameters.Add(new OleDbParameter("@userId", DbVal(p.User?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@bookingId", DbVal(p.Booking?.Id)));
            cmd.Parameters.Add(new OleDbParameter("@amount", p.Amount));
            cmd.Parameters.Add(new OleDbParameter("@method", p.PayMethod));
            cmd.Parameters.Add(new OleDbParameter("@created", p.CreatedAt));
            cmd.Parameters.Add(new OleDbParameter("@id", p.Id));
        }
    }
}
