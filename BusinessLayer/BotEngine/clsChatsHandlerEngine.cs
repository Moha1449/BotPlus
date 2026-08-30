using BusinessLayer.ErrorHandler;
using BusinessLayer.ReturnResult;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BusinessLayer.BotEngine
{
    class clsChatsHandlerEngine
    {
        private Queue<Update> _ChatsQueue { set; get; }


        private ITelegramBotClient _Bot { get; set; }


        private CancellationToken _CancelToken { get; set; }


        internal int ChatQueueMaxCapacity { get; set; }


        private int? _NextUpdateID { get; set; }

        public Dictionary<string, string> ChatsTemplates { get; set; }


        public bool IsEngineRunning = false;

        private string _WelcomingMessage { get; set; }


        public clsChatsHandlerEngine(int ChatQueueMaxCapacity, ITelegramBotClient BotClient,
            CancellationToken Token, Dictionary<string, string> ChatsTemplates)
        {
            _NextUpdateID = null;
            _CancelToken = Token;
            _ChatsQueue = new Queue<Update>();
            this.ChatQueueMaxCapacity = ChatQueueMaxCapacity;
            this.ChatsTemplates = ChatsTemplates;
            _Bot = BotClient;
        }

        internal async Task<clsReturnResult> EngineRunner()
        {
            if (ChatsTemplates == null || ChatsTemplates.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.Error
                    , "Chats templates is empty. Add chats templates to run the chat handler engine.");

            IsEngineRunning = true;
            return await (Task.Run(() => { return _RequestsHandler(); }));
        }

        private string _ChatsReposes(string key)
        {
            if (key == "Hello" || key == "Hi")
            {
                if (_WelcomingMessage == null)
                {
                    _WelcomingMessage = "Hello,how can I help you ?\n";

                    foreach (string Key in ChatsTemplates.Keys)
                        _WelcomingMessage += Key + "\n";
                }

                return _WelcomingMessage;
            }

            if (ChatsTemplates.ContainsKey(key))
            {
                return ChatsTemplates[key];
            }

            return "Please make sure that you enter Hello or Hi to see the menu or one of choices in menu.";
        }

        private async Task<clsReturnResult> _RequestsHandler()
        {
            try
            {
                do
                {
                    if (_ChatsQueue.Count > 0)
                    {
                        var Chat = _ChatsQueue.Dequeue();
                        await _Bot.SendTextMessageAsync(Chat.Message.Chat.Id
                            , _ChatsReposes(Chat.Message.Text));
                    }
                    else
                    {
                        var LoadResult = await _ChatsLoader();

                        if (LoadResult.Result != clsReturnResult.enResult.Success)
                        {
                            IsEngineRunning = false;
                            return LoadResult;
                        }
                    }
                }
                while (!_CancelToken.IsCancellationRequested);
            }
            catch (Exception ex)
            {
                IsEngineRunning = false;
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Error : " + ex.Message);
            }

            IsEngineRunning = false;
            return new clsReturnResult(clsReturnResult.enResult.Success, "The chats handler engine is stopped.");
        }


        private async Task<clsReturnResult> _ChatsLoader()
        {
            if (_CancelToken.IsCancellationRequested)
                return new clsReturnResult(clsReturnResult.enResult.Success, "The Handler is stopped.");

            if (_ChatsQueue.Count == ChatQueueMaxCapacity)
                return new clsReturnResult(clsReturnResult.enResult.Success, "Queue messages is full.");

            try
            {
                var Updates = await _Bot.GetUpdatesAsync(_NextUpdateID, ChatQueueMaxCapacity - _ChatsQueue.Count,
                    null, null, _CancelToken);

                for (int i = 0; i < Updates.Length; i++)
                {
                    _NextUpdateID = Updates[i].Id + 1;

                    if (i == 0)
                    {
                        _ChatsQueue.Enqueue(Updates[0]);
                        continue;
                    }

                    if (Updates[i].Message.Chat.Id != Updates[i - 1].Message.Chat.Id)
                        _ChatsQueue.Enqueue(Updates[i]);
                }

                return new clsReturnResult(clsReturnResult.enResult.Success, "The messages loaded successfully.");
            }
            catch (OperationCanceledException)
            {
                return new clsReturnResult(clsReturnResult.enResult.Success,"The Handler is stopped.");
            }
            catch (Exception ex)
            {
                await clsErrorLogger.LogErrorAsync(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Error : " + ex.Message);
            }
        }
    }
}
