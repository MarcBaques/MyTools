using PersonalLauncher.Commands;
using PersonalLauncher.settings;
using PersonalLauncher.Source;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
//Themes:
//https://github.com/AngryCarrot789/WPFDarkTheme (Current)
//http://materialdesigninxaml.net/
//Tutos:
//https://riptutorial.com/wpf
//Utilities configurations:
//JSON creator: https://jsoneditoronline.org/#right=local.julutu&left=local.qiqoli
//Class generator from JSON: https://json2csharp.com/code-converters/json-to-python

namespace PersonalLauncher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class PetoonsLauncher : Application
    {

        private const string CONFIGURATION_PATH = "settings/config.json";
        private const string USER_CONFIGURATION_PATH = "settings/userConfiguration.json";
        private const string SCRIPTS_PATH = "res\\scripts\\";
        private const string BAT_EXTENSION = ".bat";
        private static Configuration m_Configuration;
        private static UserConfiguration m_UserConfiguration;

        public static Configuration Configuration { get => m_Configuration; }
        public static UserConfiguration UserConfiguration { get => m_UserConfiguration; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ImportConfigurations();
        }

        public static void ImportConfigurations()
        {
            var userSettingsPath = (UserSettings.Default.userConfigurationPath != "") ? UserSettings.Default.userConfigurationPath : USER_CONFIGURATION_PATH;

            m_Configuration = ImportConfiguration<ConfigurationRoot>(CONFIGURATION_PATH).configuration;
            m_UserConfiguration = ImportConfiguration<UserConfigurationRoot>(userSettingsPath).UserConfiguration;
        }

        public static T ImportConfiguration<T>(string path = CONFIGURATION_PATH)
        {
            var configJson = FileSystem.LoadJson(path);
            return FileSystem.GetConfigurationFromJson<T>(configJson);
        }

        public static void SetUserConfiguration(UserConfiguration config)
        {
            m_UserConfiguration = config;
        }


        public static void OpenFolder(string folderPath)
        {
            Process.Start("explorer.exe", folderPath);
        }

        public static void OpenUnityProject(UnityProjectCommandModel unityProjectModel)
        {
            var unityProcess = new Process();
            unityProcess.StartInfo.FileName = unityProjectModel.UnityVersionPath;
            unityProcess.StartInfo.Arguments = "-projectPath " + unityProjectModel.ProjectPath;
            unityProcess.StartInfo.Arguments += " -buildTarget " + unityProjectModel.BuildTarget;
            unityProcess.Start();
        }

        public static void OpenChromeLink(string url)
        {
            var browserProcess = new Process();
            browserProcess.StartInfo.FileName = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            browserProcess.StartInfo.Arguments = url;
            browserProcess.Start();
        }

        public static void ExecuteBatFile(string batFile, string arguments = "")
        {
            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var batPath = appDirectory + SCRIPTS_PATH + batFile + BAT_EXTENSION;
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + batPath +  " " + arguments);

            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;

            var process = Process.Start(processInfo);

            process.WaitForExit();
            process.Close();
        }

        public static void ExecuteSoftware(string softwarePath)
        {
            var browserProcess = new Process();
            browserProcess.StartInfo.FileName = softwarePath;
            browserProcess.Start();
        }
    }
}
