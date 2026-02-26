using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;
using CalamityMod.Items.Placeables.DraedonStructures;

namespace CalValEX.Items.Tiles.FurnitureSets.Engineered
{
    public class EngineeredLockerItem : ModItem
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
            Item.createTile = ModContent.TileType<EngineeredLocker>();
            Item.width = 16;
            Item.height = 28;
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredLockerItem>());
            recipe.AddIngredient(ModContent.ItemType<LaboratoryPanels>(), 8);
            recipe.AddIngredient(ItemID.IronBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}