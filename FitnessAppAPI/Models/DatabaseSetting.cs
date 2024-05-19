namespace FitnessAppAPI.Models
{
    public class DatabaseSetting
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;
        public string MembershipsCollectionName { get; set; } = null!;
        public string UserMembershipsCollectionName { get; set; } = null!;
        public string CheckInsCollectionName { get; set; } = null!;

    }
}
