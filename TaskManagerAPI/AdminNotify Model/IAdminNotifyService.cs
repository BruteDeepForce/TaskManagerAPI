using TaskManagerAPI.Models;

namespace TaskManagerAPI.AdminNotify_Model
{
    public interface IAdminNotifyService
    {
        public void NotifyAdmin(Mission mission);
    }
}
