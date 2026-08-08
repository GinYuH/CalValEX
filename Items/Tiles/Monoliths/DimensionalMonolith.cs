using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.Monoliths;

namespace CalValEX.Items.Tiles.Monoliths
{
    public class DimensionalMonolith : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dimensional Obelisk");
            // Tooltip.SetDefault("It appears to be trying to signal something, with no success");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = CalamityID.CalRarityID.Cosmic;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.createTile = ModContent.TileType<DimensionalMonolithPlaced>();
        }
    }
}