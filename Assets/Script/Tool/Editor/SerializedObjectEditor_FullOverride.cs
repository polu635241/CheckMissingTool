using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CheckMissingTool.Tool
{
	public class SerializedObjectEditor_FullOverride<T> : SerializedObjectEditor<T>  where T:MonoBehaviour
	{
		const float beginIntervalSpace = 10f;
		const float scriptFieldKeyWidth = 50f;
		protected const float classIntervalSpace = 5f;

		public override void OnInspectorGUI ()
		{
			GUILayout.Space (beginIntervalSpace);

			DrawScriptField ();

			GUILayout.Space (classIntervalSpace);
		}

		void DrawScriptField()
		{
			DrawVariableField ("Script : ", () => {
					
				MonoScript monoScript = MonoScript.FromMonoBehaviour (runtimeScript);
				EditorTool.DrawInReadOnly(()=>
					{
						EditorGUILayout.ObjectField (monoScript, typeof(MonoScript), false);
					});
			}, scriptFieldKeyWidth);
			GUI.enabled = true;
		}
	}	
}