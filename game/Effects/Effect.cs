using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public abstract class Effect
{
	public abstract void Affect(CardScene scene);
	public abstract bool Ready(CardScene scene);

	public static Effect FromFile(XElement root)
	{
		if (root.Name == "Draw")
		{
			return new Draw(root.Element("parameter 1"), root.Element("parameter 2"));
		}
		return null;
	}
}
