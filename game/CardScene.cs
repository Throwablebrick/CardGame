using System.Collections;
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

	public override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		float tilt = -45.0f;
		for (int i = 0; i<Hand.Count; i++)
		{
			Hand[i].Rotation = tilt;
			tilt += 90/Hand.Count;
			Hand[i].Draw(Core.SpriteBatch, 640 + (i*5));//finish writing this to get card tilt
		}
		Core.SpriteBatch.End();

		base.Draw(gameTime);
	}
}
