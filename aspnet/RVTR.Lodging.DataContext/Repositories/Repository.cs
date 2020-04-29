using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RVTR.Lodging.DataContext.Repositories
{
  /// <summary>
  /// Represents the _Repository_ generic
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  public class Repository<TEntity> where TEntity : class
  {
    public readonly DbSet<TEntity> _db;

    public Repository(LodgingContext context)
    {
      _db = context.Set<TEntity>();
    }

    public async Task DeleteAsync(int id) => _db.Remove(await SelectAsync(id));

    public async Task InsertAsync(TEntity entry) => await _db.AddAsync(entry).ConfigureAwait(true);

    public async Task<IEnumerable<TEntity>> SelectAsync() => await _db.ToListAsync();

    public async Task<TEntity> SelectAsync(int id) => await _db.FindAsync(id).ConfigureAwait(true);

    public void Update(TEntity entry) => _db.Update(entry);
  }
}
