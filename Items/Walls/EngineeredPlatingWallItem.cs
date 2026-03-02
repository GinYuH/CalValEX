using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Walls;
using Terraria;
using CalValEX.Items.Tiles.Blocks;

namespace CalValEX.Items.Walls
{
    public class EngineeredPlatingWallItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wulfrum Panel Wall");
            Item.ResearchUnlockCount = 400;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.White;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = ModContent.WallType<EngineeredPlatingWall>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredPlatingWallItem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EngineeredPlatingItem>(), 1);
            recipe.Register();
        }
    }
}