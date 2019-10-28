using frontend.ModelBuilders;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace frontend.Controllers
{
    public class NewsController : Controller
    {
        private const int _pageSize = 10;

        public NewsModelBuilder NewsModelBuilder { get; set; }

        public NewsController(NewsModelBuilder newsModelBuilder)
        {
            NewsModelBuilder = newsModelBuilder;
        }
        
        public IActionResult Index(bool isAJAX)
        {
            var requestModel = new NewsRequestModel
            {
                IsAJAX = isAJAX
            };
            var sources = new List<string>()
            {
                {  Constants.DefaultSource }
            };
            sources.AddRange(NewsModelBuilder.GetSources().Select(source => source.Name));
            requestModel.AllSources = sources;

            return View(requestModel);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTopics(NewsRequestModel newsRequestModel, int pageNumber)
        {
            var topics = await NewsModelBuilder.GetTopics(newsRequestModel.Source, newsRequestModel.Option.ToString(), pageNumber, _pageSize);


            ViewBag.Topics = topics;
            return View("Index", newsRequestModel);
        }


        [HttpGet]
        public async Task<IActionResult> LoadTopics(string sourceName, string option, int page = 1)
        {
            var topics = await NewsModelBuilder.GetTopics(sourceName, option, page, _pageSize);
            
            return PartialView("_News", topics);
        }
    }
}
