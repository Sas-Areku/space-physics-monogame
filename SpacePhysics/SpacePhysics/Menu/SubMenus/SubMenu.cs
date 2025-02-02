using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePhysics.HUD;
using SpacePhysics.Menu.MenuItems;
using static SpacePhysics.GameState;
using static SpacePhysics.Menu.MenuContainer;

namespace SpacePhysics.Menu.SubMenus;

public class SubMenu : CustomGameComponent
{
  public List<CustomGameComponent> menuItems = [];

  // TODO: Remove redundant offsets
  private Vector2 offset;
  private Vector2 baseOffset;
  public Vector2 menuOffsetOverride;
  public Vector2 entireOffsetOverride;

  private State activeState;
  private State previousState;

  public float opacity;
  public float controlItemDistance;
  private float backButtonYOffset;

  public int activeMenu;

  public SubMenu(
    string title,
    Vector2 offsetOverride,
    State activeState,
    State previousState
  ) : base(
      true,
      Alignment.Right,
      11
    )
  {
    this.activeState = activeState;
    this.previousState = previousState;

    entireOffsetOverride = new Vector2(-600f, 0f);
    entireOffsetOverride += offsetOverride;
    offset = new Vector2(menuOffsetXRight, 0f);
    baseOffset = offset;
    controlItemDistance = 2000f;

    components.Add(new HudText(
      "Fonts/title-font",
      () => title,
      alignment,
      TextAlign.Left,
      () => new Vector2(-100, -400) + offset + entireOffsetOverride,
      () => Color.White * opacity,
      1.75f,
      11
    ));

    AddMenuItems();

    foreach (var menuItem in menuItems)
    {
      components.Add(menuItem);
    }
  }

  public override void Initialize()
  {
    activeMenu = 1;

    base.Initialize();
  }

  public override void Update()
  {
    backButtonYOffset = menuItems.Count - 0.5f;

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

  public virtual void AddMenuItems()
  {
    menuItems.Add(new MenuItem(
      "Back",
      () => activeMenu == menuItems.Count,
      alignment,
      () => new Vector2(0f, menuSizeY * backButtonYOffset) + menuOffsetOverride + entireOffsetOverride,
      () => opacity,
      11
    ));
  }

  private void TransitionState()
  {
    if (state != activeState)
    {
      if (opacity > 0)
        opacity = ColorHelper.FadeOpacity(opacity, 1f, 0f, opacityTransitionSpeed);

      if (opacity <= 0.1f && (state == State.MainMenu || state == State.Pause))
        activeMenu = 1;
    }
    else
    {
      opacity = ColorHelper.FadeOpacity(opacity, 0f, 1f, opacityTransitionSpeed);
    }
  }

  public virtual void UpdateMenu()
  {
    activeMenu = Math.Clamp(activeMenu, 1, menuItems.Count);

    if (state != activeState) return;

    if (state != State.Settings) isSettingsMenu = false;

    if (input.MenuDown())
      activeMenu++;

    if (input.MenuUp())
      activeMenu--;

    if ((activeMenu == menuItems.Count && input.MenuSelect()) || input.MenuBack())
      state = previousState;
  }

  private void UpdateOffset()
  {
    offset.X = baseOffset.X + menuOffset.X * 3f;
    menuOffsetOverride.X = baseOffset.X - 150 + menuOffsetFactor;
  }
}
