using Terraria.ModLoader;
using CalValEX.Tiles.Paintings;
using Terraria.ID;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ReLogic.Content;

namespace CalValEX.Items.Tiles.Paintings
{
    [ExtendsFromMod("UnCalamityModMusic")]
    public class UCMM : ModItem
    {
        public override string Texture => "UnCalamityModMusic/icon";
        public override bool IsLoadingEnabled(Mod mod) => ModLoader.HasMod("UnCalamityModMusic");
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<UCMMPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = CalamityID.CalRarityID.Auric;
        }
    }
}