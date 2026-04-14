namespace Model
{
    public class Room : BaseEntity
    {
        private string roomName;
        private Hotel hotel;
        private int adultRate;
        private int childRate;
        private int bedrooms;
        private int bathrooms;
        private bool hasKitchen;
        private bool hasParking;
        private bool hasBalcony;
        private bool hasLivingRoom;
        private int totalUnits;
        private string mainRoomImageLink;

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

        public int TotalUnits
        {
            get => totalUnits;
            set => totalUnits = value;
        }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Room = {RoomName}, Hotel = {Hotel?.Name}, " +
                   $"AdultRate = {AdultRate}, ChildRate = {ChildRate}, " +
                   $"Bedrooms = {Bedrooms}, Bathrooms = {Bathrooms}, " +
                   $"TotalUnits = {TotalUnits}";
        }
    }

    public class RoomUpdateDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomName { get; set; }
        public int AdultRate { get; set; }
        public int ChildRate { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasParking { get; set; }
        public bool HasBalcony { get; set; }
        public bool HasLivingRoom { get; set; }
        public int TotalUnits { get; set; }
    }
}