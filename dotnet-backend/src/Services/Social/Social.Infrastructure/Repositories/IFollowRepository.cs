﻿using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Repositories
{
    public interface IFollowRepository
    {
        Task CreateUserNode(User user);
        Task DeleteUserNode(User user);
        Task FollowUser(Relation relation);
        Task GetFollowers(User user);
    }
}
