using Milestone_CST350.Models;

namespace Milestone_CST350.Services
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();
        MongoUsrersDAO mongoUsrersDAO = new MongoUsrersDAO();   

        public bool IsValid(MongoUser user)
        {
            //return securityDAO.findUser(user);
            return mongoUsrersDAO.FindByNameAndPassword(user);
        }
    }
}
