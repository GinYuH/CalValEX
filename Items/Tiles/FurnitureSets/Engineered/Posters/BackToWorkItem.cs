using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalValEX.Tiles.FurnitureSets.Engineered.Posters;

namespace CalValEX.Items.Tiles.FurnitureSets.Engineered.Posters
{
    public class BackToWorkItem : ModItem
    {
        public override void SetStaticDefaults()
        {
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
            Item.createTile = ModContent.TileType<BackToWork>();
            Item.width = 28;
            Item.height = 28;
            Item.rare = ItemRarityID.White;
        }
    }
}