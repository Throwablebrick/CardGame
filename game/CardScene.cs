using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

namespace CardGame;

public class CardScene : Scene
{
	private List<CardData> Hand;
	private Sprite Zone;
	//some deck variable
	
	public override void Initialize()
	{
		Hand = new List<CardData>();
		base.Initialize();
	}

	public override void LoadContent()
	{
		//for reference this is what you do if you haven't made an xml file to make a textureatlas from that file
		TextureAtlas atlas = new TextureAtlas(Content.Load<Texture2D>("sprites/card_zone_temp"));
		atlas.AddRegion("your mother", 0, 0, 175, 117);

		Zone = atlas.CreateSprite("your mother");
		
		//this will be replaced with a method called DrawCard() that grabs and removes a card from the deck
		for (int i = 0; i<7; i++)
		{
			Hand.Add(CardData.FromFile(Content, "cards/base.xml"));
		}
	}

	public override void Update(GameTime gameTime)
	{
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(Color.CornflowerBlue);

		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		for (int i = 0; i<Hand.Count; i++)
		{
			Hand[i].Draw(Core.SpriteBatch, new Vector2(640.0f - (Hand.Count * Hand[0].Width/2) + (Hand[0].Width * i),0.0f));//finish writing this to get card tilt
		}
		Core.SpriteBatch.End();

		base.Draw(gameTime);
	}
}
