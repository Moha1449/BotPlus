using BusinessLayer.ReturnResult;
using System.Threading.Tasks;


namespace BusinessLayer.Commands
{
    public static class clsCommandTranslator
    {
        public static async Task<clsReturnResult> Execute(string Command)
        {
            string CommandLower = Command.ToLower();

            switch(CommandLower)
            {
                case "run":
                    return await clsCommandsController.Run();
                case "close":
                    return await clsCommandsController.Close();
                case "commands -g -b":
                    return await clsCommandsController.GetBotCommands();
                case "commands -g -a":
                    return GetAppCommands();
                case "bot -g":
                    return await clsCommandsController.GetBotData();
                case "bot -s":
                    return  clsCommandsController.BotStatus();
                case "chat -s":
                    return await clsCommandsController.RunTheChatsHandlerEngine();
                case "chat -c":
                    return await clsCommandsController.CloseTheChatsHandlerEngine();
                default:
                    return new clsReturnResult(clsReturnResult.enResult.Error, "Command is undefined.");
            }
        }

        private static clsReturnResult GetAppCommands()
        {
            string AppCommands = "Commands : ";

            AppCommands += " [Run] [Turns the bot on]";
            AppCommands += ", [Close] [Turns the bot off]";
            AppCommands += ", [Commands -g -b] [Gets the bot commands that store in telegram servers]";
            AppCommands += ", [Commands -g -a] [Gets the commands that app uses to control the bot]";
            AppCommands += ", [Bot -g] [Gets the bot info]";
            AppCommands += ", [Bot -s] [Gets the bot state is running or is stopped]";

            return new clsReturnResult(clsReturnResult.enResult.Success, AppCommands);
        }
        
        public static bool IsBotRunning()
        {
            return clsCommandsController.IsBoRunning();
        }

    }
}
