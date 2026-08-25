using BusinessLayer.Commands;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PresentationLayer.MainScreen
{
    public partial class usCommandsScreen : UserControl
    {
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


            if(tbCommandBox.Text.ToLower() == "run" && CommandRunResult.Result == BusinessLayer.
                ReturnResult.clsReturnResult.enResult.Success )
            lbBotState.Text = "Bot Running";
            else if(tbCommandBox.Text.ToLower() == "close" && CommandRunResult.Result == BusinessLayer.
                ReturnResult.clsReturnResult.enResult.Success)
                lbBotState.Text ="Bot Stopped";
        }

        private void _ShowCommandResult(string detail)
        {
            if (string.IsNullOrEmpty(tbCommandsStatesBox.Text))
                tbCommandsStatesBox.Text = detail;
            else
                tbCommandsStatesBox.AppendText(Environment.NewLine + detail);
        }
    }
}
