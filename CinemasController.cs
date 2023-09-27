using etickets1.Data;
using etickets1.Data.Services;
using etickets1.Data.Static;
using etickets1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace etickets1.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        //Get:Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo, Name, Description")]Cinema cinema)
        {
            if(!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            {
                await _service.AddAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
        }

		//Get:Cinema/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

		//Get:Cinema/Edit/1
		public async Task<IActionResult> Edit (int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}


		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Logo, Name, Description")] Cinema cinema)
		{
			if (!ModelState.IsValid)
			{
				return View(cinema);
			}
			else
			{
				await _service.UpdateAsync(id, cinema);
				return RedirectToAction(nameof(Index));
			}
		}

		//Get:Cinema/Delete/1

		public async Task<IActionResult> Delete(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}


		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
            var cinemaDetails = await _service.GetByIdAsync(id);
            if(cinemaDetails == null)
            {
                return View("NotFound");
			}
			else
            {
				await _service.DeleteAsync(id);
				return RedirectToAction(nameof(Index));
			}
		}
	}
}