namespace Model
{
    public class User : BaseEntity
    {
        private string fullName;
        private string guestID;
        private string email;
        private string phone;
        private string passHash;

        public string FullName { get => fullName; set => fullName = value; }
        public string GuestID { get => guestID; set => guestID = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string PassHash { get => passHash; set => passHash = value; }

        public override string ToString()
        {
            return base.ToString() + " | " +
                   $"User = {FullName}, Email = {Email}, Phone = {Phone}";
        }

        public class UserUpdateDto
        {
            public string? FullName { get; set; }
            public string? GuestID { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? PassHash { get; set; }
        }

    }
}
