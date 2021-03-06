﻿namespace TwitterBackup.Services.Contracts
{
    using System.Collections.Generic;
    using TwitterBackup.Models;

    public interface IUserService
    {
        User Save(User user);

        int GetUsersCount();

        IEnumerable<User> GetUsers();
    }
}
