using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace BlackRefactory.Utils
{
    public static class DirectiveUtils
    {
        public static void AddDirective(string directive)
        {
            var alldirectives = PlayerSettings.GetScriptingDefineSymbols(GetCurrentNamedBuildTarget());
            PlayerSettings.SetScriptingDefineSymbols(GetCurrentNamedBuildTarget(), alldirectives + ";"+directive);
        }

        public static void RemoveDirective(string directive)
        {
            var alldirectives = PlayerSettings.GetScriptingDefineSymbols(GetCurrentNamedBuildTarget());
            var directives = alldirectives.Split(';');
            directives.ToList().Remove(directive);
            PlayerSettings.SetScriptingDefineSymbols(GetCurrentNamedBuildTarget(), string.Join(';', directives));
        }

        public static bool IsDirective(string directive)
        {
            var allDirective = PlayerSettings.GetScriptingDefineSymbols(GetCurrentNamedBuildTarget());
            var directives = allDirective.Split(';');
            return directives.Contains(directive);
        }

        public static void SetDirectives(string directives)
        {
            PlayerSettings.SetScriptingDefineSymbols(GetCurrentNamedBuildTarget(), directives);
        }

        public static string GetCurrentDirectives()
        {
            return PlayerSettings.GetScriptingDefineSymbols(GetCurrentNamedBuildTarget());
        }

        /// <summary>
        /// Unity doesn't have to get directly NamedBuildTarget so we have to get it from targetGroup
        /// </summary>
        /// <returns></returns>
        public static NamedBuildTarget GetCurrentNamedBuildTarget()
        {
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup targetGroup = UnityEditor.BuildPipeline.GetBuildTargetGroup(buildTarget);
            return NamedBuildTarget.FromBuildTargetGroup(targetGroup);
        }
    }
}

