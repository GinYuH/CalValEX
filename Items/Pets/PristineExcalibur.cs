using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Pets
{
    public class PristineExcalibur : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Compensation");
            /* Tooltip
                .SetDefault("Can be sold for a decent price"); */
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Gray;
        }
    }
}