using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePhysics.HUD;

namespace SpacePhysics.Menu.MenuItems;

public class MenuIncrementerItem : CustomGameComponent
{
  private readonly Func<bool> active;

  private Color color;
  private Color targetColor;
  private Color defaultColor;
  private Color highlightColor;

  private Func<float> getValue;
  private Action<float> setValue;

  private float incrementAmount;
  private float min;
  private float max;

  private Func<bool> updatable;

  public MenuIncrementerItem(
    string label,
    Func<float> getValue,
    Action<float> setValue,
    float incrementAmount,
    float min,
    float max,
    string symbol,
    Func<bool> active,
    Func<bool> updatable,
    Alignment alignment,
    Func<Vector2> offset,
    float distanceX,
    Func<float> opacity,
    int layerIndex
  ) : base(true, alignment, layerIndex)
  {
    this.getValue = getValue;
    this.setValue = setValue;
    this.incrementAmount = incrementAmount;
    this.min = min;
    this.max = max;
    this.active = active;
    this.updatable = updatable;

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
      () => this.getValue().ToString() + symbol,
      alignment,
      TextAlign.Center,
      () => offset() + new Vector2(distanceX, 0f),
      () => color * opacity(),
      1f,
      11
    ));

    components.Add(new HudSprite(
      "Menu/menu-selector-left",
      alignment,
      Alignment.Center,
      () => offset() + new Vector2(distanceX - (components[1].width / 2) - 55f, 0f),
      () => 0f,
      () => active() ? color * opacity() : Color.White * 0f,
      1f,
      11
    ));

    components.Add(new HudSprite(
      "Menu/menu-selector-right",
      alignment,
      Alignment.Center,
      () => offset() + new Vector2(distanceX + (components[1].width / 2) + 50f, 0f),
      () => 0f,
      () => active() ? color * opacity() : Color.White * 0f,
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

    if (active())
    {
      targetColor = highlightColor;

      if (!updatable()) return;

      if (input.MenuLeft()) setValue(getValue() - incrementAmount);
      if (input.MenuRight()) setValue(getValue() + incrementAmount);
    }

    setValue(Math.Clamp(getValue(), min, max));

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
