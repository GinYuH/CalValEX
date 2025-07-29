using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Metadata;

namespace CalValEX.Tiles.Blocks
{
    public class WulfrumBeamPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.Tungsten;
            AddMapEntry(new Color(32, 51, 37));
            TileID.Sets.IsBeam[Type] = true;
        }
    }
}