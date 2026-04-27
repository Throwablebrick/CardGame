using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class Permanent : Card
{
	public static int MaxID = 0;
	public int ID;
	public Effect[] Trigers;

	public Permanent(Card card, XElement root) : base(card)
	{
		ID = MaxID;
		MaxID++;
	}

	public Permanent(Card card) : base(card)
	{
	}

	public static Permanent Null = new Permanent(Card.Null);
}
