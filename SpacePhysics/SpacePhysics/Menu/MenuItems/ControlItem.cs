using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePhysics.HUD;

namespace SpacePhysics.Menu.MenuItems;

public class ControlItem : CustomGameComponent
{
  private readonly Func<bool> active;

  private Color color;
  private Color targetColor;
  private Color defaultColor;
  private Color highlightColor;

  public ControlItem(
    string label,
    Func<string> value,
    Func<bool> active,
    Alignment alignment,
    Func<Vector2> offset,
    float distanceX,
    Func<float> opacity,
    int layerIndex
  ) : base(false, alignment, layerIndex)
  {
    this.active = active;

    components.Add(new HudText(
      "Fonts/text-font",
      () => label,
      alignment,
      TextAlign.Left,
      () => offset(),
      () => color * opacity(),
      1f,
      11
    ));

    components.Add(new HudText(
      "Fonts/text-font",
      () => value(),
      alignment,
      TextAlign.Center,
      () => offset() + new Vector2(distanceX, 0f),
      () => color * opacity(),
      1f,
      11
    ));
  }

  public override void Initialize()
  {
    defaultColor = Color.White * 0.75f;
    highlightColor = Color.Gold;
    color = defaultColor;
    targetColor = color;

    base.Initialize();
  }

  public override void Update()
  {
    targetColor = defaultColor;

    if (active()) targetColor = highlightColor;

    color = ColorHelper.Lerp(color, targetColor, 0.3f);

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
