﻿using Microsoft.Xna.Framework;
using Terraria;

namespace CalValEX.Projectiles.Pets
{
    public class CoolBlueSlime : FlyingPet
    {
        public Player Owner => Main.player[projectile.owner];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cool Blue Slime God");
            Main.projPet[projectile.type] = true;
        }

        public override void SafeSetDefaults() //SafeSetDefaults!!!
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.ignoreWater = true;
            facingLeft = false;
            spinRotation = false;
            shouldFlip = false;
            usesAura = false;
            usesGlowmask = false;
            auraUsesGlowmask = false;
        }

        public override void SetUpFlyingPet()
        {
            distance[0] = 1840f; //teleport distance
            distance[1] = 560f; //faster speed distance
            speed = Owner.GetModPlayer<CalValEXPlayer>().TubRune ? 10f : 12f;
            inertia = 60f;
            animationSpeed = 30;
            spinRotationSpeedMult = 0.2f;
            offSetX = 56f * -Main.player[projectile.owner].direction;
            offSetY = -50f;
        }

        public override void SetUpAuraAndGlowmask() //for aura and glowmasks
        {
            auraTexture = "Projectiles/Pets/SlimeAura";
            auraRotates = true;
            auraRotation = true;
            auraRotationSpeedMult = 0.05f;

            glowmaskTexture = "";
            auraGlowmaskTexture = "";
        }

        public override void SetUpLight() //for when the pet emmits light
        {
            shouldLightUp = false;
            RGB = new Vector3(1, 1, 1);
            intensity = 1f;
            abyssLightLevel = 0;
        }

        public override void SafeAI(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.bSlime = false;
            if (modPlayer.bSlime)
                projectile.timeLeft = 2;
        }
    }
}