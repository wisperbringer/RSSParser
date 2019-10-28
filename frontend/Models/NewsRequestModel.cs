using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace frontend.Models
{
    public class NewsRequestModel
    {
        public List<string> AllSources { get; set; }

        public Dictionary<OrderBy, string> OrderByOptions { get; set; } = new Dictionary<OrderBy, string>
        {
            { OrderBy.Date, "Сортировать по дате" },
            { OrderBy.Source, "Сортировать по источнику" }
        };

        [Required]
        public string Source { get; set; }
        
        [Required]
        public OrderBy Option { get; set; }

        public bool IsAJAX { get; set; }
    }

    public enum OrderBy
    {
        Date,
        Source
    }
}
