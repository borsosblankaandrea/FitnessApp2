using FitnessAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace FitnessAppAPI.Services
{
    public class UserMembershipsService
    {
        private readonly IMongoCollection<UserMemberships> _userMembershipsCollection;
        private readonly MembershipsService _membershipsService;

        public UserMembershipsService(IOptions<DatabaseSetting> fitnessAppDatabaseSetting, MembershipsService membershipsService)
        {
            var connectionString = "mongodb+srv://user:passwordka123@cluster0.lfiztow.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var databaseName = "FitnessDatabase";

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _userMembershipsCollection = mongoDatabase.GetCollection<UserMemberships>("UserMemberships");
            _membershipsService = membershipsService;

            //var mongoClient = new MongoClient(fitnessAppDatabaseSetting.Value.ConnectionString);
            //var mongoDatabase = mongoClient.GetDatabase(fitnessAppDatabaseSetting.Value.DatabaseName);
            //_userMembershipsCollection = mongoDatabase.GetCollection<UserMemberships>("UserMemberships");
        }

        public async Task<List<UserMemberships>> GetAllEntries() =>
            await _userMembershipsCollection.Find(_ => true).ToListAsync();

        public async Task<UserMemberships?> GetEntryById(string id) =>
            await _userMembershipsCollection.Find(x => x._id == id).FirstOrDefaultAsync();


        public async Task CreateEntry(UserMemberships newUserMemberships) =>
            await _userMembershipsCollection.InsertOneAsync(newUserMemberships);

        public async Task UpdateEntry(string id, UserMemberships updatedUserMemberships) =>
            await _userMembershipsCollection.ReplaceOneAsync(x => x._id == id, updatedUserMemberships);

        public async Task IncrementCheckinCount(string id)
        {
            var update = Builders<UserMemberships>.Update.Inc(x => x.checkin_count, 1);
            await _userMembershipsCollection.UpdateOneAsync(x => x._id == id, update);
        }

        public async Task UpdateActiveStatus(string userMembershipId, bool isActive)
        {
            var update = Builders<UserMemberships>.Update.Set(um => um.active, isActive);
            await _userMembershipsCollection.UpdateOneAsync(um => um._id == userMembershipId, update);
        }

        public async Task CheckExpirationDate()
        {
            var allUserMemberships = await GetAllEntries();
            foreach (var userMembership in allUserMemberships)
            {
                var membership = await _membershipsService.GetMembershipById(userMembership.membership_id);
                if (membership == null)
                {
                    throw new Exception("Membership not found.");
                }

               
                DateTime expirationDate = userMembership.purchase_date.AddDays((double)membership.hanynapigervenyes);
                if (DateTime.Now > expirationDate)
                {
                    await UpdateActiveStatus(userMembership._id, false);
                }
                
            }
        }


        public async Task RemoveEntry(string id) =>
            await _userMembershipsCollection.DeleteOneAsync(x => x._id == id);


    }
}
