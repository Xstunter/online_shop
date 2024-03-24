using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Order.Host.Services.Interfaces;

public interface IDbContextWrapper<T>
     where T : DbContext
{
     T DbContext { get; }
     Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
}