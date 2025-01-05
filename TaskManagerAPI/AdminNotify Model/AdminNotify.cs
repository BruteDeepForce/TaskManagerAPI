using TaskManagerAPI.Models;

namespace TaskManagerAPI.AdminNotify_Model
{
    public class AdminNotify : IAdminNotifyService
    {

        public void NotifyAdmin(Mission mission)
        {
            Console.WriteLine($"Mission {mission.Name} has been updated");

        }
    }

}

