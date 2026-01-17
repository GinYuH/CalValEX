using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Tiles.Blocks
{
    public class AuricBrickPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<AuricBrick>();
            HitSound = SoundID.Tink;
            AddMapEntry(new Color(242, 202, 39));
            DustType = DustID.Teleporter;
            //minPick = 249;
        }
    }
}