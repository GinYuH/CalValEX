using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Projectiles.Pets
{
    public class FogPet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fogbound");
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(197);
            AIType = ProjectileID.BabySkeletronHead;
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
                modPlayer.fog = false;
            }
            if (modPlayer.fog)
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}