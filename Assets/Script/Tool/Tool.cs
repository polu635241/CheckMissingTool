using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckMissingTool.Tool
{
	public static class Tool {

		/// <summary>
		/// 檢查傳入值是否為某root之子物件
		/// </summary>
		/// <returns><c>true</c>, if is child was checked, <c>false</c> otherwise.</returns>
		/// <param name="root">Root.</param>
		/// <param name="target">Target.</param>
		public static bool CheckIsChild(Transform root, Transform target)
		{
			var childs = root.GetComponentsInChildren<Transform> ();

			int id = new List<Transform> (childs).IndexOf(target);

			// -1是找不到 0是root層 不是子物件
			if (id != -1 && id != 0) 
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static string MixStringColor(System.Object input, Color inputColor)
		{
			string left = ColorUtility.ToHtmlStringRGB (inputColor).ToLower();

			return string.Format ("<color=#{0}>{1}</color>", left, input.ToString ());
		}

		/// <summary>
		/// 傳入list 把相同值的疊在一起
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="inputs">Inputs.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static List<KeyValuePair<T,int>> GetCount<T>(List<T> inputs)
		{
			var result = new List<KeyValuePair<T,int>> ();

			var hestory = new Dictionary<T,int> ();

			inputs.ForEach ((input)=>
				{
					if(hestory.ContainsKey(input))
					{
						hestory[input]++;
					}
					else
					{
						hestory.Add(input,1);
					}

					var count = hestory[input];

					result.Add(new KeyValuePair<T, int>(input,count));
				});

			return result;
		}
	}
}
