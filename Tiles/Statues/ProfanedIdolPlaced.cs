using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Tiles.Statues;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.ObjectInteractions;

namespace CalValEX.Tiles.Statues
{
    public class ProfanedIdolPlaced : ModTile {
        // Free me from this bugfixing hell I'm in agony
        private bool paused;
        public override void SetStaticDefaults() {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 6;
            TileObjectData.newTile.Height = 7;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(2, 5);
            TileObjectData.addTile(Type);
            
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(206, 116, 59), name);

            AnimationFrameHeight = 126;
            DustType = DustID.Iron;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter) {
            if (paused == true) {
                frame = 0;
                frameCounter = 0;
            } else { 
                frameCounter++;
                if (frameCounter > 6) {
                    frameCounter = 0;
                    frame++;
                    if (frame > 7)
                        frame = 1;
                }
            }
        }

        public override bool RightClick(int i, int j) {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Mech);
            paused = !paused;
            return true;
        }

        public override void MouseOver(int i, int j) {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ItemType<Provibust>();
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX == 0 && !paused) {
                r = 0.8f;
                g = 0.45f;
                b = 0.23f;
            }
        }

        public override void HitWire(int i, int j) {
            int x = i - Main.tile[i, j].TileFrameX / 16 % 7;
            int y = j - Main.tile[i, j].TileFrameY / 16 % 6;

            if (Wiring.running) {
                for (int l = 1; l < 7; l++)
                    Wiring.SkipWire(x, y + l);

                for (int m = 1; m < 6; m++)
                    Wiring.SkipWire(x + m, y);

                for (int n = 1; n < 6; n++) {
                    for (int o = 1; o < 7; o++)
                        Wiring.SkipWire(x + n, y + o);
                }
            }

            RightClick(x, y);
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch) =>
            CalValEXGlobalTile.TileGlowmask(i, j, Request<Texture2D>("CalValEX/Tiles/Statues/ProfanedIdolPlaced_Glow").Value, 
                spriteBatch, AnimationFrameHeight);

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;
    }
}