using System.Text;
using Unity.Profiling;
using UnityEngine;

namespace BlackRefactory.Profiling
{
    //Original idea: https://thegamedev.guru/unity-memory/profiler-module-metrics-programatically/
    public class ProfilingTool : MonoBehaviour
    {
        [SerializeField] private bool Show = true;

        private string m_StatsText;
        private Rect StatsAreaRect;

#region MEMORY RECORDERS
        //https://docs.unity3d.com/2020.2/Documentation/Manual/ProfilerMemory.html
        ProfilerRecorder m_Recorder01;
        private const string TOTAL_MEMORY_RESERVED = "Total Reserved Memory";
        ProfilerRecorder m_Recorder02;
        private const string TOTAL_MEMORY_USED = "Total Used Memory";
        ProfilerRecorder m_Recorder03;
        private const string SYSTEM_MEMORY_USED = "System Used Memory";
        ProfilerRecorder m_Recorder04;
        private const string PROFILER_MEMORY_USED = "Profiler Used Memory";

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        ProfilerRecorder m_Recorder05;
        private const string TEXTURE_MEMORY = "Texture Memory";
        ProfilerRecorder m_Recorder06;
        private const string TEXTURE_COUNT = "Texture Count";

        ProfilerRecorder m_Recorder07;
        private const string MESH_MEMORY = "Mesh Memory";
        ProfilerRecorder m_Recorder08;
        private const string MESH_COUNT = "Mesh Count";

        ProfilerRecorder m_Recorder09;
        private const string MATERIAL_MEMORY = "Material Memory";
        ProfilerRecorder m_Recorder10;
        private const string MATERIAL_COUNT = "Material Count";

        ProfilerRecorder m_Recorder11;
        private const string ANIMATIONS_MEMORY = "Animations Memory";
        ProfilerRecorder m_Recorder12;
        private const string ANIMATIONS_COUNT = "Animations Count";
#endif
#endregion

#region INTERNAL RECORDERS
        //https://docs.unity3d.com/2020.2/Documentation/ScriptReference/Unity.Profiling.ProfilerRecorder.html
        ProfilerRecorder m_Recorder13;
        private const string MAIN_THREAD_FRAME_TIME = "Main Thread";
#endregion

        private void Awake()
        {
            //Only keep this script if it's Development build or editor.
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            DontDestroyOnLoad(this.gameObject);
#else
            Destroy(this);
#endif
        }

        void OnEnable()
        {
            m_Recorder01 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, TOTAL_MEMORY_RESERVED);
            m_Recorder02 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, TOTAL_MEMORY_USED);
            m_Recorder03 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, SYSTEM_MEMORY_USED);
            m_Recorder04 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, PROFILER_MEMORY_USED);
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            m_Recorder05 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, TEXTURE_MEMORY);
            m_Recorder06 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, TEXTURE_COUNT);
            m_Recorder07 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, MESH_MEMORY);
            m_Recorder08 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, MESH_COUNT);
            m_Recorder09 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, MATERIAL_MEMORY);
            m_Recorder10 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, MATERIAL_COUNT);
            m_Recorder11 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, ANIMATIONS_MEMORY);
            m_Recorder12 = ProfilerRecorder.StartNew(ProfilerCategory.Memory, ANIMATIONS_COUNT);
#endif
            m_Recorder13 = ProfilerRecorder.StartNew(ProfilerCategory.Internal, MAIN_THREAD_FRAME_TIME, 15);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            StatsAreaRect = new Rect(10, 20, 230, 128);
#else
            StatsAreaRect = new Rect(10, 20, 230, 82);
#endif

#if UNITY_EDITOR
            //Comment/Uncomment to debug categories and and profiler markers, attach andd inspect in order to see them all.
            //ProfilerUtils.EnumerateProfilerStats();
#endif
        }


        private void OnDisable()
        {
            m_Recorder01.Dispose();
            m_Recorder02.Dispose();
            m_Recorder03.Dispose();
            m_Recorder04.Dispose();
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            m_Recorder05.Dispose();
            m_Recorder06.Dispose();
            m_Recorder07.Dispose();
            m_Recorder08.Dispose();
            m_Recorder09.Dispose();
            m_Recorder10.Dispose();
            m_Recorder11.Dispose();
            m_Recorder12.Dispose();
#endif
            m_Recorder13.Dispose();
        }

        private void Update()
        {
            var statsBuilder = new StringBuilder(600);
            statsBuilder.AppendLine($"Frame Time: {ProfilerUtils.GetRecorderFrameAverage(m_Recorder13) * (1e-6f):F1} ms");
            statsBuilder.AppendLine($"{TOTAL_MEMORY_USED}: {ProfilerUtils.ConvertBytesToGigabytes(m_Recorder02.LastValue):F2} GB");
            statsBuilder.AppendLine($"{TOTAL_MEMORY_RESERVED}: {ProfilerUtils.ConvertBytesToGigabytes(m_Recorder01.LastValue):F2} GB");
            statsBuilder.AppendLine($"{SYSTEM_MEMORY_USED}: {ProfilerUtils.ConvertBytesToGigabytes(m_Recorder03.LastValue):F2} GB");
            statsBuilder.AppendLine($"{PROFILER_MEMORY_USED}: {ProfilerUtils.ConvertBytesToMegabytes(m_Recorder04.LastValue):F2} MB");
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            statsBuilder.AppendLine($"-Texture: {m_Recorder06.LastValue}/{ProfilerUtils.ConvertBytesToMegabytes(m_Recorder05.LastValue):F0} MB");
            statsBuilder.AppendLine($"-Mesh: {m_Recorder08.LastValue}/{ProfilerUtils.ConvertBytesToMegabytes(m_Recorder07.LastValue):F0} MB");
            statsBuilder.AppendLine($"-Material: {m_Recorder10.LastValue}/{ProfilerUtils.ConvertBytesToMegabytes(m_Recorder09.LastValue):F0} MB");
            statsBuilder.AppendLine($"-Animations: {m_Recorder12.LastValue}/{ProfilerUtils.ConvertBytesToMegabytes(m_Recorder11.LastValue):F0} MB");
#endif

            m_StatsText = statsBuilder.ToString();
        }

        public void ShowTool()
        {
            Show = true;
        }

        public void HideTool()
        {
            Show = false;
        }
        
        private void OnGUI()
        {
            if(Show)
                GUI.TextArea(StatsAreaRect, m_StatsText);
        }
    }
}

