using BusinessLayer.AppStorage;
using BusinessLayer.RequestsHandling;
using BusinessLayer.ReturnResult;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Commands
{
    internal static class clsCommandsController
    {
        private static clsBotClient _Bot { get; set; }

        private static bool _IsBotRunning = false;

        private static async Task<clsReturnResult> _BotBuilderAndRunner()
        {
            (var FileState, var Connection) = await clsConnectionConfiguration.GetConnection();

            if (FileState == clsFileResults.enFileResult.Success)
            {
                _Bot = new clsBotClient(Connection.Key);
                return await _Bot.Run();
            }

            return clsReturnResult.FileReturnResultMaker(FileState);
        }

        internal static async Task<clsReturnResult> Run()
        {
            if (_IsBotRunning)
                return new clsReturnResult(clsReturnResult.enResult.Success,
                   "Bot is already connected.");

            var RunResult = await _BotBuilderAndRunner();
            _IsBotRunning = (RunResult.Result == clsReturnResult.enResult.Success);
            return RunResult;
        }

        private static void _ResetBot()
        {
            _Bot = null;
            _IsBotRunning = false;
        }


        internal static async Task<clsReturnResult> Close()
        {
            if (_Bot == null)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Bot is not connected");

            var CloseResult = await _Bot.Close();
            _ResetBot();

            return CloseResult;
        }


        public static async Task ConnectionKeySetter(string key)
        {
            await clsConnectionConfiguration.
                SetUpConnectionConfigFile(new BotData.clsConnection { Key = key });
        }

        internal static clsReturnResult BotStatus()
        {
            return new clsReturnResult(clsReturnResult.enResult.Success,( _IsBotRunning) ? "Bot is connected." :
                "Bot is not connected.");
        }

        public static bool IsBoRunning()
        {
            return _IsBotRunning;
        }

        public static async Task<clsReturnResult> GetBotCommands()
        {
            if (!_IsBotRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Bot is not connected.");

            (var GetResult,var Commands) = await _Bot.GetMyCommands();

            if (GetResult.Result == clsReturnResult.enResult.Success)
            {
                Func<string> BotCommandsToString = () =>
                {
                    string CommandsAsString = "";

                    foreach(var command in Commands)
                        CommandsAsString += $"[Command : {command.Command}] [Description : {command.Description}] ";
                    

                    return CommandsAsString; ;
                };
               
                return new clsReturnResult(GetResult.Result, "Commands : " + BotCommandsToString() + '.' );
            }

            return GetResult;
        }


        public static async  Task<clsReturnResult> GetBotData()
        {
            if (!_IsBotRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Bot is not connected.");

            (var GetResult,var BotData) = await _Bot.GetMyInfo();

            if(GetResult.Result == clsReturnResult.enResult.Success)
                return new clsReturnResult(GetResult.Result, "Bot Data : " +
                    $"[Name : {BotData.BotName}] [Description : {BotData.BotDescription}].");

            return GetResult;
        }
    }
}
