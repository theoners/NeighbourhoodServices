namespace NeighbourhoodServices.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using NeighbourhoodServices.Common;
    using NeighbourhoodServices.Data.Models;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Cloudinary cloudinary;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            Cloudinary cloudinary)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.cloudinary = cloudinary;
        }

        [Display(Name = "Потребителско Име")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Телефонен Номер")]
            public string PhoneNumber { get; set; }

            public string ProfilePictureUrl { get; set; }

            public Gender Gender { get; set; }

            [Display(Name = "Град")]
            public string City { get; set; }

            [Display(Name = "Адрес")]
            public string Address { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            this.Username = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                ProfilePictureUrl = user.ProfilePictureUrl,
                City = user.City,
                Address = user.Address,
                Gender = user.Gender,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Gender gender, IFormFile file)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            if (Input.City != user.City)
            {
                user.City = this.Input.City;
            }

            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
            }

            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
            }

            if (file != null)
            {
                var imageUrl = await CloudinaryApp.UploadFileAsync(this.cloudinary, file);
                user.ProfilePictureUrl = imageUrl;
            }

            user.Gender = gender;

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Профила е обновен";
            return RedirectToPage();
        }
    }
}
