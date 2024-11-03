using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DZ_Editing_Clients.Models;
using DZ_Editing_Clients.Services;

namespace DZ_Editing_Clients.Controllers
{
    public class UsersController : Controller
    {
        private readonly IServiceUsers _serviceUsers;
        public UsersController(IServiceUsers serviceUsers)
        {
            _serviceUsers = serviceUsers;
        }
        //GET: http://localhost:[port]/users
        public async Task<ViewResult> Index()
        {
            var products = await _serviceUsers.ReadAsync();
            return View(products);
        }
        //GET: http://localhost:[port]/users/details/{id}
        public async Task<ViewResult> Details(int id)
        {
            var product = await _serviceUsers.GetByIdAsync(id);
            return View(product);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        //GET: http://localhost:[port]/users/create
        public ViewResult Create() => View();
        [Authorize(Roles = "admin")]
        //POST: http://localhost:[port]/users/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                await _serviceUsers.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult Update() => View();
        [Authorize(Roles = "admin")]
        //POST: http://localhost:[port]/users/update/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,FirstName,LastName,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                await _serviceUsers.UpdateAsync(id, user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult Delete() => View();
        [Authorize(Roles = "admin")]
        //POST: http://localhost:[port]/users/delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceUsers.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
