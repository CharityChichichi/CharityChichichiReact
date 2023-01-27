using CharityChichichiAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;


namespace CharityChichichiAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private IServiceProvider service;
        private CustomerDBContext context;

        //constructor
        public CustomerController(IServiceProvider service, CustomerDBContext context)
        {
            this.service = service;
            this.context = context;
        }

        //get all customers
        [HttpGet(Name = "GetCustomerList")]
        public List<CustomerModel> GetAll()
        {
            List<CustomerModel> customers = new List<CustomerModel>(); 
            
            foreach (var cust in context.CustomerModel)
            {
                if(cust.IsDeleted == false)
                {
                    customers.Add(cust);
                }
            }
                return customers;
            
        }

        //get single customer
        [HttpGet(Name = "GetSingleCustomer/id")]
        public CustomerModel GetCustomer(int id)
        {
            CustomerModel cust = new CustomerModel();

            foreach(var row in context.CustomerModel)
            {
                if(id == row.Guid )
                {
                    cust = row;
                }

            }    
            return cust;

        }

        [HttpPost(Name = "CreateCustomer")]
        public CustomerModel CreateCustomer(string name, string lastname, string email, DateTime dob)
        {
            CustomerModel cust = new CustomerModel();

            //cust.Guid = 
            cust.FirstName= name;
            cust.LastName= lastname;
            cust.UserName = name + lastname;

            bool emailavlid = EmailValidation(email);
            if(EmailValidation(email) == true) { 
                cust.Email = email;
            }
            cust.DateCreated= DateTime.Now;
            cust.DateOfBirth = dob;
            cust.Age = AgeCal(dob);
            cust.IsDeleted= false;

            return cust;
        }


        //Put Method
        [HttpPut(Name = "UpdateCustomer")]
        public CustomerModel UpdateCustomer(int id, string name, string lastname, string email, DateTime dob)
        {
            CustomerModel cust = GetCustomer(id);

            if (cust != null)
            {

                //cust.Guid = 
                cust.FirstName = name;
                cust.LastName = lastname;
                cust.UserName = name + lastname;

                bool emailavlid = EmailValidation(email);
                if (EmailValidation(email) == true)
                {
                    cust.Email = email;
                }
                cust.DateCreated = DateTime.Now;
                cust.DateOfBirth = dob;
                cust.Age = AgeCal(dob);
                cust.IsDeleted = false;
                cust.DateEdited = DateTime.Now; ;
            }

            return cust;
        }

        //Delete customer
       [HttpGet(Name = "Delete/id")]
       public bool DeleteCustomer(int id)
        {
            CustomerModel cust = GetCustomer(id);

            cust.IsDeleted = true;

            return cust.IsDeleted;

        }

        // Helper methods

        //calculate age
        public int AgeCal(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
                age = age - 1;

            return age;
        }
        //validate email address
        public bool EmailValidation(string email)
        {
            string regularexp = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(regularexp);
                if(re.IsMatch(email))
            {
                return true;
            }else
            {
                return false;
            }
        }
       
    }
}
