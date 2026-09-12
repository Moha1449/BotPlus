using DataModelLayer.DataModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BotPlusDataLayer.DatabaseSettings
{
    internal static class clsCollectionsConfig
    {
        internal static IMongoCollection<clsConnection> ConnectionReference { get; private set; }

        internal static IMongoCollection<clsChatTemplate> ChatsTemplateReference { get; private set; }

        static clsCollectionsConfig()
        {   
            //Maps the ids fields
            _MapIDs();

            //Set up the collections references 

            ConnectionReference =
            clsDatabaseConnectionConfig.Database.GetCollection<clsConnection>
            (
             //Set the name of the collection here
             "Connections"
            );

            ChatsTemplateReference =
            clsDatabaseConnectionConfig.Database.GetCollection<clsChatTemplate>
            (
             //Set the name of the collection here
             "ChatTemplates"
            );
        }


        private static void _MapIDs()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(clsConnection)))
            {
                BsonClassMap.RegisterClassMap<clsConnection>(cm =>
                {
                    cm.AutoMap();

                    var idMap = cm.MapIdMember(x => x.ID);
                    idMap.SetSerializer(
                        new StringSerializer(BsonType.ObjectId)
                    );

                    idMap.SetIdGenerator(StringObjectIdGenerator.Instance);
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(clsChatTemplate)))
            {
                BsonClassMap.RegisterClassMap<clsChatTemplate>(cm =>
                {
                    cm.AutoMap();

                    var idMap = cm.MapIdMember(x => x.ID);
                    idMap.SetSerializer(
                        new StringSerializer(BsonType.ObjectId)
                    );

                    idMap.SetIdGenerator(StringObjectIdGenerator.Instance);

                });
            }
        }
    }
}
