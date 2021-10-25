using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Core_Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Asp_Core_Identity.Pages
{
    public class RegistercshtmlModel : PageModel
    {
        private readonly UserManager<IdentityUser>  userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        
        [BindProperty]
        public Register Model { get; set; }
        public RegistercshtmlModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult>  OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = Model.Email,
                    Email = Model.Email
                };
                var result=await userManager.CreateAsync(user, Model.Password); 
                if (result.Succeeded)
                {
                   await signInManager.SignInAsync(user, false);
                   return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page(); 
        }

    }
}
