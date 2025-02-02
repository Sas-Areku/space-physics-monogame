using System;
using Microsoft.Xna.Framework;
using SpacePhysics.Menu.MenuItems;
using static SpacePhysics.GameState;
using static SpacePhysics.Menu.MenuContainer;

namespace SpacePhysics.Menu.SubMenus;

public class UIMenu : SubMenu
{

  public UIMenu() : base(
    "UI",
    new Vector2(0f, -50f),
    State.UI,
    State.Settings
  )
  { }

  public override void AddMenuItems()
  {
    menuItems.Add(new MenuSelectorItem(
      "Scale",
      () => "100%",
      () => ["100%"],
      value => SettingsState.resolution = value,
      () => activeMenu == 1,
      () => true,
      alignment,
      () => new Vector2(0f, 0f) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuSelectorItem(
      "Color",
      () => "Yellow",
      () => ["Yellow"],
      value => SettingsState.resolution = value,
      () => activeMenu == 2,
      () => true,
      alignment,
      () => new Vector2(0f, menuSizeY) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuSelectorItem(
      "Safe zone",
      () => "0.0",
      () => ["0.0"],
      value => SettingsState.resolution = value,
      () => activeMenu == 3,
      () => true,
      alignment,
      () => new Vector2(0f, menuSizeY * 2) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    base.AddMenuItems();
  }
}
