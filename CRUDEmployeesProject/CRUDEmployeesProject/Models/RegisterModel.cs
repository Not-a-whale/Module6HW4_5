using System.ComponentModel.DataAnnotations;

namespace CRUDEmployeesProject.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage="There is no Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "There is no Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(otherProperty: "Password", ErrorMessage = "The passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
