using repository.entity.db;
using System.Collections.Generic;
using System.Linq;

namespace repository.operation
{
    public class SourceOperation
    {
        private IEnumerable<Source> _sources = new List<Source>
        {
            new Source
            {
                ItemParsePath = "/rss/channel/item",
                Name = "habr",
                Url = "https://habr.com/ru/rss/all/all/"
            },
            new Source
            {
                ItemParsePath = "/rss/channel/item",
                Name = "interfax",
                Url = "https://interfax.by/news/feed/"
            },
        };


        public IEnumerable<Source> GetSources()
        {
            using (var db = new DBContext())
            {
                var existSources = db.Sources.ToList();
                var urls = existSources.Select(s => s.Url).ToDictionary(key => key, value => value);
                var missingSources = _sources.Where(source => !urls.ContainsKey(source.Url)).ToList();
                if (!missingSources.Any()) return existSources;

                db.Sources.AddRange(missingSources);
                db.SaveChanges();

                return db.Sources.ToList();
            }
        }
    }
}
