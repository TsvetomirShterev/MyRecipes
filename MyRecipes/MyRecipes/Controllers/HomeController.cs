﻿namespace MyRecipes.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MyRecipes.Models;

    public class HomeController : Controller
    {

        public IActionResult Index() => View();


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}