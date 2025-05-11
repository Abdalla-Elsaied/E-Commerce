namespace DomainLayer.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string lName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public string AppUserId { get; set; } // Foreign key :Users
        public string Country { get; set; }


    }
}