using System;

namespace Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime LastUpdate { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }
    }
}
