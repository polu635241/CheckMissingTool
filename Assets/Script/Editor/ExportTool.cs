using System;
using System.IO;
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
		public static void Export_Package()
		{
//			string[] guids = AssetDatabase.FindAssets ("",new string[]{"Assets/Script"});

//			Array.ForEach (guids, (guid) => 
//				{
//					string path = AssetDatabase.GUIDToAssetPath(guid);
//					Debug.Log(path);
//				});
											//資料夾底下所有檔案都要
			ExportPackageOptions options = ExportPackageOptions.Recurse;

			string outputPath = Application.dataPath + "/../Output/CheckMissingTool.unitypackage";

			AssetDatabase.ExportPackage ("Assets/Script", outputPath, options);

			Debug.Log ("-->");
			Debug.Log (outputPath);
		}
		
	}
}

