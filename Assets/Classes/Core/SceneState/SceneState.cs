public abstract class SceneState
{
	public abstract void DrawScene();

	public virtual void Tick() { }

	public virtual void Update() { }
}