using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class UserPostModel
    {
        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "First Name start with cap and should have min three characters")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{1}[a-z]{2,}$", ErrorMessage = "Last Name start with cap and should have min three characters")]
        public string LastName { get; set; }

        [Required]
        //[RegularExpression("^([A-Za-z]{3,}([.a-z]*)@[a-z]{2,}[.][a-z]{2,3}([.a-z]*))$", ErrorMessage = "Email is not valid")]
        public string EmailId { get; set; }

        [Required]
        [RegularExpression("^(?=.*[A-Z])(?=.*[@#$!%^&-+=()])(?=.*[0-9])(?=.*[a-z]).{8,}$", ErrorMessage = "Password that should have min 8 characters with atleast 1 UpperCase,1 Numeric Number & 1 SpecialCharacter ")]
        public string Password { get; set; }
       
    }
}


