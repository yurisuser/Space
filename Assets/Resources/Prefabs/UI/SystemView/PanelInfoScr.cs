using System.Collections.Generic;
using UnityEngine;

public class PanelInfoScr : MonoBehaviour
{
	public GameObject columnNamedParam;
	public TMPro.TextMeshProUGUI ssoName;
	public GameObject frame;
	
	private SubStarBody subStarBody;
	private RectTransform parent;
	private List<ParamFrame> paramFrames;

	public void SetSubStarBody(SubStarBody subStarBody)
	{
		this.subStarBody = subStarBody;
		ssoName.text = subStarBody.name;
		parent = frame.GetComponent<RectTransform>();
		Debug.Log(parent);
		CalcParams();
		Draw();
	}

	private void CalcParams()
	{
		paramFrames = new List<ParamFrame>();
		paramFrames.Add(Astronomy());
		paramFrames.Add(Resources());
		paramFrames.Add(Storage());

		if (subStarBody.controlCentre == null) return;
	}

	private ParamFrame Astronomy()
	{
		string header = "Astronomical";
		List<string> fields = new List<string>();
		List<string> values = new List<string>();

		values.Add(subStarBody.mass.ToString());
		fields.Add("mass :");
		values.Add(subStarBody.orbitSpeed.ToString());
		fields.Add("orbital speed :");
		values.Add(subStarBody.rotateSpeed.ToString());
		fields.Add("turn length :");
		values.Add(subStarBody.subStarType.ToString());
		fields.Add("type :");
		values.Add(Data.planetsArr[subStarBody.type].name);
		fields.Add("type body :");

		return new ParamFrame(header, fields, values);
	}

	private ParamFrame Resources()
	{
		string head = "Resources deposits";
		var rd = subStarBody.resourcer.resourceDeposits;
		List<string> field = new List<string>();
		List<string> value = new List<string>();

		if (subStarBody.resourcer.resourceDeposits == null || subStarBody.resourcer.resourceDeposits.Length == 0)
		{
			field.Add("no");
			value.Add("resources");
			return new ParamFrame(head, field, value);
		}

		List<int> idRes = new List<int>();
		List<List<int>> depo = new List<List<int>>();

		for (int i = 0; i < rd.Length; i++)
		{
			var index = idRes.FindIndex(x => x == rd[i].idResource);

			if (index > -1)
			{
				depo[index].Add(rd[i].extraction);
				continue;
			}

			idRes.Add(rd[i].idResource);
			List<int> includeList = new List<int>();
			includeList.Add(rd[i].extraction);
			depo.Add(includeList);
		}

		for (int i = 0; i < idRes.Count; i++)
		{
			field.Add(Data.GetGoodsById(idRes[i]).name);
			string val = "";
			for (int q = 0; q < depo[i].Count; q++)
			{
				val += $" [{depo[i][q]}]";
			}
			value.Add(val);
		}

		return new ParamFrame(head, field, value);
	}

	private ParamFrame Storage ()
	{

		string head = "Storage";

		List<string> field = new List<string>();
		List<string> value = new List<string>();

		if (subStarBody.controlCentre.storage == null || subStarBody.controlCentre.storage.goodsArr.Length == 0)
		{
			field.Add("no");
			value.Add("storage");
			return new ParamFrame(head, field, value);
		}
		var goods = subStarBody.controlCentre.storage.goodsArr;
		for (int i = 0; i < goods.Length; i++)
		{
			field.Add(Data.GetGoodsById(goods[i].id).name);
			value.Add(goods[i].amount.ToString());
		}		
		return new ParamFrame(head, field, value);
	}

	private Vector2 MagicCoordinate(Vector2 vec)
	{
		//смещение относительно центра канваса
		vec.x -= parent.rect.width * .5f;
		vec.y += parent.rect.height * .5f;
		return vec;
	}

	private void Draw()
	{
		for (int i = 0; i < paramFrames.Count; i++)
		{
			var existObj = GameObject.Find(paramFrames[i].head);
			if (existObj != null) Destroy(existObj);

			GameObject go = Instantiate(
				columnNamedParam,
				frame.transform,
				false
				);

			go.name = paramFrames[i].head;
			go.transform.SetParent(frame.GetComponent<RectTransform>());
			go.transform.localScale = new Vector3(1, 1, 1);

			var rt = go.GetComponent<RectTransform>();
			rt.anchorMin = new Vector2(0, 1);
			rt.anchorMax = new Vector2(0, 1);
			rt.pivot = new Vector2(0, 1);
			rt.localPosition = MagicCoordinate(new Vector2(rt.rect.width * i, 0));

			go.GetComponent<ColumnNamedParamScr>().SetInfo(paramFrames[i].head, paramFrames[i].fields, paramFrames[i].values);
		}
	}

	private struct ParamFrame
	{
		public string head;
		public List<string> fields;
		public List<string> values;

		public ParamFrame(string head, List<string> fields, List<string> values)
		{
			this.head = head;
			this.fields = fields;
			this.values = values;
		}
	}
}
