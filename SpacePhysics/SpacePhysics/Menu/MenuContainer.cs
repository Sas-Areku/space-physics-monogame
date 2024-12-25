using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpacePhysics.Menu;

public class MenuContainer
{
  public static float menuOffsetX;

  public static Vector2 cameraOffset;
  public static Vector2 targetCameraOffset;

  public static float padding = 0.17f;
  public static float menuSizeY = 1000f * padding;

  public MenuContainer()
  {
    float start = 1250f;
    float end = 1f;

    menuOffsetX = start - (GameState.hudScaleOverrideFactor - 0.1f) * Math.Abs((end - start) / 0.9f);
  }

  public static void Initialize()
  {
    cameraOffset = new Vector2(GameState.screenSize.X * 0.12f, -GameState.screenSize.Y * 0.05f);
    targetCameraOffset = new Vector2(GameState.screenSize.X * 0.12f, -GameState.screenSize.Y * 0.05f);
  }

  public static void Update()
  {
    cameraOffset.X = MathHelper.Lerp(cameraOffset.X, targetCameraOffset.X, GameState.deltaTime * 3f);
    cameraOffset.Y = MathHelper.Lerp(cameraOffset.Y, targetCameraOffset.Y, GameState.deltaTime * 3f);

    Camera.Camera.offset = cameraOffset;
  }

  public static float CalculateMenuHeight(List<CustomGameComponent> menuItems)
  {
    float totalHeight = 0;
    float previousPositionY = 0;
    float previousHeight = 0;

    foreach (var menuItem in menuItems)
    {
      totalHeight += menuItem.height;

      if (Math.Abs(previousPositionY) > 0 || previousHeight > 0)
      {
        totalHeight += menuItem.position.Y - previousPositionY - previousHeight;
      }

      previousPositionY = menuItem.position.Y;
      previousHeight = menuItem.height;
    }

    return totalHeight;
  }

  public static Vector2 CenterMenu(List<CustomGameComponent> menuItems)
  {
    return new Vector2(
      0f,
      GameState.screenSize.Y * GameState.scale - CalculateMenuHeight(menuItems) / 2
    );
  }
}
