using System;
using Terraria;

namespace CalValEX.Projectiles.Pets
{
    public class Dstone : ModWalkingPet
    {
        public override bool ShouldFlip => false;

        public override bool AllowRotationReset => false;

        public override bool ShouldFlyRotate => false;

        public override float BackToFlyingThreshold => 548f;

        public override float WalkingSpeed => 17f;

        public override void SetStaticDefaults()
        {
            PetSetStaticDefaults(lightPet: false);
            // DisplayName.SetDefault("Unhappy Stone");
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.GetGlobalProjectile<CalValEXGlobalProjectile>().isCalValPet = true;
        }

        public override void ModifyJumpHeight(ref float oneTileHigherAndNotTwoTilesHigher, ref float twoTilesHigher, ref float fourTilesHigher, ref float fiveTilesHigher, ref float anyOtherJump)
        {
            oneTileHigherAndNotTwoTilesHigher = -4f;
            twoTilesHigher = -6f;
            fiveTilesHigher = -8f;
            fourTilesHigher = -7f;
            anyOtherJump = -6.5f;
        }

        public override void Animation(int state)
        {
        }

        public override void CustomBehaviour(Player player, ref int state, float walkingSpeed, float walkingInertia, float flyingSpeed, float flyingInertia)
        {
            Projectile.rotation += Projectile.velocity.X * MathHelper.Lerp(0.1f, 0.5f, 1 - (player.statLife / (float)player.statLifeMax2));
        }

        public override void PetFunctionality(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.Dstone = false;

            if (modPlayer.Dstone)
                Projectile.timeLeft = 2;
        }
    }
}

