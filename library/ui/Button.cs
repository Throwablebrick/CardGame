using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

namespace MonoGameLibrary.UI;

public class Button
{
	private Sprite image;
	private Rectangle hitbox;
	public bool Visible;

	public Button(ContentManager content, string path, string name, int x, int y)
	{
		image = new Sprite(content, path, name);
		hitbox = new Rectangle(x, y, (int)image.Width, (int)image.Height);
		Visible = true;
	}

	public void Move(int x, int y)
	{
		hitbox = new Rectangle(x,y, hitbox.Width, hitbox.Height);
	}
	public void Move(Point place)
	{
		Move(place.X, place.Y);
	}

	public bool IsWithin(Point place)
	{
		return Visible && hitbox.Contains(place);
	}

	public void Draw(SpriteBatch batch)
	{
		if (Visible)
		{
			image.Draw(batch, new Vector2((float)hitbox.X,(float)hitbox.Y));
		}
	}
}
