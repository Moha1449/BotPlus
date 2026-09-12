using DataModelLayer.ErrorHandler;
using DataModelLayer.ReturnResult;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BusinessLayer.BotEngine
{
    internal class clsChatsHandlerEngine
    {
        private struct stLoaderVariables
        {
            public Queue<Update> ChatsQueue;

            public int ChatQueueMaxCapacity;

            public int? NextUpdateID;
        }

        private struct stResponderVariables
        {
            public Dictionary<string, string> ChatsTemplates;

            public string WelcomingMessage;
        }


        private object _KeyOfCapacityChanging = new object();

        private stLoaderVariables _LoaderConfig;

        private stResponderVariables _ResponderConfig;


        private ITelegramBotClient _BotClient;

        private CancellationTokenSource _CancelToken;




        private bool _IsHandlerRunningReal = false;

        private object _IsHandlerRerunningLockKey = false;

        public bool IsHandlerRunning
        {
            get
            {
                lock (_IsHandlerRerunningLockKey)
                {
                    return _IsHandlerRunningReal;
                }
            }

            set
            {
                lock (_IsHandlerRerunningLockKey)
                {
                    _IsHandlerRunningReal = value;
                }
            }
        }




        private bool _IsEventInvoked = false;


        private Thread _HandlerThread;

        //Summary
        //The event is part of communication channel this channel allows us to notify the main threads that the handler engine of bot node is stopped for reason
        private event Action<clsReturnResult> _OnErrorOccurredDuringHandlingOperation;



        public clsChatsHandlerEngine(int ChatQueueMaxCapacity, string Key,
            Dictionary<string, string> ChatsTemplates)
        {
            _LoaderConfig = new stLoaderVariables
            {
                ChatQueueMaxCapacity = ChatQueueMaxCapacity
                ,
                ChatsQueue = new Queue<Update>()
                ,
                NextUpdateID = null
            };

            _ResponderConfig = new stResponderVariables
            {
                ChatsTemplates = ChatsTemplates
            };

            _BotClient = new TelegramBotClient(Key);
        }


        //Run Controllers
        internal clsReturnResult RunHandler()
        {
            if (IsHandlerRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error,
                    "Handler Engine Execution : The handler is already running.");

            try
            {
                _CancelToken = new CancellationTokenSource();
                _HandlerThread = new Thread(_Handler);
                _HandlerThread.IsBackground = true;
                _HandlerThread.Name = "Handler Thread";

                _HandlerThread.Start();
                return new clsReturnResult(clsReturnResult.enResult.Success, $"Handler Engine Execution : Chat Handler is running.");
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, $"Handler Engine Execution : {ex.Message}.");
            }
        }

        internal void CloseHandler()
        {
            if (!IsHandlerRunning)
                return;

            _CancelToken?.Cancel();
        }


        //Notifier that notifies main thread that engine is stopped
        internal void SubscribeToHanderStoppedEvent(Action<clsReturnResult> EventHandler)
        {
            _OnErrorOccurredDuringHandlingOperation += EventHandler;
        }

        internal void UnsubscribeToHandlerStoppedEvent(Action<clsReturnResult> EventHandler)
        {
            _OnErrorOccurredDuringHandlingOperation -= EventHandler;
        }


        //Engine Core Methods

        //Summary
        //The engine work-flow
        //The _Loader is responsible to load requests 
        //The _Responder is responsible to response on messages
        //The _Handler is responsible to load requests and handling
        private string _Responder(string Message)
        {
            if (Message == "Hello" || Message == "Hi")
            {
                if (_ResponderConfig.WelcomingMessage == null)
                {
                    _ResponderConfig.WelcomingMessage = "Hello,how can I help you ?\n";

                    foreach (string Key in _ResponderConfig.ChatsTemplates.Keys)
                        _ResponderConfig.WelcomingMessage += Key + "\n";
                }

                return _ResponderConfig.WelcomingMessage;
            }

            if (_ResponderConfig.ChatsTemplates.ContainsKey(Message))
            {
                return _ResponderConfig.ChatsTemplates[Message];
            }

            return "Please make sure that you enter Hello or Hi to see the menu or one of choices in menu.";
        }

        private async void _Handler()
        {
            IsHandlerRunning = true;
            _IsEventInvoked = false;

            try
            {
                do
                {
                    if (_LoaderConfig.ChatsQueue.Count > 0)
                    {
                        var Chat = _LoaderConfig.ChatsQueue.Dequeue();

                        await _BotClient.SendTextMessageAsync(Chat.Message.Chat.Id
                            , _Responder(Chat.Message.Text));
                    }
                    else
                    {
                        var LoadResult = await _Loader();

                        if (LoadResult.Result != clsReturnResult.enResult.Success)
                        {
                            _IsEventInvoked = true;
                            _OnErrorOccurredDuringHandlingOperation?.Invoke(LoadResult);
                            break;
                        }
                    }
                }
                while (!_CancelToken.Token.IsCancellationRequested);
            }
            catch (Exception ex)
            {
                _OnErrorOccurredDuringHandlingOperation?.Invoke(new clsReturnResult(clsReturnResult.enResult.Error, "Handler Engine Execution : " + ex.Message));
                clsErrorLogger.LogError(ex.Message);
                return;
            }


            if (!_IsEventInvoked)
            {
                _OnErrorOccurredDuringHandlingOperation?.Invoke(new clsReturnResult(clsReturnResult.enResult.Success,
                    "Handler Engine Execution : The Handler is stopped"));

                _IsEventInvoked = true; 
            }

            IsHandlerRunning = false;
        }

        private async Task<clsReturnResult> _Loader()
        {
            if (_CancelToken.IsCancellationRequested)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Handler Engine Execution : The Handler is stopped.");

            if (_LoaderConfig.ChatsQueue.Count == _LoaderConfig.ChatQueueMaxCapacity)
                return new clsReturnResult(clsReturnResult.enResult.Success);

            try
            {
                int Capacity = 0;

                lock(_KeyOfCapacityChanging)
                {
                    Capacity = _LoaderConfig.ChatQueueMaxCapacity;
                }

                var Updates = await _BotClient.GetUpdatesAsync(_LoaderConfig.NextUpdateID,Capacity,
                    null, null, _CancelToken.Token);

                if (Updates == null)
                    return new clsReturnResult(clsReturnResult.enResult.Success);

                for (int i = 0; i < Updates.Length; i++)
                {
                    _LoaderConfig.NextUpdateID = Updates[i].Id + 1;
                    _LoaderConfig.ChatsQueue.Enqueue(Updates[i]);
                }

                return new clsReturnResult(clsReturnResult.enResult.Success);
            }
            catch (OperationCanceledException)
            {
                return new clsReturnResult(clsReturnResult.enResult.Success, "Handler Engine Execution : The Handler is stopped");
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return new clsReturnResult(clsReturnResult.enResult.Error, "Handler Engine Execution : " + ex.Message);
            }
        }

        internal clsReturnResult SetChatsQueueCapacity(int capacity)
        {
            if (capacity <= 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs,
                    "Handler Engine Execution : The new capacity is invalid,because it is less then or equal to zero.");

            lock (_KeyOfCapacityChanging)
                _LoaderConfig.ChatQueueMaxCapacity = capacity;

            return new clsReturnResult(clsReturnResult.enResult.Success,
                "Handler Engine Execution : The capacity is updated successfully.");
        }

        internal int GetChatsQueueMaxCapacity()
        {
            return _LoaderConfig.ChatQueueMaxCapacity;
        }

        internal void Dispose()
        {
            _OnErrorOccurredDuringHandlingOperation = null;

            _CancelToken?.Cancel();
            _CancelToken?.Dispose();

            _HandlerThread = null;
            _BotClient = null;
        }
    }
}
