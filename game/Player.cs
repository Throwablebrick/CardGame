using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using MonoGameLibrary.Graphics;


namespace CardGame;

public class Player
{
	public Player(ContentManager content, string deckPath, string name)
	{
		Energy = 20;
		LoadDeckFromFile(content, deckPath);
		Name = name;
	}
	public Player(ContentManager content, string deckPath, int evergy, string name)
	{
		Energy = evergy;
		LoadDeckFromFile(content, deckPath);
		Name = name;
	}

	public string Name;

	public int Mana;
	public int Energy;

	public List<Card> Deck;
	public List<Card> Hand;

	public List<Permanent> NuetralZone;
	public List<Permanent> AttackZone;
	public List<Permanent> DefenseZone;

	// add default sizes for these
	public Rectangle NuetralRect;
	public Rectangle AttackRect;
	public Rectangle DefenseRect;

	public void Draw(int amount)
	{
		for (int i = 0; i<amount; i++)
		{
			Hand.Add(Deck[i]);
			Deck.RemoveAt(i);
			//tell cardScene you drew a card
		}
	}
	private void LoadDeckFromFile(ContentManager content, string fileName)
	{
		string filePath = Path.Combine(content.RootDirectory, fileName);

		using (Stream stream = TitleContainer.OpenStream(filePath))
		{
			using (XmlReader reader = XmlReader.Create(stream))
			{
				XDocument doc = XDocument.Load(reader);
				XElement root = doc.Root;

				var cards = root.Element("Cards")?.Elements("Card");

				if (cards != null)
				{
					foreach (var card in cards)
					{
						Deck.Add(card.Value);
					}
				}
			}
		}
	}
}
