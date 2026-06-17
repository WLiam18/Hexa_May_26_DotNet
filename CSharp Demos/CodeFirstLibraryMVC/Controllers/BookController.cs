using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirstLibraryMVC.Data;
using CodeFirstLibraryMVC.Models;

namespace CodeFirstLibraryMVC.Controllers;

public class BookController : Controller
{
    private readonly LibraryDbContext _context;
    private readonly ILogger<BookController> _logger;
    
    public BookController(LibraryDbContext context, ILogger<BookController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    // READ ALL
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.ToListAsync();
        return View(books);
    }
    
    // READ ONE 
    public async Task<IActionResult> Details(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
    
    // CREATE
    public IActionResult Create()
    {
        return View();
    }
    
    // CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (ModelState.IsValid)
        {
            book.AvailableCopies = book.TotalCopies;
            book.CreatedDate = DateTime.Now;
            
            _context.Add(book);
            await _context.SaveChangesAsync();  // ← SAVES TO DATABASE!
            
            TempData["Success"] = "Book added successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }
    
    // EDIT
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
    
    // EDIT 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id)
        {
            return NotFound();
        }
        
        if (ModelState.IsValid)
        {
            try
            {
                // Adjust available copies 
                var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                if (existingBook != null)
                {
                    int copyDifference = book.TotalCopies - existingBook.TotalCopies;
                    book.AvailableCopies = existingBook.AvailableCopies + copyDifference;
                }
                
                _context.Update(book);
                await _context.SaveChangesAsync(); 
                
                TempData["Success"] = "Book updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }
    
    // DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
    
    // DELETE 
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();  
            TempData["Success"] = "Book deleted successfully!";
        }
        
        return RedirectToAction(nameof(Index));
    }
    
    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}