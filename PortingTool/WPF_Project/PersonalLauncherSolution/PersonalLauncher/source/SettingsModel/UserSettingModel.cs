using System.Collections.Generic;

namespace PersonalLauncher
{
    public class Credential
    {
        public string id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }

    public class Folder
    {
        public string path { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }

    public class Link
    {
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Project
    {
        public string name { get; set; }
        public string path { get; set; }
        public string unityVersion { get; set; }
        public string image { get; set; }
    }

    public class UserConfigurationRoot
    {
        public UserConfiguration UserConfiguration { get; set; }
    }

    public class Sdk
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Software
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string image { get; set; }
    }

    public class UnityEditors
    {
        public string name { get; set; }
        public string path { get; set; }
    }

    public class UserConfiguration
    {
        public List<UnityEditors> unityEditors { get; set; }
        public List<Project> projects { get; set; }
        public List<Link> links { get; set; }
        public List<Folder> folders { get; set; }
        public List<Sdk> sdks { get; set; }
        public List<Software> software { get; set; }
        public List<Credential> credentials { get; set; }
    }
}
