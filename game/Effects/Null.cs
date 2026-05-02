using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class NullEffect : Effect
{
	public NullEffect(XElement root)
	{
	}
	public override void Affect(CardScene scene)
	{
	}
	public override bool Ready(CardScene scene)
	{
		return true;
	}
}
