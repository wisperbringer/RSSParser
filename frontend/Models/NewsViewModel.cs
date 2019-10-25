using repository.entity.db;
using System.Collections.Generic;

namespace frontend.Models
{
    public class NewsViewModel
    {
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Topic> Topics{ get; set; }
    }
}
