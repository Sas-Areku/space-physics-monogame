using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpacePhysics.HUD;
using SpacePhysics.Player;
using static SpacePhysics.GameState;

namespace SpacePhysics.Debugging
{
  internal class ManeuverInformation : CustomGameComponent
  {
    private List<DebugItem> statusItems = new List<DebugItem>();

    private SpriteFont font;

    private Vector2 offset;

    private Func<float> opacity;

    public ManeuverInformation(Func<float> opacity) : base(false, Alignment.BottomLeft, 11)
    {
      this.opacity = opacity;

      offset = new Vector2(200f, 400f);

      components.Add(new HudSprite(
        "HUD/hud-shadow-bottom-left",
        Alignment.BottomLeft,
        Alignment.BottomLeft,
        () => new Vector2(0f, 0f),
        () => 0f,
        () => Color.White * 0.75f * opacity(),
        hudScale * 2.5f,
        11
    ));

      components.Add(new HudText(
        "Fonts/text-font",
        () => "Maneuver status",
        Alignment.TopLeft,
        TextAlign.Left,
        () => offset,
        () => defaultColor * opacity(),
        hudTextScale * 1.3f,
        11
      ));

      statusItems.Add(new DebugItem("Mass", () => Ship.mass.ToString("0") + " kg"));
      statusItems.Add(new DebugItem("Liquid Fuel", () => fuel.ToString("0") + " L"));
      statusItems.Add(new DebugItem("Thrust", () => Ship.thrust.ToString("0") + " N"));

      for (int i = 0; i < statusItems.Count; i++)
      {
        statusItems[i].position = new Vector2(5f, i * 140f * hudTextScale + 110f) + offset;
      }
    }

    public override void Load(ContentManager contentManager)
    {
      font = contentManager.Load<SpriteFont>("Fonts/text-font");

      base.Load(contentManager);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      foreach (var component in components)
      {
        component.Draw(spriteBatch);
      }

      foreach (var item in statusItems)
      {
        spriteBatch.DrawString(
          font,
          item.Label + ": ",
          item.position,
          defaultColor * opacity(),
          0f,
          Vector2.Zero,
          hudTextScale,
          SpriteEffects.None,
          0f
        );

        spriteBatch.DrawString(
          font,
          item.ValueGetter(),
          item.position + new Vector2(font.MeasureString(item.Label).X * hudTextScale + 30, 0),
          highlightColor * opacity(),
          0f,
          Vector2.Zero,
          hudTextScale,
          SpriteEffects.None,
          0f
        );
      }
    }
  }
}