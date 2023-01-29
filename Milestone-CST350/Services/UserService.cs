using Milestone_CST350.Models;

namespace Milestone_CST350.Services
{
    public class UserService
    {
            UserDAO userDAO = new UserDAO();

            public bool IsValid(UserModel user)
            {
                return userDAO.addUser(user);
            }
        
    }
}
