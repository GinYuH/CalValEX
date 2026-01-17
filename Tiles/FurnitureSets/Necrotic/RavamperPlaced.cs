using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Tiles.FurnitureSets.Necrotic;

namespace CalValEX.Tiles.FurnitureSets.Necrotic
{
    public class RavamperPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 }; //
            AnimationFrameHeight = 36;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Necrotic Bonfire");
            AddMapEntry(new Color(139, 0, 0), name);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
            {
                if (Main.tile[i, j].TileFrameY >= 36)
                {
                    Main.LocalPlayer.AddBuff(BuffID.Campfire, 20);
                }
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Texture2D texture = Request<Texture2D>("CalValEX/Tiles/FurnitureSets/Necrotic/RavamperPlaced").Value;
            Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY % AnimationFrameHeight == 36 ? 18 : 16;
            int animate = 0;
            if (tile.TileFrameY >= 36)
            {
                animate = Main.tileFrame[Type] * AnimationFrameHeight;
            }
            Main.spriteBatch.Draw(texture, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY + animate, 16, height), Lighting.GetColor(i, j), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            return false;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            if (Main.tile[i, j].TileFrameY >= 36)
            {
                Tile tile = Main.tile[i, j];
                r = 0.9f;
                g = 0.9f;
                b = 0.9f;
            }
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.Campfire];
            frameCounter = Main.tileFrameCounter[TileID.Campfire];
        }
        public override bool HasSmartInteract(int i, int j, Terraria.GameContent.ObjectInteractions.SmartInteractScanSettings settings) => true;

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
            player.cursorItemIconID = ItemType<Ravamper>();
        }

        public override void HitWire(int i, int j)
        {
            int x = i - Main.tile[i, j].TileFrameX / 18 % 3;
            int y = j - Main.tile[i, j].TileFrameY / 18 % 2;
            for (int l = x; l < x + 3; l++)
            {
                for (int m = y; m < y + 2; m++)
                {
                    if (Main.tile[l, m].TileType == Type)
                    {
                        if (Main.tile[l, m].TileFrameY < 36)
                        {
                            Main.tile[l, m].TileFrameY += 36;
                        }
                        else
                        {
                            Main.tile[l, m].TileFrameY -= 36;
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
            }
            NetMessage.SendTileSquare(-1, x, y + 1, 3);
        }
    }
}