using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpeakerMeet.Core.DTOs;
using SpeakerMeet.Core.Entities;

namespace SpeakerMeet.Core.Interfaces.Services
{
    public interface ICommunityService
    {
        Task<CommunityResult> Get(Guid id);
        Task<CommunityResult> Get(string slug);
        Task<CommunitiesResult> GetAll(int pageIndex, int itemsPage, string? direction);
        Task<IEnumerable<CommunityFeatured>> GetFeatured();
        Task<Community> CreateCommunity(CommunityAdd communityAdd);  // OK maybe it should return a Task?
        Task<int> DeleteCommunity(Guid id);
    }
}
