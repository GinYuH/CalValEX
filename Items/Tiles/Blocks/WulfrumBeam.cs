using Terraria.ModLoader;
using Terraria.ID;
using CalValEX.Tiles.Blocks;

namespace CalValEX.Items.Tiles.Blocks
{
    public class WulfrumBeam : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<WulfrumBeamPlaced>());
        }
    }
}