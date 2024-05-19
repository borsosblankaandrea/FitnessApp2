using FitnessAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FitnessAppAPI.Services
{
    public class UserService
    {
        public readonly IMongoCollection<Users> _usersCollection;

        public UserService(IOptions<DatabaseSetting> fitnessAppDatabaseSetting)
        {
            var mongoClient = new MongoClient(fitnessAppDatabaseSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(fitnessAppDatabaseSetting.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<Users>(fitnessAppDatabaseSetting.Value.CollectionName);
        }

        public async Task<List<Users>> GetAllEntries() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Users?> GetEntryById(string id) =>
          await _usersCollection.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateEntry(Users newUser) =>
          await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateEntry(string id, Users updatedUser) =>
          await _usersCollection.ReplaceOneAsync(x => x._id == id, updatedUser);

        public async Task RemoveEntry(string id) =>
          await _usersCollection.DeleteOneAsync(x => x._id == id);
    }
}
