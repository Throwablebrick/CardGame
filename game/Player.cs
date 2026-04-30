using System;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CardGame;

public class Player
{
	public Player(ContentManager content, string deckPath, string name)
	{
		Deck = new List<Card>();
		Hand = new List<Card>();
		NuetralZone = new List<Permanent>();
		AttackZone = new List<Permanent>();
		DefenseZone = new List<Permanent>();
		selector = content.Load<Texture2D>("sprites/selector");
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
	private int SelectedIndexHand=-1;

	public List<Permanent> NuetralZone;
	private int SelectedIndexActivated;
	public List<Permanent> AttackZone;
	public List<Permanent> DefenseZone;
	private int SelectedIndexCombat;

	// add default sizes for these
	public Rectangle NuetralRect;
	public Rectangle AttackRect;
	public Rectangle DefenseRect;

	public Rectangle Board;
	private Texture2D selector;

	public void Draw(int amount)
	{
		for (int i = 0; i<amount; i++)
		{
			Hand.Add(Deck[0]);
			Deck.RemoveAt(0);
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
						Deck.Add(Card.FromFile(content, card.Value));
					}
				}
			}
		}
	}

	public string WhichZone(Point position)
	{
		return NuetralRect.Contains(position) ? "Nuetral" : AttackRect.Contains(position) ? "Attack" : DefenseRect.Contains(position) ? "Defense" : "null";
	}
	public Permanent WhichPermanent(Point position)
	{
		string zone = WhichZone(position);
		List<Permanent> temp;
		temp = zone == "Nuetral" ? NuetralZone : zone == "Attack" ? AttackZone : DefenseZone;
		for (int i = 0; i<temp.Count; i++)
		{
			if (temp[i].Hitbox.Contains(position))
			{
				return temp[i];
			}
		}

		return Permanent.Null;
	}
	public void Select(Point position)
	{
		bool notChanged = true;
		for (int i=0; i<Hand.Count; i++)
		{
			if (Hand[i].Hitbox.Contains(position))
			{
				SelectedIndexHand = i;
				notChanged = false;
			}
		}
		SelectedIndexHand = notChanged ? -1 : SelectedIndexHand;
	}
	public bool IsWithin(string zone, int id)
	{
		List<Permanent> temp;
		temp = zone == "Nuetral" ? NuetralZone : zone == "Attack" ? AttackZone : DefenseZone;
		for (int i = 0; i<temp.Count; i++)
		{
			if (temp[i].ID == id)
			{
				return true;
			}
		}
		return false;
	}

	private int _prevCardsInHand=0;
	public void UpdateHand()
	{
		for (int i=0; i<Hand.Count; i++)
		{
			if (_prevCardsInHand != Hand.Count)
			{
				Hand[i].Move((1280-Hand[i].Hitbox.Width)/Hand.Count * (i), 720 - (int)Hand[i].Height);
			}
			if (i!=0 && Hand[i].Hitbox.Intersects(Hand[i-1].Hitbox))
			{
				Hand[i-1].Hitbox = new Rectangle(Hand[i-1].Hitbox.X, 720 - Hand[i].Hitbox.Height, Hand[i].Hitbox.X-Hand[i-1].Hitbox.X, Hand[i-1].Hitbox.Height);
			}
		}
		//_prevCardsInHand = Hand.Count;
	}
	public void Display(SpriteBatch batch)
	{
		bool displayLast = false;
		for (int i=0; i<Hand.Count; i++)
		{
			if (i == SelectedIndexHand)
			{
				displayLast = true;
			}else
			{
				Hand[i].Draw(batch);
			}
		}
		if (displayLast)
		{
			Hand[SelectedIndexHand].Draw(batch);
			batch.Draw(selector, Hand[SelectedIndexHand].Hitbox, Color.White);
		}
	}

	public Card Play()
	{
		return Card.Null;
	}
}
