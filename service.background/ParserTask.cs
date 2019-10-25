using repository.entity.db;
using repository.operation;
using System.Linq;
using System.Timers;

namespace service.background
{
    public class ParserTask
    {
        private RSSParserOperation RSSParser { get; set; }

        private ModelOperation ModelOperation { get; set; }

        private Timer _timer { get; set; }

        private Source _source { get; set; }

        public ParserTask(Source source)
        { 
            _source = source;
            ModelOperation = new ModelOperation();
            RSSParser = new RSSParserOperation();
            _timer = new Timer(10000);
            _timer.AutoReset = true;
            _timer.Elapsed += Process;
            _timer.Start();
        }

        private void Process(object sender, ElapsedEventArgs e)
        {
            lock (_source)
            {
                System.Console.WriteLine($"Process source: '{_source.Name}'");
                var topics = RSSParser.GetTopics(_source);
                var savedTopics = 0;
                foreach (var topic in topics)
                {
                    if (ModelOperation.SaveTopic(topic, _source))
                        savedTopics++;
                }
                System.Console.WriteLine($"Source '{_source.Name}' are completed. Topics total count: {topics.Count()}. Saved count: {savedTopics}");
            }
        }
    }
}
