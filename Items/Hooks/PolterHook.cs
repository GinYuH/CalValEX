using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Items.Hooks
{
    public class PolterHook : ModItem {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.BatHook);
            Item.value = Item.sellPrice(1, 1, 0, 0);
            Item.shootSpeed = 16f;
            Item.shoot = ProjectileType<PhantomHook>();
            Item.rare = CalamityID.CalRarityID.PureGreen;
        }
    }
}