using System;
using System.Collections.Generic;
using System.Text;

namespace repository.entity
{
    public abstract class TopicBase
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Guid { get; set; }

        public string Description { get; set; }

        public DateTime PublisDate { get; set; }

        public string Creator { get; set; }
    }
}
