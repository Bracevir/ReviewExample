using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewExample.Models;
using ReviewExample.Repositories;

namespace ReviewExample.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _repository;
        private const int PageSize = 5;

        public ReviewsController(IReviewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var reviews = await _repository.GetPaginatedReviewsAsync(page, PageSize);
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review model)
        {
            if (!ModelState.IsValid) return View(model);

            await _repository.AddReviewAsync(model);
            return RedirectToAction("Create");
        }
    }
}
