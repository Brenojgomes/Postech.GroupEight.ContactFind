namespace Postech.GroupEight.ContactFind.Infra
{
    /// <summary>
    /// Represents the options for connecting to a MongoDB database.
    /// </summary>
    public class MongoDbOptions
    {
        /// <summary>
        /// The connection string for the MongoDB server.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// The name of the database to connect to.
        /// </summary>
        public string Database { get; set; }
    }
}
