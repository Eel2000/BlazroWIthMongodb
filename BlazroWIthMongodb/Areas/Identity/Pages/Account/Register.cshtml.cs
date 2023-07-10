using BlazroWIthMongodb.Areas.Identity.Data.Models;
using BlazroWIthMongodb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazroWIthMongodb.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
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
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(newUser, Input.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
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
    }
}
