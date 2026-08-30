using BusinessLayer.AppStorage.FileResults;
using BusinessLayer.DataModels;
using BusinessLayer.ErrorHandler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.AppStorage.Messages
{
    internal static class clsChatsTemplatesStorage
    {
        private static string _InLineMessagesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChatsTemplates.json");

        internal static async Task<(clsFileResults.enFileResult, List<clsChatTemplate>)> GetChatsTemplatesAsString()
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
                        await Writer.WriteLineAsync(JsonConvert.SerializeObject(Chat));
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


        private static async Task<clsFileResults.enFileResult> RewriteTheChatTemplatesFile(List<clsChatTemplate> Templates)
        {
            if (Templates == null)
            {
                return clsFileResults.enFileResult.InvalidInputs;
            }

            try
            {
                using (FileStream ChatsFile = new FileStream(_InLineMessagesFilePath, FileMode.Create))
                {
                    using (StreamWriter Writer = new StreamWriter(ChatsFile))
                    {
                        foreach (clsChatTemplate Template in Templates)
                            await Writer.WriteLineAsync(JsonConvert.SerializeObject(Template));
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

        internal static async Task<clsFileResults.enFileResult> DeleteChatTemplateByID(string ID)
        {
            try
            {
                if (!File.Exists(_InLineMessagesFilePath))
                {
                    File.Create(_InLineMessagesFilePath);
                    return clsFileResults.enFileResult.Empty;
                }

                var (FileResult, Templates) = await GetChatsTemplatesAsString();

                if (FileResult != clsFileResults.enFileResult.Success)
                    return FileResult;

                if (Templates.Count == 0)
                    return clsFileResults.enFileResult.Empty;

                int PerviousCount = Templates.Count;

                Templates = Templates.Where(T => T.ID != ID).ToList();

                if (PerviousCount == Templates.Count)
                    return clsFileResults.enFileResult.ItemNotFound;

                return await RewriteTheChatTemplatesFile(Templates);

            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return clsFileResults.enFileResult.Error;
            }
        }


    }
}
