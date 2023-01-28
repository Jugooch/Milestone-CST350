using Milestone_CST350.Models;

namespace Milestone_CST350.Services
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();

        public bool IsValid(UserModel user)
        {
            return securityDAO.findUser(user);
        }
    }
}
