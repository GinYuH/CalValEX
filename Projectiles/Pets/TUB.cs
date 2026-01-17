using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Projectiles.Pets
{
    public class TUB : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("TUB");
            Main.projFrames[Projectile.type] = 11;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(334);
            AIType = ProjectileID.Puppy;
            DrawOriginOffsetY = -14;
            DrawOffsetX = -10;
            Projectile.GetGlobalProjectile<CalValEXGlobalProjectile>().isCalValPet = true;
        }

        public override bool PreAI()
        {
            _ = Main.player[Projectile.owner];
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
            if (player.dead)
            {
                modPlayer.tub = false;
            }
            if (modPlayer.tub)
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}