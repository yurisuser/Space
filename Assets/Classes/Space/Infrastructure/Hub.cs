using System.Collections.Generic;
using UnityEngine;

public class Hub
{
	public SubStarBody body;

	public Dock dock;
	public Industry industry;
	public Storage storage;
	public Market market;

	public Hub(SubStarBody body)
	{
		if (body == null) throw new System.Exception();
		this.body = body;
		this.storage = AddStorage();
		this.industry = AddIndustry();
		this.market = AddMarket();
		this.dock = AddDock();
	}

	public bool IsDockingEnable(Ship ship)
	{
		return true;
	}

	#region private
	private Dock AddDock()
	{
		if (this.dock != null) return this.dock;
		return new Dock(body);
	}

	private Market AddMarket()
	{
		if (this.market != null) return this.market;
		return MarketCreator.CreateMarket(body);
	}

	private Industry AddIndustry()
	{
		if (this.industry != null) return this.industry;
		return IndustryCreator.TestCreateFullIndustry(body);
	}

	private Storage AddStorage()
	{
		if (this.storage != null) return this.storage;
		return StorageCreator.CreateStorage(null, body);
	}
	#endregion
}
