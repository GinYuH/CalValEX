using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Items.Hooks
{
    public class XMLightningHook : ModItem
    {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.StaticHook);
            Item.shootSpeed = 62f;
            Item.shoot = ProjectileType<THanosHook>();
            Item.rare = CalamityID.CalRarityID.Exo;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(BuffID.ChaosState))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.whoAmI == Main.myPlayer)
                player.AddBuff(BuffID.ChaosState, 300);
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}