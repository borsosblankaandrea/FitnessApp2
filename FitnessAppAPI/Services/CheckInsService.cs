using FitnessAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace FitnessAppAPI.Services
{
    public class CheckInsService
    {
        private readonly IMongoCollection<CheckIns> _checkInsCollection;

        public CheckInsService(IOptions<DatabaseSetting> fitnessAppDatabaseSetting)
        {
            var connectionString = "mongodb+srv://user:passwordka123@cluster0.lfiztow.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var databaseName = "FitnessDatabase";

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _checkInsCollection = mongoDatabase.GetCollection<CheckIns>("CheckIns");

            //var mongoClient = new MongoClient(fitnessAppDatabaseSetting.Value.ConnectionString);
            //var mongoDatabase = mongoClient.GetDatabase(fitnessAppDatabaseSetting.Value.DatabaseName);
            //_checkInsCollection = mongoDatabase.GetCollection<CheckIns>("CheckIns");
        }

        public async Task<List<CheckIns>> GetAllEntries() =>
            await _checkInsCollection.Find(_ => true).ToListAsync();

        public async Task<CheckIns?> GetEntryById(string id) =>
            await _checkInsCollection.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateEntry(CheckIns newCheckIn) =>
            await _checkInsCollection.InsertOneAsync(newCheckIn);

        public async Task UpdateEntry(string id, CheckIns updatedCheckIn) =>
            await _checkInsCollection.ReplaceOneAsync(x => x._id == id, updatedCheckIn);

        public async Task RemoveEntry(string id) =>
            await _checkInsCollection.DeleteOneAsync(x => x._id == id);
    }
}
