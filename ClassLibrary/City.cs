namespace Model
{
    public class City : BaseEntity
    {
        private string cityName;
        public string CityName { get => cityName; set => cityName = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"City = {CityName}";
        }
    }
}
