using FitnessAppAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FitnessAppAPI.Services
{
    public class MembershipsService
    {
        private readonly IMongoCollection<Memberships> _membershipsCollection;

        public MembershipsService(IOptions<DatabaseSetting> fitnessAppDatabaseSetting)
        {
            var connectionString = "mongodb+srv://user:passwordka123@cluster0.lfiztow.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var databaseName = "FitnessDatabase";

            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _membershipsCollection = mongoDatabase.GetCollection<Memberships>("Memberships");


            //var mongoClient = new MongoClient(fitnessAppDatabaseSetting.Value.ConnectionString);
            //var mongoDatabase = mongoClient.GetDatabase(fitnessAppDatabaseSetting.Value.DatabaseName);
            //_membershipsCollection = mongoDatabase.GetCollection<Memberships>(fitnessAppDatabaseSetting.Value.MembershipsCollectionName);
        }

        public async Task<List<Memberships>> GetAllMemberships() =>
            await _membershipsCollection.Find(_ => true).ToListAsync();

        public async Task<Memberships?> GetMembershipById(string id) =>
          await _membershipsCollection.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateMembership(Memberships newMembership) =>
          await _membershipsCollection.InsertOneAsync(newMembership);

        public async Task UpdateMembership(string id, Memberships updatedMembership) =>
          await _membershipsCollection.ReplaceOneAsync(x => x._id == id, updatedMembership);

        public async Task RemoveMembership(string id) =>
          await _membershipsCollection.DeleteOneAsync(x => x._id == id);
    }
}
