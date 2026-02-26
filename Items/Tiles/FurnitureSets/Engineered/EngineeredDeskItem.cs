using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;
using CalamityMod.Items.Placeables.DraedonStructures;

namespace CalValEX.Items.Tiles.FurnitureSets.Engineered
{
    public class EngineeredDeskItem : ModItem
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
            Item.createTile = ModContent.TileType<EngineeredDesk>();
            Item.width = 30;
            Item.height = 22;
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredDeskItem>());
            recipe.AddIngredient(ModContent.ItemType<LaboratoryPanels>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}