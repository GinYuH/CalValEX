using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.Items.Materials;

namespace CalValEX.Items.Tiles.FurnitureSets.Engineered
{
    public class EngineeredWallClockItem : ModItem
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
            Item.createTile = ModContent.TileType<EngineeredWallClock>();
            Item.width = 26;
            Item.height = 22;
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredWallClockItem>());
            recipe.AddIngredient(ModContent.ItemType<LaboratoryPanels>(), 6);
            recipe.AddIngredient(ModContent.ItemType<MysteriousCircuitry>(), 2);
            recipe.AddIngredient(ItemID.Glass, 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}