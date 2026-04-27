using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public abstract class INumber 
{
	public int Value;
	public abstract bool Resolve(CardScene scene);
	public static INumber FromFile(XElement element)
	{
	}
}
