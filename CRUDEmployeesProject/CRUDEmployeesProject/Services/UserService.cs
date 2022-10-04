using CRUDEmployeesProject.Models;

namespace CRUDEmployeesProject.Services
{
    public class UserService
    {

        private readonly List<User> _users = new List<User>();

        public UserService(RolesService roleService)
        {
            Role role = roleService.GetAll().FirstOrDefault(r => r.Name == "user");
            _users = new List<User>();

            _users.Add(item: new User()
            {
                Email = "sabaoth@ukr.net",
                Id = 1,
                Password = "123",
                Role = role,
                RoleId = role.Id
            });
        }

        public void Create(User user)
        {
            _users.Add(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }
    }
}
