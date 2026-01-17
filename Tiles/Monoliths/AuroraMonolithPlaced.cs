using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CalValEX.Items.Tiles.Monoliths;
//using CalamityMod;

namespace CalValEX.Tiles.Monoliths
{
    public class AuroraMonolithPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(75, 139, 166));
            DustType = DustID.Stone;
            AnimationFrameHeight = 56;
            
            AdjTiles = new int[] { TileID.LunarMonolith };
            RegisterItemDrop(ModContent.ItemType<AuroraMonolith>());
        }


        public override bool RightClick(int i, int j)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Mech, new Vector2( i * 16, j * 16));
            HitWire(i, j);
            return true;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<AuroraMonolith>();
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.LunarMonolith];
            frameCounter = Main.tileFrameCounter[TileID.LunarMonolith];
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Texture2D texture;
            texture = ModContent.Request<Texture2D>("CalValEX/Tiles/Monoliths/AuroraMonolithPlaced").Value;
            Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY % AnimationFrameHeight == 36 ? 18 : 16;
            int animate = 0;
            if (tile.TileFrameY >= 56)
            {
                animate = Main.tileFrame[Type] * AnimationFrameHeight;
            }
            Main.spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY + animate, 16, height), Lighting.GetColor(i, j), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            return false;
        }

        public override void HitWire(int i, int j)
        {
            int x = i - Main.tile[i, j].TileFrameX / 18 % 3;
            int y = j - Main.tile[i, j].TileFrameY / 18 % 3;
            for (int l = x; l < x + 3; l++)
            {
                for (int m = y; m < y + 3; m++)
                {
                    if (Main.tile[l, m].TileType == Type)
                    {
                        if (Main.tile[l, m].TileFrameY < 56)
                        {
                            Main.tile[l, m].TileFrameY += 56;
                        }
                        else
                        {
                            Main.tile[l, m].TileFrameY -= 56;
                        }
                    }
                }
            }
            if (Wiring.running)
            {
                Wiring.SkipWire(x, y);
                Wiring.SkipWire(x, y + 1);
                Wiring.SkipWire(x, y + 2);
                Wiring.SkipWire(x + 1, y);
                Wiring.SkipWire(x + 1, y + 1);
                Wiring.SkipWire(x + 1, y + 2);
                Wiring.SkipWire(x + 2, y);
                Wiring.SkipWire(x + 2, y + 1);
                Wiring.SkipWire(x + 2, y + 2);
            }
            NetMessage.SendTileSquare(-1, x, y + 1, 3);
        }
    }
}