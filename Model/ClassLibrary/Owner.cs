namespace Model
{
    public class Owner : BaseEntity
    {
        private bool isActive;

        public bool IsActive { get => isActive; set => isActive = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $" Active = {IsActive}";
        }
    }
}
