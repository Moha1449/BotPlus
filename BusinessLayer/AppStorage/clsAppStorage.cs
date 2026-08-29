using BusinessLayer.AppStorage.ConnectionConfiguration;
using BusinessLayer.AppStorage.Messages;
using BusinessLayer.DataModels;
using BusinessLayer.ReturnResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.AppStorage
{
    public static class clsAppStorage
    {
        public static async Task<clsReturnResult> ConnectionKeySetter(string key)
        {
            return clsReturnResult.FileReturnResultMaker(await clsConnectionConfiguration.
                SetUpConnectionConfigFile(new clsConnection { Key = key }));
        }

        public static async Task<clsReturnResult> AddCustomerMessage(string Message, string Response)
        {
            return clsReturnResult.FileReturnResultMaker
                  (await clsChatsTemplatesStorage.AddChatTemplate(new clsChatTemplate
                  {
                      ID = Guid.NewGuid().ToString(),
                      Message = Message,
                      Response = Response
                  }));
        }

        internal static async Task<(clsReturnResult, IEnumerable<clsChatTemplate>)> GetChatsTemplatesAsList()
        {
            (var ReadResult, var Chats) = await clsChatsTemplatesStorage.GetChatsTemplatesAsString();

            return (clsReturnResult.FileReturnResultMaker(ReadResult), Chats);
        }

        internal static async Task<(clsReturnResult, clsConnection)> GetConnectionAndItObject()
        {
            (var FileState, var Connection) = await clsConnectionConfiguration.GetConnection();

            return (clsReturnResult.FileReturnResultMaker(FileState), Connection);
        }

        private static string _CovertChatsTemplatesToString(IEnumerable<clsChatTemplate> ChatTemplates)
        {
            string ChatsTemplatesAsString = "Chats Templates : ";

            foreach (var ChatTemplate in ChatTemplates)
            {
                ChatsTemplatesAsString += $"[ID : {ChatTemplate.ID}] [Message : {ChatTemplate.Message}] [Response : {ChatTemplate.Response}],";
            }

            return ChatsTemplatesAsString;
        }

        public static async Task<clsReturnResult> GetChatsTemplates()
        {
            (var ReadResult, var Chats) = await clsChatsTemplatesStorage.GetChatsTemplatesAsString();

            if (ReadResult == FileResults.clsFileResults.enFileResult.Success)
            {
                return new clsReturnResult(clsReturnResult.enResult.Success, 
                    _CovertChatsTemplatesToString(Chats));
            }

            return (clsReturnResult.FileReturnResultMaker(ReadResult));
        }

        public static async Task<clsReturnResult> GetConnectionAsSting()
        {
            (var FileState, var Connection) = await clsConnectionConfiguration.GetConnection();

            if (FileState == FileResults.clsFileResults.enFileResult.Success)
            {
                return new clsReturnResult(clsReturnResult.enResult.Success, "Key : " + Connection.Key);
            }

            return clsReturnResult.FileReturnResultMaker(FileState);
        }



    }
}
