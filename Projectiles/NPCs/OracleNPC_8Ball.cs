using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Projectiles.NPCs
{
    public class OracleNPC_8Ball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Oracle's Magic 8 Ball");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.damage = 10;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.knockBack = 2f;
            Projectile.aiStyle = ProjAIStyleID.GroundProjectile;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] > 60)
            {
                Projectile.velocity.Y += 0.1f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            int killdust = 0;
            while (killdust < 9)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water, -Projectile.velocity.X * 0.05f, -Projectile.velocity.Y * 0.05f, 50, default, 1f);
                killdust += 1;
            }
        }
    }
}