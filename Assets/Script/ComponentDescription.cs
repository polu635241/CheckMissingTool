using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

namespace CheckMissingTool.Model
{
	[Serializable]
	public class ComponentDescription
	{
		[SerializeField]
		ComponentStatus status;

		[SerializeField]
		List<string> missingFieldPaths = new List<string>();

		[SerializeField]
		string componentName;

		public ComponentDescription(Component ownerComponent)
		{
			//GetComponents 抓的到 可是回傳null 表示整個腳本掉了
			if (ownerComponent == null)
			{
				status = ComponentStatus.ScriptMissing;
			}
			else
			{
				componentName = ownerComponent.GetType ().ToString ();

				SerializedObject componentSo = new SerializedObject (ownerComponent);

				SerializedProperty fieldProp = componentSo.GetIterator();

				while (fieldProp.Next(true))
				{
					if (fieldProp.propertyType == SerializedPropertyType.ObjectReference)
					{
						UnityEngine.Object _object = fieldProp.objectReferenceValue;

						if (_object == null)
						{
							//InstanceID為0是 none 不為0 可是卻找不到對應的物件就是掉素材了
							if (fieldProp.objectReferenceInstanceIDValue != 0)
							{
								status = ComponentStatus.FieldMissing;

								string nativeFieldProp = fieldProp.propertyPath;

								string processPath = ProcessNativePath(nativeFieldProp);

								missingFieldPaths.Add (processPath);
							}

						}
					}
				}
			}
		}

		/// <summary>
		/// 在unity的解釋器裡 gos[0] 會記錄成 gos.Array.data[0]
		/// 這邊處裡成好閱讀版本
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="input">Input.</param>
		string ProcessNativePath(string input)
		{
			Regex regex = new Regex(".Array.data");
			string result = regex.Replace(input, "");
			return result;
		}
	}

	public enum ComponentStatus
	{
		Safe,ScriptMissing,FieldMissing
	}
}