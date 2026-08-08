using Terraria.ModLoader;
using Terraria.ID;
using Terraria;

namespace CalValEX.AprilFools.Jharim
{
    public class FoolEX : ModItem
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
            Item.createTile = ModContent.TileType<FoolEXPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.value = Item.buyPrice(gold: 22);
            Item.rare = CalamityID.CalRarityID.Auric;
        }
    }
}