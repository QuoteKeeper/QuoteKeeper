using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuoteKeeper.API.Controllers;
using QuoteKeeper.API.Dtos;
using QuoteKeeper.API.Services;
using QuoteKeeper.API.Extensions;

namespace QuoteKeeper.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateQuote([FromBody] QuoteRequest request)
        {
            int userId = User.GetUserId();
            var result = _quoteService.CreateQuote(request, userId);
            if (result == null)
            {
                return Conflict(new { message = "Quote already exists for this user and book." });
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllQuotes()
        {
            var quotes = _quoteService.GetAllQuotes();
            return Ok(quotes);
        }

        [Authorize]
        [HttpGet("favorites")]
        public IActionResult GetFavoriteQuotesByUser()
        {
            int userId = User.GetUserId();
            var favorites = _quoteService.GetFavoriteQuotesByUser(userId);
            return Ok(favorites);
        }

        [Authorize]
        [HttpGet("{quoteId}/favorited-by")]
        public IActionResult GetUserWhoFavoritedQuote(int quoteId)
        {
            int userId = User.GetUserId();
            var favoriteUsers = _quoteService.GetUsersWhoFavoritedQuote(quoteId);
            return Ok(favoriteUsers);
        }
        [Authorize]
        [HttpPost("favorite")]
        public IActionResult AddToFavorites([FromBody] QuoteFavoriteRequest request)
        {
            int userId = User.GetUserId();
            request.UserId = userId;
            try
            {
                var success = _quoteService.AddToFavorites(request);
                if (!success)
                    return Conflict(new { message = "Quote already favorited." });
                return Ok(new { message = "Quote added to favorites." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpPost("unfavorite")]
        public IActionResult RemoveFromFavorites([FromBody] QuoteFavoriteRequest request)
        {

            int userId = User.GetUserId();
            request.UserId = userId;
            var success = _quoteService.RemoveFromFavorites(request);
            if (!success)
                return NotFound(new { message = "Favorite not found." });

            return Ok(new { message = "Quote removed from favorites." });
        }

        [HttpGet("{quoteId}/likes/count")]
        public IActionResult GetLikeCount(int quoteId)
        {
            int count = _quoteService.GetFavoriteCount(quoteId);
            return Ok(new { quoteId, likeCount = count });
        }

    }
}