using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Tiles.Blocks
{
    public class NecrostonePlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<Necrostone>();
            AddMapEntry(new Color(108, 59, 16));
        }
    }
}