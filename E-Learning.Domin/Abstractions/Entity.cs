namespace E_Learning.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        public Guid Id { get; protected set; }
        protected Entity(Guid Id)
            => this.Id = Id;
        public IReadOnlyList<IDomainEvent> GetDomainEvents() => _events.ToList();
        public void ClearDomainEvents() => _events.Clear();
        protected void RaiseDomainEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
    }
}
