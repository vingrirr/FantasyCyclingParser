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

        public Task<List<FantasyCyclingParser.Rider>> GetAllRidersAsync()
        {
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(config.Year);
            
            return Task.FromResult(season.Riders);
        }

    }
}
