using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class INumber 
{
	public int Value;
	public virtual bool Resolve(CardScene scene)
	{
	}
	public static INumber FromFile(XElement element)
	{
	}
}
