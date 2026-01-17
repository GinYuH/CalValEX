using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Tiles.Blocks
{
    public class AstralPlatingPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<AstralPlating>();
            AddMapEntry(new Color(101, 171, 167));
            DustType = DustID.ShadowbeamStaff;
        }
    }
}