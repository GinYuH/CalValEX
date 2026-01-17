using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.AprilFools
{
	public class NyanthropProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 17f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 600f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15f;
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = ProjAIStyleID.Yoyo;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 1f;
		}

        public override void AI()
        {
			if (Main.rand.NextBool(5)) 
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Main.rand.Next(-4,4), Main.rand.Next(-4, 4), ProjectileID.Typhoon, 20, 2f, Projectile.owner);
            }
        }
    }
}