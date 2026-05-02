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
		Deck = new Queue<Card>();
		Hand = new List<Card>();
		NuetralZone = new List<Creature>();
		AttackZone = new List<Creature>();
		DefenseZone = new List<Creature>();
		selector = content.Load<Texture2D>("sprites/selector");
		Energy = 20;
		LoadDeckFromFile(content, deckPath);
		Name = name;
		MaxMana=1;
	}
	public Player(ContentManager content, string deckPath, int evergy, string name)
	{
		Energy = evergy;
		LoadDeckFromFile(content, deckPath);
		Name = name;
	}

	public string Name;

	public int Mana;
	public int MaxMana;
	public int Energy;

	public Queue<Card> Deck;
	public List<Card> Hand;
	public int SelectedIndexHand=-1;

	public List<Creature> NuetralZone;
	public List<Permanent> NuetralZoneEmblems;
	private int SelectedIndexActivated;
	public List<Creature> AttackZone;
	public List<Creature> DefenseZone;
	private int SelectedIndexCombat;

	// add default sizes for these
	public Rectangle NuetralRect = new Rectangle(426,257,428,234);
	public Rectangle AttackRect = new Rectangle(0,257,426,234);
	public Rectangle DefenseRect = new Rectangle(854,257,426,234);

	public Rectangle Board;
	private Texture2D selector;

	public void Draw(int amount)
	{
		for (int i = 0; i<amount; i++)
		{
			if (Deck.Count != 0)
			{
				Hand.Add(Deck.Peek());
				Deck.Dequeue();
				if (Hand.Count > 3)
				{
					SortCards(Hand, 0, Hand.Count-1);
				}
				//tell cardScene you drew a card
			}else
			{
				Console.WriteLine("no more cards");
			}
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
						Deck.Enqueue(Card.FromFile(content, card.Value));
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
		List<Creature> temp;
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
	public bool Select(Point position)
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
		return !notChanged;
	}
	public bool IsWithin(string zone, int id)
	{
		List<Creature> temp;
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

	public void SwapPlayers(bool first)
	{
		if (first)
		{
			AttackRect.Offset(-428,234);
			NuetralRect.Offset(0,234);
			DefenseRect.Offset(428,234);
		}else
		{
			AttackRect.Offset(428,-234);
			NuetralRect.Offset(0,-234);
			DefenseRect.Offset(-428,-234);
		}
	}
	private int _prevCardsInHand=0;
	public void UpdateHand()
	{
		for (int i=0; i<Hand.Count; i++)
		{
			if (_prevCardsInHand != Hand.Count)
			{
				Hand[i].Move((int)((1280-Hand[i].Width)/(Hand.Count-1)*i), 720 - (int)Hand[i].Height);
			}
			if (i!=0 && i-1!=SelectedIndexHand && Hand.Count < 3)
			{
				Hand[i-1].Hitbox = new Rectangle(Hand[i-1].Hitbox.X, 720 - Hand[i].Hitbox.Height, Hand[i].Hitbox.X-Hand[i-1].Hitbox.X, Hand[i-1].Hitbox.Height);
			}
		}
		//_prevCardsInHand = Hand.Count;
	}
	public void UpdateZones()
	{
		for (int i=0; i<NuetralZone.Count; i++)
		{
			NuetralZone[i].Move((int)((NuetralRect.Width-NuetralZone[i].Width)/(NuetralZone.Count-1)*i)+NuetralRect.X, NuetralRect.Y);
			if (i!=0)
			{
				//NuetralZone[i-1].Hitbox = new Rectangle(Hand[i-1].Hitbox.X, 720 - Hand[i].Hitbox.Height, Hand[i].Hitbox.X-Hand[i-1].Hitbox.X, Hand[i-1].Hitbox.Height);
			}
		}
	}
	public void DisplayHand(SpriteBatch batch)
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
			batch.Draw(selector, new Vector2((float)Hand[SelectedIndexHand].X,(float)Hand[SelectedIndexHand].Y), Color.White);
		}
	}
	public void DisplayZones(SpriteBatch batch)
	{
		for (int i=0; i<NuetralZone.Count; i++)
		{
			NuetralZone[i].Draw(batch);
		}
	}

	public void SortCards(List<Card> ards, int low, int high)
	{
		if (low<high)
		{
			int pivot = Partition(ards, low, high);
			SortCards(ards, low, pivot-1);
			SortCards(ards, pivot+1, high);
		}
	}
	private int Partition(List<Card> ards, int low, int high)
	{
		int pivot = ards[high].ManaCost;
		int i=low-1;

		for (int j=low; j<high; j++)
		{
			if (ards[j].ManaCost < pivot)
			{
				i++;
				swap(ards, i, j);
			}
		}

		swap(ards, i+1, high);
		return i+1;
	}
	private void swap(List<Card> ards, int first, int second)
	{
		Card temp = ards[first];
		ards[first] = ards[second];
		ards[second] = temp;
	}
}
