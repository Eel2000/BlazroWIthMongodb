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
    private readonly UserManager<ApplicationUser> _userManager;
    public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
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
                var userToSignIn = await _userManager.FindByEmailAsync(Input.Email);
                if (userToSignIn is null)
                    return Page();

                var result = await _signInManager.PasswordSignInAsync(userToSignIn, Input.Password, isPersistent: false, false);
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
