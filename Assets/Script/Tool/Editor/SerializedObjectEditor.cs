using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CheckMissingTool.Tool
{
	public abstract class SerializedObjectEditor<T> : Editor where T:MonoBehaviour 
	{
		protected T runtimeScript;

		protected GUIStyle buttonGUIStyle
		{
			get
			{
				return EditorStyles.miniButton;
			}
		}
		protected GUIStyle fieldNameGUIStyle
		{
			get
			{
				return EditorStyles.miniLabel;
			}
		}
		protected GUIStyle titleNameGUIStyle
		{
			get
			{
				return EditorStyles.label;
			}
		}

		protected const float fieldNameWidth = 70f;

		protected virtual void OnEnable()
		{
			runtimeScript = (T)target;
		}

		protected void DrawVariableField (string variableName, Action drawAndGetInput, float? overrideFieldWidth = null)
		{
			EditorTool.DrawInHorizontal (() => 
			{
				EditorGUILayout.LabelField (variableName, fieldNameGUIStyle, GUILayout.Width (fieldNameWidth));
				drawAndGetInput.Invoke ();
			});
		}
	}	
}