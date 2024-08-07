﻿using API.Blog.DTO.Response;
using API.Blog.Entities;
using API.Blog.Repositories;
using AutoMapper;

namespace API.Blog.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagResponse>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            var tagResponses = _mapper.Map<IEnumerable<TagResponse>>(tags);

            return tagResponses;
        }

        public async Task<TagResponse> GetTagByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null)
            {
                return null;
            }
            var tagResponse = _mapper.Map<TagResponse>(tag);
            return tagResponse;
        }
    }
}
