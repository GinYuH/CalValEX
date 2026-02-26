using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;
using CalamityMod.Items.Placeables.DraedonStructures;

namespace CalValEX.Items.Tiles.FurnitureSets.Engineered
{
    public class EngineeredWorkBenchItem : ModItem
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
            Item.createTile = ModContent.TileType<EngineeredWorkBench>();
            Item.width = 32;
            Item.height = 16;
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredWorkBenchItem>());
            recipe.AddIngredient(ModContent.ItemType<LaboratoryPanels>(), 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}