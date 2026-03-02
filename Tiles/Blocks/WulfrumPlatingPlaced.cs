using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;


namespace CalValEX.Tiles.Blocks
{
    public class WulfrumPlatingPlaced : ModTile {
		public override void SetStaticDefaults()  {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileShine2[Type] = true;
            TileID.Sets.GemsparkFramingTypes[Type] = Type;
			
			HitSound = SoundID.Tink;
			//ItemDrop = ItemType<WulfrumPlating>();
			
			AddMapEntry(new Color(149, 222, 168));
		}
		public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Framing.SelfFrame8Way(i, j, Main.tile[i, j], resetFrame);
            return false;
        }

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch) 
		{
            Tile tile = Main.tile[i, j];

            Texture2D glowTexture = Request<Texture2D>("CalValEX/Tiles/Blocks/WulfrumPlatingPlaced_Glow").Value;

            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;

            Rectangle frame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 18, 16);

            spriteBatch.Draw(glowTexture, drawPosition, frame, Color.White * 0.75f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (j < Main.maxTilesY - 1) 
			{
				Tile tileDown = Main.tile[i, j + 1];

				if (tileDown.TileType != Type)
				{
						r = 0.18f;
						g = 0.47f;
						b = 0.703f;
				}
			}
		}
	}
}