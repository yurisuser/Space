using System.Collections.Generic;
using UnityEngine;

public class PreviewerBuilderScr : MonoBehaviour
{
	private List<ResourcePoint> resources;
	private List<List<int>> values;

	public void CreatePreview(Planet planet, Transform parent)
	{
		Calculate(planet);
		Draw(parent.Find("res"));
	}

	public int GetCountResources()
	{
		return resources.Count;
	}

	private void Calculate(Planet planet)
	{
		resources = new List<ResourcePoint>();
		values = new List<List<int>>();
		List<int> temp = new List<int>();
		for (int i = 0; i < planet.resources.Length; i++)
		{
			if (!resources.Exists(x => x.resource.idResource == planet.resources[i].resource.idResource))
			{
				resources.Add(planet.resources[i]);
				values.Add(new List<int> { planet.resources[i].resource.extraction });
				continue;
			}
			int index = resources.FindIndex(x => x.resource.idResource == planet.resources[i].resource.idResource);
			values[index].Add(planet.resources[i].resource.extraction);
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
			resourcePreview.GetComponent<PreviewPlanetResourceScr>().SetParam(resources[i].resource.idResource, values[i].ToArray());
		}
	}
}
