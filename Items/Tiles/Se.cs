using Terraria.ModLoader;
using Terraria.ID;
using CalValEX.Tiles.MiscFurniture;
using Terraria;

namespace CalValEX.Items.Tiles
{
    public class Se : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tubeman of Entropy");
            // Tooltip.SetDefault("'Embrace chaos'\n"+"Inflates into a flailing Sepulcher balloon\n");
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
            Item.width = 16;
            Item.height = 28;
            Item.rare = CalamityID.CalRarityID.CalamityRed;
            Item.createTile = ModContent.TileType<SePlaced>();
        }
    }
}
