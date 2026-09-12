using MongoDB.Driver;

namespace BotPlusDataLayer.DatabaseSettings
{
    internal static class clsDatabaseConnectionConfig
    {
        internal static MongoClient _Connection { get; private set; }


        internal static IMongoDatabase Database { get; private set; }


        static clsDatabaseConnectionConfig()
        {
            _Connection = new MongoClient
            (
              //Set connection string here
              "mongodb://localhost:27017"
            );

            Database = _Connection.GetDatabase
             (
                //Set database name that contains app data
               "BotPlus"
             );
        }


    }
}
