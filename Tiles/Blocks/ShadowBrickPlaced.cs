using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Tiles.Blocks
{
    public class ShadowBrickPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<ShadowBrick>();
            AddMapEntry(new Color(47, 1, 51));
            DustType = DustID.BlueFlare;
        }
    }
}