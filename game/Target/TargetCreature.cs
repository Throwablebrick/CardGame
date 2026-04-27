namespace CardGame;

public class TargetCreature : Target
{
	public TargetZone Zone;
	public Permanent Value;

	public override bool Resolve(CardScene scene)
	{
		if (Found)
		{
			return true;
		}
		if (Zone.Resolve(scene))
		{
			scene.PermanentChoose = true;
			if (scene.GivePermanent.CardName != "null")
			{
				Value = scene.GivePermanent;
				if (Value.CardType != "Creature")
				{
					return false;
				}
				if (scene.IsWithin(Zone.Value, Zone.Player.Value, Value.ID))
				{
					Found = true;
					scene.PermanentChoose = false;
					scene.GivePermanent = Permanent.Null;
					return true;
				}
			}
		}

		return false;
	}
}
