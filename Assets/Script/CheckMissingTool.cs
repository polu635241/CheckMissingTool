using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using CheckMissingTool.Model;

namespace CheckMissingTool
{
	[CreateAssetMenu(menuName = "Setting/CheckMissingTool")]
	public class CheckMissingTool : ScriptableObject {

		[SerializeField]
		CheckMissingToolSetting setting;

		[SerializeField]
		SceneCheckMissingResult sceneCheckMissingResult;

		public void CheckScene()
		{
			#if UNITY_EDITOR
			Debug.Log (MixStringColor ("Begin Check Scene ~~", setting.defultMsgColor));

			Scene currentScene = EditorSceneManager.GetActiveScene ();
			string scenePath = currentScene.path;

			List<GameObject> rootGOs = new List<GameObject>(currentScene.GetRootGameObjects());

			List<GoDescription> goDescriptions = new List<GoDescription> ();

			EditorUtility.DisplayProgressBar ("Search", "正在收集場景中所有的物件", 0f);

			rootGOs.ForEach (rootGO=>
				{
					List<GoDescription> _goDescriptions = GoDescription.RecursiveCollection (rootGO);

					goDescriptions.AddRange(_goDescriptions);
				});

			List<GoDescription> missingGoDescriptions = ProcessSceneGO(goDescriptions);

			this.sceneCheckMissingResult = new SceneCheckMissingResult(scenePath, missingGoDescriptions);
			EditorUtility.ClearProgressBar();
			#endif
		}

		List<GoDescription> ProcessSceneGO(List<GoDescription> goDescriptions)
		{
			List<GoDescription> missingGoDescriptions = new List<GoDescription> ();

			for (int i = 0; i < goDescriptions.Count; i++) 
			{
				//加一因為index從0開始
				float progress = (i + 1) / goDescriptions.Count;
				EditorUtility.DisplayProgressBar ("Search", $"正在處理第{i}個物件", 0f);
				GoDescription goDescription = goDescriptions [i];

				goDescription.ProcessComponent();

				if (CheckMissing (goDescription)) 
				{
					goDescription.ProcessRecursivePath();
					ProcessLog (goDescription);

					missingGoDescriptions.Add(goDescription);
				}
			}

			return missingGoDescriptions;
		}

		bool CheckMissing(GoDescription goDescription)
		{
			//如果所有Component都通過那就跳過他
			if(goDescription.ComponentDescriptions.TrueForAll(comDesc=>comDesc.Status== ComponentStatus.Safe))
			{
				return false;
			}
			else
			{
				return true;
			}

			return true;
		}

		void ProcessLog(GoDescription goDescription)
		{
			goDescription.ProcessRecursivePath();

			Debug.Log(MixStringColor(goDescription.Path, setting.pathColor));

			goDescription.ComponentDescriptions.ForEach(comDesc=>
				{
					//跳過Safe的
					if(comDesc.Status == ComponentStatus.Safe)
					{
						return;
					}
					else
					{
						Debug.Log(MixStringColor(comDesc.ComponentName, setting.componentColor));

						if(comDesc.Status == ComponentStatus.ScriptMissing)
						{
							Debug.Log(MixStringColor("ScriptMissing", setting.missingScriptColor));
						}

						if(comDesc.Status == ComponentStatus.FieldMissing)
						{
							Debug.Log(MixStringColor("FieldMissing ->", setting.fieldColor));

							comDesc.MissingFieldPaths.ForEach(fieldPath=>
								{
									Debug.Log(MixStringColor(fieldPath, setting.fieldColor));
								});
						}
					}
				});
		}

		public void CheckProject()
		{
			Debug.Log (MixStringColor ("Begin Check Project ~~", setting.defultMsgColor));
		}

		/// <summary>
		/// 方便使用者抽換
		/// </summary>
		/// <returns>The string color.</returns>
		/// <param name="input">Input.</param>
		/// <param name="inputColor">Input color.</param>
		string MixStringColor(System.Object input, Color inputColor)
		{
			return Tool.Tool.MixStringColor (input, inputColor);
		}
	}

	[Serializable]
	class CheckMissingToolSetting
	{
		[Header("顯示物件路徑的顏色")]
		public Color pathColor = Color.gray;

		[Header("顯示Component的顏色")]
		public Color componentColor = Color.magenta;

		[Header("顯示變數的顏色")]
		public Color fieldColor = new Color (0, 0.847f, 1); //預設淡藍色

		[Header("顯示掉腳本的顏色")]
		public Color missingScriptColor = Color.yellow;

		[Header("顯示掉素材的顏色")]
		public Color missingFieldColor = Color.red;

		[Header("顯示一般訊息的顏色")]
		public Color defultMsgColor = Color.grey;
	}

	[Serializable]
	class SceneCheckMissingResult
	{
		public SceneCheckMissingResult (string scenePath, List<GoDescription> missingGoDescriptions)
		{
			this.scenePath = scenePath;
			this.missingGoDescriptions = new List<GoDescription> (missingGoDescriptions);
		}
		
		[SerializeField]
		string scenePath;

		[SerializeField]
		List<GoDescription> missingGoDescriptions = new List<GoDescription> ();
	}

}
