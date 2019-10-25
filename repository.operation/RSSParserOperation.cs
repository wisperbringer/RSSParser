using repository.entity;
using System.Collections.Generic;
using System.Linq;

namespace repository.operation
{
    public class RSSParserOperation
    {
        public IEnumerable<Topic> GetTopics(entity.db.Source souce)
        {
            var browserOpearation = new BrowserOperation();
            var xmlDocument = browserOpearation.DawloadXML(souce.Url);
            var rawTopics = ParseItems(xmlDocument, souce);
            var topics = rawTopics.Select(rawTopic => CastToTopic(rawTopic));
            return topics;
        }

        private IEnumerable<System.Xml.XmlNode> ParseItems(System.Xml.XmlDocument xmlDocument, entity.db.Source source)
        {
            try
            {
                return xmlDocument.SelectNodes(source.ItemParsePath).Cast<System.Xml.XmlNode>();
            }
            catch (System.Xml.XmlException e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }

        private Topic CastToTopic(System.Xml.XmlNode topicNode)
        {
            try
            {
                var title = topicNode.SelectSingleNode("./title").InnerText;
                var link = topicNode.SelectSingleNode("./link").InnerText;
                var pubDate = topicNode.SelectSingleNode("./pubDate").InnerText;
                var guid = topicNode.SelectSingleNode("./guid").InnerText;
                var creator = topicNode.SelectNodes("./*").Cast<System.Xml.XmlNode>().SingleOrDefault(node => node.LocalName.Equals("creator"))?.InnerText;
                var categories = topicNode.SelectNodes("./category").Cast<System.Xml.XmlNode>().Select(node => node.InnerText);
                var descriprion = topicNode.SelectSingleNode("./description").InnerText;
                
                return new Topic
                {
                    Title = title,
                    Link = link,
                    PublisDate = System.DateTime.Parse(pubDate),
                    Creator = creator,
                    Categories = categories,
                    Description = descriprion,
                    Guid = guid
                };

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }
    }
}