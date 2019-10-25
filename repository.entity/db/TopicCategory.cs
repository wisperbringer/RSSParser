namespace repository.entity.db
{
    public class TopicCategory
    {
        public int Id { get; set; }

        public virtual Topic Topic { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
