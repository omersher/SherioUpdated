using System;

namespace Model
{
    public class Payment : BaseEntity
    {
        private int userID;
        private int bookingID;
        private decimal amount;
        private string payMethod;
        private DateTime createdAt = DateTime.Now;

        public int UserID
        {
            get => userID;
            set => userID = value;
        }

        public int BookingID
        {
            get => bookingID;
            set => bookingID = value;
        }

        public decimal Amount
        {
            get => amount;
            set => amount = value;
        }

        public string PayMethod
        {
            get => payMethod;
            set => payMethod = value;
        }

        public DateTime CreatedAt
        {
            get => createdAt;
            set => createdAt = value;
        }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Payment: UserID = {UserID}, BookingID = {BookingID}, " +
                   $"Amount = {Amount}, Method = {PayMethod}";
        }
    }
}