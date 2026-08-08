using Terraria.ID;
using CalValEX.Tiles.MiscFurniture;
using Terraria.ModLoader;
using Terraria;
using CalValEX.CalamityID;

namespace CalValEX.Items.Tiles
{
    public class CalamityLamp : ModItem {
        public override void SetStaticDefaults() {
            // DisplayName.SetDefault("Brimstone Flambeau");
            // Tooltip.SetDefault("Something malicious is brewing");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults() {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<CalamityLampPlaced>();
            Item.width = 32;
            Item.height = 50;
            Item.rare = CalRarityID.CalamityRed;
        }
    }
}