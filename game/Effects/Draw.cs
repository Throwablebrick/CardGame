namespace CardGame;

public Draw : Effect
{
	public Number Amount;
	public TargetPlayer Target;

	public Draw(XElement number, XElement player)
	{
		Amount = Number.FromFile(number);
		Target = TargetPlayer.FromFile(player);
	}
	override void Affect(CardScene scene)
	{
		Target.Resolve(scene).Draw(Amount.Resolve(scene));
	}
}
