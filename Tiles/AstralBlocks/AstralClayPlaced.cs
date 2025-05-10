using Terraria;
using Terraria.ModLoader;
using CalValEX.Dusts;

namespace CalValEX.Tiles.AstralBlocks
{
    public class AstralClayPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            DustType = ModContent.DustType<AstralDust>();
            //ItemDrop = ModContent.ItemType<AstralClay>();
            AddMapEntry(new Color(78, 45, 91));
            Main.tileBlendAll[this.Type] = true;
            Main.tileMerge[Type][ModContent.TileType<AstralDirtPlaced>()] = true;
            Main.tileMerge[Type][ModContent.TileType<XenostonePlaced>()] = true;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        /*public override void ChangeWaterfallStyle(ref int style) {
			style = mod.GetWaterfallStyleSlot("AstralWaterfallStyle");
		}*/
    }
}