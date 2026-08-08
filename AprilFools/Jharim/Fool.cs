using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace CalValEX.AprilFools.Jharim
{
    public class Fool : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<FoolPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.value = Item.buyPrice(silver: 22);
            Item.rare = CalamityID.CalRarityID.Auric;
        }
    }
}