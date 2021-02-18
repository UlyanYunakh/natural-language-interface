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
        private Dictionary _dictionary;
        public Home(Dictionary context)
        {
            _dictionary = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_dictionary.Items.OrderBy(i => i.Word));
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
        public IActionResult Save(DictionaryItem item)
        {
            _dictionary.Save(item);
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
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                DictionaryItem item = _dictionary.Find(id);
                if (item != null)
                {
                    return View(item);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult DeleteItem(int? id)
        {
            if (id != null)
            {
                _dictionary.Remove(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}