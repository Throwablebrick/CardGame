using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

namespace CardGame;
public class Card
{

    //CardData fields
    private string _cardName;
    private string _cardType;
    private string _cardDescriptionText;
    private Sprite _cardSprite;
    private int[] _cardCost;

	public Rectangle Hitbox = new Rectangle(0,0,160,224); //add default values
	public int X=0;
	public int Y=0;
    
    //CardData properties
    public string CardName {get{return _cardName;} set {_cardName = value;}}
    public string CardType {get {return _cardType;} set {_cardType = value;}}
    public string CardDescriptionText {get {return _cardDescriptionText;} set {_cardDescriptionText = value;} /*set {_cardDescriptionText = value;} Might make it so that the value can be changed depending on how we want to implement card on card interaction UI*/}
    //public string CardDescriptionCode
    public int[] CardCost {get {return _cardCost;} set {_cardCost = value;}}

    public Sprite CardSprite {get {return _cardSprite;} set {_cardSprite = value;}}
	public float Width => _cardSprite.Width;
	public float Height => _cardSprite.Height;

	public Effect OnPlay;

    //CardData constructors
    public Card(string cardName, string cardType, string cardDescriptionText, Sprite cardSprite, int[] cardCost)
    {
        _cardName = cardName;
        _cardType = cardType;
        _cardDescriptionText = cardDescriptionText;
        _cardSprite = cardSprite;
        _cardCost = cardCost;
    }
    public Card(Card cardData)
    {
        _cardName = cardData.CardName;
        _cardType = cardData.CardType;
        _cardDescriptionText = cardData.CardDescriptionText;
        //_cardDescriptionCode = cardData.CardDescriptionCode;
        _cardSprite = cardData.CardSprite;
        _cardCost = cardData.CardCost;
    }
	public Card()
	{
		//this should only really be called by the FromFile method unless you want to manually set values yourself
		//like in the coresponding constructor for TextureAtlas which this approach is coppied from with the FromFile method
		_cardCost = new int[2]; //change to whatever the max number of kinds of costs there are, 2 for now.
	}
	public Card(string name, string type)
	{
		_cardName=name;
		_cardType=type;
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 position)
	{
		_cardSprite.Draw(spriteBatch, position);
	}

	public void Play(CardScene state)
	{
		//wait for counterspells
		//make this add trigered abilities to the trigger list
		state.stack.Push(OnPlay);
	}

	public static Card FromFile(ContentManager content, string fileName)
	{
		Card card = new Card();

		string filePath = Path.Combine(content.RootDirectory, fileName);

		using (Stream stream = TitleContainer.OpenStream(filePath))
		{
			using (XmlReader reader = XmlReader.Create(stream))
			{
				XDocument doc = XDocument.Load(reader);
				XElement root = doc.Root;

				string spritePath = root.Element("SpritePath").Value;
				string spriteName = root.Element("SpriteName").Value;

				float scaleX = float.Parse(root.Element("Scale").Attribute("x").Value ?? "4.0");
				float scaleY = float.Parse(root.Element("Scale").Attribute("y").Value ?? "4.0");

				//ToDo make sprite constructor work like this
				card.CardSprite = new Sprite(content, spritePath, spriteName);
				card.CardSprite.Scale = new Vector2(scaleX, scaleY);

				card.CardName = root.Element("Name").Value;
				card.CardType = root.Element("Type").Value;
				card.CardDescriptionText = root.Element("Text").Value;

				var costs = root.Element("Costs")?.Elements("Cost");

				if (costs != null)
				{
					int i = 0;
					foreach (var cost in costs)
					{
						card.CardCost[i] = int.Parse(cost.Attribute("value")?.Value ?? "0");
						//this could also have an attribute akin to color that could be set here
						i++;
					}
				}

				card.OnPlay = Effect.FromFile(root.Element("OnPlay"));

				if (card.CardType == "Emblem")
				{
					card = new Permanent(card, root);
				}else if (card.CardType == "Creature")
				{
					card = new Creature(card, root);
				}

				return card;
			}
		}
	}

	public static Card Null = new Card("null", "null");
}
