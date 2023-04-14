using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Core.Application.Interfaces;

/// <summary>
/// Fasada kontekstu bazy danych.
/// </summary>
public interface IAppDbContext
{
    DatabaseFacade Database { get; }
    
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    
    EntityEntry Add(object entity);
    
    EntityEntry Remove(object entity);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}