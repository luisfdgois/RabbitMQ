namespace Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
