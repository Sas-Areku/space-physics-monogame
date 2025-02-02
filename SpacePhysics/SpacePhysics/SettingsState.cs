using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SpacePhysics;

public static class SettingsState
{
  public static Dictionary<string, ResolutionOption> resolutionOptions = new()
  {
    { "1280x720", new ResolutionOption(new Vector2(1280, 720), "16:9") },
    { "1600x900", new ResolutionOption(new Vector2(1600, 900), "16:9") },
    { "1920x1080", new ResolutionOption(new Vector2(1920, 1080), "16:9") },
    { "2560x1440", new ResolutionOption(new Vector2(2560, 1440), "16:9") },
    { "3840x2160", new ResolutionOption(new Vector2(3840, 2160), "16:9") }
  };

  public static Color uiColor;

  public static float uiSafeZone;

  public static int masterVolume;
  public static int musicVolume;
  public static int soundEffectsVolume;
  public static int menuSoundEffectsVolume;

  public static int volumeIncrementAmount;

  public static int uiScale;

  public static int[] uiScaleOptions = [
    50,
    60,
    70,
    80,
    90,
    100,
    110,
    120,
    130,
    140,
    150
  ];

  public static string resolution = "2560x1440";
  public static string aspectRatio = "16:9";

  public static bool vsync;

  public static void Initialize()
  {
    resolution = "2560x1440";
    aspectRatio = "16:9";
    vsync = true;

    masterVolume = 100;
    musicVolume = 100;
    soundEffectsVolume = 100;
    menuSoundEffectsVolume = 100;

    volumeIncrementAmount = 5;

    uiColor = GameState.highlightColor;
    uiScale = uiScaleOptions[5];
  }

  public static void Update()
  {

  }
}
