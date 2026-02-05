using System;

namespace Model
{
    public class Payment : BaseEntity
    {
        private User user;
        private Booking booking;
        private decimal amount;
        private string payMethod;
        private DateTime createdAt;

        public User User { get => user; set => user = value; }
        public Booking Booking { get => booking; set => booking = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public string PayMethod { get => payMethod; set => payMethod = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Payment: User = {User?.FullName}, Booking = {Booking?.Id}, " +
                   $"Amount = {Amount}, Method = {PayMethod}";
        }
    }
}
