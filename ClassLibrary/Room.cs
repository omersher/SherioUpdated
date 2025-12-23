namespace Model
{
    public class Room : BaseEntity
    {
        private Hotel hotel;
        private string roomName;
        private int adultRate;
        private int childRate;
        private int bedrooms;
        private int bathrooms;
        private bool hasKitchen;
        private bool hasParking;
        private bool hasBalcony;
        private bool hasLivingRoom;

        public Hotel Hotel { get => hotel; set => hotel = value; }
        public string RoomName { get => roomName; set => roomName = value; }
        public int AdultRate { get => adultRate; set => adultRate = value; }
        public int ChildRate { get => childRate; set => childRate = value; }
        public int Bedrooms { get => bedrooms; set => bedrooms = value; }
        public int Bathrooms { get => bathrooms; set => bathrooms = value; }
        public bool HasKitchen { get => hasKitchen; set => hasKitchen = value; }
        public bool HasParking { get => hasParking; set => hasParking = value; }
        public bool HasBalcony { get => hasBalcony; set => hasBalcony = value; }
        public bool HasLivingRoom { get => hasLivingRoom; set => hasLivingRoom = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Room = {RoomName}, Hotel = {Hotel?.Name}, " +
                   $"AdultRate = {AdultRate}, ChildRate = {ChildRate}, " +
                   $"Bedrooms = {Bedrooms}, Bathrooms = {Bathrooms}";
        }
    }
}
