Set projectPath=%cd%\..
echo Launching Unity Build...
"D:\2017\Editor\Unity.exe" ^
-batchmode ^
-projectPath %projectPath% ^
-executeMethod CheckMissingTool.Tool.ExportTool.Export_Package ^
-quit ^
-logFile %projectPath%\Output\package_export.log

echo Build Finished!

PAUSE