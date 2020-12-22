using System.Collections.Generic;
using UnityEngine;

public class PreviewerBuilderScr : MonoBehaviour
{
	private List<ResourceDeposit> resources;
	private List<List<int>> values;

	public void CreatePreview(SubStarBody planet, Transform parent)
	{
		Calculate(planet);
		Draw(parent.Find("res"));
	}

	public int GetCountResources()
	{
		return resources.Count;
	}

	private void Calculate(SubStarBody planet)
	{
		resources = new List<ResourceDeposit>();
		values = new List<List<int>>();
		List<int> temp = new List<int>();
		for (int i = 0; i < planet.resourceDepositsArr.Length; i++)
		{
			if (!resources.Exists(x => x.idResource == planet.resourceDepositsArr[i].idResource))
			{
				resources.Add(planet.resourceDepositsArr[i]);
				values.Add(new List<int> { planet.resourceDepositsArr[i].extraction });
				continue;
			}
			int index = resources.FindIndex(x => x.idResource == planet.resourceDepositsArr[i].idResource);
			values[index].Add(planet.resourceDepositsArr[i].extraction);
		}
		foreach (var item in values)
		{
			item.Sort((x, y) => y - x);
		}
	}

	private void Draw(Transform parent)
	{
		float margin = 4f;
		float heightPrefab = PrefabService.UI.PlanetResourcePreview.GetComponent<RectTransform>().rect.height + margin;
		for (int i = 0; i < resources.Count; i++)
		{
			GameObject resourcePreview = Instantiate(
				PrefabService.UI.PlanetResourcePreview,
				new Vector3(
					5,
					parent.GetComponent<RectTransform>().rect.height - heightPrefab * i - heightPrefab - margin - 20 ,
					0
					),
				Quaternion.identity,
				parent
				);
			resourcePreview.name += i.ToString();
			resourcePreview.GetComponent<PreviewPlanetResourceScr>().SetParam(resources[i].idResource, values[i].ToArray());
		}
	}
}
