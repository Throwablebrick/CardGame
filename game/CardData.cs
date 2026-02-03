using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

namespace CardGame;
public class CardData
{

    //CardData fields
    private string _cardName;
    private string _cardType; //Might consider changing this to a integer with an enum for int to string translations, also might consider if we want to make this an array or list to allow for multiple types
    private string _cardDescriptionText;
    //private string _cardDescriptionCode; Thinking about making this a list of strings for the different hashs if we decide to go with that. For now ill leave this commented out until we figure it out.
    private int _cardPower;
    private int _cardToughness;
    private int _cardNumberOfZoneChanges;
    private Sprite _cardSprite;
    private int[] _cardCost; //might change to a list, but I think we want some sort of multi-container to allow for multiple different cost types which we can identify by index
    
    //CardData properties
    public string CardName {get{return _cardName;}}
    public string CardType {get {return _cardType;} set {_cardType = value;}}
    public string CardDescriptionText {get {return _cardDescriptionText;} /*set {_cardDescriptionText = value;} Might make it so that the value can be changed depending on how we want to implement card on card interaction UI*/}
    //public string CardDescriptionCode
    public int CardPower {get {return _cardPower;} set {_cardPower = value;}}
    public int CardToughness {get {return _cardToughness;} set {_cardToughness = value;}}
    public int CardNumberOfZoneChanges {get {return _cardNumberOfZoneChanges;} set {_cardNumberOfZoneChanges = value;}}
    public Sprite CardSprite {get {return _cardSprite;}}
    public int[] CardCost {get {return _cardCost;} set {_cardCost = value;}}

    //CardData constructors
    public CardData(string cardName, string cardType, string cardDescriptionText, /*string cardDescriptionCode,*/ int cardPower, int cardToughness, int cardNumberOfZoneChanges, Sprite cardSprite, int[] cardCost)
    {
        _cardName = cardName;
        _cardType = cardType;
        _cardDescriptionText = cardDescriptionText;
        //_cardDescriptionCode = cardDescriptionCode;
        _cardPower = cardPower;
        _cardToughness = cardToughness;
        _cardNumberOfZoneChanges = cardNumberOfZoneChanges;
        _cardSprite = cardSprite;
        _cardCost = cardCost;
    }
    public CardData(CardData cardData)
    {
        _cardName = cardData.CardName;
        _cardType = cardData.CardType;
        _cardDescriptionText = cardData.CardDescriptionText;
        //_cardDescriptionCode = cardData.CardDescriptionCode;
        _cardPower = cardData.CardPower;
        _cardToughness = cardData.CardToughness;
        _cardNumberOfZoneChanges = cardData.CardNumberOfZoneChanges;
        _cardSprite = cardData.CardSprite;
        _cardCost = cardData.CardCost;
    }

    //CardDate methods
    //Going to wait to implement methods until we finalize some card interaction rules and whatnot
}