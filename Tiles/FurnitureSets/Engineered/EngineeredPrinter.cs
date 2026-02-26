using CalValEX.Items.Tiles.FurnitureSets.Engineered;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
    public class EngineeredPrinter : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;

            //Size rules
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
            //Placement - Flipping
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);

            TileObjectData.addTile(Type);

            AddMapEntry(new Color(110, 52, 52));
            DustType = DustID.Clay;
            
            RegisterItemDrop(ModContent.ItemType<EngineeredPrinterItem>());
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch) =>
			CalValEXGlobalTile.TileGlowmask(i, j, Request<Texture2D>("CalValEX/Tiles/FurnitureSets/Engineered/EngineeredPrinterGlow").Value, spriteBatch);

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 3f / 255f;
            g = 67f / 255f;
            b = 146f / 255f;
        }

        /*public override bool RightClick(int i, int j)
        {
            CalValEXPlayer modPlayer = Main.LocalPlayer.GetModPlayer<CalValEXPlayer>();
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Mech, new Vector2( i * 16, j * 16));
            HitWire(i, j);
            return true;
        }*/

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<EngineeredPrinterItem>();
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