namespace CharityChichichiAPI.Models
{
    public class CustomerModel
    {

        public int Guid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateEdited { get; set; }

        public Boolean IsDeleted { get; set; }
    }
}
