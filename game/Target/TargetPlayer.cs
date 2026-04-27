namespace CardGame;

public class TargetPlayer : Target
{
	public string Value;

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
}
