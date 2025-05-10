using Terraria;

namespace CalValEX.Projectiles.Pets
{
    public class AeroBaby : ModFlyingPet
    {
        public override Vector2 FlyingOffset => new(38f * -Main.player[Projectile.owner].direction, -70f);

        public override float TeleportThreshold => 1440f;

        public override float FlyingSpeed => 16f;

        public override void SetStaticDefaults()
        {
            PetSetStaticDefaults(lightPet: false);
            // DisplayName.SetDefault("Aero Baby");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            PetSetDefaults();
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.ignoreWater = true;
            Projectile.GetGlobalProjectile<CalValEXGlobalProjectile>().isCalValPet = true;
        }

        public override void Animation(int state)
        {
            SimpleAnimation(speed: 12);
        }
        public override void PetFunctionality(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.mAero = false;

            if (modPlayer.mAero)
                Projectile.timeLeft = 2;}
    }
}