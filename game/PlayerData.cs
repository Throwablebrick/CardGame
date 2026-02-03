using CardGame;

public class PlayerData
{
    //PlayerData fields
    private string _playerName;
    private int _playerMana;
    private int _playerMaxEnergy;
    private int _playerCurrentEnergy;
    private int _playerOverworldXCoordinate;
    private int _playerOverworldYCoordinate;
    private CardData[] _playerDeck; //will probably change this to a list
    private CardData[] _playerCardInventory; //will probably change this to a list
    private bool _playerInOverworld;

    //PlayerData properties
    public string PlayerName {get {return _playerName;}}
    public int PlayerMana {get {return _playerMana;} set{_playerMana = value;}}
    public int PlayerMaxEnergy {get {return _playerMaxEnergy;}}
    public int PlayerCurrentEnergy {get {return _playerCurrentEnergy;} set {_playerCurrentEnergy = value;}}
    public int PlayerOverworldXCoordinate {get {return _playerOverworldXCoordinate;} set {_playerOverworldXCoordinate = value;}}
    public int PlayerOverworldYCoordinate {get {return _playerOverworldYCoordinate;} set {_playerOverworldYCoordinate = value;}}
    public CardData[] PlayerDeck {get {return _playerDeck;} set {_playerDeck = value;}}
    public CardData[] PlayerCardInventory {get {return _playerCardInventory;} set {_playerCardInventory = value;}}

    //PlayerData Constructors
    public PlayerData(string playerName, int playerMaxEnergy)
    {
        _playerName = playerName;
        _playerMana = 0;
        _playerMaxEnergy = playerMaxEnergy;
        _playerCurrentEnergy = playerMaxEnergy;
        _playerOverworldXCoordinate = 0;
        _playerOverworldYCoordinate = 0;
        _playerInOverworld = false;
    }
    public PlayerData(string playerName)
    {
        _playerName = playerName;
        _playerMana = 0;
        _playerMaxEnergy = 20;
        _playerCurrentEnergy = 20;
        _playerOverworldXCoordinate = 0;
        _playerOverworldYCoordinate = 0;
        _playerInOverworld = false;
    }
}