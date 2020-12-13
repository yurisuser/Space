using System.Threading.Tasks;

public static class TaskManager
{
	private static Task task = new Task(AI.AIManager.Tick);

	public static bool isAllFinished
	{
		get
		{
			if (task.Status == TaskStatus.RanToCompletion) return true;
			return false;
		}
	}

	public static void Tick()
	{

		if (task.Status == TaskStatus.RanToCompletion)
		{
			task = Task.Run(AI.AIManager.Tick);
			return;
		}
		task.Start();
	}
}
