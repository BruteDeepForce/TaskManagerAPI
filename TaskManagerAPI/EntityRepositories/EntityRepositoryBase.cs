using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.EntityRepositories
{
    public class EntityRepositoryBase<TEntity> : IEntityRepository<TEntity>

        where TEntity : class

    {

        private readonly AppDbContext _context;

        public EntityRepositoryBase(AppDbContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {

            _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();

        }
        public void Update(TEntity entity)
        {

            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();

        }
        public void Delete(TEntity entity)
        {

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

        }

    }
}
