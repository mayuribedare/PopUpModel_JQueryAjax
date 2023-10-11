using Microsoft.AspNetCore.Mvc;
using PopUpModel_JQueryAjax.Models;
using PopUpModel_JQueryAjax.Context;
using Microsoft.EntityFrameworkCore;

namespace PopUpModel_JQueryAjax.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDbContext _userDbContext;

        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userDbContext.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var model = new UserModel(); // Create an empty model
                ModelState.Clear();
                return View(model);
            }
            else
            {
                var userModel = await _userDbContext.Users.FindAsync(id);
                if (userModel == null)
                {
                    return NotFound();
                }
                return View(userModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,Department")] UserModel model)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _userDbContext.Users.Add(model);
                    _userDbContext.SaveChanges();
                   // return RedirectToAction("Index");

                }
                //Update
                else
                {
                    try
                    {
                        _userDbContext.Users.Update(model);
                        _userDbContext.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _userDbContext.Users.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", model) });
        }
     
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userDbContext.Users.FindAsync(id);
            _userDbContext.Users.Remove(user);
            await _userDbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _userDbContext.Users.ToList()) });
        }
    }
}
