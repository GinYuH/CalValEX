using Terraria.ModLoader;
using CalValEX.Tiles.Paintings;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ReLogic.Content;

namespace CalValEX.Items.Tiles.Paintings
{
    [ExtendsFromMod("CalamityMod")]
    public class CalamityPaintRetold : ModItem
    {
        public override string Texture => "CalamityMod/icon";

        public override bool IsLoadingEnabled(Mod mod) => ModLoader.HasMod("CalamityMod");
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<CalamityPaintRetoldRetoldPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = CalamityID.CalRarityID.Auric;
        }
    }
}