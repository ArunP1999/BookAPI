using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<ActionResult> GetBooksSortedByPublisherAsync();
        Task<ActionResult> GetBooksSortedByAuthorAsync();
        Task<decimal> GetTotalBookPriceAsync();
        Task BulkInsertBooksAsync(IEnumerable<BookCreate> books);
        Task<ActionResult> GetBooksSortedByPublisherwithSPAsync(string sortQuery);
        Task<ActionResult> GetBooksSortedByAuthorwithSPAsync(string sortQuery);
    }
}
