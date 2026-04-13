using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Aggregates.Workers
{
    public class Worker : AuditableEntity, IAggregateRoot
    {
        public Worker(
            string workerId,
            string phone,
            string name,
            string lastName)
        {
            WorkerId = workerId;
            Phone = phone;
            Name = name;
            LastName = lastName;
        }

        private Worker()
        {
            //ef core
        }

        public string WorkerId { get; private set; }

        public string Phone { get; private set; }

        public string Name { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string PhotoUri { get; private set; }

        public bool IsArchived { get; private set; }

        public Worker Invite(
                        string phone,
                        string name,
                        string lastName)
        {
            Phone = phone;
            Name = name;
            LastName = lastName;

            return this;
        }

        public void Update(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }

        public void CompleteInviteWithExternalId(string workerId)
        {
            WorkerId = workerId;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void Archive()
        {
            IsArchived = true;
        }
    }
}
