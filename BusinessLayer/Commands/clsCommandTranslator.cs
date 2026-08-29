using BusinessLayer.AppStorage;
using BusinessLayer.BotEngine;
using BusinessLayer.ReturnResult;
using System.Threading.Tasks;


namespace BusinessLayer.Commands
{
    public static class clsCommandTranslator
    {
        public static async Task<clsReturnResult> Execute(string Command)
        {
            var Result = await AppStoragesCommands(Command);
        
            if (!Result.Detail.Contains("Command is undefined."))
                return Result;

            return await BotEngineCommands(Command);
        }

        private static clsReturnResult GetAppCommands()
        {
            string AppCommands = "Commands : ";

            AppCommands += " [RunBot] [Turns the bot on]";
            AppCommands += ", [CloseConnection] [Turns the bot off]";
            AppCommands += ", [Commands -g -b] [Gets the bot commands that store in telegram servers]";
            AppCommands += ", [Commands -g -a] [Gets the commands that app uses to control the bot]";
            AppCommands += ", [Bot -g] [Gets the bot info]";
            AppCommands += ", [Bot -s] [Gets the bot state is running or is stopped]";
            AppCommands += ", [Chat -s] [Run the chats handler engine.]";
            AppCommands += ", [Chat -c] [Close the chats handler engine.]";
            AppCommands += ", [Message -a] [Add new chat template.]";
            AppCommands += ", [Message -g] [Get the chats templates.]";
            AppCommands += ", [Connection -a] [Updates or Renews the connection key.]";
            AppCommands += ", [Connection -g] [Gets the connection key.]";

            return new clsReturnResult(clsReturnResult.enResult.Success, AppCommands);
        }


        private static async Task<clsReturnResult> AppStoragesCommands(string Command)
        { 
            if (Command.Contains("message -g"))
            {
                return await clsAppStorage.GetChatsTemplates();
            }

            if (Command.Contains("message -a"))
            {
                var MessageParts = Command.Split(',');

                return await clsAppStorage.AddCustomerMessage(MessageParts[1], MessageParts[2]);
            }

            if(Command.Contains("Connection -a"))
            {
                return await clsAppStorage.ConnectionKeySetter(Command.Substring(14));
            }

            if( Command == "Connection -g")
            {
                return await clsAppStorage.GetConnectionAsSting();
            }

            return new clsReturnResult(clsReturnResult.enResult.Error, "Command is undefined.");
        }


        private static async Task<clsReturnResult> BotEngineCommands(string Command)
        {
            string CommandLower = Command.ToLower();

            switch (CommandLower)
            {
                case "run":
                    return await clsBotEngine.RunBot();
                case "close":
                    return await clsBotEngine.Close();
                case "commands -g -b":
                    return await clsBotEngine.GetBotCommands();
                case "commands -g -a":
                    return GetAppCommands();
                case "bot -g":
                    return await clsBotEngine.GetBotData();
                case "bot -s":
                    return clsBotEngine.BotStatus();
                case "chat -s":
                    return await clsBotEngine.RunTheChatsHandlerEngine();
                case "chat -c":
                    return await clsBotEngine.CloseTheChatsHandlerEngine();
                default:
                    return new clsReturnResult(clsReturnResult.enResult.Error, "Command is undefined.");
            }
        }
    }
}
