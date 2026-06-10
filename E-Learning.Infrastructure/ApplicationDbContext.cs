using E_Learning.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await PublishDomainEventsAsync(cancellationToken);
        }
        private async Task PublishDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(e =>
                {
                    var events = e.GetDomainEvents();
                    e.ClearDomainEvents();
                    return events;
                });
            foreach (var domainEvent in domainEvents)
                await _publisher.Publish(domainEvent, cancellationToken);
        }
    }
}
