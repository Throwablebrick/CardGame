using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class Draw : Effect
{
	public INumber Amount;
	public TargetPlayer Target;

	public Draw(XElement number, XElement player)
	{
		Amount = INumber.FromFile(number);
		/*
		 * under construction
		Target = player.Name;
		*/
	}
	public override void Affect(CardScene scene)
	{
		if (Target == "Player1")
		{
			scene.Player1.Draw(Amount.Value);
		} else if (Target == "Player2")
		{
			scene.Player2.Draw(Amount.Value);
		}
	}
	public override bool Ready(CardScene scene)
	{
		return Amount.Resolve(scene) && Target.Resolve(scene);
	}
}
