using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class Effect
{
	public virtual void Affect(CardScene scene)
	{
	}
	public virtual bool Ready(CardScene scene)
	{
	}

	public static Effect FromFile(XElement root)
	{
		if (root.Name == "example")
		{
			return Draw(root.Element("parameter 1"), root.Element("parameter 2"));
		}
	}
}
