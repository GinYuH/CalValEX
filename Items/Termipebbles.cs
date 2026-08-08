using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items
{
    public class Termipebbles : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Termipebbles");
            // Tooltip.SetDefault("Used to craft Terminus-themed vanities\n" + "'Do NOT eat.'");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = false;
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 36;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.maxStack = 9999;
            Item.rare = CalamityID.CalRarityID.Auric;
        }
    }
}