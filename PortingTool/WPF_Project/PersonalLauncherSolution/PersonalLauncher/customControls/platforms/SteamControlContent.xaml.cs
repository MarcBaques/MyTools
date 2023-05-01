using PersonalLauncher.Commands;
using PersonalLauncher.settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalLauncher.customControls.platforms
{
    /// <summary>
    /// Interaction logic for SteamControlContent.xaml
    /// </summary>
    public partial class SteamControlContent : UserControl
    {
        public SteamControlContent()
        {
            InitializeComponent();

            var steamContentTextBox = (TextBox)this.FindName("SteamContentFolder");
            steamContentTextBox.Text = UserSettings.Default.steamContentFolder;

            var steamExeTextBox = (TextBox)this.FindName("SteamExe");
            steamExeTextBox.Text = UserSettings.Default.steamExeFile;

            var steamAppIDTextBox = (TextBox)this.FindName("AppID");
            steamAppIDTextBox.Text = UserSettings.Default.steamAppID;
        }

        public void SetUp(Steam steam)
        {
            var linksPanel = (WrapPanel)this.FindName("SteamLinksPanel");
            linksPanel.Children.Clear();
            FillLinksPanel(linksPanel, steam.links);
        }

        private void FillLinksPanel(WrapPanel panelView, List<Link> links)
        {
            for (int i = 0; i < links.Count; i++)
            {
                var urlButton = new Button();
                urlButton.Margin = new Thickness(10);
                urlButton.Content = links[i].name;
                urlButton.Command = new ActionCommand<string>(PetoonsLauncher.OpenChromeLink);
                urlButton.CommandParameter = links[i].link;
                urlButton.Height = 75;

                var content = new TextBlock();
                content.Text = links[i].name;
                content.TextWrapping = TextWrapping.Wrap;
                content.HorizontalAlignment = HorizontalAlignment.Center;
                content.VerticalAlignment = VerticalAlignment.Center;
                content.TextAlignment = TextAlignment.Center;
                urlButton.Content = content;

                panelView.Children.Add(urlButton);
            }
        }

        private void ExecuteSteamUploader(object sender, RoutedEventArgs e)
        {
            var appID = GetTextFromTextBox("AppID");
            var contentPath = GetTextFromTextBox("SteamContentFolder");
            var exeFile = GetTextFromTextBox("SteamExe");

            UserSettings.Default.steamAppID = appID;
            UserSettings.Default.Save();

            PetoonsLauncher.ExecuteBatFile("SteamBuilder", $"{appID} {contentPath} {exeFile}");
        }

        private void OpenFolderExplorer(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var textBox = (TextBox)this.FindName("SteamContentFolder");
                textBox.Text = openFileDialog.SelectedPath;
                UserSettings.Default.steamContentFolder = textBox.Text;
                UserSettings.Default.Save();
            }
        }

        private void OpenExeExplorer(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var textBox = (TextBox)this.FindName("SteamExe");
                textBox.Text = openFileDialog.SelectedPath;
                UserSettings.Default.steamExeFile = textBox.Text;
                UserSettings.Default.Save();
            }
        }

        private string GetTextFromTextBox(string controlName)
        {
            return ((TextBox)this.FindName(controlName)).Text;
        }

    }
}
