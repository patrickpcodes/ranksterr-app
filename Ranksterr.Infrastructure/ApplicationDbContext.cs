﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ranksterr.Application.Clock;
using Ranksterr.Application.Exceptions;
using Ranksterr.Domain.Abstractions;
using Ranksterr.Domain.ListableItems;
using Ranksterr.Domain.Listables;
using Ranksterr.Infrastructure.Outbox;

namespace Ranksterr.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public DbSet<ItemList> ItemLists { get; set; }
    public DbSet<MovieItem> MovieItems { get; set; }
    public DbSet<TvShowItem> TvShowItems { get; set; }
    public DbSet<TvShowEpisodeItem> TvShowEpisodeItems { get; set; }
    public DbSet<ListItemAssignment> ListItemAssignments { get; set; }
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly IDateTimeProvider _dateTimeProvider;

    public ApplicationDbContext(
        DbContextOptions options,
        IDateTimeProvider dateTimeProvider)
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ListItem>().HasKey( li => li.Id );
        
        modelBuilder.Entity<ListItemAssignment>()
                    .HasKey(lia => new { lia.ListId, lia.ListItemId });

        modelBuilder.Entity<ListItemAssignment>()
                    .HasOne(lia => lia.ItemList)
                    .WithMany(l => l.ListItemAssignments)
                    .HasForeignKey(lia => lia.ListId);

        modelBuilder.Entity<ListItemAssignment>()
                    .HasOne(lia => lia.ListItem)
                    .WithMany()
                    .HasForeignKey(lia => lia.ListItemId)
                    .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            AddDomainEventsAsOutboxMessages();

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    private void AddDomainEventsAsOutboxMessages()
    {
        var outboxMessages = ChangeTracker
                             .Entries<Entity>()
                             .Select(entry => entry.Entity)
                             .SelectMany(entity =>
                             {
                                 IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();

                                 entity.ClearDomainEvents();

                                 return domainEvents;
                             })
                             .Select(domainEvent => new OutboxMessage(
                                 Guid.NewGuid(),
                                 _dateTimeProvider.UtcNow,
                                 domainEvent.GetType().Name,
                                 JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
                             .ToList();

        AddRange(outboxMessages);
    }
}