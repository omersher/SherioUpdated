using System;

namespace Model
{
    public class RoomAvailability : BaseEntity
    {
        private int roomID;
        private DateTime startDate;
        private DateTime endDate;

        public int RoomID
        {
            get => roomID;
            set => roomID = value;
        }

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"RoomID = {RoomID}, From = {StartDate:dd/MM/yyyy}, To = {EndDate:dd/MM/yyyy}";
        }
    }
}