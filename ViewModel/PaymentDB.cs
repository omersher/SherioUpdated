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
            command.Parameters.Clear();
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

        // =========================
        // CREATE MODEL
        // =========================
        protected override BaseEntity CreateModel(BaseEntity entity)
        {
            Payment p = entity as Payment ?? new Payment();

            p.Id = Convert.ToInt32(reader["ID"]);

            if (reader["UserID"] != DBNull.Value)
                p.UserID = Convert.ToInt32(reader["UserID"]);

            if (reader["BookingID"] != DBNull.Value)
                p.BookingID = Convert.ToInt32(reader["BookingID"]);

            if (reader["Amount"] != DBNull.Value)
                p.Amount = Convert.ToDecimal(reader["Amount"]);

            p.PayMethod = reader["PayMethod"]?.ToString();

            if (reader["CreatedAt"] != DBNull.Value)
                p.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

            base.CreateModel(p);
            return p;
        }

        public override BaseEntity NewEntity() => new Payment();

        // =========================
        // INSERT
        // =========================
        protected override void CreateInsertdSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Payment p) return;

            cmd.CommandText =
                "INSERT INTO Payments (UserID, BookingID, Amount, PayMethod, CreatedAt) " +
                "VALUES (?,?,?,?,?)";

            cmd.Parameters.Clear();

            cmd.Parameters.Add("@userId", OleDbType.Integer).Value = p.UserID;
            cmd.Parameters.Add("@bookingId", OleDbType.Integer).Value = p.BookingID;
            cmd.Parameters.Add("@amount", OleDbType.Currency).Value = p.Amount;
            cmd.Parameters.Add("@method", OleDbType.VarChar).Value = p.PayMethod;
            cmd.Parameters.Add("@created", OleDbType.Date).Value = p.CreatedAt;
        }

        // =========================
        // UPDATE
        // =========================
        protected override void CreateUpdatedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Payment p) return;

            cmd.CommandText =
                "UPDATE Payments SET UserID=?, BookingID=?, Amount=?, PayMethod=?, CreatedAt=? WHERE ID=?";

            cmd.Parameters.Clear();

            cmd.Parameters.Add("@userId", OleDbType.Integer).Value = p.UserID;
            cmd.Parameters.Add("@bookingId", OleDbType.Integer).Value = p.BookingID;
            cmd.Parameters.Add("@amount", OleDbType.Currency).Value = p.Amount;
            cmd.Parameters.Add("@method", OleDbType.VarChar).Value = p.PayMethod;
            cmd.Parameters.Add("@created", OleDbType.Date).Value = p.CreatedAt;
            cmd.Parameters.Add("@id", OleDbType.Integer).Value = p.Id;
        }

        // =========================
        // DELETE
        // =========================
        protected override void CreateDeletedSQL(BaseEntity entity, OleDbCommand cmd)
        {
            if (entity is not Payment p) return;

            cmd.CommandText = "DELETE FROM Payments WHERE ID=?";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id", OleDbType.Integer).Value = p.Id;
        }
    }
}