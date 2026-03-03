using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.Blocks;
using Terraria;
using CalamityMod.Items.Materials;

namespace CalValEX.Items.Tiles.Blocks
{
    public class EngineeredContructPanelsItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.rare = ItemRarityID.White;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<EngineeredContructPanels>();
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<EngineeredContructPanelsItem>(), 25);
            recipe.AddIngredient(ItemID.IronBar, 1);
            recipe.AddIngredient(ItemID.ClayBlock, 25);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
        }
    }
}