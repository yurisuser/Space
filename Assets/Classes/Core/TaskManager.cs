using System.Diagnostics;
using System.Threading.Tasks;

public static class TaskManager
{
	public static bool isWorkThread = false;
	public static float elapsedTimeWork = 0;

	private static Stopwatch watch = new Stopwatch();

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
