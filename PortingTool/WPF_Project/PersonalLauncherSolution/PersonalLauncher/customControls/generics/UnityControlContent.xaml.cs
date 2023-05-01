using PersonalLauncher.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PersonalLauncher.customControls.generics
{
    /// <summary>
    /// Interaction logic for UnityControlContent.xaml
    /// </summary>
    public partial class UnityControlContent : UserControl
    {
        private string BuildTarget = "Standalone";
        private int GENERIC_BUTTON_HEIGHT = 125;
        private int GENERIC_BUTTON_WIDTH = 125;

        public UnityControlContent()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            var comboBox = (ComboBox)this.FindName("ProjectsUnityPlatforms_ComboBox");
            comboBox.SelectionChanged += UpdateProjectPlatform;
        }

        public void Fill(UserConfiguration userConfig, Configuration config)
        {
            var projectPanel = (WrapPanel)this.FindName("ProjectsPanel");
            projectPanel.Children.Clear();
            FillProjectsPanel(projectPanel, userConfig.projects);

            var comboBox = (ComboBox)this.FindName("ProjectsUnityPlatforms_ComboBox");
            comboBox.Items.Clear();

            for (int i = 0; i < config.UnityPlatforms.Count; i++)
            {
                comboBox.Items.Add(config.UnityPlatforms[i]);
            }
            comboBox.SelectedItem = config.UnityPlatforms[0];
        }

        private void FillProjectsPanel(WrapPanel panelView, List<Project> projects)
        {
            for (int i = 0; i < projects.Count; i++)
            {
                var projectButton = new Button();
                projectButton.Margin = new Thickness(10);
                projectButton.Width = GENERIC_BUTTON_WIDTH;
                projectButton.Height = GENERIC_BUTTON_HEIGHT;

                projectButton.Command = new ActionCommand<UnityProjectCommandModel>(PetoonsLauncher.OpenUnityProject);
                var unityEditor = "";
                for (int j = 0; j < PetoonsLauncher.UserConfiguration.unityEditors.Count; j++)
                {
                    if (PetoonsLauncher.UserConfiguration.unityEditors[j].name == projects[i].unityVersion)
                    {
                        unityEditor = PetoonsLauncher.UserConfiguration.unityEditors[j].path;
                    }
                }
                projectButton.CommandParameter = new UnityProjectCommandModel(projects[i].path, unityEditor, BuildTarget);

                var contentExtend = new StackPanel();
                contentExtend.HorizontalAlignment = HorizontalAlignment.Center;
                contentExtend.VerticalAlignment = VerticalAlignment.Center;
                contentExtend.Orientation = Orientation.Vertical;

                if (projects[i].image != "")
                {
                    var image = new Image();
                    string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    image.Source = new BitmapImage(new Uri(String.Format("file:///{0}/" + projects[i].image, currentAssemblyPath)));
                    image.Height = 50;
                    image.Width = 50;
                    contentExtend.Children.Add(image);
                }

                var content = new TextBlock();
                content.Text = projects[i].name;
                content.TextWrapping = TextWrapping.WrapWithOverflow;
                content.HorizontalAlignment = HorizontalAlignment.Center;
                content.VerticalAlignment = VerticalAlignment.Center;
                content.TextAlignment = TextAlignment.Center;
                content.Margin = new Thickness(0, 10, 0, 0);
                contentExtend.Children.Add(content);

                projectButton.Content = contentExtend;

                panelView.Children.Add(projectButton);
            }
        }

        private void UpdateProjectPlatform(object sender, SelectionChangedEventArgs e)
        {
            ComboBox? cmb = sender as ComboBox;
            foreach (var platformKey in PetoonsLauncher.Configuration.UnityPlatforms)
            {
                if (cmb.SelectedItem == null)
                    return;

                if (cmb.SelectedItem.ToString() == platformKey)
                {
                    BuildTarget = platformKey;
                    break;
                }
            }
        }
    }
}
