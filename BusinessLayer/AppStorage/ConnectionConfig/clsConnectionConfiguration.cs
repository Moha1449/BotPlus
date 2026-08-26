using BusinessLayer.BotData;
using BusinessLayer.ErrorHandler;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.AppStorage
{
    internal abstract class clsConnectionConfiguration
    {
        private static string _ConnectionConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConnectionConfig.json");

        internal static async Task<clsFileResults.enFileResult> SetUpConnectionConfigFile(clsConnection Connection)
        {
            if(Connection == null || string.IsNullOrEmpty(Connection.Key))
            {
                return clsFileResults.enFileResult.InvalidInputs;
            }

            try
            {
                string ConnectionAsJson = JsonConvert.SerializeObject(Connection,Formatting.None);

                using (StreamWriter Writer = File.CreateText(_ConnectionConfigPath))
                {
                    await Writer.WriteLineAsync(ConnectionAsJson);
                }

                return clsFileResults.enFileResult.Success;
            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return clsFileResults.enFileResult.Error;
            }
        }


        internal static async Task<(clsFileResults.enFileResult,clsConnection)> GetConnection()
        {
            try
            {
                if (!File.Exists(_ConnectionConfigPath))
                    return (clsFileResults.enFileResult.NotExist, null);

                string ConnectionAsJson = "";

                using (FileStream Connection = new FileStream(_ConnectionConfigPath, FileMode.Open,FileAccess.Read))
                {
                    using (StreamReader Reader = new StreamReader(Connection))
                    {
                        ConnectionAsJson = await Reader.ReadLineAsync();
                    }
                }

                if(string.IsNullOrEmpty(ConnectionAsJson))
                {
                    return (clsFileResults.enFileResult.Empty, null);
                }

                   return (clsFileResults.enFileResult.Success,JsonConvert.DeserializeObject<clsConnection>(ConnectionAsJson));
            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return(clsFileResults.enFileResult.Error,null); 
               
            }
        }
    }
}
