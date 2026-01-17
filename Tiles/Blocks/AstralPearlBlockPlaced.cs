using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Tiles.Blocks
{
    public class AstralPearlBlockPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<AstralPearlBlock>();
            DustType = DustID.ShadowbeamStaff;
            AddMapEntry(new Color(183, 69, 60));
            Main.tileBlendAll[this.Type] = true;
            HitSound = SoundID.Item50;
        }
    }
}