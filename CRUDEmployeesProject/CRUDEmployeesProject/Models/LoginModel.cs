using System.ComponentModel.DataAnnotations;

namespace CRUDEmployeesProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "There is no valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "There is no password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
