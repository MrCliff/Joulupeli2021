#!/bin/bash

PROJECT_PATH="./Joulupeli-2021"
BUILD_PATH="${PROJECT_PATH}/Builds"
LOG_PATH="${PROJECT_PATH}/Logs"

if [ ! "${UNITY_KEYSTORE_PATH}" ];then
    echo "Environment variable UNITY_KEYSTORE_PATH has no value."
    exit 1
elif [ ! "${UNITY_KEYSTORE_MASTER_PASSWORD}" ];then
    echo "Environment variable UNITY_KEYSTORE_MASTER_PASSWORD has no value."
    exit 1
elif [ ! "${UNITY_KEY_ALIAS}" ];then
    echo "Environment variable UNITY_KEY_ALIAS has no value."
    exit 1
elif [ ! "${UNITY_KEY_PASSWORD}" ];then
    echo "Environment variable UNITY_KEY_PASSWORD has no value."
    exit 1
fi

echo "Start building for all targets..."
unity.sh -quit -batchmode -projectPath "${PROJECT_PATH}" -executeMethod Assets.Editor.Build.BuildProject.BuildAll -logfile "${LOG_PATH}/build.log"
echo "Build done!"
