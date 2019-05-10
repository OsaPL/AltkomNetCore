using System;
using System.Collections.Generic;
using System.Text;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.IServices
{
    public interface IUsersService: IEntityServices<User>
    {
        User Authenticate(string login, string password);
    }
}
