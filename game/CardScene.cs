using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;
using MonoGameLibrary.Graphics;

namespace CardGame;

public class CardScene : Scene
{
	public bool CardChoose;
	public Card GiveCard;

	public bool PermanentChoose;
	public Permanent GivePermanent;

	public bool PlayerChoose;
	public string GivePlayer;

	public bool ZoneChoose;
	public string GiveZone;

	private Sprite _zone;
	private Random rand;

	public Player Player1;
	public Player Player2;

	public Stack<Effect> stack;
	
	public override void Initialize()
	{
		rand = new Random();
		base.Initialize();
	}

	public override void LoadContent()
	{
		//for reference this is what you do if you haven't made an xml file to make a textureatlas from that file
		TextureAtlas atlas = new TextureAtlas(Content.Load<Texture2D>("sprites/card_zone_temp"));
		atlas.AddRegion("your mother", 0, 0, 175, 117);
		_zone = atlas.CreateSprite("your mother");

		Player1 = new Player(Content, "decks/base.xml", "Player1");
		Player2 = new Player(Content, "decks/base.xml", "Player2");
		
		Player1.Draw(7);
		Player2.Draw(7);
	}

	public override void Update(GameTime gameTime)
	{
		if (Core.Input.Mouse.WasButtonJustPressed(MouseButton.Left))
		{
			if (PermanentChoose)
			{
				GivePermanent = ClickPermanent(Core.Input.Mouse.Position);
			}
			if (PlayerChoose)
			{
				GivePlayer = ChoosePlayer(Core.Input.Mouse.Position);
			}
			if (ZoneChoose)
			{
				GiveZone = ClickZone(Core.Input.Mouse.Position);
			}
		}
		if (stack.Count != 0)
		{
			if (stack.Peek().Ready(this))
			{
				stack.Peek().Affect(this);
				stack.Pop();
			}
		}
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(Color.CornflowerBlue);

		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		Core.SpriteBatch.End();

		base.Draw(gameTime);
	}

	public Permanent ClickPermanent(Point position)
	{
		string player = ChoosePlayer(position);
		return player == "Player1" ? Player1.WhichPermanent(position) : player == "Player2" ? Player2.WhichPermanent(position) : Permanent.Null;
	}
	public string ClickZone(Point position)
	{
		string player = ChoosePlayer(position);
		return player == "Player1" ? Player1.WhichZone(position) : player == "Player2" ? Player2.WhichZone(position) : "null";
	}
	public string ChoosePlayer(Point position)
	{
		return Player1.Board.Contains(position) ? "Player1" : Player2.Board.Contains(position) ? "Player2" : "null";
	}
	public bool IsWithin(string zone, string player, int id)
	{
		return player == "Player1" ? Player1.IsWithin(zone, id) : player == "Player2" ? Player2.IsWithin(zone, id) : false;
	}
}
