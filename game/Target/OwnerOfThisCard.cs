using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class OwnerOfThisCard : TargetPlayer
{
	public OwnerOfThisCard() : base()
	{
	}
	public override bool Resolve(CardScene scene)
	{
		Value = scene.CurrentPlayer.Name;
		return true;
	}
}
