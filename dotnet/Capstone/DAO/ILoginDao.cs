using Capstone.Models;

namespace Capstone.DAO
{
    public interface ILoginDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);
    }
}
