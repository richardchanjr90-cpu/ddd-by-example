using System;
using System.Collections.Generic;
using MediatR;

namespace Loyalty.Core.Entities.SeedWork
{
    public abstract class Entity
    {
        private int? requestedHashCode;

        public virtual long Id { get; protected set; }

        private List<INotification> domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents ??= new List<INotification>();
            domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id == default(Int32);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            var item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
            {
                return false;
            }
            else
            {
                return item.Id == Id;
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                requestedHashCode ??= Id.GetHashCode() ^ 31;

                return requestedHashCode.Value;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (Object.Equals(left, null))
            {
                return Equals(right, null);
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
