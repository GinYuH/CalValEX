using Terraria.ModLoader;
using Terraria.ID;
using CalValEX.Tiles.MiscFurniture;

namespace CalValEX.Items.Tiles
{
    public class BunkerPlatform : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<BunkerPlatformPlaced>());
        }
    }
}