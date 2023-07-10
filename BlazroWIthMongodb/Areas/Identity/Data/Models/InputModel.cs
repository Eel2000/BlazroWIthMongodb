using System.ComponentModel.DataAnnotations;

namespace BlazroWIthMongodb.Areas.Identity.Data.Models;

#nullable disable
public class InputModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Please fill the input with the email")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please fill the password correctly")]
    public string Password { get; set; }

    public bool RemembeMe { get; set; } = false;
}