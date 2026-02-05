namespace Model
{
    public class RoomImage : BaseEntity
    {
        private Room room;
        private string imageLink;

        public Room Room { get => room; set => room = value; }
        public string ImageLink { get => imageLink; set => imageLink = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"Room = {Room?.RoomName}, Image = {ImageLink}";
        }
    }
}
