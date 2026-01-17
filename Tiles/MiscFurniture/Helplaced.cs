using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Tiles;

namespace CalValEX.Tiles.MiscFurniture
{
    public class Helplaced : ModTile
    {
        public static Terraria.Audio.SoundStyle helpm = new("CalValEX/Sounds/Help");
        public static Terraria.Audio.SoundStyle chug = new("CalValEX/Sounds/Nom");
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileID.Sets.HasOutlines[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 }; //

            AnimationFrameHeight = 54;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Help me");
            AddMapEntry(new Color(139, 0, 0), name);
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (choketimer < 240 && choking)
            {
                if (frameCounter > 4) //make this number lower/bigger for faster/slower animation
                {
                    frameCounter = 0;
                    frame++;
                    if (frame > 1)
                    {
                        frame = 0;
                    }
                }
            }
            else
            {
                if (frameCounter > 15) //make this number lower/bigger for faster/slower animation
                {
                    frameCounter = 0;
                    frame++;
                    if (frame > 1)
                    {
                        frame = 0;
                    }
                }
            }
        }

        public override bool HasSmartInteract(int i, int j, Terraria.GameContent.ObjectInteractions.SmartInteractScanSettings settings) => true;

        public override void MouseOver(int i, int j)
        {
            Player localPlayer = Main.LocalPlayer;
            localPlayer.noThrow = 2;
            localPlayer.cursorItemIconEnabled = true;
            localPlayer.cursorItemIconID = ItemType<Help>();
            int dust = Dust.NewDust(new Vector2(i, j) * 16f, 5, 5, DustID.Stone, 0f, 6.315789f, 161, new Color(0, 217, 255), 1.315789f);
            if (Main.LocalPlayer.name == "Bumbledoge")
            {
                Main.LocalPlayer.AddBuff(CalValEX.CalamityBuff("TemporalSadness"), 10);
            }
            else if (Main.LocalPlayer.name == "willowmaine")
            {
                Main.LocalPlayer.AddBuff(CalValEX.CalamityBuff("TemporalSadness"), 10);
            }
            else if (Main.LocalPlayer.name == "Mochi")
            {
                Main.LocalPlayer.AddBuff(CalValEX.CalamityBuff("TemporalSadness"), 10);
            }
            else
            {
                Main.LocalPlayer.AddBuff(BuffID.Sunflower, 20);
            }
        }
        int choketimer = 0;
        private bool choking = false;
        private bool feed = false;

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            if (player.HasItem(ItemID.GreenMushroom) && !choking)
            {
                player.ConsumeItem(ItemID.GreenMushroom);
                Terraria.Audio.SoundEngine.PlaySound(chug);
                feed = false;
                choking = true;
            }
            else if (!choking)
            {
                Terraria.Audio.SoundEngine.PlaySound(helpm);
            }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (choking)
            {
                choketimer++;
                if (choketimer == 240 && !feed)
                {
                    feed = true;
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath13);
                    Item.NewItem(new Terraria.DataStructures.EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, CalValEX.CalamityItem("SulphurousSandstone"), Main.rand.Next(6, 21));
                }
                if (choketimer == 360)
                {
                    Terraria.Audio.SoundEngine.PlaySound(helpm);
                    choking = false;
                    feed = false;
                    choketimer = 0;
                }
            }
            if (closer)
            {
                if (Main.LocalPlayer.HasItem(CalValEX.CalamityItem("OrthoceraShell")))
                {
                    int dust = Dust.NewDust(new Vector2(i, j) * 16f, 5, 5, DustID.Stone, 0f, 6.315789f, 161, new Color(0, 217, 255), 1.315789f);
                    if (Main.LocalPlayer.name == "Bumbledoge")
                    {
                        Main.LocalPlayer.AddBuff(CalValEX.CalamityBuff("TemporalSadness"), 10);
                    }
                    else if (Main.LocalPlayer.name == "willowmaine")
                    {
                        Main.LocalPlayer.AddBuff(CalValEX.CalamityBuff("TemporalSadness"), 10);
                    }
                    else if (Main.LocalPlayer.name == "Mochi")
                    {
                        Main.LocalPlayer.AddBuff(CalValEX.CalamityBuff("TemporalSadness"), 10);
                    }
                    else
                    {
                        Main.LocalPlayer.AddBuff(BuffID.Sunflower, 10);
                    }
                }
            }
        }
    }
}