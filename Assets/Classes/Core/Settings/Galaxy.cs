﻿namespace Settings
{
	public struct Galaxy
	{
		//количество звезд в галлактике
		public readonly static int STARS_AMMOUNT = 1200;
		//количество созвездий
		public readonly static int CONSTELLATION_AMMOUNT = 70;
		//радиус галактики для генерации ху координат звезд
		public readonly static int GALAXY_RADIUS = 500;
		//минимальное расстояние между звездами
		public readonly static float MIN_STAR_INTERVAL = 8f;
		//позиция слоя звезд на карте галактики
		public readonly static int GALAXY_STAR_LAYER = -20;
		//плотность галактического рукава 1-5
		public readonly static int DENSITY_ARMS = 3;
		//Ширина рукава
		public readonly static float WIDTH_ARMS = 100f;
		//Пустое пространство вокруг центральной черной дырыб коэфициент
		public readonly static float CENTRAL_BLACK_HOLE_INTERVAL_K = 3.5f;
	}
}
