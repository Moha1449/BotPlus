using BotPlusDataLayer.DatabaseSettings;
using DataModelLayer.DataModels;
using DataModelLayer.ErrorHandler;
using DataModelLayer.ReturnResult;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.DataProviders
{
    public sealed class clsConnectionDataProvider
    {
        public static async Task<(clsReturnResult, List<clsConnection>)> GetConnections()
        {
            try
            {
                var cursor = await clsCollectionsConfig.ConnectionReference
               .FindAsync<clsConnection>(
                 Builders<clsConnection>.Filter.Empty);

                var BotConnections = await cursor.ToListAsync();

                return (new clsReturnResult(clsReturnResult.enResult.Success), BotConnections);

            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return (new clsReturnResult(clsReturnResult.enResult.Error,"Database Execution : " + ex.Message), null);
            }
        }

        public static async Task<(clsReturnResult, clsConnection)> GetConnectionByID(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return (new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Database Execution : The Connection ID Is Empty."), null);
            }

            try
            {
                var Filter = Builders<clsConnection>
                    .Filter.Eq(x => x.ID, ID);

                var Cursor = await
                   clsCollectionsConfig.ConnectionReference.
                   FindAsync<clsConnection>(Filter);

                var Connection = await Cursor.FirstOrDefaultAsync();

                if (Connection == null)
                    return (new clsReturnResult(clsReturnResult.enResult.NotFound, "Database Execution : The Connection Is Not Found."), null);

                return (new clsReturnResult(clsReturnResult.enResult.Success), Connection);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return (new clsReturnResult(clsReturnResult.enResult.Error, "Database Execution : " + ex.Message), null);
            }
        }

        public static async Task<clsReturnResult> AddNewConnection(clsConnection Connection)
        {
            if (Connection == null || string.IsNullOrEmpty(Connection.Key))
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, 
                    "Database Execution : Invalid Inputs The Connection Object Is Empty Or Key Or Both.");

            try
            {
                await clsCollectionsConfig.ConnectionReference.InsertOneAsync(Connection);
                return new clsReturnResult(clsReturnResult.enResult.Success, 
                    "Database Execution : New Connection Is Created.");
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Database Execution : " + ex.Message);
            }
        }

        public static async Task<clsReturnResult> DeleteConnectionByID(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs,
                    "Database Execution : The Connection ID Is Empty.");
            }

            try
            {
                var Filter = Builders<clsConnection>
                    .Filter.Eq(x => x.ID, ID);

                var DeleteResult = await clsCollectionsConfig.ConnectionReference.
                    DeleteOneAsync(Filter);

                if (DeleteResult.DeletedCount == 0)
                    return new clsReturnResult(clsReturnResult.enResult.NotFound,"Database Execution : The Connection Is Not Found.");

                return new clsReturnResult(clsReturnResult.enResult.Success, 
                    $"Database Execution : New Connection With {ID} Is Deleted.");
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error,"Database Execution : " + ex.Message);
            }
        }
    }
}
