using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Controllers
{
    public class TblBooksController : Controller
    {
        private readonly LibraryDBContext _db;

        [BindProperty]
        public TblBooks Book { get; set; }

        public TblBooksController(LibraryDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(uint? id)
        {
            Book = new TblBooks();
            if (id == null)
            {
                return View(Book);
            }

            Book = _db.TblBooks.FirstOrDefault(u=>u.Id==id);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if(Book.Id == 0){
                    //create
                    _db.TblBooks.Add(Book);

                }
                else
                {
                    _db.TblBooks.Update(Book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Book);
        }
        #region API Calls
        [HttpDelete]
        public async Task<IActionResult> Delete(uint id)
        {
            var bookFromDB = await _db.TblBooks.FirstOrDefaultAsync(u => u.Id == id);
            if (bookFromDB == null)
            {
                return Json(new { success = false, message = "Error while Deleting." });
            }
            _db.TblBooks.Remove(bookFromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Item deleted." });
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.TblBooks.ToListAsync() });

        }

        #endregion
    }
}
