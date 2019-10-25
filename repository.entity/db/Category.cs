using System.Collections.Generic;

namespace repository.entity.db
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<TopicCategory> TopicCategories { get; set; }
    }
}
