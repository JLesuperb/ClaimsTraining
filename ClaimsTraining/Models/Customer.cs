using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimsTraining.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32? CustomerId { get; set; }

        public String FirstName { get; set; }

        public String ForeName { get; set; }

        public String LastName { get; set; }

        public String PhoneNumer { get; set; }

        public String Email { get; set; }

        public String CustomerName { set; get; }

        public String CustomerPass { set; get; }

        public String Token { get; set; }

        public Int32 RoleFId { get; set; }

        [ForeignKey("RoleFId")]
        public Role Role { get; set; }
    }
}
