using BusinessLayer.BotEngine;
using BusinessLayer.Commands;
using DataModelLayer.ReturnResult;
using System;
using System.Windows.Forms;

namespace PresentationLayer.MainScreen
{
    public partial class usCommandsScreen : UserControl
    {

        private int _PerviousStoppedChatsHandlerEngine = 0;

        public usCommandsScreen()
        {
            InitializeComponent();
        }

        private async void btRunCommand_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbCommandBox.Text))
                return;

            var CommandRunResult = await clsCommandTranslator.Execute(tbCommandBox.Text);

            _ShowCommandResult(CommandRunResult.Detail);
        }

        private void _ShowCommandResult(string detail)
        {
            if (string.IsNullOrEmpty(tbCommandsStatesBox.Text))
                tbCommandsStatesBox.Text = detail;
            else
                tbCommandsStatesBox.AppendText(Environment.NewLine + detail);
        }

        // Runs on every timer tick to check for newly stopped bot chat-handler engines.
        private void tmLoggerChecker_Tick(object sender, EventArgs e)
        {
            int LogsCount = clsBotEngine.GetLogsCount();

            if (_PerviousStoppedChatsHandlerEngine == LogsCount)
                return;

            _PerviousStoppedChatsHandlerEngine = LogsCount;

            var GetLogResult = clsCommandTranslator.Execute("Bot-h-l").Result;

            if (GetLogResult.Result == clsReturnResult.enResult.EmptyResult)
                return;

            _ShowCommandResult(GetLogResult.Detail);
        }

        private void usCommandsScreen_Load(object sender, EventArgs e)
        {
            tmLoggerChecker.Enabled = true;
        }
    }
}
