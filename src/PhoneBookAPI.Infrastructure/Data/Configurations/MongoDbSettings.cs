namespace PhoneBookAPI.Infrastructure.Data.Configurations
{
    public class MongoDbSettings
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
        public required string PersonCollectionName { get; set; }
    }
}