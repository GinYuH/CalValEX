using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.MiscFurniture
{
    // This class shows off many things common to Lamp tiles in Terraria. The process for creating this example is detailed in: https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#examplelamp-tile
    // If you can't figure out how to recreate a vanilla tile, see that guide for instructions on how to figure it out yourself.
    public class BrimstoneHeartPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Main.tileFlame[Type] = true; This breaks it.
            Main.tileLighted[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 }; //
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Hanging Heart");
            AddMapEntry(new Color(139, 0, 0), name);
            HitSound = SoundID.NPCDeath1;
        }

        public override void HitWire(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int topY = j - tile.TileFrameY / 16 % 2;
            int topX = i - tile.TileFrameX / 16 % 2;
            short frameAdjustment = (short)(tile.TileFrameX > 0 ? -16 : 16);
            Main.tile[i, topY].TileFrameX += frameAdjustment;
            Main.tile[i, topY + 1].TileFrameX += frameAdjustment;
            Main.tile[i, topY + 2].TileFrameX += frameAdjustment;
            Wiring.SkipWire(topX, topY);
            Wiring.SkipWire(topX, topY + 1);
            Wiring.SkipWire(topX, topY + 2);
            Wiring.SkipWire(topX + 1, topY);
            Wiring.SkipWire(topX + 1, topY + 1);
            Wiring.SkipWire(topX + 1, topY + 2);
            NetMessage.SendTileSquare(-1, i, topY + 1, 2, TileChangeType.None);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX == 0)
            {
                // We can support different light colors for different styles here: switch (tile.TileFrameY / 54)
                r = 1.5f;
                g = 0.75f;
                b = 0.6f;
            }
        }

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Terraria.DataStructures.TileDrawInfo drawData)
        {
            if (!Main.gamePaused && Main.instance.IsActive && (!Lighting.UpdateEveryFrame || Main.rand.NextBool(4)))
            {
                Tile tile = Main.tile[i, j];
                short TileFrameX = tile.TileFrameX;
                short TileFrameY = tile.TileFrameY;
            }
        }

        public float PrimitiveWidthFunction(float completionRatio)
        {
            float widthInterpolant = Utils.GetLerpValue(0f, 0.16f, completionRatio, true) * Utils.GetLerpValue(1f, 0.84f, completionRatio, true);
            widthInterpolant = (float)Math.Pow(widthInterpolant, 8D);
            float baseWidth = MathHelper.Lerp(6f, 4f, widthInterpolant);
            float pulseWidth = MathHelper.Lerp(0f, 3.2f, (float)Math.Pow(Math.Sin(Main.GlobalTimeWrappedHourly * 2.6f + completionRatio), 16D));
            return baseWidth + pulseWidth;
        }

        public Color PrimitiveColorFunction(float completionRatio)
        {
            float colorInterpolant = MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(0f, 0.34f, completionRatio, true) * Utils.GetLerpValue(1.07f, 0.66f, completionRatio, true));
            return Color.Lerp(Color.DarkRed * 0.7f, Color.Red, colorInterpolant) * 0.425f;
        }


        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile t = Main.tile[i, j];
            if (t.TileFrameX == 0 && t.TileFrameY == 36)
            {
                for (int l = 0; l < 2; l++)
                {
                    List<Vector2> cordPos = new();
                    for (int k = 0; k < 5; k++)
                    {
                        float mod = i % 22 + j % 22;
                        mod /= 88;
                        Vector2 endPos = l == 0 ? new Vector2(i, j) * 16 - Vector2.UnitY.RotatedBy(MathHelper.ToRadians(2)).RotatedBy(mod) * 40 : new Vector2(i, j) * 16 - Vector2.UnitY.RotatedBy(-MathHelper.ToRadians(2)).RotatedBy(-mod) * 40;
                        endPos.X += 16;
                        cordPos.Add(Vector2.Lerp(new Vector2(i, j) * 16 + Vector2.UnitX * 16, endPos, k / 4f) + new Vector2(Main.offScreenRange));
                    }
                    Terraria.Graphics.VertexStrip artery = new();
                    List<float> rots = new();
                    for (int k = 0; k < cordPos.Count; k++)
                    {
                        rots.Add(0);
                    }
                    artery.PrepareStripWithProceduralPadding(cordPos.ToArray(), rots.ToArray(), new Terraria.Graphics.VertexStrip.StripColorFunction(PrimitiveColorFunction), new Terraria.Graphics.VertexStrip.StripHalfWidthFunction(PrimitiveWidthFunction), -Main.screenPosition, true);

                    Effect vertexShader = ModContent.Request<Effect>($"{nameof(CalValEX)}/Effects/VertexShader", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
                    vertexShader.Parameters["uColor"].SetValue(Vector4.One);
                    vertexShader.Parameters["uTransformMatrix"].SetValue(Main.GameViewMatrix.NormalizedTransformationmatrix);
                    vertexShader.CurrentTechnique.Passes[0].Apply();

                    artery.DrawTrail();
                }
            }
            return true;
        }
    }
}
