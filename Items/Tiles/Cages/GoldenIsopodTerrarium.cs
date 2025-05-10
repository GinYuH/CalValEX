using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.Cages;

namespace CalValEX.Items.Tiles.Cages
{
    public class GoldenIsopodTerrarium : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gold Isopod Cage");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<GoldenIsopodTerrariumPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = CalamityID.CalRarityID.PureGreen;
            Item.value = Item.sellPrice(0, 20, 0, 0);
        }
    }
}