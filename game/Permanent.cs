namespace CardGame;

public class Permanent : Card
{
	public static int MaxID = 0;
	public int ID;
	public OnCondition[] Trigers;

	public Permanent(Card card, XElement root) : base(card)
	{
		ID = MaxID;
		MaxID++;
	}
}
