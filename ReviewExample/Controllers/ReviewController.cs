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
            const int PageSize = 5; // Количество отзывов на страницу
            var reviews = await _repository.GetAllReviewsAsync();
            var pagedReviews = reviews.OrderByDescending(r => r.CreatedAt)
                                      .Skip((page - 1) * PageSize)
                                      .Take(PageSize)
                                      .ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)reviews.Count() / PageSize);
            ViewBag.CurrentPage = page;

            return View(pagedReviews);
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
            model.CreatedAt = DateTime.UtcNow; // Сохраняем время в UTC
            await _repository.AddReviewAsync(model);
            return RedirectToAction("Create");
        }

        // GET: /Reviews/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var review = await _repository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            review.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(review.CreatedAt, TimeZoneInfo.Local);
            return View(review);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreReviews(int page)
        {
            try
            {
                const int PageSize = 5;
                var reviews = await _repository.GetAllReviewsAsync();
                var pagedReviews = reviews.OrderByDescending(r => r.CreatedAt)
                                          .Skip((page - 1) * PageSize)
                                          .Take(PageSize)
                                          .ToList();

                if (!pagedReviews.Any())
                {
                    return Content(""); // Возвращаем пустой контент, если больше нет отзывов
                }

                return PartialView("_ReviewList", pagedReviews);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return StatusCode(500); // Возвращаем статус ошибки сервера
            }
        }
    }

}
