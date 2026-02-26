namespace Model
{
    public class Hotel : BaseEntity
    {
        private string name;
        private string phoneNumber;
        private string email;
        private string webSite;
        private Owner owner;
        private City city;
        private string streetAddress;
        private int starRating;
        private bool hasPool;
        private bool hasGym;
        private bool hasRestaurant;
        private string mainHotelImageLink; 

        public string Name { get => name; set => name = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public string WebSite { get => webSite; set => webSite = value; }
        public Owner Owner { get => owner; set => owner = value; }
        public City City { get => city; set => city = value; }
        public string StreetAddress { get => streetAddress; set => streetAddress = value; }
        public int StarRating { get => starRating; set => starRating = value; }
        public bool HasPool { get => hasPool; set => hasPool = value; }
        public bool HasGym { get => hasGym; set => hasGym = value; }
        public bool HasRestaurant { get => hasRestaurant; set => hasRestaurant = value; }
        public string MainHotelImageLink { get => mainHotelImageLink; set => mainHotelImageLink = value; }

        public override string ToString()
        {
            return $"{Name} | {City?.CityName} | ⭐ {StarRating}";
        }


    }

    public class HotelUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string StreetAddress { get; set; }

        public int StarRating { get; set; }

        public bool HasPool { get; set; }
        public bool HasGym { get; set; }
        public bool HasRestaurant { get; set; }

        public string MainHotelImageLink { get; set; }
    }

}
