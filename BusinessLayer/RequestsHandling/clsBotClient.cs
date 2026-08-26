using BusinessLayer.BotData;
using BusinessLayer.ErrorHandler;
using BusinessLayer.ReturnResult;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace BusinessLayer.RequestsHandling
{
    internal class clsBotClient
    {
        private TelegramBotClient _ClientBot { get; set; }

        private CancellationTokenSource _BotCancelerToken { get; set; }

        private CancellationTokenSource _ChatsHandersCancelerToken { get; set; }


        internal clsBotClient(string Key)
        {
            _ClientBot = new TelegramBotClient(Key);
            _BotCancelerToken = new CancellationTokenSource();
            _ChatsHandersCancelerToken = new CancellationTokenSource();
        }

        internal async Task<clsReturnResult> Run()
        {
            try
            {
                await _ClientBot.GetMeAsync(_BotCancelerToken.Token);
                return new clsReturnResult(clsReturnResult.enResult.Success, "Bot is connected.");
            }
            catch (ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, ex.Message);
            }
        }


        internal async Task<clsReturnResult> Close()
        {
            if (_ClientBot == null)
                return new clsReturnResult(clsReturnResult.enResult.BotNotFound, "Bot is not found.");

            try
            {
                _BotCancelerToken.Cancel();
                _BotCancelerToken.Dispose();

                //If bot is closed stope handlings messages
                _ChatsHandersCancelerToken?.Cancel();
                _ChatsHandersCancelerToken?.Dispose();

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
                    BotDescription = (await _ClientBot.GetMyDescriptionAsync()).Description
                    ,
                    BotName = (await _ClientBot.GetMyNameAsync()).Name
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
                var BotCommands = await _ClientBot.GetMyCommandsAsync();
                return (new clsReturnResult(clsReturnResult.enResult.Success), BotCommands);
            }
            catch (ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync($"{ex.Message}");
                return (new clsReturnResult(clsReturnResult.enResult.Error, ex.Message), null);
            }
        }

        private void _RenewChatsHandlers()
        {
            _ChatsHandersCancelerToken = new CancellationTokenSource();
        }

        internal async Task<clsReturnResult> RunChatsHandlerEngine()
        {
            if (_ClientBot == null)
                return new clsReturnResult(clsReturnResult.enResult.BotNotFound, "Bot is not found.");

             return  await clsChatsHandlerEngine.StartHandler
                     (this._ClientBot, _ChatsHandersCancelerToken.Token);
        }


        internal async Task<clsReturnResult> CloseChatsHandlerEngine()
        {
            try
            {
                _ChatsHandersCancelerToken.Cancel();
                _ChatsHandersCancelerToken.Dispose();

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
