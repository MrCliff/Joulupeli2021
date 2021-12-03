#!/bin/bash

PROJECT_PATH="./Joulupeli-2021"
BUILD_PATH="${PROJECT_PATH}/Builds"
LOG_PATH="${PROJECT_PATH}/Logs"
WEBSITE_PATH="../Joulupelinettisivu/public/Joulupeli2021"

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

echo "Removing old files from website path '${WEBSITE_PATH}/'..."
rm "${WEBSITE_PATH}/"*
echo "Copying WebGL build to the website path..."
cp "${BUILD_PATH}/WebGL/Build/"* "${WEBSITE_PATH}/"
echo "Copying Android build to the website path..."
cp "${BUILD_PATH}/Joulupeli2021.apk" "${WEBSITE_PATH}/Joulupeli2021.apk"
echo "Copying version number to the website path..."
cp "${PROJECT_PATH}/version.txt" "${WEBSITE_PATH}/version.txt"

echo "Done!"
