using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimsTraining.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32? UserId { get; set; }

        public String FirstName { get; set; }

        public String ForeName { get; set; }

        public String LastName { get; set; }

        public String PhoneNumer { get; set; }

        public String Email { get; set; }
    }
}
