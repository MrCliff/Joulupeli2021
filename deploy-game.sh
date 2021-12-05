#!/bin/bash

PROJECT_PATH="./Joulupeli-2021"
BUILD_PATH="${PROJECT_PATH}/Builds"
WEBSITE_PATH="../Joulupelinettisivu/public/Joulupeli2021"
BUILD_VERSION=`cat "${PROJECT_PATH}/version.txt"`

echo "Start game deployment"

echo "Removing old files from website path '${WEBSITE_PATH}/'..."
rm "${WEBSITE_PATH}/"*
echo "Copying WebGL build to the website path..."
cp "${BUILD_PATH}/WebGL/Build/"* "${WEBSITE_PATH}/"
echo "Copying Android build to the website path..."
cp "${BUILD_PATH}/Joulupeli2021_v${BUILD_VERSION}.aab" "${WEBSITE_PATH}/Joulupeli2021.aab"
echo "Copying version number to the website path..."
cp "${PROJECT_PATH}/version.txt" "${WEBSITE_PATH}/version.txt"

echo "Done!"
