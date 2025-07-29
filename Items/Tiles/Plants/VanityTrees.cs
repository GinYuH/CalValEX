using Terraria.ID;
using CalValEX.Tiles.MiscFurniture;
using Terraria.ModLoader;
using Terraria;
using CalValEX.Tiles.Plants;
using Terraria.GameContent;

namespace CalValEX.Items.Tiles.Plants
{
    public class CrepuscularMallow : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<CrepuscularMallowPlaced>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 5);
        }
    }
    public class ArcticWisteria : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<ArcticWisteriaPlaced>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 5);
        }
    }
    public class PaleblotBane : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<PaleblotBanePlaced>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 5);
        }
    }
    public class MarrowWillow : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<MarrowWillowPlaced>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 5);
        }
    }
    public class SpiderPalm : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SpiderPalmPlaced>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 5);
        }
    }
    public class EverfrostPine : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<EverfrostPinePlaced>());
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 5);
        }
    }
}