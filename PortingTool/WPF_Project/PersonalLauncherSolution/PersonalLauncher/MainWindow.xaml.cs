using Microsoft.Win32;
using PersonalLauncher.Commands;
using PersonalLauncher.customControls.generics;
using PersonalLauncher.customControls.platforms;
using PersonalLauncher.settings;
using PersonalLauncher.windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PersonalLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int GENERIC_BUTTON_HEIGHT = 125;
        private int GENERIC_BUTTON_WIDTH = 125;

        public MainWindow()
        {
            InitializeComponent();

            Configuration config = PetoonsLauncher.Configuration;
            UserConfiguration userConfig = PetoonsLauncher.UserConfiguration;

            SetUpTab();
            //Init initialize XAML that doesnt need reUpdate
            InitProjectTab();
            //Fill XAML that need refill when reimport
            FillTabs(config, userConfig);
        }

        private void InitProjectTab()
        {
            var unityContent = (UnityControlContent)this.FindName("UnityContent");
            unityContent.Initialize();
        }

        public void FillTabs(Configuration config, UserConfiguration userConfig)
        {
            DrawProjectsTab(userConfig, config);
            DrawFoldersTab(userConfig);
            DrawSoftawareTab(userConfig);
            DrawLinksTab(userConfig);
            DrawSteamTab(config.Steam);
        }

        private void FillTabsCommand(object sender, RoutedEventArgs e)
        {
            PetoonsLauncher.ImportConfigurations();
            FillTabs(PetoonsLauncher.Configuration, PetoonsLauncher.UserConfiguration);
        }

        private void SetUpTab()
        {
            var tabsWidget = (TabControl)this.FindName("Tabs");
            tabsWidget.SelectedIndex = UserSettings.Default.lastTab;
            tabsWidget.SelectionChanged += OnTabChange;
        }
        private void OnTabChange(object sender, SelectionChangedEventArgs e)
        {
            var tab = (TabControl)sender;
            UserSettings.Default.lastTab = tab.SelectedIndex;
            UserSettings.Default.Save();
        }

        #region DRAW
        private void FillFoldersPanel(WrapPanel panelView, List<Folder> folders)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                var button = new Button();
                button.Margin = new Thickness(10);
                button.Width = GENERIC_BUTTON_WIDTH;
                button.Height = GENERIC_BUTTON_HEIGHT;
                
                button.Command = new ActionCommand<string>(PetoonsLauncher.OpenFolder);
                button.CommandParameter = folders[i].path;

                var content = new TextBlock();
                content.Text = folders[i].name;
                content.TextWrapping = TextWrapping.WrapWithOverflow;
                content.HorizontalAlignment = HorizontalAlignment.Center;
                content.VerticalAlignment = VerticalAlignment.Center;
                content.TextAlignment = TextAlignment.Center;
                button.Content = content;

                panelView.Children.Add(button);
            }
        }
        private void FillSoftwarePanel(WrapPanel panelView, List<Software> softwares)
        {
            for (int i = 0; i < softwares.Count; i++)
            {
                var button = new Button();
                button.Margin = new Thickness(10);
                button.Width = GENERIC_BUTTON_WIDTH;
                button.Height = GENERIC_BUTTON_HEIGHT;

                button.Command = new ActionCommand<string>(PetoonsLauncher.ExecuteSoftware);
                button.CommandParameter = softwares[i].path;

                var contentExtend = new StackPanel();
                contentExtend.HorizontalAlignment = HorizontalAlignment.Center;
                contentExtend.VerticalAlignment = VerticalAlignment.Center;
                contentExtend.Orientation = Orientation.Vertical;

                if (softwares[i].image != "")
                {
                    var image = new Image();
                    string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    image.Source = new BitmapImage(new Uri(String.Format("file:///{0}/" + softwares[i].image, currentAssemblyPath)));
                    image.Height = 75;
                    image.Width = 75;
                    contentExtend.Children.Add(image);
                }
                
                var content = new TextBlock();
                content.Text = softwares[i].name;
                content.TextWrapping = TextWrapping.WrapWithOverflow;
                content.HorizontalAlignment = HorizontalAlignment.Center;
                content.VerticalAlignment = VerticalAlignment.Center;
                content.TextAlignment = TextAlignment.Center;
                content.Margin = new Thickness(0, 10, 0, 0);
                contentExtend.Children.Add(content);

                button.Content = contentExtend;

                panelView.Children.Add(button);
            }
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

        private void DrawProjectsTab(UserConfiguration userConfig, Configuration config)
        {
            var unityContent = (UnityControlContent)this.FindName("UnityContent");
            unityContent.Fill(userConfig, config);
        }

        private void DrawFoldersTab(UserConfiguration config)
        {
            var foldersPanel = (WrapPanel)this.FindName("FolderPanel");
            foldersPanel.Children.Clear();
            FillFoldersPanel(foldersPanel, config.folders);
        }

        private void DrawSoftawareTab(UserConfiguration config)
        {
            var softwarePanel = (WrapPanel)this.FindName("SoftwarePanel");
            softwarePanel.Children.Clear();
            FillSoftwarePanel(softwarePanel, config.software);
        }

        private void DrawLinksTab(UserConfiguration config)
        {
            var linksPanel = (WrapPanel)this.FindName("LinkPanel");
            linksPanel.Children.Clear();
            FillLinksPanel(linksPanel, config.links);
        }

        private void DrawSteamTab(Steam steam)
        {
            var steamContent = (SteamControlContent)this.FindName("SteamContent");
            steamContent.SetUp(steam);
        }

        #endregion

        #region FUNCIONALITY

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenCustomUserConfiguration(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json";
            if(UserSettings.Default.userConfigurationPath != "")
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(UserSettings.Default.userConfigurationPath);
            }

            if (openFileDialog.ShowDialog() == true)
            {
                var path = openFileDialog.FileName;
                var userConfig = PetoonsLauncher.ImportConfiguration<UserConfigurationRoot>(path);
                if (userConfig.UserConfiguration == null)
                {
                    return;
                }
                PetoonsLauncher.SetUserConfiguration(userConfig.UserConfiguration);
                FillTabs(PetoonsLauncher.Configuration, PetoonsLauncher.UserConfiguration);
                UserSettings.Default.userConfigurationPath = path;
                UserSettings.Default.Save();
            }
            else
            {

            }    
        }

        private void OpenSDKs(object sender, RoutedEventArgs e)
        {
            
        }

        private void OpenAboutWindow(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new About();
            aboutWindow.Show();
        }

        #endregion
    }
}
