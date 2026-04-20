using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class Creature : Permanent
{
	public int MaxAttackPower;
	public int CurrentAttackPower;
	public int MaxLife;
	public int CurrentLife;

	public int MaxZoneChanges;
	public int ZoneChangesLeft;

	public int Zone; //int for now, change as needed

	public Creature(Card card, XElement root) : base(card)
	{
	}

	public Effect Attack;
	public Effect Block;
}
