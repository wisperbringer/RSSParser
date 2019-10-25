namespace repository.entity.db
{
    public class Topic : TopicBase
    {
        public virtual System.Collections.Generic.IEnumerable<TopicCategory> TopicCategories { get; set; }

        public virtual Source Source { get; set; }
    }
}
