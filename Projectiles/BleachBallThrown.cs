using Terraria;
using Terraria.ModLoader;
using CalValEX.Items;
using Terraria.ID;

namespace CalValEX.Projectiles
{
    public class BleachBallThrown : ModProjectile {
        public override void SetDefaults() {
            Projectile.netImportant = true;
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.aiStyle = ProjAIStyleID.BeachBall;
            Projectile.friendly = true;
        }

        public override void OnKill(int timeLeft) {
            Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ModContent.ItemType<BleachBallItem>());
        }
    }
}