using System.Xml;
using System.Xml.Linq;

namespace CardGame;

class Effect
{
	virtual void Affect(CardScene scene)
	{
	}

	static Effect FromFile(XElement root)
	{
		if (root.Name == "example")
		{
			return example(root.Element("parameter 1"), root.Element("parameter 2"));
		}
	}
}
