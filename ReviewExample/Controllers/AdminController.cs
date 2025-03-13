using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReviewExample.Repositories;

namespace ReviewExample.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public AdminController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // GET: /adminka
        [Route("adminka")]
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewRepository.GetAllReviewsAsync();
            return View(reviews);
        }

        // POST: /adminka/delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adminka/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await _reviewRepository.DeleteReviewAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
