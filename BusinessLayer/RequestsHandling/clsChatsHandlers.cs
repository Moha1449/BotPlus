using BusinessLayer.ErrorHandler;
using BusinessLayer.ReturnResult;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BusinessLayer.RequestsHandling
{
    internal static class clsChatsHandlerEngine
    {
        private static Queue<Update> _ChatsQueue { set; get; }

        private static ITelegramBotClient _Bot { get; set; }

        private static CancellationToken _CancelToken { get; set; }

        private static int _RequestPerSecond { get; set; }

        private static int? _NextUpdateID { get; set; }


        static clsChatsHandlerEngine()
        {
            _NextUpdateID = null;
            _CancelToken = CancellationToken.None;
            _ChatsQueue = new Queue<Update>();
            _RequestPerSecond = 30;
            _Bot = null;
        }

        internal static async Task<clsReturnResult> StartHandler(ITelegramBotClient Bot, CancellationToken Token)
        {
            _Bot = Bot;
            _CancelToken = Token;
            return await (Task.Run(() => { return HandlerRunner(); }));
        }

        private static async Task<clsReturnResult> HandlerRunner()
        {
            try
            {
                do
                {
                    if (_ChatsQueue.Count > 0)
                    {
                        var Chat = _ChatsQueue.Dequeue();
                        await _Bot.SendTextMessageAsync(Chat.Message.Chat.Id, "Hello i am so happy");
                    }
                    else
                    {
                        var LoadResult = await _ChatsLoader();

                        if (LoadResult.Result != clsReturnResult.enResult.Success)
                            return LoadResult;
                    }
                }
                while (!_CancelToken.IsCancellationRequested);
            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Error : " + ex.Message);
            }

            return new clsReturnResult(clsReturnResult.enResult.Success, "The chats handler engine is stopped.");
        }


        private static async Task<clsReturnResult> _ChatsLoader()
        {
            if (_CancelToken.IsCancellationRequested)
                return new clsReturnResult(clsReturnResult.enResult.Success, "The Handler is stopped.");

            if (_ChatsQueue.Count == _RequestPerSecond)
                return new clsReturnResult(clsReturnResult.enResult.Success, "Queue messages is full.");

            try
            {
                var Updates = await _Bot.GetUpdatesAsync(_NextUpdateID, _RequestPerSecond - _ChatsQueue.Count,
                    null,null,_CancelToken);

                foreach (var update in Updates)
                {
                    _NextUpdateID = update.Id + 1;

                    if (update.Message.Text == "/start")
                        continue;

                    _ChatsQueue.Enqueue(update);
                }

                return new clsReturnResult(clsReturnResult.enResult.Success, "The messages loaded successfully.");
            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Error : " + ex.Message);
            }
        }
    }
}
