using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckMissingTool.Demo
{
	public class BlackBoard : MonoBehaviour {
		
		[SerializeField]
		Material material;

		[SerializeField]
		List<Material> materials = new List<Material>();

		[SerializeField]
		MaterialTable materialTable;
	}

	[Serializable]
	class MaterialTable
	{
		[SerializeField]
		Material material;
	}
}
