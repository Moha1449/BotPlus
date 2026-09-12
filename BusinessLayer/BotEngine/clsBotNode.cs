using DataModelLayer.DataModels;
using DataModelLayer.ErrorHandler;
using DataModelLayer.ReturnResult;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace BusinessLayer.BotEngine
{
    internal class clsBotNode
    {
        internal struct stBotNodeConfig
        {
            internal int NodeID;

            internal string ChatTemplateID;

            internal string NodeName;
        }

        private struct stBotClientConfig
        {
            public TelegramBotClient BotClient;

            public User Bot;
        }


        internal stBotNodeConfig BotNodeConfig { get; private set; }


        private stBotClientConfig _BotClientConfig;


        internal User BotInfo { get { return _BotClientConfig.Bot; } }


        private clsChatsHandlerEngine _ChatsHandler;


        private CancellationTokenSource _CancelSource;

        internal bool IsBotConnected { get { return _BotClientConfig.Bot != null; } }


        private event Action<clsReturnResult> OnChatsHandlerStopped;


        //BotInfo Node Methods
        private clsBotNode(int NodeID, string NodeName, TelegramBotClient Client, User Bot,
            clsChatsHandlerEngine Handler, CancellationTokenSource Cancellation, string ChatTemplateID,
            Action<clsReturnResult> ChatsHandlerEngineStoppedEventHandler)
        {
            BotNodeConfig = new stBotNodeConfig
            {
                NodeID = NodeID,
                ChatTemplateID = ChatTemplateID,
                NodeName = NodeName
            };

            _BotClientConfig = new stBotClientConfig
            {
                Bot = Bot,
                BotClient = Client,
            };

            _CancelSource = Cancellation;
            _ChatsHandler = Handler;
             OnChatsHandlerStopped += ChatsHandlerEngineStoppedEventHandler;
            _ChatsHandler.SubscribeToHanderStoppedEvent(_ChatsHandlerEngineStoppedEventHandler);
        }


        private static clsReturnResult _ValidateCreateNodeInputs(int NodeID,string Key, string NodeName, int MaxCapacityPerQueue,
            clsChatTemplate ChatsTemplates, Action<clsReturnResult> ChatsHandlerStoppedEventHandler)
        {
            string InvalidInputs = "";

            if (NodeID <= 0)
                InvalidInputs += "NodeID, ";

            if (string.IsNullOrWhiteSpace(Key))
                InvalidInputs += "Key, ";

            if (string.IsNullOrWhiteSpace(NodeName))
                InvalidInputs += "NodeName, ";

            if (MaxCapacityPerQueue <= 0)
                InvalidInputs += "MaxCapacityPerQueue, ";

            if (ChatsTemplates == null)
                InvalidInputs += "ChatsTemplates, ";
            else if (ChatsTemplates.Chats == null)
                InvalidInputs += "ChatsTemplates.Chats, ";

            if (ChatsHandlerStoppedEventHandler == null)
                InvalidInputs += "ChatsHandlerStoppedEventHandler, ";

            if (!string.IsNullOrEmpty(InvalidInputs))
            {
                InvalidInputs = InvalidInputs.TrimEnd(',', ' ');

                return new clsReturnResult(
                    clsReturnResult.enResult.InvalidInputs,
                    "Bot Node Execution : Invalid Inputs : " + InvalidInputs);
            }

            return new clsReturnResult(clsReturnResult.enResult.Success);
        }

        internal static async Task<(clsReturnResult, clsBotNode)> CreateNode(int NodeID, string Key,string NodeName, int MaxCapacityPerQueue
            , clsChatTemplate ChatsTemplates, Action<clsReturnResult> ChatsHandlerStoppedEventHandler)
        {

            var ChecksInputs = _ValidateCreateNodeInputs(NodeID,Key,NodeName,MaxCapacityPerQueue,ChatsTemplates,ChatsHandlerStoppedEventHandler);

            if (ChecksInputs.Result == clsReturnResult.enResult.InvalidInputs)
                return (ChecksInputs,null);

            try
            {
                CancellationTokenSource Cancellation = new CancellationTokenSource();
                TelegramBotClient Client = new TelegramBotClient(Key);

                var (ConnectResult, BotData) = await _ConnectWithBot(Client);

                if (ConnectResult.Result == clsReturnResult.enResult.Success)
                {
                    clsChatsHandlerEngine Handler = new clsChatsHandlerEngine(MaxCapacityPerQueue, Key,
                        ChatsTemplates.Chats.ToDictionary(key => key.Question, value => value.Answer));

                    return (new clsReturnResult(clsReturnResult.enResult.Success, $"Node Creation : Node with ID {NodeID} is created."),
                        new clsBotNode(NodeID,NodeName, Client, BotData, Handler, Cancellation, ChatsTemplates.ID, ChatsHandlerStoppedEventHandler));
                }

                return (new clsReturnResult(clsReturnResult.enResult.Error, $"Error : {ConnectResult.Detail}"),
                        null);
            }
            catch (Exception ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return (new clsReturnResult(clsReturnResult.enResult.Error, $"Error : {ex.Message}"),
                        null);
            }
        }


        internal clsReturnResult Dispose()
        {
            if (_ChatsHandler.IsHandlerRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "You can not dispose while the chat handler engine is working.close and try again.");

            _ChatsHandler.Dispose();

            _CancelSource?.Cancel();
            _CancelSource?.Dispose();

            _BotClientConfig.BotClient = null;
            _BotClientConfig.Bot = null;

            OnChatsHandlerStopped = null;

            return new clsReturnResult(clsReturnResult.enResult.Success, $"The node with id {BotNodeConfig.NodeID} is disposed.");
        }


        //Chats Handler Engine Controller Methods
        internal clsReturnResult RunChatsHandler()
        {
            return _ChatsHandler.RunHandler();
        }

        internal clsReturnResult CloseChatsHandler()
        {
            if (!_ChatsHandler.IsHandlerRunning)
                return new clsReturnResult(clsReturnResult.enResult.Error, "Error : The handler is already stopped.");

            _ChatsHandler.CloseHandler();

            return new clsReturnResult(clsReturnResult.enResult.Success, "Processing...");
        }

        private void _ChatsHandlerEngineStoppedEventHandler(clsReturnResult Details)
        { 
            OnChatsHandlerStopped?.Invoke(new clsReturnResult(Details.Result,Details.Detail + $"N ode ID :{BotNodeConfig.NodeID}"));
        }

        internal clsReturnResult UpdateCapacityOfChatQueue(int capacity)
        {
            return _ChatsHandler.SetChatsQueueCapacity(capacity);
        }

        internal bool IsHandlerRunning()
        {
            return _ChatsHandler.IsHandlerRunning;
        }

        internal clsReturnResult GetChatsQueueMaxCapacity()
        {
            return new clsReturnResult(clsReturnResult.enResult.Success, $"Bot Node Execution : Queue Capacity Is {_ChatsHandler.GetChatsQueueMaxCapacity()}.");
        }


        //Bot Client Controller Methods
        internal async Task<(clsReturnResult, BotCommand[])> GetBotCommands()
        {
            if (!IsBotConnected)
                return (new clsReturnResult(clsReturnResult.enResult.Error, "BotInfo is not connected"), null);

            try
            {
                var BotCommands = await _BotClientConfig.BotClient.GetMyCommandsAsync(null, null, _CancelSource.Token);
                return (new clsReturnResult(clsReturnResult.enResult.Success), BotCommands);
            }
            catch (ApiRequestException ex)
            {
                clsErrorLogger.LogError($"{ex.Message}");
                return (new clsReturnResult(clsReturnResult.enResult.Error, ex.Message), null);
            }
        }

        private static async Task<(clsReturnResult, User)> _ConnectWithBot(TelegramBotClient Client)
        {
            try
            {
                User Bot = await Client.GetMeAsync();
                return (new clsReturnResult(clsReturnResult.enResult.Success, "BotInfo is connected."), Bot);
            }
            catch (ApiRequestException ex)
            {
                clsErrorLogger.LogError(ex.Message);
                return (new clsReturnResult(clsReturnResult.enResult.Error, ex.Message), null);
            }
        }
    }
}
