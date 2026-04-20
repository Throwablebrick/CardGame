namespace CardGame;

public class TargetCreature : Target
{
	public TargetZone Zone;

	public override bool Resolve(CardScene scene)
	{
		if (Found)
		{
			return true;
		}
		if (Zone.Resolve(scene))
		{
		}

		return false;
	}
}
