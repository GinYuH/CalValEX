using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using CalValEX.Tiles.MiscFurniture;

namespace CalValEX.Items.Tiles
{
    public class HeartoftheCommunity : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heart of the Community");
            /* Tooltip
                .SetDefault("My greatest creation, handle it with care"); */
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
            Item.createTile = ModContent.TileType<HeartoftheCommunityPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 0, 40, 0);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //rarity 12 (Turquoise) = new Color(0, 255, 200)
            //rarity 13 (Pure Green) = new Color(0, 255, 0)
            //rarity 14 (Dark Blue) = new Color(43, 96, 222)
            //rarity 15 (Violet) = new Color(108, 45, 199)
            //rarity 16 (Hot Pink/Developer) = new Color(255, 0, 255)
            //rarity rainbow (no expert tag on item) = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
            //rarity rare variant = new Color(255, 140, 0)
            //rarity dedicated(patron items) = new Color(139, 0, 0)
            //look at https://calamitymod.gamepedia.com/Rarity to know where to use the colors
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB); //change the color accordingly to above
                }
            }
        }
    }
}