using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class studentinfo
    {

        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + LastName; } }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Faculty { get; set; }

        //public List<studentinfo> stdinfo
        //{
        //    get
        //    {
        //        return new List<studentinfo>
        //    {
        //        new studentinfo{Id=1,FirstName="Sudeep",LastName="Neupane",Address ="Parasi",PhoneNumber=9847545938,Email="SudeepNeupane@hotmail.com",Faculty="Science" }


        //    };

        //    }

        //}
    }
}