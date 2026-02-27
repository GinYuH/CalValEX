using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
    public class EngineeredWallClock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 }; //
            TileObjectData.newTile.CoordinatePadding = 2;
            AnimationFrameHeight = 36;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(110, 52, 52), name);
            DustType = DustID.Clay;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter > 60) //make this number lower/bigger for faster/slower animation
            {
                frameCounter = 0;
                frame++;
                if (frame > 7)
                {
                    frame = 0;
                }
            }
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            CalValEXGlobalTile.TileGlowmask(i, j, Request<Texture2D>("CalValEX/Tiles/FurnitureSets/Engineered/EngineeredWallClockGlow").Value, spriteBatch, AnimationFrameHeight);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            float pulse = 0.75f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f) * 0.25f;
            pulse = MathHelper.Clamp(pulse, 0.8f, 1f);

            float noise = Main.rand.NextFloat(0.85f, 1f);

            r = ((3f / 155f) * pulse) * (noise * 0.5f);
            g = ((67f / 155f) * pulse) * (noise * 0.5f);
            b = ((146f / 155f) * pulse) * (noise * 0.5f);
        }
    }
}