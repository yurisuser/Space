using static Data;
public class IndustryConstruction
{
	public Recipe recipe;
	public float stageProcess;
	public EProducingState state;
	public ResourceDeposit resourceDeposit;

	//TODO resourceDeposit доделать
	public void SetRecipe(Data.Recipe recipe)
	{
		//TODO доделать
		if (recipe == null)
		{
			this.recipe = recipe;
			stageProcess = 0;
			state = EProducingState.empty;
		}
	}
}
