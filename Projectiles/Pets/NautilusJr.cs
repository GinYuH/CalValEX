using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace CalValEX.Projectiles.Pets
{
    public class NautilusJr : ModFlyingPet
    {
        public override float TeleportThreshold => 1440f;

        public override void SetStaticDefaults()
        {
            PetSetStaticDefaults(lightPet: false);
            Main.projFrames[Projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            PetSetDefaults();
            Projectile.width = 40;
            Projectile.height = 18;
            Projectile.ignoreWater = true;
            Projectile.GetGlobalProjectile<CalValEXGlobalProjectile>().isCalValPet = true;
        }

        public override void Animation(int state)
        {
            if (Projectile.ai[1] <= 0)
            {
                Projectile.frameCounter++;
                if (Projectile.frameCounter > 6)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame += 1;
                }
                if (Projectile.frame >= 8)
                {
                    Projectile.frame = 0;
                }
            }
            else
            {
                Projectile.frame = 8;
            }
        }

        public override void PetFunctionality(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            Projectile.ai[0]++;
            if (Projectile.ai[0] > 300)
            {
                if (Main.rand.NextBool(300))
                {
                    Projectile.ai[0] = 0;
                    Projectile.ai[1] = 40;
                }
            }

            if (Projectile.ai[1] > 0 && Projectile.ai[0] % 10 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item87 with { Pitch = 0.5f, Volume = 0.8f }, Projectile.Center);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Gore bubble = Gore.NewGorePerfect(Projectile.GetSource_FromAI(), Projectile.Center + Vector2.UnitX * Projectile.direction * 10, new Vector2(Projectile.direction * 4, 0).RotatedByRandom(MathHelper.ToRadians(60f)) * 0.5f, 411);
                    bubble.timeLeft = 9 + Main.rand.Next(7);
                    bubble.scale = Main.rand.NextFloat(0.6f, 1f);
                    bubble.type = Main.rand.NextBool(3) ? 412 : 411;
                }
            }

            Projectile.ai[1]--;

            if (player.dead)
                modPlayer.nautilus = false;

            if (modPlayer.nautilus)
                Projectile.timeLeft = 2;
        }
    }
}