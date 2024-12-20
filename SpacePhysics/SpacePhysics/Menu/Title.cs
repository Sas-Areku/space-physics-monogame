using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpacePhysics.HUD;
using SpacePhysics.Scenes.Start;
using static SpacePhysics.GameState;

namespace SpacePhysics.Menu;

public class Title : CustomGameComponent
{
  private Vector2 offset;

  private float offsetY;
  private float targetOffsetY;

  private float opacity;

  public Title(
    bool allowInput,
    Alignment alignment,
    int layerIndex) : base(
      allowInput,
      alignment,
      layerIndex
    )
  {
    offset = new Vector2(1000, 0);
    targetOffsetY = 0f;

    components.Add(new HudSprite(
      "Menu/icon",
      alignment,
      alignment,
      () => new Vector2(0, -300) + offset,
      () => 0f,
      () => Color.White * opacity,
      2.5f,
      11
    ));

    components.Add(new HudText(
      "Fonts/title-font",
      () => "Space Physics",
      alignment,
      TextAlign.Left,
      () => new Vector2(700, -300) + offset,
      () => Color.White * opacity,
      1.75f,
      11
    ));
  }

  public override void Update()
  {
    if (state != State.TitleScreen && state != State.MainMenu)
    {
      opacity = ColorHelper.FadeOpacity(opacity, 1f, 0f, StartScene.transitionSpeed);
      targetOffsetY = -50;
    }
    else if (state == State.MainMenu)
    {
      targetOffsetY = -50;
    }
    else
    {
      opacity = ColorHelper.FadeOpacity(opacity, -1f, 1f, 5f);
      targetOffsetY = 0f;
    }

    offsetY = MathHelper.Lerp(offsetY, targetOffsetY, deltaTime * 4f);
    offset.Y = offsetY;

    base.Update();
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    foreach (var component in components)
    {
      component.Draw(spriteBatch);
    }
  }
}
