using etickets1.Data;
using etickets1.Data.Services;
using etickets1.Data.Static;
using etickets1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace etickets1.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class ActorsController : Controller
	{
		private readonly IActorsService _service;

		public ActorsController(IActorsService service)
		{
			_service = service;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAllAsync();
			return View(data);
		}

		//Get: Actors/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
		{
			if (!ModelState.IsValid)
			{
				return View(actor);
			}
			else
			{
				await _service.AddAsync(actor);
				return RedirectToAction(nameof(Index));
			}
		}

		//Get: Actors/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);
			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}


		//Get: Actors/Edit/1
		public async Task<IActionResult> EditAsync(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);
			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id, FullName,ProfilePictureURL,Bio")] Actor actor)
		{
			if (!ModelState.IsValid)
			{
				return View(actor);
			}
			else
			{
				await _service.UpdateAsync(id, actor);
				return RedirectToAction(nameof(Index));
			}
		}


		//Get: Actors/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);

			if (actorDetails == null) return View("NotFound");
			return View(actorDetails);
		}


		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var actorDetails = await _service.GetByIdAsync(id);

			if (actorDetails == null) return View("Not Found");

			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}

