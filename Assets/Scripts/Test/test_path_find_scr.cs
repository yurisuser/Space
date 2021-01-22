using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class test_path_find_scr : MonoBehaviour
{
	private int[] way;
	public void Calc()
	{
		var from = System.Int32.Parse(transform.GetChild(0).GetComponent<InputField>().text);
		var to = System.Int32.Parse(transform.GetChild(1).GetComponent<InputField>().text);
		var range = System.Int32.Parse(transform.GetChild(2).GetComponent<InputField>().text);
		var isJump = transform.GetChild(3).GetComponent<Toggle>().isOn;
		var t = transform.GetChild(4).GetComponent<Text>();

		way = new int[] { 13, 24, 719, 1011 };
		Stopwatch s = new Stopwatch();
		s.Start();
		way = AI.AlgorithmA.GalaxyPathFinder(from, to, range, isJump);
		s.Stop();
		t.text = s.ElapsedMilliseconds.ToString();
		UnityEngine.Debug.Log($"WAYPOINTS {way.Length}");
		DrawLine();
	}

	private void CalcWay(int start, int finish, float range)
	{
		List<int> points = new List<int>();
		points.Add(start);
		for (int p = 0; p < points.Count; p++)
		{

		}
		for (int i = 0; i < Settings.Galaxy.STARS_AMMOUNT - 1; i++)
		{
			float distance = Galaxy.DistancesSortedNear[start][i].distance;
			if (distance > range) break;
		}
		
	}

	private void DrawLine()
	{
		var wayGO = GameObject.Find("way");
		if (wayGO != null) Destroy(wayGO);
		if (way.Length < 1)
		{
			UnityEngine.Debug.Log("NO WAY!");
			return;
		}
		GameObject folder = new GameObject { name = "way" };


		GameObject[] lines = new GameObject[way.Length - 1];
		for (int i = 0; i < lines.Length; i++)
		{
			lines[i] = new GameObject();
			lines[i].transform.position = Galaxy.StarSystemsArr[way[i]].position;
			lines[i].AddComponent<LineRenderer>();
			LineRenderer lr = lines[i].GetComponent<LineRenderer>();
			lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
			lr.startColor = lr.endColor = new Color(1f, 0, 0, .5f);
			lr.startWidth = lr.endWidth = 0.8f;
			lr.SetPosition(0, Galaxy.StarSystemsArr[way[i]].position);
			lr.SetPosition(1, Galaxy.StarSystemsArr[way[i + 1]].position);
			lines[i].transform.SetParent(folder.transform);
		}
	}
}
