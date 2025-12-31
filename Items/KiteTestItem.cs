using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items
{
    public class KiteTestItem : ModItem
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.KiteShark;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsAKite[Type] = true;
            ItemID.Sets.HasAProjectileThatHasAUsabilityCheck[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.KiteShark);
            Item.shoot = ModContent.ProjectileType<Projectiles.KiteTest>();
        }

        public override bool CanUseItem(Player player)
        {
            return !player.pulley && player.ownedProjectileCounts[Item.shoot] <= 0;
        }

        public override void HoldItem(Player player)
        {
            Item.holdStyle = 0;
            if (!player.ItemTimeIsZero || player.itemAnimation != 0)
            {
                return;
            }
            foreach (Projectile p in Main.ActiveProjectiles)
            {
                if (p.active && p.owner == player.whoAmI && p.type == Item.shoot)
                {
                    Item.holdStyle = 1;
                    player.ChangeDir((!(p.Center.X - player.Center.X < 0f)) ? 1 : (-1));
                }
            }
        }
    }
}