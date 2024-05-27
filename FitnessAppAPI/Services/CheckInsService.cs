using FitnessAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace FitnessAppAPI.Services
{
    public class CheckInsService
    {
        private readonly IMongoCollection<CheckIns> _checkInsCollection;
        private readonly UserMembershipsService _userMembershipsService;
        private readonly MembershipsService _membershipsService;

        public CheckInsService(IOptions<DatabaseSetting> fitnessAppDatabaseSetting, UserMembershipsService userMembershipsService, MembershipsService membershipsService)
        {
            var connectionString = "mongodb+srv://user:passwordka123@cluster0.lfiztow.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var databaseName = "FitnessDatabase";

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _checkInsCollection = mongoDatabase.GetCollection<CheckIns>("CheckIns");
            _userMembershipsService = userMembershipsService;
            _membershipsService = membershipsService;

            //var mongoClient = new MongoClient(fitnessAppDatabaseSetting.Value.ConnectionString);
            //var mongoDatabase = mongoClient.GetDatabase(fitnessAppDatabaseSetting.Value.DatabaseName);
            //_checkInsCollection = mongoDatabase.GetCollection<CheckIns>("CheckIns");
        }

        public async Task<List<CheckIns>> GetAllEntries() =>
            await _checkInsCollection.Find(_ => true).ToListAsync();

        public async Task<CheckIns?> GetEntryById(string id) =>
            await _checkInsCollection.Find(x => x._id == id).FirstOrDefaultAsync();

        //public async Task CreateEntry(CheckIns newCheckIn) =>
        //    await _checkInsCollection.InsertOneAsync(newCheckIn);

        public async Task<string> CreateEntry(CheckIns newCheckIn)
        {
            var userMembership = await _userMembershipsService.GetEntryById(newCheckIn.user_membership_id);
            if (userMembership != null)
            {
                var membership = await _membershipsService.GetMembershipById(userMembership.membership_id);
                if (membership != null)
                {
                    if(userMembership.active == true) { 
                        if (userMembership.checkin_count < membership.hanybelepesreervenyes || membership.hanybelepesreervenyes == 0)
                        {
                            await _checkInsCollection.InsertOneAsync(newCheckIn);
                            await _userMembershipsService.IncrementCheckinCount(newCheckIn.user_membership_id);

                            if (userMembership.checkin_count + 1 == membership.hanybelepesreervenyes)
                            {
                                await _userMembershipsService.UpdateActiveStatus(userMembership._id, false);
                            }

                            return "Sikeresen bejelentkezve.";
                        }
                        else
                        {
                            return "A bejelentkezési limit meghaladva.";
                        }
                    }
                    else
                    {
                        return "A bérlet inaktív.";
                    }

                    
                }
                else
                {
                    return "Bérlet nem található.";
                }
            }
            else
            {
                return "A felhasználói bérlet nem található.";
            }
        }

        public async Task UpdateEntry(string id, CheckIns updatedCheckIn) =>
            await _checkInsCollection.ReplaceOneAsync(x => x._id == id, updatedCheckIn);

        public async Task RemoveEntry(string id) =>
            await _checkInsCollection.DeleteOneAsync(x => x._id == id);
    }
}
