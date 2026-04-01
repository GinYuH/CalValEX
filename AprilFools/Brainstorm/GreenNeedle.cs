using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.AprilFools.Brainstorm
{
    public class GreenNeedle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 0;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 36;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = Item.CommonMaxStack;
        }
    }
}