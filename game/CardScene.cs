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
using MonoGameLibrary.UI;

namespace CardGame;

public enum Phase
{
	Upkeep,
	ManaGen,
	Main1,
	CombatAttack,
	CombatBlock,
	CombatDamage,
	Main2,
	EndStep
}

public class CardScene : Scene
{
	public bool CardChoose=false;
	public Card GiveCard;
	public bool PermanentChoose=false;
	public Permanent GivePermanent;
	public bool PlayerChoose=false;
	public string GivePlayer;
	public bool ZoneChoose=false;
	public string GiveZone;

	private Random rand;

	public Player Player1;
	public Player Player2;
	public Player CurrentPlayer;

	public Stack<Effect> stack;

	public Phase CurrentPhase;
	private bool changePhase;

	private Button PlayCardButton;
	private Button NextPhaseButton;
	
	public override void Initialize()
	{
		rand = new Random();
		CurrentPhase = Phase.ManaGen;
		changePhase=true;
		base.Initialize();
	}

	public override void LoadContent()
	{
		Player1 = new Player(Content, "decks/AllInsight.xml", "Player1");
		Player2 = new Player(Content, "decks/AllInsight.xml", "Player2");
		CurrentPlayer = Player1;
		
		Player1.Draw(7);
		Player2.Draw(7);

		stack = new Stack<Effect>();

		PlayCardButton = new Button(Content, "sprites/buttons.xml", "Play", 1050, 430);
		NextPhaseButton = new Button(Content, "sprites/buttons.xml", "Pass", 1160, 430);
	}

	public override void Update(GameTime gameTime)
	{
		switch (CurrentPhase)
		{
		case Phase.Upkeep:
			CurrentPlayer.Draw(1);
			NextPhase(true);
			break;
		case Phase.ManaGen:
			CurrentPlayer.Mana = CurrentPlayer.MaxMana++;
			NextPhase(false);
			break;
		case Phase.Main1:
			NextPhase(true);
			break;
		case Phase.CombatAttack:
			NextPhase(true);
			break;
		case Phase.CombatBlock:
			NextPhase(true);
			break;
		case Phase.CombatDamage:
			NextPhase(false);
			break;
		case Phase.Main2:
			NextPhase(true);
			break;
		case Phase.EndStep:
			CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
			CurrentPhase = changePhase ? Phase.Upkeep : CurrentPhase;
			break;
		}
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
			if (CurrentPhase == Phase.Main1 || CurrentPhase == Phase.Main2)
			{
				NextPhaseButton.Visible = true;
				if (CurrentPlayer.SelectedIndexHand != -1)
				{
					if (PlayCardButton.IsWithin(Core.Input.Mouse.Position))
					{
						if (CurrentPlayer.Mana >= CurrentPlayer.Hand[CurrentPlayer.SelectedIndexHand].CardCost[1])
						{
							stack.Push(CurrentPlayer.Hand[CurrentPlayer.SelectedIndexHand].OnPlay);
							CurrentPlayer.Hand.RemoveAt(CurrentPlayer.SelectedIndexHand);
						}else
						{
							Console.WriteLine("not enough resources");
						}
					}
				}
				if (NextPhaseButton.IsWithin(Core.Input.Mouse.Position))
				{
					changePhase = true;
				}
			}else
			{
				NextPhaseButton.Visible = false;
			}
			PlayCardButton.Visible = CurrentPlayer.Select(Core.Input.Mouse.Position);
		}
		if (stack.Count != 0)
		{
			changePhase = false;
			if (stack.Peek().Ready(this))
			{
				stack.Peek().Affect(this);
				stack.Pop();
			}
		}
		if (Core.Input.Keyboard.WasKeyJustPressed(Keys.E))
		{
			changePhase = true;
		}
		CurrentPlayer.UpdateHand();
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(Color.CornflowerBlue);

		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		CurrentPlayer.Display(Core.SpriteBatch);
		PlayCardButton.Draw(Core.SpriteBatch);
		NextPhaseButton.Draw(Core.SpriteBatch);
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

	public void NextPhase(bool stepThrough)
	{
		if (changePhase)
		{
			CurrentPhase = (Phase)((int)CurrentPhase + 1);
			Console.WriteLine(CurrentPhase);
			changePhase = stepThrough;
		}
	}
}
