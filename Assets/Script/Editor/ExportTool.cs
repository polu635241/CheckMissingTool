using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CheckMissingTool;

namespace CheckMissingTool.Tool
{
	static class ExportTool
	{
		[MenuItem("CheckMissingTool/Export Package")]
		static void Export_Package()
		{
//			string[] guids = AssetDatabase.FindAssets ("",new string[]{"Assets/Script"});

//			Array.ForEach (guids, (guid) => 
//				{
//					string path = AssetDatabase.GUIDToAssetPath(guid);
//					Debug.Log(path);
//				});
											//資料夾底下所有檔案都要        //輸出完會彈出視窗
			ExportPackageOptions options = ExportPackageOptions.Recurse | ExportPackageOptions.Interactive;

			AssetDatabase.ExportPackage ("Assets/Script", "Output/AAA.unitypackage", options);
		}
		
	}
}

