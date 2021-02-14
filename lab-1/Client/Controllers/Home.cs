using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Client.Models;

namespace Client.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index() // словарь
        {
            return View();
        }
        public IActionResult Add() // добавить в словарь (POST) и обновить Index.
        {
            return View();
        }
        public IActionResult Create() // обработать текст (POST) и обновить Index
        {
            return View();
        }
    }
}