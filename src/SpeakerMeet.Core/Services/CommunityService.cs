using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SpeakerMeet.Core.Cache;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;
using SpeakerMeet.Core.Interfaces.Caching;
using SpeakerMeet.Core.Interfaces.Repositories;
using SpeakerMeet.Core.Interfaces.Services;
using SpeakerMeet.Core.Specifications;

namespace SpeakerMeet.Core.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICacheManager _cache;
        private readonly ISpeakerMeetRepository _repository;

        public CommunityService(
            ICacheManager cache,
            ISpeakerMeetRepository repository
        )
        {
            _cache = cache;
            _repository = repository;
        }

        public async Task<CommunityResult> Get(Guid id)
        {
            var community =  await _repository.Get(new CommunitySpecification(id));

            return new CommunityResult
            {
                Id = community.Id,
                Location = community.Location,
                Name = community.Name,
                Slug = community.Slug,
                Description = community.Description
            };
        }

        public async Task<CommunityResult> Get(string slug)
        {
            var community =  await _repository.Get(new CommunitySpecification(slug));

            return new CommunityResult
            {
                Id = community.Id,
                Location = community.Location,
                Name = community.Name,
                Slug = community.Slug,
                Description = community.Description,
                Tags = community.CommunityTags.Select(x => x.Tag.Name),
                SocialPlatforms = community.CommunitySocialPlatforms.Select(x => new SocialMedia
                {
                    Name = x.SocialPlatform.Name,
                    Url = x.Url
                })
            };
        }

        public async Task<CommunitiesResult> GetAll(int pageIndex, int itemsPage, string? direction)
        {
            var spec = new CommunitySpecification(itemsPage * pageIndex, itemsPage, direction);

            var communities = await _repository.List(spec);
            var total = await _repository.Count<Community>();

            return new CommunitiesResult{
                Communities = communities.Select(x => new CommunitiesResult.Community
                {
                    Id = x.Id,
                    Location = x.Location,
                    Name = x.Name,
                    Slug = x.Slug,
                    Description = x.Description,
                }),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = communities.Count,
                    TotalItems = total,
                    TotalPages =
                        int.Parse(Math.Ceiling((decimal)total / itemsPage)
                            .ToString(CultureInfo.InvariantCulture))
                }
            };
        }

        public async Task<IEnumerable<CommunityFeatured>> GetFeatured()
        {
            return await _cache.GetOrCreate(CacheKeys.FeaturedCommunities, async () => await GetRandomCommunities());
        }

        private async Task<IEnumerable<CommunityFeatured>> GetRandomCommunities()
        {
            var communities = await _repository.List(new CommunityRandomSpecification());

            var results = communities.Select(x => new CommunityFeatured
            {
                Id = x.Id,
                Location = x.Location,
                Name = x.Name,
                Slug = x.Slug,
                Description = x.Description
            });

            return results;
        }

        public Task<Community> CreateCommunity(CommunityAdd communityAdd)
        {
            //var tags = 

            Community community = new Community()
            {
                Id = Guid.NewGuid(),
                Name = communityAdd.Name,
                Slug = communityAdd.Slug,
                Location = communityAdd.Location,
                Description = communityAdd.Description,
                IsActive = communityAdd.IsActive,
                CreatedBy = communityAdd.CreatedBy,
                Created = DateTime.UtcNow,
                UpdatedBy = communityAdd.UpdatedBy,
                Updated = new DateTime(0001, 01, 01)  //Almost null... since this is a create, thought it best to give this a default value which can be changed when/if update occurs
            };
            var addedCommunity =_repository.Add<Community>(community);
            return addedCommunity;
        }

        public async Task UpdateCommunity(CommunityUpdate communityUpdate)
        {
            Community community = new Community()
            {
                Id = communityUpdate.Id,
                Name = communityUpdate.Name,
                Slug = communityUpdate.Slug,
                Location = communityUpdate.Location,
                Description = communityUpdate.Description,
                IsActive = communityUpdate.IsActive,
                CreatedBy = communityUpdate.CreatedBy,
                Created = communityUpdate.Created,
                UpdatedBy = communityUpdate.UpdatedBy,
                Updated = communityUpdate.Updated
            };
             await _repository.Update<Community>(community);

          }

        public async Task<int> DeleteCommunity(Guid id)
        {
            var community = await _repository.Get(new CommunitySpecification(id));
            if(community != null)
            {
                await _repository.Delete<Community>(community);
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public async Task<CommunityResult> UpdateTags(Guid id, string[] tags)
        {
            var community = await _repository.Get(new CommunitySpecification(id));
            // Get community tags for ID
            // Get Tags
            // Foreach ConferenceTagDto not in Conference.Tags - Add
            // Foreach ConferenceTagDto in Conference.Tags - Update (not sure if there's anything to do in our example)
            // Foreach Conference.Tags not in ConferenceTagDto - Delete

            var dbTags = await _repository.List(new TagSpecification());
            foreach (var tag in tags)
            {
                if(!community.CommunityTags.Any(t => t.Tag.Name == tag))
                {
                    community.CommunityTags.Add(new CommunityTag { Tag = dbTags.Single(c => c.Name == tag) });
                }
            }
            foreach (var dbTag in dbTags)
            {
                foreach (var tag in tags)
                {
                    if (community.CommunityTags.Any(t => t.Tag.Name != tag))
                    {
                        await _repository.Delete(dbTag);
                    }
                }
            }
            await _repository.Update(community);
            return new CommunityResult
            {
                Id = community.Id,
                Location = community.Location,
                Name = community.Name,
                Slug = community.Slug,
                Description = community.Description
            };
        }
    }
}