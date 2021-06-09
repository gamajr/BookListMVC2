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

        public TblBooksController(LibraryDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region API Calls
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
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
