using System;
using Microsoft.Xna.Framework;
using SpacePhysics.Menu.MenuItems;
using static SpacePhysics.GameState;
using static SpacePhysics.Menu.MenuContainer;

namespace SpacePhysics.Menu.SubMenus;

public class DisplayMenu : SubMenu
{

  public DisplayMenu() : base(
    "Display",
    new Vector2(0f, -50f),
    State.Display,
    State.Settings
  )
  { }

  public override void AddMenuItems()
  {
    menuItems.Add(new MenuSelectorItem(
      "Aspect ratio",
      () => SettingsState.aspectRatio,
      () => ["4:3", "16:9", "16:10"],
      value => SettingsState.aspectRatio = value,
      () => activeMenu == 1,
      () => true,
      alignment,
      () => new Vector2(0f, 0f) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuSelectorItem(
      "Resolution",
      () => SettingsState.resolution,
      () => ["1280x720", "1600x900", "1920x1080", "2560x1440"],
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
      "Vsync",
      () => SettingsState.vsync.ToString(),
      () => ["Off", "On"],
      value => SettingsState.vsync = false,
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
