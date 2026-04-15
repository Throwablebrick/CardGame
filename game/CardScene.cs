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
	public bool NeedInput;
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

		LoadDeckFromFile(Content, "decks/base.xml", _deck);
		
		//this will be replaced with a method called DrawCard() that grabs and removes a card from the deck
		for (int i = 0; i<7; i++)
		{
			_hand.Add(DrawCard());
		}
	}

	public override void Update(GameTime gameTime)
	{
		if (Core.Input.Mouse.WasButtonJustPressed(MouseButton.Left))
		{
			if (NeedInput)
			{
				//under construction
			}
		}
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(Color.CornflowerBlue);

		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		for (int i = 0; i<_hand.Count; i++)
		{
			_hand[i].Draw(Core.SpriteBatch, new Vector2(640.0f - (_hand.Count * _hand[i].Width/2) + (_hand[0].Width * i), 720.0f - _hand[0].Height));
		}
		Core.SpriteBatch.End();

		base.Draw(gameTime);
	}

	public Card ClickCard()
	{
		//under construction
	}
}
