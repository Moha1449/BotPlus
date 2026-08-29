using BusinessLayer.DataModels;
using BusinessLayer.ErrorHandler;
using BusinessLayer.AppStorage.FileResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.AppStorage.Messages
{
    internal static class clsChatsTemplatesStorage
    {
        private static string _InLineMessagesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChatsTemplates.json");

        internal static async Task<(clsFileResults.enFileResult, IEnumerable<clsChatTemplate>)> GetChatsTemplatesAsString()
        {
            try
            {
                if (!File.Exists(_InLineMessagesFilePath))
                {
                    File.Create(_InLineMessagesFilePath);
                    return (clsFileResults.enFileResult.Empty, null);
                }

                using (FileStream Chat = File.OpenRead(_InLineMessagesFilePath))
                {
                    List<clsChatTemplate> Chats = new List<clsChatTemplate>();

                    using (StreamReader Reader = new StreamReader(Chat))
                    {
                        string Line = string.Empty;

                        while ((Line = await Reader.ReadLineAsync()) != null)
                        {
                            Chats.Add(JsonConvert.DeserializeObject<clsChatTemplate>(Line));
                        }
                    }

                    return (clsFileResults.enFileResult.Success, Chats);
                }



            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return (clsFileResults.enFileResult.Error, null);
            }
        }


        internal static async Task<clsFileResults.enFileResult> AddChatTemplate(clsChatTemplate Chat)
        {
            try
            {
                if (!File.Exists(_InLineMessagesFilePath))
                {
                    File.Create(_InLineMessagesFilePath);
                }

                using (FileStream ChatsFile = new FileStream(_InLineMessagesFilePath, FileMode.Append))
                {
                    using (StreamWriter Writer = new StreamWriter(ChatsFile))
                    {
                      await  Writer.WriteLineAsync(JsonConvert.SerializeObject(Chat));
                    }

                    return clsFileResults.enFileResult.Success;
                }


            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return clsFileResults.enFileResult.Error;
            }
        }
    }
}
