using System.Diagnostics;
using System.Threading.Tasks;

public static class TaskManager
{
	public static bool isWorkThread = false;
	public static float elapsedTimeWork = 0;

	private static Task task;
	private static Stopwatch watch = new Stopwatch();


	public static bool isAllFinished
	{
		get
		{
			if (task == null) return true;
			if (task.Status == TaskStatus.RanToCompletion) return true;
			return false;
		}
	}

	public static async void Tick()
	{
		Utilities.ShowMe(2, "work");
		watch.Restart();
		isWorkThread = true;
		
		await Task.Run(AI.AIManager.Tick);

		isWorkThread = false;
		watch.Stop();
		elapsedTimeWork = watch.ElapsedMilliseconds;
		Utilities.ShowMe(2, "finish");
		Utilities.ShowMe(1, $"Task Work: {elapsedTimeWork} ms.");
	}
}
