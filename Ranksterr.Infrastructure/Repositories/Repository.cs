﻿using Microsoft.EntityFrameworkCore;
using Ranksterr.Domain.Abstractions;

namespace Ranksterr.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    // protected readonly ApplicationDbContext DbContext;
    //
    // protected Repository( ApplicationDbContext dbContext )
    // {
    //     DbContext = dbContext;
    // }
    //
    // public async Task<T?> GetByIdAsync( Guid id, CancellationToken cancellationToken = default )
    // {
    //     return await DbContext
    //                  .Set<T>()
    //                  //why user??
    //                  .FirstOrDefaultAsync( user => user.Id == id, cancellationToken );
    // }
    //
    // public virtual void Add( T entity )
    // {
    //     DbContext.Add( entity );
    // }
    
}