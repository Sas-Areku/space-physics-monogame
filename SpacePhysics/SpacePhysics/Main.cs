﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePhysics.Menu;
using SpacePhysics.Scenes;

namespace SpacePhysics;

public class Main : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private InputManager input;

    public Main()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = (int)GameState.screenSize.X;
        graphics.PreferredBackBufferHeight = (int)GameState.screenSize.Y;
        graphics.IsFullScreen = true;
        IsFixedTimeStep = false;
        graphics.SynchronizeWithVerticalRetrace = true;
        graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        GraphicsDevice.PresentationParameters.MultiSampleCount = 4;

        SceneManager.Initialize(Content);

        GameState.Initialize();
        Camera.Camera.Initialize();
        MenuContainer.Initialize();

        input = new InputManager();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        SceneManager.AddScene(new Scenes.Start.StartScene());
    }

    protected override void Update(GameTime gameTime)
    {
        input.Update();

        if (GameState.quit)
            Exit();

        SceneManager.GetCurrentScene().Update();

        MenuContainer.Update();

        Camera.Camera.Update();

        GameState.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        SceneManager.GetCurrentScene().Draw(spriteBatch);

        base.Draw(gameTime);
    }
}
