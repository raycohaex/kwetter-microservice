using Eventbus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kweet.Cmd.Domain.Aggregates
{
    public class AggregateRoot
    {
        protected Guid _id;
        private readonly List<IntegrationBaseEvent> _changes = new();

        public Guid Id
        {
            get { return _id; }
        }

        public int Version { get; set; } = -1;

        public IEnumerable<IntegrationBaseEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        private void ApplyChange(IntegrationBaseEvent @event, bool isNew)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}!");
            }

            method.Invoke(this, new object[] { @event });

            if (isNew)
            {
                _changes.Add(@event);
            }
        }

        protected void RaiseEvent(IntegrationBaseEvent @event)
        {
            ApplyChange(@event, true);
        }

        public void ReplayEvents(IEnumerable<IntegrationBaseEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyChange(@event, false);
            }
        }
    }
}
