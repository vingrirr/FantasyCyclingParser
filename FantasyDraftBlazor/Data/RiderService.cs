using FantasyCyclingParser;

namespace FantasyDraftBlazor.Data
{
    public class RiderService
    {
        public Task<RiderPhoto> GetPhotoAsync()
        {
            RiderPhoto photo = Repository.RiderPhotoGetAll().First();
            return Task.FromResult(photo); 
        }

    }
}
