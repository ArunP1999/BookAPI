using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;

        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> GetBooksSortedByPublisherAsync()
        {
            try
            {
                return new ObjectResult( await _context.Books
                    .OrderBy(b => b.Publisher)
                    .ThenBy(b => b.AuthorLastName)
                    .ThenBy(b => b.AuthorFirstName)
                    .ThenBy(b => b.Title)
                    .Select(b => new
                    {
                        b.Publisher,
                        b.Title,
                        b.AuthorLastName,
                        b.AuthorFirstName,
                        b.Price,
                        MlaCitation = b.MLACitation,
                        ChicagoCitation = b.ChicagoCitation
                    })
                    .ToListAsync());
            }
            catch (SqlException ex)
            {
                Log.Error($"Database error while GetBooksSortedByPublisher: {ex.Message}");
                throw new Exception("A database error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                Log.Error($"Unexpected error while GetBooksSortedByPublisher : {ex.Message}");
                throw new Exception("An unexpected error occurred.");
            }
        }

        public async Task<ActionResult> GetBooksSortedByAuthorAsync()
        {
            try
            {
                return new ObjectResult(await _context.Books
                    .OrderBy(b => b.AuthorLastName)
                    .ThenBy(b => b.AuthorFirstName)
                    .ThenBy(b => b.Title)
                     .Select(b => new
                     {
                         b.Publisher,
                         b.Title,
                         b.AuthorLastName,
                         b.AuthorFirstName,
                         b.Price,
                         MlaCitation = b.MLACitation,
                         ChicagoCitation = b.ChicagoCitation
                     })
                    .ToListAsync());

            }
            catch (SqlException ex)
            {
                Log.Error($"Database error while GetBooksSortedByAuthor: {ex.Message}");
                throw new Exception("A database error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                Log.Error($"Unexpected error while GetBooksSortedByAuthor : {ex.Message}");
                throw new Exception("An unexpected error occurred.");
            }
        }

        public async Task<decimal> GetTotalBookPriceAsync()
        {
            try
            {
                return await _context.Books.SumAsync(b => b.Price);
            }
            catch (SqlException ex)
            {
                Log.Error($"Database error while GetTotalBookPrice: {ex.Message}");
                throw new Exception("A database error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                Log.Error($"Unexpected error while GetTotalBookPrice : {ex.Message}");
                throw new Exception("An unexpected error occurred.");
            }
        }

        public async Task BulkInsertBooksAsync(IEnumerable<Book> books)
        {
            try
            {
                await _context.Books.AddRangeAsync(books);
                await _context.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                Log.Error($"Database error while BulkInsertBooks: {ex.Message}");
                throw new Exception("A database error occurred. Please try again later.");
            }
            catch (Exception ex)
            {
                Log.Error($"Unexpected error while BulkInsertBooks : {ex.Message}");
                throw new Exception("An unexpected error occurred.");
            }
        }
    }
}



