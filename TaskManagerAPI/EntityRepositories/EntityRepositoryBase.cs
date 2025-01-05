using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.AdminNotify_Model;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.EntityRepositories
{
    public class EntityRepositoryBase<TEntity> : IEntityRepository<TEntity>

        where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly IAdminNotifyService _adminNotifyService;

        public EntityRepositoryBase(AppDbContext context, IAdminNotifyService adminNotifyService)
        {
            _context = context;
            _adminNotifyService = adminNotifyService;
        }
        public void Add(TEntity entity)
        {

            _context.Set<TEntity>().Add(entity);

            _context.SaveChanges();

        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);


            if (entity is Mission mission)
            {
                _adminNotifyService.NotifyAdmin(mission);
            }


        }
        public void Delete(TEntity entity)
        {

            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();

        }

    }
}
