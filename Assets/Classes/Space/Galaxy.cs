public static class Galaxy
{
	private static int _lastId;

	public static StarSystem[] StarSystemsArr;
	public static StarDistance[][] Distances;

	public static int GetNextId()
	{
		return _lastId++;
	}

}