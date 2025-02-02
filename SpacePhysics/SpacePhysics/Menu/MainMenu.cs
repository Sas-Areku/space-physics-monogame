using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePhysics.Menu.MenuItems;
using static SpacePhysics.GameState;
using static SpacePhysics.Menu.MenuContainer;

namespace SpacePhysics.Menu;

public class MainMenu : CustomGameComponent
{
  private Vector2 offset;
  private Vector2 baseOffset;

  private float opacity;

  private int menuItemsLength;
  private int activeMenu;

  public MainMenu(
    bool allowInput,
    Alignment alignment,
    int layerIndex) : base(
      allowInput,
      alignment,
      layerIndex
    )
  {
    offset = new Vector2(menuOffsetXLeft, 50f);
    baseOffset = offset;

    components.Add(new MenuItem(
        "Play",
        () => activeMenu == 1,
        alignment,
        () => new Vector2(0f, 0f) + offset,
        () => opacity,
        11
      ));

    components.Add(new MenuItem(
      "Settings",
      () => activeMenu == 2,
      alignment,
      () => new Vector2(0f, menuSizeY) + offset,
      () => opacity,
      11
    ));

    components.Add(new MenuItem(
      "Quit",
      () => activeMenu == 3,
      alignment,
      () => new Vector2(0f, menuSizeY * 2f) + offset,
      () => opacity,
      11
    ));
  }

  public override void Initialize()
  {
    menuItemsLength = 3;
    activeMenu = 1;

    base.Initialize();
  }

  public override void Update()
  {
    TransitionState();

    UpdateMenu();

    UpdateOffset();

    base.Update();
  }

  public override void Draw(SpriteBatch spriteBatch)
  {
    foreach (var component in components)
    {
      component.Draw(spriteBatch);
    }
  }

  private void TransitionState()
  {
    if (state != State.MainMenu)
    {
      if (opacity > 0)
        opacity = ColorHelper.FadeOpacity(opacity, 1f, 0f, opacityTransitionSpeed);

      if (opacity <= 0.1f && state == State.Play)
        activeMenu = 1;
    }
    else
    {
      opacity = ColorHelper.FadeOpacity(opacity, 0f, 1f, opacityTransitionSpeed);
    }
  }

  private void UpdateMenu()
  {
    activeMenu = Math.Clamp(activeMenu, 1, menuItemsLength);

    if (state != State.MainMenu) return;

    if (input.MenuDown())
      activeMenu++;

    if (input.MenuUp())
      activeMenu--;

    if (activeMenu == 1 && input.MenuSelect())
      state = State.Play;

    if (activeMenu == 2 && input.MenuSelect())
      state = State.Settings;

    if (activeMenu == 3 && input.MenuSelect())
      quit = true;

    isSettingsMenu = false;
  }

  private void UpdateOffset()
  {
    offset.X = baseOffset.X + menuOffsetFactor;
  }
}
