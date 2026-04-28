using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class TargetPlayerLiteral : TargetPlayer
{
	public TargetPlayerLiteral(XElement root)
	{
		Value = root.Value;
	}
	public override bool Resolve(CardScene scene)
	{
		return true;
	}
}
