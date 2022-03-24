using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CalValEX.Projectiles.Pets
{
    public class PhantomPet : WalkingPet
    {
        public Player Owner => Main.player[projectile.owner];
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantom Larva");
            Main.projFrames[projectile.type] = 7; //frames
            Main.projPet[projectile.type] = true;
            base.drawOffsetX = -7;
        }

        public override void SafeSetDefaults() //SAFE SET DEFAULTS!!!
        {
            projectile.width = 20;
            projectile.height = 14;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            /* you don't need to set these anymore!!
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            */
            //THIS IS NEEDED
            facingLeft = false; //is the sprite facing left? if so, put this to true. if its facing to right keep it false.
            spinRotation = false; //should it spin? if that's the case, set to true. else, leave it false.
            shouldFlip = true; //should the sprite flip? if that's the case, set to true. else, put it to false.
        }

        //these below are not needed. only use what you need to change. most of the time you only need to change SetFrameLimitsAndFrameSpeed().

        public override void SetPetGravityAndDrag()
        {
            gravity = 0.1f; //needs to be positive for the pet to be pushed down platforms plus for it to have gravity
            drag[0] = 0.92f; //idle drag
            drag[1] = 0.95f; //walking drag
        }

        public override void SetPetDistances()
        {
            distance[0] = 2400f; //teleport
            distance[1] = 560f; //speed increase
            distance[2] = 220f; //when to walk
            distance[3] = 80f; //when to stop walking
            distance[4] = 448f; //when to fly
            distance[5] = 180f; //when to stop flying
        }

        public override void SetPetSpeedsAndInertia()
        {
            speed[0] = Owner.GetModPlayer<CalValEXPlayer>().MaladyBells ? 20f : 16f; //walking speed
            speed[1] = Owner.GetModPlayer<CalValEXPlayer>().MaladyBells ? 16f : 12f; //flying speed

            inertia[0] = 20f; //walking inertia
            inertia[1] = 80f; //flight inertia
        }

        public override void SetJumpSpeeds()
        {
            jumpSpeed[0] = -4f; //1 tile above pet
            jumpSpeed[1] = -6f; //2 tiles above pet
            jumpSpeed[2] = -8f; //5 tiles above pet
            jumpSpeed[3] = -7f; //4 tiles above pet
            jumpSpeed[4] = -6.5f; //any other tile number above pet
        }

        public override void SetFrameLimitsAndFrameSpeed()
        {
            idleFrameLimits[0] = 0; //what your min idle frame is (start of idle animation)
            idleFrameLimits[1] = 0; //what your max idle frame is (end of idle animation)

            walkingFrameLimits[0] = 0; //what your min walking frame is (start of walking animation)
            walkingFrameLimits[1] = 3; //what your max walking frame is (end of walking animation)

            flyingFrameLimits[0] = 4; //what your min flying frame is (start of flying animation)
            flyingFrameLimits[1] = 6; //what your max flying frame is (end of flying animation)

            animationSpeed[0] = 30; //idle animation speed
            animationSpeed[1] = 7; //walking animation speed
            animationSpeed[2] = 7; //flying animation speed
            spinRotationSpeedMult = 2.5f; //how fast it should spin
            //put the below to -1 if you dont want a jump animation (so its just gonna continue it's walk animation
            animationSpeed[3] = -1; //jumping animation speed

            jumpFrameLimits[0] = -1; //what your min jump frame is (start of jump animation)
            jumpFrameLimits[1] = -1; //what your max jump frame is (end of jump animation)

            jumpAnimationLength = -1; //how long the jump animation should stay
        }

        public override void SafeAI(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.mDebris = false;
            if (modPlayer.mDebris)
                projectile.timeLeft = 2;

            /* THIS CODE ONLY RUNS AFTER THE MAIN CODE RAN.
             * for custom behaviour, you can check if the projectile is walking or not via projectile.localAI[1]
             * you should make new custom behaviour with numbers higher than 2, or less than 0
             * the next few lines is an example on how to implement this
             *
             * switch ((int)projectile.localAI[1])
             * {
             *     case -1:
             *         break;
             *     case 3:
             *         break;
             * }
             *
             * 0, 1 and 2 are already in use.
             * 0 = idling
             * 1 = walking
             * 2 = flying
             *
             * you can still use these, changing thing inside (however it's not recomended unless you want to add custom behaviour to these)
             */
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowMask = mod.GetTexture("Projectiles/Pets/PhantomPet_Glow");
	    if (CalValEX.month == 12)
	    {
		glowMask = mod.GetTexture("ExtraTextures/ChristmasPets/PhantomPetGlow");
 	    }
	    else
	    {
		glowMask = mod.GetTexture("Projectiles/Pets/PhantomPet_Glow");
 	    }
            Rectangle frame = glowMask.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            frame.Height -= 1;
            float originOffsetX = (glowMask.Width - projectile.width) * 0.5f + projectile.width * 0.5f + drawOriginOffsetX;
            spriteBatch.Draw
            (
                glowMask,
                projectile.position - Main.screenPosition + new Vector2(originOffsetX + drawOffsetX, projectile.height / 2 + projectile.gfxOffY),
                frame,
                Color.White,
                projectile.rotation,
                new Vector2(originOffsetX, projectile.height / 2 - drawOriginOffsetY),
                projectile.scale,
                projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f
            );
        }
    }
}