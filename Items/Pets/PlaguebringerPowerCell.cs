using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Pets
{
    [LegacyName("BeeCan")]
    public class PlaguebringerPowerCell : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Plaguebringer Power Cell");
            // Tooltip.SetDefault("Full of vitamin Bee!\n" + "Summons a miniature Plaguebringer");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Gray;
        }
    }
}