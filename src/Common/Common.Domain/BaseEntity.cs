using System;

namespace Common.Domain
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime CreationDate { get; private set; }

        public BaseEntity()
        {
            CreationDate = DateTime.Now;
        }
    }
}