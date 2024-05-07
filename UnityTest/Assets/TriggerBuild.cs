using UnityEditor.Build.Profile;
using UnityEngine;

public class TriggerBuild
{
    
    
    public static void LaunchBuild()
    {
        var myCurrentProfile = BuildProfile.GetActiveBuildProfile();

        BuildProfile myBuildProfile = new BuildProfile();
        BuildProfile.SetActiveBuildProfile(myBuildProfile);
        Debug.Log("Active build profile setted");
    }
}
