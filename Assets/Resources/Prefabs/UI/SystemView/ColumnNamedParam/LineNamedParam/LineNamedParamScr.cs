using UnityEngine;

public class LineNamedParamScr : MonoBehaviour
{
	public TMPro.TextMeshProUGUI field;
	public TMPro.TextMeshProUGUI value;

    public void SetStrings(string field, string value)
	{
		this.field.text = field;
		this.value.text = value;
	}
}
