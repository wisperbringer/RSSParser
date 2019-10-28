using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Models;
using Microsoft.EntityFrameworkCore;
using repository.entity.db;
using repository.operation;

namespace frontend.ModelBuilders
{
    public class NewsModelBuilder
    {
        private Dictionary<OrderBy, Func<Topic, IComparable>> OrderFunctions = new Dictionary<OrderBy, Func<Topic, IComparable>>
        {
            { OrderBy.Date, (Topic => Topic.PublisDate) },
            { OrderBy.Source, topic => topic.Source.Name }
        };

        public NewsModelBuilder()
        {

        }

        public IEnumerable<Source> GetSources()
        {
            using (var db = new DBContext())
            {
                return db.Sources.ToList();
            }
        }



        public async Task<PaginatedList<repository.entity.db.Topic>> GetTopics(string sourceName, string orderOption, int page, int pageSize)
        {
            IQueryable<repository.entity.db.Topic> topics = GetTopics((OrderBy)Enum.Parse(typeof(OrderBy), orderOption, true), sourceName);


            return (PaginatedList<Topic>.Create(topics, page, pageSize));
        }

        private IQueryable<Topic> GetTopics(OrderBy orderBy, string sourceName)
        {
            using (var db = new DBContext())
            {
                IQueryable<Topic> topics = db.Topics;

                if (!string.IsNullOrEmpty(sourceName) && sourceName != Constants.DefaultSource)
                    topics = topics.Where(topic => topic.Source.Name == sourceName);

                var result = topics.Include(t => t.Source).ToList();

                return result.OrderBy(OrderFunctions[orderBy]).AsQueryable<Topic>();
            }
        }
        
    }
}
