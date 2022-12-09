using AutoMapper;
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
        private readonly IFollowRepository _followRepository;
        private readonly IMapper _mapper;

        public FollowService(IFollowRepository followRepository, IMapper mapper)
        {
            _followRepository = followRepository;
            _mapper = mapper;
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
    }
}
