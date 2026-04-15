namespace CardGame;

public Draw : Effect
{
	public Number Amount;
	public TargetPlayer Target;

	public Draw(XElement number, XElement player)
	{
		Amount = Number.FromFile(number);
		/*
		 * under construction
		Target = player.Name;
		*/
	}
	override void Affect(CardScene scene)
	{
		if (Target == "Player1")
		{
			scene.Player1.Draw(Amount.Resolve(scene));
		} else if (Target == "Player2")
		{
			scene.Player2.Draw(Amount.Resolve(scene));
		}
	}
	override bool Ready(CardScene scene)
	{
		a
	}
}
