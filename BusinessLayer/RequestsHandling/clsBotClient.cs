using System;
using Telegram.Bot;
using System.Threading.Tasks;
using System.Threading;
using BusinessLayer.ErrorHandler;
using BusinessLayer.ReturnResult;
using Telegram.Bot.Exceptions;
using BusinessLayer.BotData;
using Telegram.Bot.Types;

namespace BusinessLayer.RequestsHandling
{
    internal class clsBotClient
    {
        private TelegramBotClient _ClientBot { get; set; }

        private CancellationTokenSource _CancelToken {  get; set; } 


        internal clsBotClient(string Key)
        {
            _ClientBot = new TelegramBotClient(Key);
            _CancelToken = new CancellationTokenSource();
        }

        internal async Task<clsReturnResult> Run()
        {
            try
            {
                await _ClientBot.GetMeAsync(_CancelToken.Token);
                return new clsReturnResult(clsReturnResult.enResult.Success,"Bot is connected.");
            }
            catch (ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error,ex.Message);
            }
        }

        
        internal async Task<clsReturnResult> Close()
        {
            if( _ClientBot == null)
                return new clsReturnResult(clsReturnResult.enResult.BotNotFound,"Bot is not found.");

            try
            {
                _CancelToken.Cancel();
                _CancelToken.Dispose();
                return new clsReturnResult(clsReturnResult.enResult.Success,"Bot is stopped.");
            }
            catch(Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error,"Internal Error");
            }
        }



        internal async Task<(clsReturnResult,clsBotData)> GetMyInfo()
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
            catch(ApiRequestException ex)
            {
                await clsErrorLogger.LogErrorAsync($"{ex.Message}");
                return (new clsReturnResult(clsReturnResult.enResult.Error,ex.Message), null);
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


       
    }
}
