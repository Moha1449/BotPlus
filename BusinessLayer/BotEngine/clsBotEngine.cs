using BusinessLayer.AppStorage;
using BusinessLayer.ErrorHandler;
using BusinessLayer.ReturnResult;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;

namespace BusinessLayer.BotEngine
{
    public static class clsBotEngine
    {

        private static clsBotClient _Bot = null;


        private static CancellationTokenSource _CancelChatsEngineSource = new CancellationTokenSource();


        private static clsChatsHandlerEngine _HandlerEngine = null;


        private static bool _IsBotRunning = false;


        private static async Task<clsReturnResult> _BotBuilderAndRunner()
        {
            (var GetResult, var Connection) = await clsAppStorage.GetConnectionAndItObject();

            if (GetResult.Result == clsReturnResult.enResult.Success)
            {
                _Bot = new clsBotClient(Connection.Key);
                return await _Bot.ConnectTheBot();
            }

            return GetResult;
        }

        public static async Task<clsReturnResult> RunBot()
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


        public static async Task<clsReturnResult> Close()
        {
            if (_Bot == null)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Bot is not connected");

            if (_HandlerEngine != null && _HandlerEngine.IsEngineRunning)
            {
                _CancelChatsEngineSource?.Cancel();
                _CancelChatsEngineSource?.Dispose();

                _RenewChatsHandlers();
            }

            var CloseResult = await _Bot.CloseConnection();
            _ResetBot();

            return CloseResult;
        }

        public static clsReturnResult BotStatus()
        {
            return new clsReturnResult(clsReturnResult.enResult.Success, (_IsBotRunning) ? "Bot is connected." :
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

            (var GetResult, var Commands) = await _Bot.GetMyCommands();

            if (GetResult.Result == clsReturnResult.enResult.Success)
            {
                Func<string> BotCommandsToString = () =>
                {
                    string CommandsAsString = "";

                    foreach (var command in Commands)
                        CommandsAsString += $"[Command : {command.Command}] [Description : {command.Description}] ";


                    return CommandsAsString; ;
                };

                return new clsReturnResult(GetResult.Result, "Commands : " + BotCommandsToString() + '.');
            }

            return GetResult;
        }

        internal static async Task<clsReturnResult> GetBotData()
        {
            if (!_IsBotRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Bot is not connected.");

            (var GetResult, var BotData) = await _Bot.GetMyInfo();

            if (GetResult.Result == clsReturnResult.enResult.Success)
                return new clsReturnResult(GetResult.Result, "Bot Data : " +
                    $"[Name : {BotData.BotName}] [Description : {BotData.BotDescription}].");

            return GetResult;
        }

        private static void _RenewChatsHandlers()
        {
            _CancelChatsEngineSource = new CancellationTokenSource();
            _HandlerEngine = null;
        }

        internal static async Task<clsReturnResult> RunTheChatsHandlerEngine()
        {
            if (!_IsBotRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Bot is not connected.");

            (var LoadResult, var ChatsTemplates) = await clsAppStorage.GetChatsTemplatesAsList();

            if (LoadResult.Result != clsReturnResult.enResult.Success)
                return LoadResult;

            if (_HandlerEngine == null)
                _HandlerEngine = new clsChatsHandlerEngine(30, _Bot.ClientBot, _CancelChatsEngineSource.Token
                    , ChatsTemplates.ToDictionary(key => key.Message, value => value.Response));

            else if (_HandlerEngine.IsEngineRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Chats handler engine is already running.");


            return await _HandlerEngine.EngineRunner();
        }

        internal static async Task<clsReturnResult> CloseTheChatsHandlerEngine()
        {
            if (_HandlerEngine == null || !_HandlerEngine.IsEngineRunning || !_IsBotRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Chats handler engine is off.");

            try
            {
                _CancelChatsEngineSource?.Cancel();
                _CancelChatsEngineSource?.Dispose();

                //After closing the chats handler renew the cancelation source to make user able to run it again
                _RenewChatsHandlers();

                return new clsReturnResult(clsReturnResult.enResult.Success, "Chats Engine is stopped.");
            }
            catch (ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Error : " + ex.Message);
            }

        }
    }
}
