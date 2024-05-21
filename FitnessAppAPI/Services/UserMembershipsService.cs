using FitnessAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace FitnessAppAPI.Services
{
    public class UserMembershipsService
    {
        private readonly IMongoCollection<UserMemberships> _userMembershipsCollection;

        public UserMembershipsService(IOptions<DatabaseSetting> fitnessAppDatabaseSetting)
        {
            var connectionString = "mongodb+srv://user:passwordka123@cluster0.lfiztow.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var databaseName = "FitnessDatabase";

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _userMembershipsCollection = mongoDatabase.GetCollection<UserMemberships>("UserMemberships");

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

        public async Task RemoveEntry(string id) =>
            await _userMembershipsCollection.DeleteOneAsync(x => x._id == id);
    }
}
