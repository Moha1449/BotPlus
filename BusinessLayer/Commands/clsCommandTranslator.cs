using BusinessLayer.BotEngine;
using DataLayer.DataProviders;
using DataModelLayer.DataModels;
using DataModelLayer.ReturnResult;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer.Commands
{
    public static class clsCommandTranslator
    {
        public static async Task<clsReturnResult> Execute(string Command)
        {
            if (string.IsNullOrWhiteSpace(Command))
                return new clsReturnResult(clsReturnResult.enResult.InvalidInputs,
                    "Command Execution : Command Is Empty.");

            if (Command == "Commands-g")
                return _GetAllCommands();

            var CommandParts = Command.Split('-');

            if (CommandParts.Length < 2)
                return new clsReturnResult(clsReturnResult.enResult.Error,
                    "Command Execution : Command is undefined.");

            if (CommandParts[0] == "Connection")
                return await _ConnectionStorageCommands(CommandParts);

            if (CommandParts[0] == "Chats")
                return await _ChatsTemplateStorageCommands(CommandParts);

            return await _BotEngineCommands(CommandParts);
        }


        // DB commands
        private static async Task<clsReturnResult> _ConnectionStorageCommands(string[] CommandParts)
        {
            if (CommandParts[1] == "g" && CommandParts.Length == 2)
            {
                var (GetResult, Connections) = await clsConnectionDataProvider.GetConnections();

                if (GetResult.Result != clsReturnResult.enResult.Success)
                    return GetResult;

                StringBuilder ConnectionsAsString = new StringBuilder();
                ConnectionsAsString.AppendLine("Connections : [");

                foreach (var Connection in Connections)
                {
                    ConnectionsAsString.AppendLine(
                        $"[ID : {Connection.ID} , Key : {Connection.Key}] ");
                }

                ConnectionsAsString.Append("]");

                return new clsReturnResult(clsReturnResult.enResult.Success,
                    ConnectionsAsString.ToString());
            }

            if (CommandParts.Length != 3)
                return new clsReturnResult(clsReturnResult.enResult.Error,
                    "Command Execution : Command is undefined.");

            if (CommandParts[1] == "g")
            {
                string ConnectionId = CommandParts[2];

                var (GetResult, Connection) =
                    await clsConnectionDataProvider.GetConnectionByID(ConnectionId);

                if (GetResult.Result != clsReturnResult.enResult.Success)
                    return GetResult;

                StringBuilder ConnectionAsString = new StringBuilder("Connection : [");

                ConnectionAsString.AppendLine();
                ConnectionAsString.AppendLine($"[ID : {Connection.ID} , Key : {Connection.Key}] ");
                ConnectionAsString.AppendLine("]");

                return new clsReturnResult(clsReturnResult.enResult.Success,
                    ConnectionAsString.ToString());
            }

            if (CommandParts[1] == "a")
            {
                string Key = CommandParts[2];

                return await clsConnectionDataProvider.
                    AddNewConnection(new clsConnection { Key = Key });
            }

            if (CommandParts[1] == "d")
            {
                string ConnectionID = CommandParts[2];

                return await clsConnectionDataProvider.DeleteConnectionByID(ConnectionID);
            }

            return new clsReturnResult(clsReturnResult.enResult.Error,
                "Command Execution : Command is undefined.");
        }

        private static async Task<clsReturnResult> _ChatsTemplateStorageCommands(string[] CommandParts)
        {
            if (CommandParts[1] == "g" && CommandParts.Length == 2)
            {
                var (GetResult, ChatsTemplates) =
                    await clsChatsTemplatesDataProvider.GetChatsTemplates();

                if (GetResult.Result != clsReturnResult.enResult.Success)
                    return GetResult;

                StringBuilder ChatsAsString =
                    new StringBuilder("Chats Templates : [");

                ChatsAsString.AppendLine();

                foreach (var ChatTemplate in ChatsTemplates)
                {
                    ChatsAsString.Append(
                        $"[ID : {ChatTemplate.ID} Chats[ ");

                    foreach (var Chat in ChatTemplate.Chats)
                    {
                        ChatsAsString.Append(
                            $"(Question : {Chat.Question},Answer : {Chat.Answer})");
                    }

                    ChatsAsString.Append(" ]");
                    ChatsAsString.AppendLine();
                }

                ChatsAsString.AppendLine("]");

                return new clsReturnResult(clsReturnResult.enResult.Success,
                    ChatsAsString.ToString());
            }

            if (CommandParts[1] == "a")
            {
                if (CommandParts.Length < 5 ||
                    (CommandParts.Length - 3) % 2 != 0)
                {
                    return new clsReturnResult(
                        clsReturnResult.enResult.Error,
                        "Command Execution : Command is undefined.");
                }

                List<clsQuestionAndAnswer> Chats =
                    new List<clsQuestionAndAnswer>();

                for (int i = 3; i < CommandParts.Length; i += 2)
                {
                    Chats.Add(new clsQuestionAndAnswer
                    {
                        Question = CommandParts[i],
                        Answer = CommandParts[i + 1]
                    });
                }

                return await clsChatsTemplatesDataProvider.AddNewChatTemplate
                    (new clsChatTemplate { Chats = Chats });
            }

            if (CommandParts.Length != 3)
                return new clsReturnResult(clsReturnResult.enResult.Error,
                    "Command Execution : Command is undefined.");

            if (CommandParts[1] == "g")
            {
                string ChatTemplateID = CommandParts[2];

                var (GetResult, ChatTemplate) =
                    await clsChatsTemplatesDataProvider.
                        GetChatTemplateByID(ChatTemplateID);

                if (GetResult.Result != clsReturnResult.enResult.Success)
                    return GetResult;

                StringBuilder ChatsAsString =
                    new StringBuilder(
                        $"Chat Template : [ID : {ChatTemplate.ID} Chats[");

                foreach (var Chat in ChatTemplate.Chats)
                {
                    ChatsAsString.Append(
                        $"(Question : {Chat.Question}  Answer : {Chat.Answer})");
                }

                ChatsAsString.Append("] ");

                return new clsReturnResult(clsReturnResult.enResult.Success,
                    ChatsAsString.ToString());
            }

            if (CommandParts[1] == "d")
            {
                string ChatTemplateID = CommandParts[2];

                return await clsChatsTemplatesDataProvider
                    .DeleteConnectionByID(ChatTemplateID);
            }

            return new clsReturnResult(clsReturnResult.enResult.Error,
                "Command Execution : Command is undefined.");
        }


        // Bot Engine Commands 
        private static async Task<clsReturnResult> _BotEngineCommands(string[] CommandPart)
        {
            if (CommandPart.Length < 2)
                return new clsReturnResult(clsReturnResult.enResult.Error,
                    "Command Execution : Command is undefined.");


            if (CommandPart[1] == "c")
            {
                if (CommandPart.Length != 5)
                    return new clsReturnResult(
                        clsReturnResult.enResult.Error,
                        "Command Execution : Please make sure that you entered the full required data and in right format");

                string BotName = CommandPart[2];
                string ConnectionID = CommandPart[3];
                string ChatTemplateID = CommandPart[4];

                return await clsBotEngine.CreateNewBotNode(
                    BotName,
                    ConnectionID,
                    ChatTemplateID);
            }


            if (CommandPart[1] == "d")
            {
                if (CommandPart.Length != 3)
                    return new clsReturnResult(clsReturnResult.enResult.Error,
                        "Command Execution : Command is undefined.");

                if (int.TryParse(CommandPart[2], out int NodeID))
                    return clsBotEngine.DeleteNodeByID(NodeID);

                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Invalid ID Input Data Type.");
            }


            if (CommandPart.Length < 3)
            {
                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Command is undefined.");
            }


            if  (CommandPart[1] == "h" && CommandPart[2] == "r")
            {
                if (CommandPart.Length != 4)
                    return new clsReturnResult(clsReturnResult.enResult.Error,
                        "Command Execution : Command is undefined.");

                if (int.TryParse(CommandPart[3], out int NodeID))
                    return clsBotEngine.StartHandlingChatsForBot(NodeID);

                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Invalid ID Input Data Type.");
            }


            if (CommandPart[1] == "h" && CommandPart[2] == "c")
            {
                if (CommandPart.Length != 4)
                    return new clsReturnResult(clsReturnResult.enResult.Error,
                        "Command Execution : Command is undefined.");

                if (int.TryParse(CommandPart[3], out int NodeID))
                    return clsBotEngine.CloseTheChatsHandlerEngine(NodeID);

                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Invalid ID Input Data Type.");
            }


            if (CommandPart[1] == "h" && CommandPart[2] == "l")
            {
                var GetStoppedHandlersEnginesAndDetails =
                    clsBotEngine.GetStoppedChatsHandlersEnginesDetails();

                if (GetStoppedHandlersEnginesAndDetails.Count == 0)
                    return new clsReturnResult(
                        clsReturnResult.enResult.EmptyResult,
                        "Command Execution : The log is empty.");

                StringBuilder Details =
                    new StringBuilder("Stopped Handlers Log : ");

                Details.AppendLine();

                foreach (var Detail in GetStoppedHandlersEnginesAndDetails)
                {
                    Details.AppendLine();
                    Details.AppendLine($"[{Detail.Detail}] ");
                }

                return new clsReturnResult(
                    clsReturnResult.enResult.Success,
                    Details.ToString());
            }


            if (CommandPart[1] == "g" && CommandPart[2] == "n")
            {
                if (CommandPart.Length != 4)
                    return new clsReturnResult(clsReturnResult.enResult.Error,
                        "Command Execution : Command is undefined.");

                if (int.TryParse(CommandPart[3], out int NodeID))
                    return clsBotEngine.GetBotNodeInfoByNodeID(NodeID);

                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Invalid Node ID Input Data Type.");
            }


            if (CommandPart[1] == "g" && CommandPart[2] == "b")
            {
                if (CommandPart.Length != 4)
                    return new clsReturnResult(clsReturnResult.enResult.Error,
                        "Command Execution : Command is undefined.");

                if (int.TryParse(CommandPart[3], out int NodeID))
                    return clsBotEngine.GetBotInfoByNodeID(NodeID);

                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Invalid Node ID Input Data Type.");
            }


            if (CommandPart[1] == "h" && CommandPart[2] == "q")
            {
                if (CommandPart.Length != 5)
                    return new clsReturnResult(clsReturnResult.enResult.Error,
                        "Command Execution : Command is undefined.");

                if (int.TryParse(CommandPart[3], out int NodeID) &&
                    int.TryParse(CommandPart[4], out int Capacity))
                {
                    return clsBotEngine
                        .UpdateTheHandlerEngineQueueCapacityForNodeByNodeID(
                            NodeID,
                            Capacity);
                }

                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Invalid Input/s");
            }


            if (CommandPart[1] == "h" && CommandPart[2]=="-m")
            {
                if (int.TryParse(CommandPart[3], out int NodeID))
                    return clsBotEngine.GetBotNodeChatQueueMaxCapacity(NodeID);

                return new clsReturnResult(
                    clsReturnResult.enResult.Error,
                    "Command Execution : Invalid Node ID Input Data Type.");
            }


            return new clsReturnResult(
                clsReturnResult.enResult.Error,
                "Command Execution : Command is undefined.");
        }

        // App Commands
        private static clsReturnResult _GetAllCommands()
        {
            StringBuilder Commands = new StringBuilder();

            Commands.AppendLine("========== Application Commands ==========");
            Commands.AppendLine();

            Commands.AppendLine("Connection Commands:");
            Commands.AppendLine("Connection-g");
            Commands.AppendLine("    Get all connections.");
            Commands.AppendLine();

            Commands.AppendLine("Connection-g-ID");
            Commands.AppendLine("    Get a connection by ID.");
            Commands.AppendLine();

            Commands.AppendLine("Connection-a-Key");
            Commands.AppendLine("    Add a new connection using the bot key.");
            Commands.AppendLine();

            Commands.AppendLine("Connection-d-ID");
            Commands.AppendLine("    Delete a connection by ID.");
            Commands.AppendLine();


            Commands.AppendLine("Chats Template Commands:");
            Commands.AppendLine("Chats-g");
            Commands.AppendLine("    Get all chat templates.");
            Commands.AppendLine();

            Commands.AppendLine("Chats-a-Question-Answer-[Question-Answer]...");
            Commands.AppendLine("    Add a chat template with question/answer pairs.");
            Commands.AppendLine();

            Commands.AppendLine("Chats-g-ID");
            Commands.AppendLine("    Get a chat template by ID.");
            Commands.AppendLine();

            Commands.AppendLine("Chats-d-ID");
            Commands.AppendLine("    Delete a chat template by ID.");
            Commands.AppendLine();

            Commands.AppendLine("Commands -g");
            Commands.AppendLine("    Get the list of all available application commands.");
            Commands.AppendLine();

            Commands.AppendLine("Bot Commands:");
            Commands.AppendLine("Bot-c-BotName-ConnectionID-ChatTemplateID");
            Commands.AppendLine("    Create a new bot node.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-d-NodeID");
            Commands.AppendLine("    Delete a bot node by ID.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-h-r-NodeID");
            Commands.AppendLine("    Run the bot node's chat handler.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-h-c-NodeID");
            Commands.AppendLine("    Close the bot node's chat handler.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-h-l");
            Commands.AppendLine("    Get the stopped handlers log.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-h-m-NodeID");
            Commands.AppendLine("    Get the bot node's chat handling queue max capacity.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-g-n-NodeID");
            Commands.AppendLine("    Get information about a bot node.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-g-b-NodeID");
            Commands.AppendLine("    Get information about the Telegram bot.");
            Commands.AppendLine();

            Commands.AppendLine("Bot-h-q-NodeID-Capacity");
            Commands.AppendLine("    Update the handler queue capacity.");
            Commands.AppendLine();

            Commands.AppendLine("==========================================");

            return new clsReturnResult(clsReturnResult.enResult.Success, Commands.ToString());
        }
    }
}
