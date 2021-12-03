using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.IO;
using System;

namespace Assets.Editor.Build
{
    public class BuildProject
    {
        private const string ENV_VAR_KEYSTORE_PATH = "UNITY_KEYSTORE_PATH";
        private const string ENV_VAR_KEYSTORE_MASTER_PASS = "UNITY_KEYSTORE_MASTER_PASSWORD";
        private const string ENV_VAR_KEY_ALIAS = "UNITY_KEY_ALIAS";
        private const string ENV_VAR_KEY_PASS = "UNITY_KEY_PASSWORD";

        [MenuItem("Build/Build all")]
        public static int BuildAll()
        {
            IncrementBundleVersion();

            int returnCode;
            returnCode = BuildAndroid(false);
            int returnCodeWebGL = BuildWebGL(false);
            if (returnCode == 0)
            {
                returnCode = returnCodeWebGL;
            }
            
            return returnCode;
        }

        [MenuItem("Build/Build WebGL")]
        public static int BuildWebGL()
        {
            return BuildWebGL(true);
        }

        public static int BuildWebGL(bool incrementVersion)
        {
            if (incrementVersion)
            {
                IncrementBundleVersion();
            }
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

        [MenuItem("Build/Build Android")]
        public static int BuildAndroid()
        {
            return BuildAndroid(true);
        }

        public static int BuildAndroid(bool incrementVersion)
        {
            if (incrementVersion)
            {
                IncrementBundleVersion();
            }
            Debug.Log("Start build for Android...");

            PlayerSettings.Android.keystoreName = Environment.GetEnvironmentVariable(ENV_VAR_KEYSTORE_PATH);
            PlayerSettings.Android.keystorePass = Environment.GetEnvironmentVariable(ENV_VAR_KEYSTORE_MASTER_PASS);
            PlayerSettings.Android.keyaliasName = Environment.GetEnvironmentVariable(ENV_VAR_KEY_ALIAS);
            PlayerSettings.Android.keyaliasPass = Environment.GetEnvironmentVariable(ENV_VAR_KEY_PASS);
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

        private static void IncrementBundleVersion()
        {
            string versionText = File.ReadAllText("version.txt");

            if (versionText != null)
            {
                string[] lines = versionText.Trim().Split('.');

                int majorVersion = int.Parse(lines[0]);
                int minorVersion = int.Parse(lines[1]);
                int subMinorVersion = int.Parse(lines[2]) + 1;

                versionText = majorVersion.ToString() + "." +
                    minorVersion.ToString() + "." +
                    subMinorVersion.ToString();

                PlayerSettings.bundleVersion = versionText;
                PlayerSettings.Android.bundleVersionCode = majorVersion*10000 + minorVersion*100 + subMinorVersion;
                File.WriteAllText("version.txt", versionText);
            }
        }
    }
}