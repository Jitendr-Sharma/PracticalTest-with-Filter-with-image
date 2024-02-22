using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PracticalTest.Models;
using PracticalTest.Models.ViewModel;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace PracticalTest.Controllers
{
    public class ProductsController : Controller
    {
        private readonly PracticalTestContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(PracticalTestContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index() 
        {
            return View(await _context.Product.Take(10).ToListAsync());
        }

        public IActionResult GetProductList()
        {
            var data = _context.Product.ToList();
            return View(data);
            //return new JsonResult(data);
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

 
        [HttpPost]
       
        public IActionResult Create( ProductViewModel product)
        {
            if (ModelState.IsValid)
            {

                var path = _hostEnvironment.WebRootPath;
                var filePath = "Content/Image/" + product.Picture.FileName;
                var fullpath = Path.Combine(path, filePath);
                UploadFile(product.Picture, fullpath);
                var data = new Product()
                {
                    Name = product.Name,
                    Detail = product.Detail,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    TotalPrice = product.TotalPrice,
                    CreatedDate = product.CreatedDate,

                    Picture = filePath,
                };


                _context.Add(data);
                 _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
           // Session["Image"] = product.Picture;
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product users)
        {
            var files = Request.Form.Files;
            string dbpath = string.Empty;
            if (files.Count > 0)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fullpath = Path.Combine(wwwRootPath + "/Content/Image/", files[0].FileName);
                dbpath = "Content/Image/" + files[0].FileName;
                FileStream stream = new FileStream(fullpath, FileMode.Create);
                files[0].CopyTo(stream);
            }
            users.Picture = dbpath;
            _context.Product.Update(users);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
     

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string searchString)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var movies = from m in _context.Product
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Name!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        [HttpPost]
        public string Search(string searchString, bool notUsed)
        {
            return "Filter Name " + searchString;
        }


        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
