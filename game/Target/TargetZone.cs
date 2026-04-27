namespace CardGame;

public class TargetZone : Target
{
	public TargetPlayer Player;
	public string Value;

	public override bool Resolve(CardScene scene)
	{
		if (Found)
		{
			return true;
		}
		if (Player.Resolve(scene))
		{
			scene.ZoneChoose = true;
			if (scene.GiveZone != "null")
			{
				Value = scene.GiveZone;
				Found = true;
				scene.ZoneChoose=false;
				scene.GiveZone = "null";
				return true;
			}
		}
		return false;
	}
}
