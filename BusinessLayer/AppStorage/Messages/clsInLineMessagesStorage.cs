using BusinessLayer.DTOs.Messages;
using BusinessLayer.ErrorHandler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.AppStorage.Messages
{
    class clsInLineMessagesStorage
    {
        private static string _InLineMessagesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InLinesMessages.json");

        internal async Task<(clsFileResults.enFileResult, List<clsInLineChat>)> GetInLinesMessages()
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
                    List<clsInLineChat> Chats = new List<clsInLineChat>();

                    using (StreamReader Reader = new StreamReader(Chat))
                    {
                        string Line = string.Empty;

                        while ((Line = await Reader.ReadToEndAsync()) != null)
                        {
                            Chats.Add(JsonConvert.DeserializeObject<clsInLineChat>(Line));
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


        internal async Task<clsFileResults.enFileResult> AddNewInLineChat(clsInLineChat Chat)
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
