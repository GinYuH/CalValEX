using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.Rubble
{
    public class BunkerBreakerBox : ModTile
    {
        public static int WulfrumDull => CalamityID.CalamityID.ItemRelation("WulfrumMetalScrap", "DullPlatingItem", ItemID.TinBrick);
        public override void SetStaticDefaults()
        {
            this.SetupFurniture(2, 2, true);
            FlexibleTileWand.RubblePlacementMedium.AddVariation(WulfrumDull, Type, 0);
            RegisterItemDrop(WulfrumDull);
        }
    }
}