using System.Collections.Generic;
using UnityEngine;

public class ColumnNamedParamScr : MonoBehaviour
{
    public TMPro.TextMeshProUGUI head;
    public GameObject lineNamedString;

    private float step = 20;
    private float heightHeader = 30;
    private List<string> fields = new List<string>();
    private List<string> values = new List<string>();

    public void SetInfo(string headText, List<string> fields, List<string> values)
	{
        if (fields.Count != values.Count) throw new System.Exception();
        head.text = headText;
        this.fields = fields;
        this.values = values;
        CreateLines();
	}

    private void CreateLines()
	{
		for (int i = 0; i < fields.Count; i++)
		{
            var go = Instantiate(
                lineNamedString, 
                gameObject.GetComponent<RectTransform>(), 
                false);

            go.GetComponent<LineNamedParamScr>().SetStrings(fields[i], values[i]);
            var rt = go.GetComponent<RectTransform>();
            rt.pivot = new Vector2(0, 0);
            var prt = gameObject.GetComponent<RectTransform>();
            rt.localPosition = new Vector2(0, step * i - fields.Count * step - heightHeader);
		}
	}
}
