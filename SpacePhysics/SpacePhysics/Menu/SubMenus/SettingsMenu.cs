using System;
using Microsoft.Xna.Framework;
using SpacePhysics.Menu.MenuItems;
using static SpacePhysics.GameState;
using static SpacePhysics.Menu.MenuContainer;

namespace SpacePhysics.Menu.SubMenus;

public class SettingsMenu : SubMenu
{

  public SettingsMenu() : base(
    "Settings",
    new Vector2(450f, -150f),
    State.Settings,
    State.MainMenu
  )
  {
    components.Add(new SoundMenu());

    components.Add(new DisplayMenu());

    components.Add(new UIMenu());

    components.Add(new ControlsMenu());
  }

  public override void AddMenuItems()
  {
    menuItems.Add(new MenuItem(
        "Sound",
        () => activeMenu == 1,
        alignment,
        () => new Vector2(0f, 0f) + menuOffsetOverride + entireOffsetOverride,
        () => opacity,
        11
      ));

    menuItems.Add(new MenuItem(
      "Display",
      () => activeMenu == 2,
      alignment,
      () => new Vector2(0f, menuSizeY) + menuOffsetOverride + entireOffsetOverride,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuItem(
      "UI",
      () => activeMenu == 3,
      alignment,
      () => new Vector2(0f, menuSizeY * 2f) + menuOffsetOverride + entireOffsetOverride,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuItem(
      "Controls",
      () => activeMenu == 4,
      alignment,
      () => new Vector2(0f, menuSizeY * 3f) + menuOffsetOverride + entireOffsetOverride,
      () => opacity,
      11
    ));

    base.AddMenuItems();
  }

  public override void UpdateMenu()
  {
    base.UpdateMenu();

    if (state != State.Settings) return;

    if (activeMenu == 1 && input.MenuSelect() && isSettingsMenu)
      state = State.Sound;

    if (activeMenu == 2 && input.MenuSelect())
      state = State.Display;

    if (activeMenu == 3 && input.MenuSelect())
      state = State.UI;

    if (activeMenu == 4 && input.MenuSelect())
      state = State.Controls;

    isSettingsMenu = true;
  }
}
