using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.NPCs.Critters;

namespace CalValEX.Items.Critters
{
    public class GodSlayerSlugItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.GlowingSnail);
            Item.bait = 60;
            Item.makeNPC = (short)NPCType<GodSlayerSlug>();
            Item.rare = CalamityID.CalRarityID.Cosmic;
        }
    }
}