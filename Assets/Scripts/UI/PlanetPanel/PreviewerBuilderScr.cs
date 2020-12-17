using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewerBuilderScr : MonoBehaviour
{
    private List<ResourcePoint> resources = new List<ResourcePoint>();
    private List<string> values = new List<string>();

    public void BuildPreview(Planet planet)
	{
		int count = 0;
		for (int i = 0; i < planet.resources.Length; i++)
		{
			if (!resources.Exists(x => x.resource.idResource == planet.resources[i].resource.idResource))
			{
				resources.Add(planet.resources[i]);
				values.Add(planet.resources[i].resource.extraction.ToString());
				count++;
				continue;
			}
			int index = resources.FindIndex(x => x.resource.idResource == planet.resources[i].resource.idResource);
			values[index] += $", {planet.resources[i].resource.extraction}";
		}
		Utilities.ShowMeObject(resources);
		Utilities.ShowMeObject(values);
	}
}
