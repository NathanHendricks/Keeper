using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _profilesRepository;
        public ProfilesService(ProfilesRepository profilesRepository)
        {
            _profilesRepository = profilesRepository;
        }

        public Profile GetById(string id)
        {
            Profile foundProfile = _profilesRepository.GetById(id);
            if (foundProfile == null)
            {
                throw new System.Exception("unable to find profile");
            }
            return foundProfile;
        }
    }
}