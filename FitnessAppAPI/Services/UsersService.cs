using FitnessAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FitnessAppAPI.Services
{
    public class UsersService
    {
        public readonly IMongoCollection<Users> _usersCollection;

        public UsersService(IOptions<DatabaseSetting> fitnessAppDatabaseSetting)
        {
            var connectionString = "mongodb+srv://user:passwordka123@cluster0.lfiztow.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var databaseName = "FitnessDatabase";

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _usersCollection = mongoDatabase.GetCollection<Users>("Users");

            //var mongoClient = new MongoClient(fitnessAppDatabaseSetting.Value.ConnectionString);
            //var mongoDatabase = mongoClient.GetDatabase(fitnessAppDatabaseSetting.Value.DatabaseName);
            //_usersCollection = mongoDatabase.GetCollection<Users>(fitnessAppDatabaseSetting.Value.UsersCollectionName);
        }

        //public async Task<List<Users>> GetAllEntries() =>
        //    await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<List<Users>> GetAllEntries() =>
            await _usersCollection.Find(x => x.admin == false).ToListAsync();

        public async Task<Users?> GetEntryById(string id) =>
          await _usersCollection.Find(x => x._id == id).FirstOrDefaultAsync();
        public async Task<Users?> GetEntryByCnp(string cnp) =>
          await _usersCollection.Find(x => x.CNP == cnp).FirstOrDefaultAsync();

        public async Task<Users?> GetEntryByEmail(string email) =>
         await _usersCollection.Find(x => x.email == email).FirstOrDefaultAsync();

        public async Task<Users?> GetEntryByPhone(string phone) =>
          await _usersCollection.Find(x => x.phone == phone).FirstOrDefaultAsync();

        public async Task<string?> GetNameById(string id)
        {
            var user = await GetEntryById(id);
            return user?.name;
        }

        public async Task<string?> GetCnpById(string id)
        {
            var user = await GetEntryById(id);
            return user?.CNP;
        }

        public async Task CreateEntry(Users newUser) =>
          await _usersCollection.InsertOneAsync(newUser);

        public async Task UpdateEntry(string id, Users updatedUser) =>
          await _usersCollection.ReplaceOneAsync(x => x._id == id, updatedUser);

        public async Task RemoveEntry(string id) =>
          await _usersCollection.DeleteOneAsync(x => x._id == id);
    }
}
