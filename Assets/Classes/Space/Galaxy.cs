public static class Galaxy
{
	private static int _lastId;

	public static StarSystem[] StarSystemsArr;
	public static StarDistance[][] Distances; //[from id system][to id system]
	public static StarDistance[][] DistancesSortedNear; //[from id system][id near system], [][0] - self

	public static int GetNextId()
	{
		return _lastId++;
	}

}