namespace CardGame;

public abstract class Target
{
	public bool Found;

	public abstract bool Resolve(CardScene scene);
}
