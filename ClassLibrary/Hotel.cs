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

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Hotel = {Name}, City = {City?.CityName}, Stars = {StarRating}, " +
                   $"Phone = {PhoneNumber}, Email = {Email}";
        }
    }
}
