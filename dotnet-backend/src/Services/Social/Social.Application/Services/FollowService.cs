﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using Social.Application.Contracts;
using Social.Application.Dto;
using Social.Domain.Entities;
using Social.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Services
{
    public class FollowService : IFollowService
    {
        public const string FOLLOWERS = "followers";
        public const string FOLLOWING = "following";
        private readonly IFollowRepository _followRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<FollowService> _logger;

        public FollowService(IFollowRepository followRepository, IMapper mapper, ILogger<FollowService> logger)
        {
            _followRepository = followRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateFollowRelation(RelationDto relation)
        {
            var _relation = _mapper.Map<RelationDto, Relation>(relation);
            await _followRepository.FollowUser(_relation);
        }

        public async Task<UserDto> CreateUserNode(UserDto user)
        {
            var _user = _mapper.Map<UserDto, User>(user);
            await _followRepository.CreateUserNode(_user);
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetFollowers(string username)
        {
            _logger.LogInformation($"Getting followers for {username}");
            var users = await _followRepository.GetFollowers(username);
            var usersdto = _mapper.Map<IEnumerable<UserDto>>(users);
            return usersdto;
        }

        public async Task<Dictionary<string, long>> GetUserSocialStats(string userName)
        {
            var followingcount = await _followRepository.GetFollowingCount(userName);
            var followerscount = await _followRepository.GetFollowersCount(userName);

            var dict = new Dictionary<string, long>();

            dict.Add(FOLLOWERS, followerscount);
            dict.Add(FOLLOWING, followingcount);

            return dict;
        }
    }
}
