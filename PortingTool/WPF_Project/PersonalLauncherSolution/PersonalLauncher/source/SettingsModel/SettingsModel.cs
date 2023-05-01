using System.Collections.Generic;

namespace PersonalLauncher
{
    public class Configuration
    {
        public List<string> UnityPlatforms { get; set; }
        public Steam Steam { get; set; }
        public Switch Switch { get; set; }
        public Stadia Stadia { get; set; }
        public Xbox Xbox { get; set; }
        public Ps4 Ps4 { get; set; }
        public Ps5 Ps5 { get; set; }
    }

    public class Program
    {
        public string softwareID { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
    }

    public class Ps4
    {
        public List<Program> programs { get; set; }
        public List<Link> links { get; set; }
    }

    public class Ps5
    {
        public List<Program> programs { get; set; }
        public List<Link> links { get; set; }
    }

    public class ConfigurationRoot
    {
        public Configuration configuration { get; set; }
    }

    public class Stadia
    {
        public List<Link> links { get; set; }
    }

    public class Steam
    {
        public List<Link> links { get; set; }
    }

    public class Switch
    {
        public List<Program> programs { get; set; }
        public List<Link> links { get; set; }
    }

    public class Xbox
    {
        public List<Program> programs { get; set; }
        public List<Link> links { get; set; }
    }


}