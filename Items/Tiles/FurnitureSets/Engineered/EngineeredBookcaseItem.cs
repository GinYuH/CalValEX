using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.FurnitureSets.Engineered;
using CalamityMod.Items.Placeables.DraedonStructures;
using CalamityMod.Items.Materials;
using CalValEX.Items.Tiles.Blocks;
using CalamityMod.Items.DraedonMisc;

namespace CalValEX.Items.Tiles.FurnitureSets.Engineered
{
    public class EngineeredBookcaseItem : ModItem
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
            Item.createTile = ModContent.TileType<EngineeredBookcase>();
            Item.width = 24;
            Item.height = 32;
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredBookcaseItem>());
            recipe.AddIngredient(ModContent.ItemType<EngineeredPlatingItem>(), 15);
            recipe.AddIngredient(ModContent.ItemType<HolographicGlassItem>(), 5);
            recipe.AddIngredient(ModContent.ItemType<MysteriousCircuitry>(), 3);
            recipe.AddIngredient(ItemID.Book, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}