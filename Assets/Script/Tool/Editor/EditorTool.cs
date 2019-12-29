using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CheckMissingTool.Tool
{
	public static class EditorTool {

		public static class GUILayout
		{
			public static GUIStyle button = EditorStyles.miniButton;
			public static GUIStyle titleName = EditorStyles.label;
			public static GUIStyle fieldName = EditorStyles.miniLabel;
			public static float  classSpace = 10f;

			public static GUIStyle boxStyle = GUI.skin.box;
		}

		public static void DrawInReadOnly(Action body)
		{
			GUI.enabled = false;
			body.Invoke ();
			GUI.enabled = true;
		}

		public static void DrawInHorizontal(Action body,GUIStyle style=null)
		{
			if (style == null) 
			{
				EditorGUILayout.BeginHorizontal ();
			}
			else 
			{
				EditorGUILayout.BeginHorizontal (style);
			}
			
			body.Invoke ();
			EditorGUILayout.EndHorizontal ();
		}

		public static void DrawInVertical(Action body,GUIStyle style=null)
		{
			if (style == null) 
			{
				EditorGUILayout.BeginVertical ();
			}
			else 
			{
				EditorGUILayout.BeginVertical (style);
			}

			body.Invoke ();
			EditorGUILayout.EndVertical ();
		}


	}
}