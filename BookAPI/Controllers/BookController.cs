using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BookAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("sorted-by-publisher")]
        public async Task<ActionResult> GetBooksSortedByPublisher()
        {
            try
            {
                Log.Information("Sorting books by publisher");
                var books = await _bookRepository.GetBooksSortedByPublisherAsync();                
                return Ok(books);
            }
            catch (Exception ex)
            {
                Log.Error($"Error while sorting books by publisher: {ex.Message}");
                return StatusCode(500, new
                {
                    Message = "Error while sorting books by publisher",
                    Details = ex.Message
                });
            }
        }

        [HttpGet("sorted-by-author")]
        public async Task<ActionResult> GetBooksSortedByAuthor()
        {
            try
            {
                Log.Information("Sorting books by Author");
                var books = await _bookRepository.GetBooksSortedByAuthorAsync();               
                return Ok(books);               
            }
            catch (Exception ex)
            {
                Log.Error($"Error while sorting books by Author: {ex.Message}");
                return StatusCode(500, new
                {
                    Message = "Error while sorting books by Author",
                    Details = ex.Message
                });
            }
        }

        [HttpGet("total-price")]
        public async Task<ActionResult<decimal>> GetTotalPrice()
        {
            try
            {
                Log.Information("Get Total Price of Books");
                var totalPrice = await _bookRepository.GetTotalBookPriceAsync();
                return Ok(totalPrice);
            }
            catch (Exception ex)
            {
                Log.Error($"Error while get total price of Books: {ex.Message}");
                return StatusCode(500, new
                {
                    Message = "Error while get total price of Books",
                    Details = ex.Message
                });
            }
        }

        [HttpPost("bulk-insert")]
        public async Task<IActionResult> BulkInsertBooks([FromBody] IList<BookCreate> books)
        {
            try
            {
                Log.Information("Inserting Books");
                await _bookRepository.BulkInsertBooksAsync(books);
                return Ok(new { Message = "Books inserted successfully" });
            }
            catch (Exception ex)
            {
                Log.Error($"Error while Inserting Books: {ex.Message}");
                return StatusCode(500, new
                {
                    Message = "Error while Inserting Books",
                    Details = ex.Message
                });
            }
        }

        //retrive data by SP

        [HttpGet("sorted-by-publisher-sp")]
        public async Task<ActionResult> GetBooksSortedByPublisherwithSP()
        {
            try
            {
                Log.Information("Sorting books by publisher by SP");
                var books = await _bookRepository.GetBooksSortedByPublisherwithSPAsync("Publisher, AuthorLastName, AuthorFirstName, Title");
                return Ok(books);
            }
            catch (Exception ex)
            {
                Log.Error($"Error while sorting books by publisher: {ex.Message}");
                return StatusCode(500, new
                {
                    Message = "Error while sorting books by publisher",
                    Details = ex.Message
                });
            }
        }

        [HttpGet("sorted-by-author-sp")]
        public async Task<ActionResult> GetBooksSortedByAuthorwithSP()
        {
            try
            {
                Log.Information("Sorting books by Author by SP");
                var books = await _bookRepository.GetBooksSortedByAuthorwithSPAsync("AuthorLastName, AuthorFirstName, Title");
                return Ok(books);
            }
            catch (Exception ex)
            {
                Log.Error($"Error while sorting books by Author: {ex.Message}");
                return StatusCode(500, new
                {
                    Message = "Error while sorting books by Author",
                    Details = ex.Message
                });
            }
        }
    }

}
