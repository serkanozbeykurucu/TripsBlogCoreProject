using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;
using TripsBlogCoreProject.Areas.Admin.Models;

namespace TripsBlogCoreProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        UserManager _userManager1 = new UserManager(new EfUserDal());

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserList()
        {
            var result = _userManager1.GetList();
            return View(result);
        }

        [HttpGet]
        public IActionResult UserUpdate(int id)
        {
            var values = _userManager1.GetById(id);
            UserDeleteViewModel model = new UserDeleteViewModel
            {
                 FirstName = values.FirstName,
                 Lastname = values.LastName,
                 Email = values.Email,
                 PhoneNumber = values.PhoneNumber,
                 username = values.UserName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserUpdate(UserDeleteViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.username);
            user.FirstName = model.FirstName;
            user.LastName = model.Lastname;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.username;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");

            }
            return View();
        }

        [HttpGet]
        public IActionResult UserDelete(int id)
        {
            var result = _userManager1.GetById(id);
            UserDeleteViewModel model = new UserDeleteViewModel
            {
                FirstName = result.FirstName,
                Lastname= result.LastName,
                Email= result.Email,
                PhoneNumber= result.PhoneNumber,
                username = result.UserName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UserDelete(UserDeleteViewModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.username);
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserAdd(UserAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Username,
                    Email = model.EMail,
                    PhoneNumber = model.PhoneNumber
                };
                if (model.Password == model.ConfirmPassword)
                {
                    var result = await _userManager.CreateAsync(appUser,model.Password);
                    var addRoletoUser = await _userManager.AddToRoleAsync(appUser, "Writer");
                    if (result.Succeeded && addRoletoUser.Succeeded)
                    {
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        foreach (var item1 in addRoletoUser.Errors)
                        {
                            ModelState.AddModelError("", item1.Description);
                        }
                    }
                }
            }            
            return View();
        }
    }
}
