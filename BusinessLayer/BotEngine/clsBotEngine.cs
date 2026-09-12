using DataLayer.DataProviders;
using DataModelLayer.ReturnResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.BotEngine
{
    public static class clsBotEngine
    {
        private static Dictionary<int, clsBotNode> _BotsNodesList = new Dictionary<int, clsBotNode>();

        private static int _NextNodeID = 1;


        //Bot Node Controller Methods
        internal static async Task<clsReturnResult> CreateNewBotNode(string NodeName, string ConnectionID, string ChatTemplatesID)
        {
            var (GetConnectionResult, ConnectionObject) = await clsConnectionDataProvider.GetConnectionByID(ConnectionID);

            if (GetConnectionResult.Result != clsReturnResult.enResult.Success)
                return GetConnectionResult;

            var (GetChatTemplatesResult, ChatTemplates) = await clsChatsTemplatesDataProvider.GetChatTemplateByID(ChatTemplatesID);

            if (GetChatTemplatesResult.Result != clsReturnResult.enResult.Success)
                return GetChatTemplatesResult;

            var (CreationResult, NewNode) = await clsBotNode.CreateNode(_NextNodeID, ConnectionObject.Key, NodeName, 30, ChatTemplates,
                _ChatHandlerStoppedEventHandler);

            if (CreationResult.Result != clsReturnResult.enResult.Success)
                return CreationResult;

            _BotsNodesList.Add(_NextNodeID, NewNode);
            _NextNodeID++;

            return CreationResult;
        }

        internal static clsReturnResult DeleteNodeByID(int ID)
        {
            if (_BotsNodesList.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Bot Engine Execution : There Is No Node Was Created.");

            if (!_BotsNodesList.ContainsKey(ID))
                return new clsReturnResult(clsReturnResult.enResult.NotFound, "Bot Engine Execution : Node Is Not Found.");

            var CleanResult = _BotsNodesList[ID].Dispose();

            if (CleanResult.Result != clsReturnResult.enResult.Success)
                return CleanResult;

            return (_BotsNodesList.Remove(ID)) ? new clsReturnResult(clsReturnResult.enResult.Success, $"Bot Engine Execution : Node With Id {ID} Is Deleted") :
                new clsReturnResult(clsReturnResult.enResult.Error, $"Bot Engine Execution : Something Went Wrong.Unknown Error.");
        }

        internal static clsReturnResult StartHandlingChatsForBot(int botNodeID)
        {
            if (_BotsNodesList.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Bot Engine Execution : There Is No Node Was Created.");

            if (!_BotsNodesList.ContainsKey(botNodeID))
                return new clsReturnResult(clsReturnResult.enResult.NotFound, "Bot Engine Execution : Node Is Not Found.");

            return _BotsNodesList[botNodeID].RunChatsHandler();
        }

        internal static clsReturnResult GetBotNodeInfoByNodeID(int NodeID)
        {
            if (_BotsNodesList.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Bot Engine Execution : There Is No Node Was Created.");

            if (!_BotsNodesList.ContainsKey(NodeID))
                return new clsReturnResult(clsReturnResult.enResult.NotFound, "Bot Engine Execution : Node Is Not Found.");

            return new clsReturnResult(clsReturnResult.enResult.Success,
                $"Bot Engine Execution : Node Data : [Name : {_BotsNodesList[NodeID].BotNodeConfig.NodeName}, " +
                $"Used Chat Template ID : {_BotsNodesList[NodeID].BotNodeConfig.ChatTemplateID}]");
        }

        internal static clsReturnResult GetBotInfoByNodeID(int NodeID)
        {
            if (_BotsNodesList.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Bot Engine Execution : There Is No Node Was Created.");

            if (!_BotsNodesList.ContainsKey(NodeID))
                return new clsReturnResult(clsReturnResult.enResult.NotFound, "Bot Engine Execution : Node Is Not Found.");

            return new clsReturnResult(clsReturnResult.enResult.Success,
                $"Bot Engine Execution : Bot Data : [User Name : {_BotsNodesList[NodeID].BotInfo.Username}, " +
                $"First Name : {_BotsNodesList[NodeID].BotInfo.FirstName}," +
                $"lAST Name : {_BotsNodesList[NodeID].BotInfo.LastName}]");
        }

        internal static clsReturnResult CloseTheChatsHandlerEngine(int ID)
        {
            if (_BotsNodesList.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Bot Engine Execution : There Is No Node Was Created.");

            if (!_BotsNodesList.ContainsKey(ID))
                return new clsReturnResult(clsReturnResult.enResult.NotFound, "Bot Engine Execution : Node Is Not Found.");

            return _BotsNodesList[ID].CloseChatsHandler();
        }

        internal static clsReturnResult UpdateTheHandlerEngineQueueCapacityForNodeByNodeID(int NodeID, int Capacity)
        {
            if (_BotsNodesList.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Bot Engine Execution : There Is No Node Was Created.");

            if (!_BotsNodesList.ContainsKey(NodeID))
                return new clsReturnResult(clsReturnResult.enResult.NotFound, "Bot Engine Execution : Node Is Not Found.");

            return _BotsNodesList[NodeID].UpdateCapacityOfChatQueue(Capacity);
        }

        private static void _ChatHandlerStoppedEventHandler(clsReturnResult Detail)
        {
            clsStoppedHandlersLogger.LogNew(Detail);
        }

        internal static clsReturnResult GetBotNodeChatQueueMaxCapacity(int NodeID)
        {
            if (_BotsNodesList.Count == 0)
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs, "Bot Engine Execution : There Is No Node Was Created.");

            if (!_BotsNodesList.ContainsKey(NodeID))
                return new clsReturnResult(clsReturnResult.enResult.NotFound, "Bot Engine Execution : Node Is Not Found.");

            return _BotsNodesList[NodeID].GetChatsQueueMaxCapacity();
        }


        //Logger
        internal static List<clsReturnResult> GetStoppedChatsHandlersEnginesDetails()
        {

            return clsStoppedHandlersLogger.GetLog();
        }

        public static int GetLogsCount()
        {
            return clsStoppedHandlersLogger.LogsNumber();
        }
    }
}
