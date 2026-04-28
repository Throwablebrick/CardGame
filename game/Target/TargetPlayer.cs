using System.Xml;
using System.Xml.Linq;

namespace CardGame;

public class TargetPlayer : Target
{
	public string Value;

	public TargetPlayer()
	{
		Found = false;
	}
	public override bool Resolve(CardScene scene)
	{
		if (Found)
		{
			return true;
		}
		scene.PlayerChoose = true;
		if (scene.PlayerChoose)
		{
			if (scene.GivePlayer != "null")
			{
				Value = scene.GivePlayer;
				Found = true;
				scene.PlayerChoose = false;
				scene.GivePlayer = "null";
				return true;
			}
		}
		return false;
	}
	public static TargetPlayer FromFile(XElement root)
	{
		var elms = root.Elements();
		foreach (var el in elms)
		{
			if (el.Name == "TargetPlayerLiteral")
			{
				return new TargetPlayerLiteral(el);
			}else if (el.Name == "OwnerOfThisCard")
			{
				return new OwnerOfThisCard();
			}
		}
		return new TargetPlayer();
	}
}
