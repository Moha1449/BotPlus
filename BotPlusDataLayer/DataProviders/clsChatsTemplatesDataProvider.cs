using BotPlusDataLayer.DatabaseSettings;
using DataModelLayer.DataModels;
using DataModelLayer.ErrorHandler;
using DataModelLayer.ReturnResult;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace DataLayer.DataProviders
{
    public sealed class clsChatsTemplatesDataProvider
    {
        public static async Task<(clsReturnResult, List<clsChatTemplate>)> GetChatsTemplates()
        {
            try
            {
                var cursor = await clsCollectionsConfig.ChatsTemplateReference
               .FindAsync<clsChatTemplate>(
                 Builders<clsChatTemplate>.Filter.Empty);

                var ChatTemplate = await cursor.ToListAsync();

                return (new clsReturnResult(clsReturnResult.enResult.Success), ChatTemplate);

            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return (new clsReturnResult(clsReturnResult.enResult.Error, "Database Execution : " + ex.Message), null);
            }
        }

        public static async Task<(clsReturnResult, clsChatTemplate)> GetChatTemplateByID(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return (new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Database Execution : The ChatsTemplate ID Is Empty"), null);
            }

            try
            {
                var Filter = Builders<clsChatTemplate>
                    .Filter.Eq(x => x.ID, ID);

                var Cursor = await
                   clsCollectionsConfig.ChatsTemplateReference.
                   FindAsync<clsChatTemplate>(Filter);

                var ChatTemplate = await Cursor.FirstOrDefaultAsync();

                if (ChatTemplate == null)
                    return (new clsReturnResult(clsReturnResult.enResult.NotFound,"Database Execution : The chat template is not found."), null);

                return (new clsReturnResult(clsReturnResult.enResult.Success), ChatTemplate);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return (new clsReturnResult(clsReturnResult.enResult.Error, "Database Execution : " + ex.Message), null);
            }
        }

        public static async Task<clsReturnResult> AddNewChatTemplate(clsChatTemplate ChatTemplate)
        {
            if (ChatTemplate == null || ChatTemplate.Chats == null)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs,
                    "Database Execution : Invalid Inputs, The Chats Template Object Is Empty Or The Chats Object Is Empty Or Both.");

            try
            {
                await clsCollectionsConfig.ChatsTemplateReference.InsertOneAsync(ChatTemplate);
                return new clsReturnResult(clsReturnResult.enResult.Success, "Database Execution : New ChatTemplate Is Created.");
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, ex.Message);
            }
        }

        public static async Task<clsReturnResult> DeleteConnectionByID(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Database Execution : The ChatsTemplate ID Is Empty");
            }

            try
            {
                var Filter = Builders<clsChatTemplate>
                    .Filter.Eq(x => x.ID, ID);

                var DeleteResult = await clsCollectionsConfig.ChatsTemplateReference.
                    DeleteOneAsync(Filter);

                if (DeleteResult.DeletedCount == 0)
                    return new  clsReturnResult(clsReturnResult.enResult.NotFound, "Database Execution : The chat template is not found.");

                return new clsReturnResult(clsReturnResult.enResult.Success,
                    $"Database Execution : New ChatTemplate With {ID} Is Deleted.");
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, ex.Message);
            }
        }
    }
}
