using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.EntityRepositories;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Register_Model
{
    public class Register : IRegisterService
    {
        private readonly AppDbContext _context;
        private readonly IEntityRepository<Employee> _employeeRepository;   

        public Register(AppDbContext context, IEntityRepository<Employee> employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        public bool RegisterOp(Employee employee)
        {
            var singleEmployee = _context.Employees.FirstOrDefault(x => x.Email == employee.Email);

            if (employee == null)
            {
                return false;
            }

            _employeeRepository.Add(employee);
            return true;
        }
    }
}
