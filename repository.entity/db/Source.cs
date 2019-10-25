using System.Collections.Generic;

namespace repository.entity.db
{
    public class Source
    {
        public string Name { get; set; }
        
        public int Id { get; set; }
        
        public string ItemParsePath { get; set; }
        
        public string Url { get; set; }

        public virtual IEnumerable<Topic> Topics { get; set; }
    }
}
