using System.Collections.Generic;

namespace repository.entity
{
    public class Topic : TopicBase
    {
        public virtual IEnumerable<string> Categories { get; set; }
    }
}
