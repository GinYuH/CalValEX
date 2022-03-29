﻿using Terraria;
using Microsoft.Xna.Framework;

namespace CalValEX.Projectiles.Pets
{
    public class CoolBlueSignut : FlyingPet
    {
        public Player Owner => Main.player[projectile.owner];
        private bool sigtep = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cool Blue Signut");
            Main.projFrames[projectile.type] = 4; //frames
            Main.projPet[projectile.type] = true;
        }

        public override void SafeSetDefaults() //SafeSetDefaults!!!
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.ignoreWater = true;
            /* you don't need to set these anymore!
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            projectile.tileCollide = false;
            */
            facingLeft = true; //is the sprite facing left? if so, put this to true. if its facing to right keep it false.
            spinRotation = false; //should it spin? if that's the case, set to true. else, leave it false.
            shouldFlip = true; //should the sprite flip? set true if it should, false if it shouldnt
            usesAura = false; //does this pet use an aura?
            usesGlowmask = false; //does this pet use a glowmask?
            auraUsesGlowmask = false; //does the aura use a glowmask?
        }

        public override void SetUpFlyingPet()
        {
            distance[0] = 1440f; //teleport distance
            distance[1] = 560f; //faster speed distance
            speed = Owner.GetModPlayer<CalValEXPlayer>().TubRune ? 10f : 12f;
            inertia = 60f;
            animationSpeed = 12; //how fast the animation should play
            spinRotationSpeedMult = 0.2f; //rotation speed multiplier, keep it positive for it to spin in the right direction
            offSetX = 90f * -Main.player[projectile.owner].direction; //this is needed so it's always behind the player.
            offSetY = -75f; //how much higher from the center the pet should float
        }

        //you usualy don't have to use the lower two unless you want the pet to have an aura, glowmask
        //or if you want the pet to emit light

        public override void SafeAI(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.bSignut = false;
            if (modPlayer.bSignut)
                projectile.timeLeft = 2;

            /* THIS CODE ONLY RUNS AFTER THE MAIN CODE RAN.
             * for custom behaviour, you can check if the projectile is walking or not via projectile.localAI[1]
             * you should make new custom behaviour with numbers higher than 0, or less than 0
             * the next few lines is an example on how to implement this
             *
             * switch ((int)projectile.localAI[1])
             * {
             *     case -1:
             *         break;
             *     case 1:
             *         break;
             * }
             *
             * 0 is already in use.
             * 0 = flying
             *
             * you can still use this, changing thing inside (however it's not recomended unless you want to add custom behaviour to this)
             */
            Player owner = Main.player[projectile.owner];
            Vector2 vectorToOwner = owner.Center - projectile.Center;
            float distanceToOwner = vectorToOwner.Length();
            if (distanceToOwner > 1138)
            {
                sigtep = true;
            }
            if (distanceToOwner < 1139)
            {
                sigtep = false;
                projectile.alpha--;
                projectile.alpha--;
            }

            if (projectile.alpha == 45)
            {
                for (int x = 0; x < 20; x++)
                {
                    Dust dust;
                    dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 30, 30, 173, 0f, 0f, 0, new Color(255, 255, 255), 0.8f)];
                }
            }

            if (sigtep == true)
            {
                projectile.alpha = 255;
                for (int x = 0; x < 5; x++)
                {
                    Dust dust;
                    dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, 30, 30, 173, 0f, 0f, 0, new Color(255, 255, 255), 0.8f)];
                }
            }
        }
    }
}