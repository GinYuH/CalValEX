using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Tiles.Blocks
{
    public class FrostflakeBrickPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            //ItemDrop = ModContent.ItemType<FrostflakeBrick>();
            AddMapEntry(new Color(66, 242, 245));
            DustType = DustID.Frost;
            MinPick = 65;
            HitSound = SoundID.Tink;
        }
    }
}