#!/bin/bash

unity.bat -quit -batchmode -projectPath ./Joulupeli-2021 -executeMethod Assets.Editor.Build.BuildProject.BuildAll

cp ./Joulupeli-2021/Builds/WebGL/Build/* ../Joulupelinettisivu/public/Joulupeli2021/
cp ./Joulupeli-2021/Builds/Joulupeli2021.apk ../Joulupelinettisivu/public/Joulupeli2021/Joulupeli2021.apk
