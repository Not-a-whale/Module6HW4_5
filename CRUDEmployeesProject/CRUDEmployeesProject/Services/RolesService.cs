using CRUDEmployeesProject.Models;

namespace CRUDEmployeesProject.Services
{
    public class RolesService
    {
        private readonly List<Role> _roles;

        public RolesService()
        {
            _roles = new List<Role>();

            _roles.Add(item: new Role { Id = 1, Name = "admin" });
            _roles.Add(item: new Role { Id = 2, Name = "user" });
        }


        public IEnumerable<Role> GetAll()
        {
            return _roles;
        }
    }
}
