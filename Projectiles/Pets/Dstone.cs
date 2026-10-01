using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

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
            Main.projFrames[Projectile.type] = 2;
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
            if (Projectile.ai[1] == 1)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 0;
            }
        }

        public override void CustomBehaviour(Player player, ref int state, float walkingSpeed, float walkingInertia, float flyingSpeed, float flyingInertia)
        {
            Projectile.rotation += Projectile.velocity.X * MathHelper.Lerp(0.1f, 0.5f, 1 - (player.statLife / (float)player.statLifeMax2));

            if (Projectile.ai[0] <= 0 && player.statLife < player.statLifeMax2 / 2 && Projectile.ai[1] == 0)
            {
                SoundEngine.PlaySound(SoundID.Item89 with { Pitch = 2 }, Projectile.Center);
                Projectile.ai[0] = 300;
                Projectile.ai[1] = 1;
                if (Projectile.velocity.Y == 0)
                {
                    Projectile.velocity.Y = -6f;
                }
            }
            else if (player.statLife >= player.statLifeMax2 / 2 && Projectile.ai[0] <= 0&& Projectile.ai[1] == 1)
            {
                SoundEngine.PlaySound(SoundID.Item89 with { Pitch = 1 }, Projectile.Center);
                Projectile.ai[0] = 300;
                Projectile.ai[1] = 0;
                if (Projectile.velocity.Y == 0)
                {
                    Projectile.velocity.Y = -6f;
                }
            }
            Projectile.ai[0]--;
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

