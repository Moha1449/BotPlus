using BusinessLayer.DataModels;
using BusinessLayer.ErrorHandler;
using BusinessLayer.ReturnResult;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace BusinessLayer.BotEngine
{
    internal class clsBotClient
    {
        internal TelegramBotClient ClientBot { get; private set; }

        private CancellationTokenSource _BotCancelerToken { get; set; }


        internal clsBotClient(string Key)
        {
            ClientBot = new TelegramBotClient(Key);
            _BotCancelerToken = new CancellationTokenSource();

        }

        internal async Task<clsReturnResult> ConnectTheBot()
        {
            try
            {
                await ClientBot.GetMeAsync(_BotCancelerToken.Token);
                return new clsReturnResult(clsReturnResult.enResult.Success, "Bot is connected.");
            }
            catch (ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, ex.Message);
            }
        }


        internal async Task<clsReturnResult> CloseConnection()
        {
            if (ClientBot == null)
                return new clsReturnResult(clsReturnResult.enResult.BotNotFound, "Bot is not found.");

            try
            {
                _BotCancelerToken.Cancel();
                _BotCancelerToken.Dispose();

                return new clsReturnResult(clsReturnResult.enResult.Success, "Bot is stopped.");
            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Internal Error");
            }
        }

        internal async Task<(clsReturnResult, clsBotData)> GetMyInfo()
        {
            try
            {
                var BotData = new clsBotData
                {
                    BotDescription = (await ClientBot.GetMyDescriptionAsync()).Description
                    ,
                    BotName = (await ClientBot.GetMyNameAsync()).Name
                };

                return (new clsReturnResult(clsReturnResult.enResult.Success), BotData);
            }
            catch (ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync($"{ex.Message}");
                return (new clsReturnResult(clsReturnResult.enResult.Error, ex.Message), null);
            }
        }


        internal async Task<(clsReturnResult, BotCommand[])> GetMyCommands()
        {
            try
            {
                var BotCommands = await ClientBot.GetMyCommandsAsync();
                return (new clsReturnResult(clsReturnResult.enResult.Success), BotCommands);
            }
            catch (ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync($"{ex.Message}");
                return (new clsReturnResult(clsReturnResult.enResult.Error, ex.Message), null);
            }
        }
    }
}
