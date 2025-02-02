using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpacePhysics.HUD;

namespace SpacePhysics.Menu.MenuItems;

public class MenuSelectorItem : CustomGameComponent
{
  private readonly Func<bool> active;

  private Color color;
  private Color targetColor;
  private Color defaultColor;
  private Color highlightColor;

  private Func<string> getValue;
  private Func<string[]> getOptions;
  private Action<string> setValue;

  private Func<bool> updatable;

  private int optionIndex;

  public MenuSelectorItem(
    string label,
    Func<string> getValue,
    Func<string[]> getOptions,
    Action<string> setValue,
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
    this.getOptions = getOptions;
    this.setValue = setValue;
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
      () => this.getOptions()[optionIndex].ToString(),
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
    optionIndex = Array.IndexOf(getOptions(), getValue()) != -1 ? Array.IndexOf(getOptions(), getValue()) : 0;

    base.Initialize();
  }

  public override void Update()
  {
    targetColor = defaultColor;

    if (active())
    {
      targetColor = highlightColor;

      if (!updatable()) return;

      if (input.MenuLeft()) optionIndex--;
      if (input.MenuRight()) optionIndex++;

      if (optionIndex + 1 > getOptions().Length) optionIndex = 0;
      if (optionIndex < 0) optionIndex = getOptions().Length - 1;

      setValue(getOptions()[optionIndex]);
    }

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
