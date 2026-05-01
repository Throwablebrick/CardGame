using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class INumberLiteral : INumber 
{
	public override bool Resolve(CardScene scene)
	{
		return true;
	}
	public INumberLiteral(int val)
	{
		Value = val;
	}
}
