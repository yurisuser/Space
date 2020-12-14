using System.Diagnostics;
using UnityEngine;
using static Settings.Time;
public class Turner 
{
	private static Turner turner;
	private float elapsedTime = 0f;
	private static int _currentTime = 1;
	private static Stopwatch aiDelay = new Stopwatch();

	public event _delegate TimeTrigger;
	public int allowedTimeSteps = 0;
	public static float turn_length = MIN_TURN_LENGTH;
	public delegate void _delegate();
	
	private Turner() { }

	public static Turner getInstance()
	{
		if (turner != null)
		{
			return turner;
		}
		return turner = new Turner();
	}
	public static int GetCurrentTime()
	{
		 return _currentTime;
	}

	public void Update()
	{
		InnerClock();
	}

	private void InnerClock()
	{
		if (allowedTimeSteps == 0) return;
		elapsedTime += Time.deltaTime;
		if (!TaskManager.isAllFinished) return;
		
		aiDelay.Stop();

		if(elapsedTime > turn_length)
		{
			turn_length = CalculateTurnLength();
			elapsedTime = 0f;
			allowedTimeSteps--;
			_currentTime++;
			TimeTrigger();
			aiDelay.Restart();
			return;
		}
	}

	private float CalculateTurnLength()
	{
		if (elapsedTime * TIME_BUFFER < MIN_TURN_LENGTH) 
			return MIN_TURN_LENGTH;
		float aiDelaySec = aiDelay.ElapsedMilliseconds / 1000f;
		if (aiDelaySec < MIN_TURN_LENGTH) return MIN_TURN_LENGTH;
		return aiDelaySec * TIME_BUFFER;
	}
	public void SetCurrentTime(int newTime)
	{
		_currentTime = newTime;
	}

	public void GoStep()
	{
		allowedTimeSteps = (allowedTimeSteps < 0) ? 0 : allowedTimeSteps;
		allowedTimeSteps++;
	}

	public void GoStream()
	{
		allowedTimeSteps = -1;
	}

	public void Stop()
	{
		allowedTimeSteps = 0;
	}
}
