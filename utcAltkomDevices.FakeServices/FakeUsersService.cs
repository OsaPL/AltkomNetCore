using System;
using System.Collections.Generic;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.FakeServices
{
    public class FakeUsersService : IUsersService
    {
        public FakeUsersService()
        {

        }

        public User Authenticate(string login, string password)
        {
            if (login == "admin" && password == "1234")
            {
                return new User() { Login = "admin", Name = "Master", Id = 0 ,Role = "AdminRole"};
            }
            else
            {
                return null;
            }
        }

        public bool Add(User input)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> Get()
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(string name)
        {
            throw new NotImplementedException();
        }

        public User Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User input)
        {
            throw new NotImplementedException();
        }
    }
}
