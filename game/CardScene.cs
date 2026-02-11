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
		TextureAtlas atlas = new TextureAtlas(Content.Load<Texture2D>("sprites/card_zone_temp"));
		atlas.AddRegion("your mother", 0, 0, 175, 117);

		Zone = atlas.CreateSprite("your mother");
		
		//this will be replaced with a method called Draw() that grabs and removes a card from the deck
		for (int i = 0; i<7; i++)
		{
			a
		}
	}
}
