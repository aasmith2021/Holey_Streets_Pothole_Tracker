using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IUserManagementDao
    {
        public bool ChangeUserRole(int userId, string newRole);

        public List<ReturnUser> ListUsersByRole(string role);

        public bool RequestEmployeeAccess(ReturnUser user);

        public bool SetRequestStatusToInactive(int userId);

        public List<AccessChangeRequest> ListAllActiveChangeRequests();
        public bool CheckIfActiveRequest(ReturnUser user);
    }
}
