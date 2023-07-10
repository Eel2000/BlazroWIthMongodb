using BlazroWIthMongodb.Areas.Identity.Data.Models;
using BlazroWIthMongodb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BlazroWIthMongodb.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LoginModel(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public void OnGet()
    {
        ReturnUrl = Url.Content("~/");
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, isPersistent: false, false);
                if (result.Succeeded)
                {
                    return LocalRedirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("Auth_Failed", "Failed to authenticated");
                    return Page();
                }
            }

            return Page();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            ModelState.AddModelError("LoginError", e.Message);
            return Page();
        }
    }
}
