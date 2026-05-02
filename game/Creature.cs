using System;
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

	public Creature(Card card, XElement root) : base(card, root)
	{
		var elms = root.Elements();
		foreach (var el in elms)
		{
			if (el.Name == "AttackPower")
			{
				MaxAttackPower = Int32.Parse(el.Value);
			}else if (el.Name == "Life")
			{
				MaxLife = Int32.Parse(el.Value);
			}else if (el.Name == "ZoneChanges")
			{
				MaxZoneChanges = Int32.Parse(el.Value);
			}
		}
	}

	public Effect Attack;
	public Effect Block;
}
