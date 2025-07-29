using Terraria.ID;
using CalValEX.Tiles.MiscFurniture;
using Terraria.ModLoader;
using Terraria;

namespace CalValEX.Items.Tiles
{
    public class FamiliarPot : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<FamiliarPotPlaced>());
            Item.rare = ItemRarityID.Blue;
            if (CalValEX.FablesActive)
            {
                Item.rare = CalValEX.Fables.Find<ModRarity>("CursedRarity").Type;
            }
        }
    }

    public class FamiliarUrnPot : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<FamiliarUrnPotPlaced>());
            Item.rare = ItemRarityID.Blue;
            if (CalValEX.FablesActive)
            {
                Item.rare = CalValEX.Fables.Find<ModRarity>("CursedRarity").Type;
            }
        }
    }

    public class FamiliarTallPot : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<FamiliarTallPotPlaced>());
            Item.rare = ItemRarityID.Blue;
            if (CalValEX.FablesActive)
            {
                Item.rare = CalValEX.Fables.Find<ModRarity>("CursedRarity").Type;
            }
        }
    }

    public class FamiliarSkinnyPot : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<FamiliarSkinnyPotPlaced>());
            Item.rare = ItemRarityID.Blue;
            if (CalValEX.FablesActive)
            {
                Item.rare = CalValEX.Fables.Find<ModRarity>("CursedRarity").Type;
            }
        }
    }
}