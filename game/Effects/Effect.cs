using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public abstract class Effect
{
	public abstract void Affect(CardScene scene);
	public abstract bool Ready(CardScene scene);

	public static Effect FromFile(XElement root)
	{
		var elms = root.Elements();
		foreach (var el in elms)
		{
			if (el.Name == "Draw")
			{
				return new Draw(el);
			}
		}
		return null;
	}
}
