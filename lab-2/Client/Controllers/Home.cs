﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class Home : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.IsResult = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
            if (file != null)
            {
                byte[] fileBytes = null;
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileBytes = binaryReader.ReadBytes((int)file.Length);
                }
                ViewBag.Text = Encoding.UTF8.GetString(fileBytes);

                ViewBag.ResponceModel = null;
                ViewBag.ResponceModel = await PostToPythonServer(ViewBag.Text);

                if (ViewBag.ResponceModel == null)
                    ViewBag.ServerError = true;
                else
                    ViewBag.IsResult = true;

                return View();
            }
            else
            {
                ViewBag.IsResult = false;
                return View();
            }
        }

        private async Task<ResponceModel> PostToPythonServer(string text)
        {
            ResponceModel responceModel = null;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:8080/");
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");

                string jsonRequest = JsonConvert.SerializeObject(new RequestModel() { Text = text });
                request.Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                await client.SendAsync(request)
                    .ContinueWith(responseTask =>
                    {
                        byte[] responce = responseTask.Result.Content.ReadAsByteArrayAsync().Result;
                        string json = Encoding.UTF8.GetString(responce);
                        responceModel = JsonConvert.DeserializeObject<ResponceModel>(json);
                    });

                return responceModel;
            }
            catch
            {
                return null;
            }
        }

        public IActionResult Help()
        {
            return View();
        }
    }
}
