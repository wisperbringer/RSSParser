using repository.entity.db;
using System.Linq;
using System.Collections.Generic;

namespace repository.operation
{
    public class ModelOperation
    {
        public bool SaveTopic(entity.Topic topic, Source source)
        {
            using (var db = new DBContext())
            {
                if (db.Topics.Any(dbTopic => dbTopic.Guid == topic.Guid)) return false;
                
                var categoriesModel = topic.Categories.Select(category => GetCategory(db, category)).ToList();
                var topicModel = GetTopic(db, topic, source, categoriesModel);
                db.SaveChanges();
                if (topicModel != null)
                    return true;
            }
            return false;
        }

        private Category GetCategory(DBContext db, string categoryName)
        {
            var existCategory = db.Categories.SingleOrDefault(dbCategory=> categoryName == dbCategory.Name);
            if (existCategory != null)
                return existCategory;

            existCategory = new Category
            {
                Name = categoryName
            };
            db.Categories.Add(existCategory);
            db.SaveChanges();
            return existCategory;
        }

        private Topic GetTopic(DBContext db, entity.Topic topic, entity.db.Source sourceModel, IEnumerable<Category> categories)
        {
            var existTopic = db.Topics.Find(topic.Guid);
            if (existTopic != null)
                return existTopic;

            var topicCategories = categories.Select(category => new TopicCategory { Category = category }).ToList();

            var dbSources = db.Sources.Find(sourceModel.Id);

            existTopic = new Topic
            {
                Guid = topic.Guid,
                Creator = topic.Creator,
                Description = topic.Description,
                Link = topic.Link,
                PublisDate = topic.PublisDate,
                Title = topic.Title,
                Source = dbSources,
                TopicCategories = topicCategories
            };
            db.Topics.Add(existTopic);
            db.SaveChanges();
            return existTopic;
        }
    }
}
