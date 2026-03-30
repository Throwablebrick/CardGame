namespace CardGame;

public Draw : Effect
{
	public Number Amount;
	public string Target;

	public Draw(XElement number, XElement player)
	{
		Amount = Number.FromFile(number);
		Target = player.Name;
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
}
