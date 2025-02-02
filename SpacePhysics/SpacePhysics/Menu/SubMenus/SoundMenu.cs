using System;
using Microsoft.Xna.Framework;
using SpacePhysics.Menu.MenuItems;
using static SpacePhysics.GameState;
using static SpacePhysics.Menu.MenuContainer;

namespace SpacePhysics.Menu.SubMenus;

public class SoundMenu : SubMenu
{
  private bool updatable;

  public SoundMenu() : base(
    "Sound",
    new Vector2(0f, -150f),
    State.Sound,
    State.Settings
  )
  { }

  public override void Update()
  {
    updatable = state == State.Sound;

    base.Update();
  }

  public override void AddMenuItems()
  {
    menuItems.Add(new MenuIncrementerItem(
      "Master",
      () => SettingsState.masterVolume,
      value => SettingsState.masterVolume = (int)value,
      5f,
      0f,
      100f,
      "%",
      () => activeMenu == 1,
      () => updatable,
      alignment,
      () => new Vector2(0f, 0f) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuIncrementerItem(
      "Music",
      () => SettingsState.musicVolume,
      value => SettingsState.musicVolume = (int)value,
      5f,
      0f,
      100f,
      "%",
      () => activeMenu == 2,
      () => updatable,
      alignment,
      () => new Vector2(0f, menuSizeY) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuIncrementerItem(
      "Sound effects",
      () => SettingsState.soundEffectsVolume,
      value => SettingsState.soundEffectsVolume = (int)value,
      5f,
      0f,
      100f,
      "%",
      () => activeMenu == 3,
      () => updatable,
      alignment,
      () => new Vector2(0f, menuSizeY * 2f) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    menuItems.Add(new MenuIncrementerItem(
      "Menu sound effects",
      () => SettingsState.menuSoundEffectsVolume,
      value => SettingsState.menuSoundEffectsVolume = (int)value,
      5f,
      0f,
      100f,
      "%",
      () => activeMenu == 4,
      () => updatable,
      alignment,
      () => new Vector2(0f, menuSizeY * 3f) + menuOffsetOverride + entireOffsetOverride,
      controlItemDistance,
      () => opacity,
      11
    ));

    base.AddMenuItems();
  }
}
