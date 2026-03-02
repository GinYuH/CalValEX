using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Walls
{
    public class WulfrumPanelWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(50, 92, 61));
        }

    public static void DrawWallGlow(int wallType, int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            int xLength = 32;

            Texture2D glowTexture = Request<Texture2D>("CalValEX/Walls/WulfrumPanelWallPlacedGlow").Value;
            Texture2D wallTexture = Request<Texture2D>("CalValEX/Walls/WulfrumPanelWallPlaced").Value;

            int xPos = tile.WallFrameX;
            int yPos = tile.WallFrameY;

            Rectangle frame = new Rectangle(xPos, yPos, xLength, 32);

            Color drawcolor;
            drawcolor = WorldGen.paintColor(tile.WallColor);
            drawcolor.A = 255;

            Color drawcolor2;
            drawcolor2 = WorldGen.paintColor(tile.WallColor);
            drawcolor2 = Lighting.GetColor(i, j);
            drawcolor2.A = 255;

            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            Vector2 pos = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;

            for (int k = 0; k < 3; k++)
            {
                spriteBatch.Draw(wallTexture, pos + new Vector2(-8, -8), frame, drawcolor2, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            for (int k = 0; k < 3; k++)
            {
                spriteBatch.Draw(glowTexture, pos + new Vector2(-8, -8), frame, drawcolor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            DrawWallGlow(Type, i, j, spriteBatch);
            return false;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (j >= Main.maxTilesY - 1) return;

            Tile tileDown = Main.tile[i, j + 1];

            if (!tileDown.HasTile && tileDown.WallType == WallID.None)
            {
                r = 0.12f;
                g = 0.31f;
                b = 0.46f;
            }
            else 
            {
                r = 0f;
                g = 0f;
                b = 0f;
            }
                }
        }
}