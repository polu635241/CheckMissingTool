using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CheckMissingTool.Tool;

namespace CheckMissingTool
{
	[CustomEditor(typeof(CheckMissingTool))]
	public class CheckMissingToolEditor : SerializedObjectEditor<CheckMissingTool> {

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();

			GUILayout.Space (15f);

			EditorTool.DrawInHorizontal (()=>
				{
					if (GUILayout.Button ("Check Scene", buttonGUIStyle, GUILayout.Height (buttoHeight)))
					{
						runtimeScript.CheckScene ();
					}

					if (GUILayout.Button ("Check Project", buttonGUIStyle, GUILayout.Height (buttoHeight)))
					{
						runtimeScript.CheckProject ();
					}
				});
		}

	}
	
}