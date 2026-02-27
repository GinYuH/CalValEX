using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;
using System;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Tiles.Blocks
{
    public class HolographicGlass : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = false;
            Main.tileBrick[Type] = true;
            Main.tileNoSunLight[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
            AddMapEntry(new Color(157, 63, 63));
            TileID.Sets.GemsparkFramingTypes[Type] = Type;
            HitSound = SoundID.Tink;
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
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        { 
            Tile tile = Main.tile[i, j];
            Texture2D texture = Request<Texture2D>("CalValEX/Tiles/Blocks/HolographicGlass").Value;
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;
            spriteBatch.Draw(texture, drawPosition, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), Lighting.GetColor(i, j));
            return false;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            int xFrameOffset = Main.tile[i, j].TileFrameX;
            int yFrameOffset = Main.tile[i, j].TileFrameY;

            int animationOffsetY = Main.tileFrame[Type] * 90;

            Texture2D glowmask = ModContent.Request<Texture2D>("CalValEX/Tiles/Blocks/HolographicGlass").Value;
            
			Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
			Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
            
			Color drawColour = Color.White;

			Rectangle sourceRect = new Rectangle(xFrameOffset, yFrameOffset + animationOffsetY, 16, 16);

			Tile trackTile = Main.tile[i, j];
			if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
			{
				spriteBatch.Draw(glowmask, drawPosition, sourceRect, drawColour, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			}
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
            return false;
        }
        public override bool CreateDust(int i, int j, ref int type)
        {
            Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, 57 + Main.rand.Next(2), 0f, 0f, 1, new Color(255, 255, 255));
            return false;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}
