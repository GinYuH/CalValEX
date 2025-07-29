using CalValEX.Tiles.Plants;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX
{
    public class CalValEXGlobalTile : GlobalTile
    {
        private int cragCount;
        private int berryCount;

        public static int PhantomSproutID;

        public override void SetStaticDefaults()
        {
            PhantomSproutID = ModContent.TileType<PhantoSproutPlaced>();
        }

        /// <summary>This function lets you easily set a tile glowmask. Compatible with both blocks and furniture.</summary>
        /// <param name="i">The x coord of the tile</param>
        /// <param name="j">The y coord of the tile</param> 
        /// <param name="text">The glowmask's file path</param>
        /// <param name="sprit">Just put 'spriteBatch' here</param> 
        /// <param name="frameheight">Amount of frames the glowmask uses, typically you'll just need to put 'animationFrameHeight'</param>
        public static void TileGlowmask(int i, int j, Texture2D text, SpriteBatch sprit, int frameheight = 0)
        {
            var frame = new Rectangle(Main.tile[i, j].TileFrameX, Main.tile[i, j].TileFrameY + frameheight * Main.tileFrame[Main.tile[i, j].TileType], 16, 16);
            int xFrameOffset = Main.tile[i, j].TileFrameX;
            int yFrameOffset = Main.tile[i, j].TileFrameY;
            Texture2D glowmask = text;
            Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
            Color drawColour = GetDrawColour(i, j, Color.White);
            Tile trackTile = Main.tile[i, j];
            if (frameheight == 0)
            {
                if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
                    sprit.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 16, 16), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
                else if (trackTile.IsHalfBlock)
                    sprit.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 16, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
            }
            else
            {
                if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
                    sprit.Draw(glowmask, drawPosition, frame, drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
                else if (trackTile.IsHalfBlock)
                    sprit.Draw(glowmask, drawPosition + new Vector2(0f, 8f), frame, drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
            }
        }

        public static Color GetDrawColour(int i, int j, Color colour)
        {
            int colType = Main.tile[i, j].TileColor;
            Color paintCol = WorldGen.paintColor(colType);
            if (colType >= 13 && colType <= 24)
            {
                colour.R = (byte)(paintCol.R / 255f * colour.R);
                colour.G = (byte)(paintCol.G / 255f * colour.G);
                colour.B = (byte)(paintCol.B / 255f * colour.B);
            }
            return colour;
        }

        public static void ChestGlowmask(int i, int j, Texture2D text, SpriteBatch sprit)
        {
            Tile tile = Main.tile[i, j];
            int left = i;
            int top = j;
            if (tile.TileFrameX % 36 != 0)
            {
                left--;
            }
            if (tile.TileFrameY != 0)
            {
                top--;
            }
            int chestI = Chest.FindChest(left, top);
            Chest chest = Main.chest[chestI];
            int cFrame = chest.frame;
            Texture2D glowmask = text;
            Rectangle frame = new(tile.TileFrameX, 38 * cFrame + tile.TileFrameY, 16, 16);
            Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            Vector2 pos = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;
            sprit.Draw(glowmask, pos, frame, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override void RandomUpdate(int i, int j, int type) 
        {
            if (NPC.downedBoss3 || Main.hardMode)
            {
                Tile tile = Framing.GetTileSafely(i, j);
                if (tile.TileType == TileID.Tombstones)
                {
                    if (Main.rand.NextBool(30))
                    {
                        int xTile = Main.rand.Next(Math.Max(10, i - 10), Math.Min(Main.maxTilesX - 10, i + 10));
                        int yTile = Main.rand.Next(Math.Max(10, j - 10), Math.Min(Main.maxTilesY - 10, j + 10));
                        if (ValidGroundForFlower(xTile, yTile) && NoNearbyFlower(xTile, yTile) && WorldGen.PlaceTile(xTile, yTile, PhantomSproutID, mute: true))
                        {
                            if (Main.dedServ && Main.tile[xTile, yTile] != null && Main.tile[xTile, yTile].HasTile)
                            {
                                NetMessage.SendTileSquare(-1, xTile, yTile);
                            }
                        }
                    }
                }
            }
        }

        private static bool NoNearbyFlower(int i, int j)
        {
            int xRangeNeg = Utils.Clamp(i - 120, 10, Main.maxTilesX - 1 - 10);
            int xRangePos = Utils.Clamp(i + 120, 10, Main.maxTilesX - 1 - 10);
            int yRangeNeg = Utils.Clamp(j - 120, 10, Main.maxTilesY - 1 - 10);
            int yRangePos = Utils.Clamp(j + 120, 10, Main.maxTilesY - 1 - 10);
            for (int k = xRangeNeg; k <= xRangePos; k++)
            {
                for (int l = yRangeNeg; l <= yRangePos; l++)
                {
                    Tile tile = Main.tile[k, l];
                    if (tile.HasTile && tile.TileType == PhantomSproutID)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool ValidGroundForFlower(int x, int y)
        {
            if (!WorldGen.InWorld(x, y, 2))
            {
                return false;
            }
            Tile tile = Main.tile[x, y + 1];
            if (tile == null || !tile.HasTile)
            {
                return false;
            }
            ushort type = tile.TileType;
            if (type < 0)
            {
                return false;
            }
            if (type != TileID.MushroomGrass && type != TileID.AshGrass && !TileID.Sets.Conversion.Grass[type])
            {
                return false;
            }
            return WorldGen.SolidTileAllowBottomSlope(x, y + 1);
        }
    }
}