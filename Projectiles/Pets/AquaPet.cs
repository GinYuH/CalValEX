using Terraria;
using Terraria.ID;

namespace CalValEX.Projectiles.Pets
{
    public class AquaPet : BaseWormPet
    {
        public override string Texture => "CalValEX/Projectiles/Pets/AquaHead";
        public override WormPetVisualSegment HeadSegment() => new("CalValEX/Projectiles/Pets/AquaHead", frames: 8);
        public override WormPetVisualSegment BodySegment() => new("CalValEX/Projectiles/Pets/AquaBody", frames: 8);
        public override WormPetVisualSegment TailSegment() => new("CalValEX/Projectiles/Pets/AquaTail", frames: 8);

        public override int SegmentSize() => 10;

        public override int SegmentCount() => 10;

        public override bool ExistenceCondition() => ModOwner.asPet;

        public override float GetSpeed => MathHelper.Lerp(10, 20, MathHelper.Clamp(Projectile.Distance(IdealPosition) / (WanderDistance * 2.2f) - 1f, 0, 1));

        public override int BodyVariants => 1;
        public override float BashHeadIn => 5;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aquatic Pest");
            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;
        }

        public override void UpdateIdealPosition()
        {
            TimeTillReset++;
            if (TimeTillReset > 150)
            {
                RelativeIdealPosition = Vector2.Zero;
                TimeTillReset = 0;
            }
            Dust.NewDustPerfect(RelativeIdealPosition, DustID.RainbowMk2);
            //Reset the ideal position if the ideal position was reached
            if (Projectile.Distance(IdealPosition) < GetSpeed)
                RelativeIdealPosition = Vector2.Zero;
            //Get a new ideal position
            if (RelativeIdealPosition == null || RelativeIdealPosition == Vector2.Zero)
            {
				RelativeIdealPosition = Main.rand.NextVector2CircularEdge(WanderDistance, WanderDistance);
                return;
            }
            // Chase after beach balls
            if (Projectile.localAI[2] >= 0)
            {
                Projectile bol = Main.projectile[(int)Projectile.localAI[2]];
                if (bol.active && bol.aiStyle == ProjAIStyleID.BeachBall)
                {
                    RelativeIdealPosition = bol.Center - Owner.Center;
                    // inflate the scourge's hitbox to assure it hits because sometimes it doesnt because projectile collision is lol
                    Rectangle bigScourge = Projectile.getRect();
                    bigScourge.Inflate(bigScourge.Width * 2, bigScourge.Height * 2);
                    // if it intersects, forget the ball exists and go on a 3 second cooldown before it can bounce another ball
                    if (bigScourge.Intersects(bol.getRect()))
                    {
                        Projectile.localAI[2] = -1;
                        Projectile.localAI[1] = 180;
                    }
                }
                else
                {
                    Projectile.localAI[2] = -1;
                }
            }

        }
        public override void MoveTowardsIdealPosition()
        {

            //Rotate towards its ideal position
            Projectile.rotation = Projectile.rotation.AngleTowards((IdealPosition - Projectile.Center).ToRotation(), MathHelper.Lerp(MaximumSteerAngle, MinimumSteerAngle, MathHelper.Clamp(Projectile.Distance(IdealPosition) / 80f, 0, 1)));
            Projectile.velocity = Projectile.rotation.ToRotationVector2() * GetSpeed;

            //Update its segment
            Segments[0].oldPosition = Segments[0].position;
            Segments[0].position = Projectile.Center;
        }

        public override void CustomAI()
        {
            // every so often do a lil spin
            if (Main.rand.NextBool(300) && Projectile.ai[2] <= 0)
            {
                Projectile.ai[2] = 600;
            }
            Projectile.ai[2]--;

            // Go after beach ball-like projectiles within a 2000px radius
            if (Projectile.localAI[1] <= 0)
            {
                foreach (Projectile p in Main.ActiveProjectiles)
                {
                    if (p.aiStyle == ProjAIStyleID.BeachBall)
                    {
                        if (p.Distance(Projectile.Center) < 2000)
                        {
                            Projectile.localAI[2] = p.whoAmI;
                            break;
                        }
                    }
                }
            }
            Projectile.localAI[1]--;
        }

        public override void Animate()
        {
            foreach (WormPetSegment segment in Segments)
            {
                // do a spin if ai[2] has been tampered with
                if (Projectile.ai[2] > 0)
                {
                    segment.visual.FrameCounter++;
                    if (segment.visual.FrameCounter > segment.visual.FrameDuration)
                    {
                        segment.visual.Frame--;
                        if (segment.visual.Frame < 0)
                            segment.visual.Frame = segment.visual.FrameCount - 1;
                        // if we're back on frame 1, set ai[2] to 1,
                        // due to the constant decrementing, this will make the pet exit its animation after the tail is done
                        if (segment.visual.Frame == segment.visual.FrameCount - 1)
                            Projectile.ai[2] = 1;

                        segment.visual.FrameCounter = 0;
                    }
                }
                // otherwise just use static frames
                else
                {
                    segment.visual.Frame = segment.visual.FrameCount - 1;
                }
            }
        }
    }
}