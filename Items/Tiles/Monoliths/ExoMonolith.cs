using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.Monoliths;

namespace CalValEX.Items.Tiles.Monoliths
{
    public class ExoMonolith : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gunmetal Mechanism");
            Item.ResearchUnlockCount = 1;
            //Tooltip.SetDefault("Releases lightning charged nanomachines into the atmosphere\n" + "Cannot be used if any other Calamity monoliths are currently active\n"+"UNOBTAINABLE UNFINISHED ITEM");
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
            Item.rare = CalamityID.CalRarityID.Exo;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.createTile = ModContent.TileType<ExoMonolithPlaced>();
        }
    }
}