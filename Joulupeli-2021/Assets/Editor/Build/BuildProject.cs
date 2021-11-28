using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

namespace Assets.Editor.Build
{
    public class BuildProject
    {

        public static int BuildAll()
        {
            int returnCode;
            returnCode = BuildAndroid();
            int returnCodeWebGL = BuildWebGL();
            if (returnCode == 0)
            {
                returnCode = returnCodeWebGL;
            }
            
            return returnCode;
        }
        
        public static int BuildWebGL()
        {
            Debug.Log("Start build for WebGL...");
            BuildReport report = PerformBuild("Builds/WebGL/", BuildTarget.WebGL);
            if (report.summary.result == BuildResult.Failed)
            {
                Debug.Log("WebGL build failed!");
                return 1;
            }
            Debug.Log("WebGL build successful!");
            return 0;
        }

        public static int BuildAndroid()
        {
            Debug.Log("Start build for Android...");
            BuildReport report = PerformBuild("Builds/Joulupeli2021.apk", BuildTarget.Android);
            if (report.summary.result == BuildResult.Failed)
            {
                Debug.Log("Android build failed!");
                return 1;
            }
            Debug.Log("Android build successful!");
            return 0;
        }

        private static BuildReport PerformBuild(string outPath, BuildTarget target)
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.scenes = new string[] {
                "Assets/Scenes/MainScene.unity"
            };
            options.locationPathName = outPath;
            options.target = target;
            return BuildPipeline.BuildPlayer(options);
        }
    }
}