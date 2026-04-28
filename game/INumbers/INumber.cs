using System;
using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public abstract class INumber 
{
	public int Value;
	public abstract bool Resolve(CardScene scene);
	public static INumber FromFile(XElement element)
	{
		var elms = element.Elements();
		foreach (var el in elms)
		{
			if (el.Name == "INumberLiteral")
			{
				return new INumberLiteral(Convert.ToInt32(el.Value));
			}
		}
		return One;
	}
	public static INumber One = new INumberLiteral(1);
}
