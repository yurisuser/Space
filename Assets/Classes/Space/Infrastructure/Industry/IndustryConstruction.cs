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
		if (recipe == null)
		{
			state = EProducingState.empty;
		}
		this.recipe = recipe;
		stageProcess = 0;
		state = EProducingState.finished;
	}
}
