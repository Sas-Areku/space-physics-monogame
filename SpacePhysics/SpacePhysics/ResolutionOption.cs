using System;
using Microsoft.Xna.Framework;

namespace SpacePhysics;

public class ResolutionOption
{
  public Vector2 Resolution { get; set; }
  public string AspectRatio { get; set; }

  public ResolutionOption(Vector2 resolution, string aspectRatio)
  {
    Resolution = resolution;
    AspectRatio = aspectRatio;
  }
}
