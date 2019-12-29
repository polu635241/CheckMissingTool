using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckMissingTool.Model
{
	[Serializable]
	class GoDescription
	{
		[NonSerialized]
		GoDescription parent;

		[SerializeField]
		string path;

		public string Path
		{
			get
			{
				return path;
			}
		}

		/// <summary>
		/// 若為實體prefab 外部傳入檔案位置
		/// </summary>
		/// <param name="filePath">File path.</param>
		public void InsertPrefabPath(string filePath)
		{
			path = filePath + "~/" + path;
		}

		GameObject entity;

		[SerializeField]
		List<ComponentDescription> componentDescriptions = new List<ComponentDescription> ();

		public List<ComponentDescription> ComponentDescriptions
		{
			get
			{
				return componentDescriptions;
			}
		}

		public static List<GoDescription> RecursiveCollection (GameObject entity)
		{
			List<GoDescription> collections = new List<GoDescription> ();

			Action<GoDescription> collectionCallback = (_goDescription) => 
			{
				collections.Add (_goDescription);
			};

			//透過建構式遞迴建構
			GoDescription rootGoDescription = new GoDescription (entity, null);

			collections.Add (rootGoDescription);
			collections.AddRange (rootGoDescription.GetChildDescriptions ());

			return collections;
		}

		GoDescription(GameObject entity, GoDescription parent)
		{
			this.entity = entity;

			this.parent = parent;
		}

		List<GoDescription> GetChildDescriptions()
		{
			List<GoDescription> childDescriptions = new List<GoDescription> ();
			
			foreach (Transform child in this.entity.transform) 
			{
				GoDescription childDescription = new GoDescription (child.gameObject, this);

				childDescriptions.Add (childDescription);
				childDescriptions.AddRange (childDescription.GetChildDescriptions ());
			}

			return childDescriptions;
		}

		/// <summary>
		/// 底下的component有問題的時候才會去遞迴查找出完整的子物件Path
		/// </summary>
		public void ProcessRecursivePath()
		{
			List<GameObject> parentsGos = new List<GameObject> ();

			GoDescription focusDescription = parent;

			//遞迴往上找 直到沒有上一層為止
			while (true)
			{
				if (focusDescription != null) 
				{
					parentsGos.Add (focusDescription.entity);

					focusDescription = focusDescription.parent;
				}
				else
				{
					break;
				}
			}

			//因為是一層一層往回找 所以為了方便閱讀要顛倒成Hierachy上的順序
			parentsGos.Reverse ();

			StringBuilder pathStringBuilder = new StringBuilder ();

			parentsGos.ForEach (parentsGo=>
				{
					pathStringBuilder.Append($"{parentsGo.name}/");
				});
			pathStringBuilder.Append (this.entity.name);

			path = pathStringBuilder.ToString ();
		}

		public void ProcessComponent()
		{
			Component[] subComponents= this.entity.GetComponents<Component>();

			componentDescriptions = new List<ComponentDescription> ();

			Array.ForEach<Component>(subComponents,(subComponent)=>
				{
					ComponentDescription componentDescription = new ComponentDescription(subComponent);

					componentDescriptions.Add(componentDescription);
				});
		}
	}
}