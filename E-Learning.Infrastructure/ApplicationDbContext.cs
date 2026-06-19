using E_Learning.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace E_Learning.Infrastructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {

            await base.SaveChangesAsync(cancellationToken);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync(cancellationToken);

            return result;
        }

        private async Task PublishDomainEventsAsync(CancellationToken cancellationToken)
        {
            var domainEntries = ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.GetDomainEvents().Any())
                .ToList();

            var domainEvents = domainEntries
                .SelectMany(x => x.Entity.GetDomainEvents())
                .ToList(); 

            domainEntries.ForEach(x => x.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
