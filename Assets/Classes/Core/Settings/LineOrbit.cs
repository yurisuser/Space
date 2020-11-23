using UnityEngine;

namespace Settings
{
	public struct LineOrbit
	{
		public readonly static int EMPTY_PLANET_ORBITS_COUNT = Settings.StarSystem.EMPTY_PLANET_ORBIT;
		public readonly static int ORBITS_PLANET_SIZE = Settings.StarSystem.RADIUS_PLANET_ORBIT;
		public readonly static int ORBIT_MOON_SIZE = Settings.StarSystem.RADIUS_MOON_ORBIT;
		public readonly static float WIDTH = 3f;
		public readonly static int CIRCLES_POINS = 120;
		public readonly static Color COLOR = new Color(0, 255, 255, .3f);
	}
}
