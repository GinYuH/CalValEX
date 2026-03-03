using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalValEX.Items.Tiles.Blocks;

namespace CalValEX.Items.Tiles.FurnitureSets.Engineered
{
    public class EngineeredPlatformItem : ModItem
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
            Item.createTile = ModContent.TileType<EngineeredPlatform>();
            Item.width = 32;
            Item.height = 16;
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredPlatformItem>());
            recipe.AddIngredient(ModContent.ItemType<EngineeredPlatingItem>());
            recipe.Register();
        }
    }
}