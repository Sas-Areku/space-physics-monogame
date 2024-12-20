﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpacePhysics.Menu;
using SpacePhysics.Scenes;

namespace SpacePhysics;

public class Main : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private SceneManager sceneManager;

    public Main()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = (int)GameState.screenSize.X;
        graphics.PreferredBackBufferHeight = (int)GameState.screenSize.Y;
        graphics.IsFullScreen = true;
        IsFixedTimeStep = false;
        graphics.SynchronizeWithVerticalRetrace = false;
        graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        GraphicsDevice.PresentationParameters.MultiSampleCount = 4;

        GameState.Initialize();
        Camera.Camera.Initialize();

        sceneManager = new(Content);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        sceneManager.AddScene(new Scenes.Start.StartScene(sceneManager));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) || MainMenu.quit)
            Exit();

        GameState.Update(gameTime);
        Camera.Camera.Update();

        sceneManager.GetCurrentScene().Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        sceneManager.GetCurrentScene().Draw(spriteBatch);

        base.Draw(gameTime);
    }
}
