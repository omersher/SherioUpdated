namespace Model
{
    public class HotelImage : BaseEntity
    {
        private Hotel hotel;
        private string imageLink;

        public Hotel Hotel { get => hotel; set => hotel = value; }
        public string ImageLink { get => imageLink; set => imageLink = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Hotel = {Hotel?.Name}, Image = {ImageLink}";
        }
    }
}
