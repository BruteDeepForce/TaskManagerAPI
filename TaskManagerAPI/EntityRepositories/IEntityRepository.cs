using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.EntityRepositories
{
    public interface IEntityRepository<Tentity> 
        where Tentity : class

    {
        public void Add(Tentity entity);  

        public void Update(Tentity entity);

        public void Delete(Tentity entity);
    }
}
