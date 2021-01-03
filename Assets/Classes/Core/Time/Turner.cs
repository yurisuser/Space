using System.Diagnostics;
using UnityEngine;
using static Settings.Time;
public static class Turner 
{
	private static float elapsedTime = 0f;
	private static int _currentTime = 1;
	private static Stopwatch aiDelay = new Stopwatch();

	public static event _delegate TimeTrigger;
	public static int allowedTimeSteps = 0;
	public static float turn_length = MIN_TURN_LENGTH;
	public delegate void _delegate();
	
	public static int GetCurrentTime()
	{
		 return _currentTime;
	}

	public static void Update()
	{
		InnerClock();
	}

	private static void InnerClock()
	{
		if (allowedTimeSteps == 0) return;
		elapsedTime += Time.deltaTime;
		if (TaskManager.isWorkThread) return;

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

	private static float CalculateTurnLength()
	{
		if (elapsedTime * TIME_BUFFER < MIN_TURN_LENGTH) 
			return MIN_TURN_LENGTH;
		float aiDelaySec = aiDelay.ElapsedMilliseconds / 1000f;
		if (aiDelaySec < MIN_TURN_LENGTH) return MIN_TURN_LENGTH;
		return aiDelaySec * TIME_BUFFER;
	}
	public static void SetCurrentTime(int newTime)
	{
		_currentTime = newTime;
	}

	public static void GoStep()
	{
		allowedTimeSteps = (allowedTimeSteps < 0) ? 0 : allowedTimeSteps;
		allowedTimeSteps++;
	}

	public static void GoStream()
	{
		allowedTimeSteps = -1;
	}

	public static void Stop()
	{
		allowedTimeSteps = 0;
	}
}
