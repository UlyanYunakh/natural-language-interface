using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Client.Models;

namespace Client.Controllers
{
    public class Home : Controller
    {
        private DictionaryItemContext db;
        public Home(DictionaryItemContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.DictionaryItems.ToListAsync());
        }

        [HttpGet]
        public IActionResult Form()
        {
            return PartialView("_Form");
        }
        [HttpGet]
        public IActionResult TextWordForm()
        {
            return PartialView("_TextWordForm");
        }
        [HttpPost]
        public async Task<IActionResult> Save(DictionaryItem item)
        {
            db.DictionaryItems.Update(item);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // *** do not touch yet **

        [HttpGet]
        public IActionResult TextForm()
        {
            return PartialView("_TextForm");
        }
        [HttpPost]
        public IActionResult TextForm(int? id)
        {
            return RedirectToAction("Index");
        }

        // *** exit ***

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                DictionaryItem item = await db.DictionaryItems.FirstOrDefaultAsync(p => p.Id == id);
                if (item != null)
                {
                    return View(item);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id != null)
            {
                DictionaryItem item = await db.DictionaryItems.FirstOrDefaultAsync(p => p.Id == id);
                if (item != null)
                {
                    db.DictionaryItems.Remove(item);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}