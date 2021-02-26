using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
using Client.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
// using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Client.Utilities;
using System.Net.Http;
using System.Net.Http.Headers;

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

        [HttpGet]
        public IActionResult TextForm()
        {
            return PartialView("_TextForm");
        }
        [HttpPost]
        public async Task<IActionResult> TextForm(string text)
        {
            try
            {
                string jsonRequest = ItemsConverter.ConvertStringToJsonRequest(text);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/");
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");

                request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");


                await client.SendAsync(request)
                      .ContinueWith(responseTask =>
                      {
                          byte[] responce = responseTask.Result.Content.ReadAsByteArrayAsync().Result;
                          string json = Encoding.UTF8.GetString(responce);
                          if (ItemsConverter.ValidateJson(json))
                          {
                              _dictionary.Items = ItemsConverter.ConvertFromJson(json);
                              _dictionary.RecountItems();
                          }
                      });
            }
            catch
            {
            }

            return RedirectToAction("Index");
        }

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


        [HttpGet]
        public IActionResult Upload()
        {
            return PartialView("_UploadForm");
        }
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null)
            {
                byte[] fileBytes = null;
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileBytes = binaryReader.ReadBytes((int)file.Length);
                }
                string json = Encoding.UTF8.GetString(fileBytes);
                if (ItemsConverter.ValidateJson(json))
                {
                    _dictionary.Items = ItemsConverter.ConvertFromJson(json);
                    _dictionary.RecountItems();
                }
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Download()
        {
            string file = ItemsConverter.ConvertItemsToJson(_dictionary.Items);
            byte[] bytes = Encoding.UTF8.GetBytes(file);
            MemoryStream memoryStream = new MemoryStream(bytes);
            return File(memoryStream, "application/json", "dictionary.json");
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}