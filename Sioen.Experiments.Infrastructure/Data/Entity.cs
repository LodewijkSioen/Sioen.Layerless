using System;

namespace Sioen.Experiments.Infrastructure.Data
{
    public class Entity
    {
        public virtual Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != this.GetType()) return false;

            var other = obj as Entity;

            if (this.Id == Guid.Empty && other.Id == Guid.Empty)
            {
                return base.Equals(other);
            }
            else
            {
                return this.Id == other.Id;
            }
        }

        public override int GetHashCode()
        {
            if (Id == Guid.Empty) return base.GetHashCode();
            return Id.GetHashCode();
        }
    }
}
