using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Metadata;

namespace CalValEX.Tiles.Blocks
{
    public class MoldyWoodPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.GlowingMushroom;
            AddMapEntry(new Color(61, 122, 103));
        }
    }
}