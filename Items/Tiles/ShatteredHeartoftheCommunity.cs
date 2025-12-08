using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using CalValEX.Tiles.MiscFurniture;

namespace CalValEX.Items.Tiles
{
    public class ShatteredHeartoftheCommunity : ModItem
    {
        private static readonly Color rarityColorOne = new Color(128, 62, 128);
        private static readonly Color rarityColorTwo = new Color(245, 105, 245);
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ShatteredHeartoftheCommunityPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = CalamityRarity.Violet;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = CVUtils.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
                }
            }
        }
    }
}