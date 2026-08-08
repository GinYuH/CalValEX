using Terraria.ModLoader;
using CalValEX.Tiles.Paintings;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ReLogic.Content;

namespace CalValEX.Items.Tiles.Paintings
{
    public class ModIconPainting : ModItem
    {
        public override string Texture => "CalValEX/icon";
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ModIconCVPaintingPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = CalamityID.CalRarityID.Violet;
        }
    }
}