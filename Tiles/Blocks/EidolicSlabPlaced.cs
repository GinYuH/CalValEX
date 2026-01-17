using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace CalValEX.Tiles.Blocks
{
    public class EidolicSlabPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<EidolicSlab>();
            AddMapEntry(new Color(0, 76, 82));
            DustType = DustID.BlueFlare;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            CalValEXGlobalTile.TileGlowmask(i, j, ModContent.Request<Texture2D>("CalValEX/Tiles/Blocks/EidolicSlabPlaced_Glow").Value, spriteBatch);
        }
    }
}