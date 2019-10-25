using frontend.ModelBuilders;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace frontend.Controllers
{
    public class NewsController : Controller
    {
        public NewsModelBuilder NewsModelBuilder { get; set; }

        public NewsController(NewsModelBuilder newsModelBuilder)
        {
            NewsModelBuilder = newsModelBuilder;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var requestModel = new NewsRequestModel();
            var sources = new List<string>()
            {
                (Constants.DefaultSource)
            };
            sources.AddRange(NewsModelBuilder.GetSources().Select(source => source.Name));
            requestModel.AllSources = sources;
            return View(requestModel);
        }

        [HttpGet]
        public IActionResult LoadTopics(string sourceName, string option, int page = 1)
        {
            int pageSize = 10;

            var topics = NewsModelBuilder.GetTopics((OrderBy)Enum.Parse(typeof(Models.OrderBy), option, true), sourceName);
            var count = topics.Count();
            var items = topics.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            return PartialView("_News", items);
        }
    }
}
