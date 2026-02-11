using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

namespace CardGame;

public class Game1 : Core
{
	private Sprite _card;

	public Game1() : base("Card Game", 1280, 720, false)
    {
    }

	protected override void Initialize()
	{
		base.Initialize();
	}

	protected override void LoadContent()
	{
		TextureAtlas atlas = TextureAtlas.FromFile(Content, "sprites/card.xml");

		_card = atlas.CreateSprite("0");
		_card.Scale = new Vector2(4.0f, 4.0f);
	}

	protected override void Update(GameTime gameTime)
	{
		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);

		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		_card.Draw(SpriteBatch, new Vector2(0.0f,0.0f));
		Core.SpriteBatch.End();

		base.Draw(gameTime);
	}
}
