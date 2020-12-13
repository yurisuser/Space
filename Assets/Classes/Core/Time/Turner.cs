using UnityEngine;

public class Turner 
{
	private static Turner turner;
	private float elapsedTime = 0f;
	public int allowedTimeSteps = 0;
	private static int _currentTime = 1;
	private Turner() { }
	public event _delegate TimeTrigger;
	public delegate void _delegate();
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

	public void Tick()
	{
		InnerClock();
	}

	private void InnerClock()
	{
		if (allowedTimeSteps == 0) return;
		if(elapsedTime > Settings.Time.TURN_LENGTH && TaskManager.isAllFinished)
		{
			elapsedTime = 0f;
			allowedTimeSteps--;
			_currentTime++;
			TimeTrigger();
			return;
		}
		elapsedTime += Time.deltaTime;
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
