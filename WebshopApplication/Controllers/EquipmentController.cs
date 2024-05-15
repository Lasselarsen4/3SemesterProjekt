﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebshopApplication.Models;

namespace WebshopApplication.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(ILogger<EquipmentController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}