using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
    public class EngineeredComputer : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 18;

            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);

            TileObjectData.addTile(Type);

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
            
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(110, 52, 52), name);
            
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            //sorry for not using the helper method, I needed to do some custom drawing due CoordinateWidth = 18 instead of 16
            Tile tile = Main.tile[i, j];

            float pulse = 0.75f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f) * 0.25f;
            pulse = MathHelper.Clamp(pulse, 0.8f, 1f);

            Color color = Color.White * pulse; // Adjust brightness based on pulse

            Texture2D glowTexture = Request<Texture2D>("CalValEX/Tiles/FurnitureSets/Engineered/EngineeredComputerGlow").Value;

            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);

            // Calculate the horizontal offset. 
            int xOffset = -1; 
            
            Vector2 drawPosition = new Vector2(i * 16 + xOffset - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;

            Rectangle frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 18, 16);

            spriteBatch.Draw(glowTexture, drawPosition, frame, color , 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

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