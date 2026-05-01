using System;
using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class Draw : Effect
{
	public INumber Amount;
	public TargetPlayer Target;

	public Draw(XElement root)
	{
		Amount = INumber.FromFile(root.Element("INumber"));
		Target = TargetPlayer.FromFile(root.Element("TargetPlayer"));
	}
	public override void Affect(CardScene scene)
	{
		if (Target.Value == "Player1")
		{
			scene.Player1.Draw(Amount.Value);
		} else if (Target.Value == "Player2")
		{
			scene.Player2.Draw(Amount.Value);
		}
		//Console.WriteLine($"{Target.Value} drew {Amount.Value} cards");
	}
	public override bool Ready(CardScene scene)
	{
		return Amount.Resolve(scene) && Target.Resolve(scene);
	}
}
