#!/bin/bash

PROJECT_PATH="./Joulupeli-2021"
BUILD_PATH="${PROJECT_PATH}/Builds"
WEBSITE_GAME_PATH="../Joulupelinettisivu/public/Joulupeli2021/game"
BUILD_VERSION=`cat "${PROJECT_PATH}/version.txt"`

echo "Start game deployment (v${BUILD_VERSION})"

echo "Removing old files from website path '${WEBSITE_GAME_PATH}/'..."
rm "${WEBSITE_GAME_PATH}/"*
echo "Copying WebGL build to the website path..."
cp "${BUILD_PATH}/WebGL/Build/"* "${WEBSITE_GAME_PATH}/"
# Android build is handled on Google Play and GitHub repository's releases.
# echo "Copying Android build to the website path..."
# cp "${BUILD_PATH}/Joulupeli2021_v${BUILD_VERSION}.aab" "${WEBSITE_GAME_PATH}/Joulupeli2021.aab"
echo "Copying version number to the website path..."
cp "${PROJECT_PATH}/version.txt" "${WEBSITE_GAME_PATH}/version.txt"

echo "Done!"
