using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Tiles.Blocks
{
    public class AzufreSludgePlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<AzufreSludge>();
            AddMapEntry(new Color(242, 202, 39));
            DustType = DustID.CursedTorch;
            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Sticky"]);
        }
    }
}