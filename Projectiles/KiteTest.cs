using CalamityMod.NPCs.TownNPCs;
using CalValEX.Projectiles.Pets;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Projectiles
{
    public class KiteTest : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.KiteShark;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 100;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 60;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            // 1.4.5 is adding Kite anchoring I think, adjust this when it's out
            Vector2 anchorPos = player.RotatedRelativePoint(player.MountedCenter);
            Projectile.timeLeft = 60;

            // Check if the kite should still exist and kill if so
            bool kill = false;
            if (player.CCed || player.noItems)
            {
                kill = true;
            }
            else if (player.inventory[player.selectedItem].shoot != Projectile.type)
            {
                kill = true;
            }
            else if (player.pulley)
            {
                kill = true;
            }
            else if (player.dead)
            {
                kill = true;
            }
            if (!kill)
            {
                kill = (player.Center - Projectile.Center).Length() > 2000f;
            }
            if (kill)
            {
                Projectile.Kill();
                return;
            }

            // Line distance calculation
            float minDist = 4f;
            float maxDist = 500f;
            float targetDist = maxDist / 2f;
            if (Projectile.owner == Main.myPlayer && Projectile.extraUpdates == 0)
            {
                float currentDist = Projectile.ai[0];
                if (Projectile.ai[0] == 0f)
                {
                    Projectile.ai[0] = targetDist;
                }
                float newDist = Projectile.ai[0];
                // Move outwards
                if (Main.mouseRight)
                {
                    newDist -= 5f;
                }
                // Move inwards
                if (Main.mouseLeft)
                {
                    newDist += 5f;
                }
                Projectile.ai[0] = MathHelper.Clamp(newDist, minDist, maxDist);
                if (currentDist != newDist)
                {
                    Projectile.netUpdate = true;
                }
            }
            if (Projectile.numUpdates == 1)
            {
                Projectile.extraUpdates = 0;
            }

            // Weather variables
            float cloudAlpha = Main.cloudAlpha;
            float windSpeed = 0f;
            if (WorldGen.InAPlaceWithWind(Projectile.position, Projectile.width, Projectile.height))
            {
                windSpeed = Main.WindForVisuals;
            }
            float adjustedWindSpeed = Utils.GetLerpValue(0.2f, 0.5f, Math.Abs(windSpeed), clamped: true) * 0.5f;
                    
            // Move towards the desired position
            Vector2 targetPosition = Projectile.Center + new Vector2(windSpeed, (float)Math.Sin(Main.GlobalTimeWrappedHourly) + cloudAlpha * 5f) * 25f;
            Vector2 dirToProj = targetPosition - Projectile.Center;
            dirToProj = dirToProj.SafeNormalize(Vector2.Zero) * (3f + cloudAlpha * 7f);
            if (adjustedWindSpeed == 0f)
            {
                dirToProj = Projectile.velocity;
            }
            float distToMaus = Projectile.Distance(targetPosition);
            float lerpValue = Utils.GetLerpValue(5f, 10f, distToMaus, clamped: true);
            float yVelocity = Projectile.velocity.Y;
            if (distToMaus > 10f)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, dirToProj, 0.075f * lerpValue);
            }
            Projectile.velocity.Y = yVelocity;
            Projectile.velocity.Y -= adjustedWindSpeed;
            Projectile.velocity.Y += 0.02f + adjustedWindSpeed * 0.25f;
            Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -2f, 2f);
            if (Projectile.Center.Y + Projectile.velocity.Y < targetPosition.Y)
            {
                Projectile.velocity.Y = MathHelper.Lerp(Projectile.velocity.Y, Projectile.velocity.Y + adjustedWindSpeed + 0.01f, 0.75f);
            }
            Projectile.velocity.X *= 0.98f;
            float distance = Projectile.Distance(anchorPos);
            float curDust = Projectile.ai[0];
            if (distance > curDust)
            {
                Vector2 toPlayer = Projectile.DirectionTo(anchorPos);
                float distDif = distance - curDust;
                Projectile.Center += toPlayer * distDif;
                bool shouldRise = Vector2.Dot(toPlayer, Vector2.UnitY) < 0.8f || adjustedWindSpeed > 0f;
                Projectile.velocity.Y += toPlayer.Y * 0.05f;
                if (shouldRise)
                {
                    Projectile.velocity.Y -= 0.15f;
                }
                Projectile.velocity.X += toPlayer.X * 0.2f;
                if (curDust == minDist && Projectile.owner == Main.myPlayer)
                {
                    Projectile.Kill();
                    return;
                }
            }
                    
            Projectile.timeLeft = 2;
            Vector2 distToPlayer = Projectile.Center - anchorPos;
            int dir = ((distToPlayer.X > 0f) ? 1 : (-1));
            if (Math.Abs(distToPlayer.X) > Math.Abs(distToPlayer.Y) / 2f)
            {
                player.ChangeDir(dir);
            }
            Vector2 dirToPlayer = Projectile.DirectionTo(anchorPos).SafeNormalize(Vector2.Zero);
            if (adjustedWindSpeed == 0f && Projectile.velocity.Y > -0.02f)
            {
                Projectile.rotation *= 0.95f;
            }
            else
            {
                float extraRot = ((-dirToPlayer).ToRotation() + MathHelper.PiOver4);
                if (Projectile.spriteDirection == -1)
                {
                    extraRot -= MathHelper.PiOver2 * (float)player.direction;
                }
                Projectile.rotation = extraRot + Projectile.velocity.X * 0.05f;
            }
            Projectile.spriteDirection = player.direction;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Vanilla kite drawcode is an absolute abomination, what you are about to witness is a mere fraction of The Horrors after optimization and removal of unused code 

            Texture2D headTexture = TextureAssets.Projectile[ModContent.ProjectileType<Projectiles.Pets.Dog>()].Value;
            Texture2D fishingLine = TextureAssets.FishingLine.Value;

            // Segment numbers
            int segmentAmount = 5;
            int physicsIterations = 10;

            // Trail positioning
            int xOffset = -10;
            int yOffset = -66;

            // Spacing
            int segmentSpacingAmt = 120;
            int segmentSpacingAmtButLess = 100;

            // Rotation
            float rotPosOff = (float)-MathHelper.PiOver2 * (float)Projectile.spriteDirection;

            // Things I know what they do
            bool drawLineBetween = true;

            // "Greeble"
            SpriteEffects effects = ((Projectile.spriteDirection != 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            Vector2 headOrigin = new (headTexture.Width / 2, headTexture.Height * 0.8f);
            Vector2 headPosition = Projectile.Center - Main.screenPosition;
            Color color = Lighting.GetColor(Projectile.Center.ToTileCoordinates());
            Color projectileColor = Projectile.GetAlpha(color);
            Rectangle lineFrame = fishingLine.Frame();
            Vector2 lineOrigin = new (lineFrame.Width / 2, 2f);
            // 1.4.5 is adding Kite anchoring I think, adjust this when it's out
            Vector2 anchorPosition = Main.GetPlayerArmPosition(Projectile);
            Vector2 center = Projectile.Center;
            Vector2.Distance(center, anchorPosition);
            float armOffset = 12f;
            _ = (anchorPosition - center).SafeNormalize(Vector2.Zero) * armOffset;
            Vector2 armPositionCorrected = anchorPosition;
            Vector2 directionToArm = center - armPositionCorrected;
            Vector2 velocity = Projectile.velocity;
            if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
            {
                Utils.Swap(ref velocity.X, ref velocity.Y);
            }
            float armDistance = directionToArm.Length();
            float curveVar1 = 16f;
            float curveVar2 = 80f;
            bool drawLine = true;
            if (armDistance == 0f)
            {
                drawLine = false;
            }
            else
            {
                directionToArm *= 12f / armDistance;
                armPositionCorrected -= directionToArm;
                directionToArm = center - armPositionCorrected;
            }
            // Draw line
            while (drawLine)
            {
                float segLength = 12f;
                float distanceToArm = directionToArm.Length();
                float alsoDistanceToArm = distanceToArm;
                if (float.IsNaN(distanceToArm) || distanceToArm == 0f)
                {
                    drawLine = false;
                    continue;
                }
                if (distanceToArm < 20f)
                {
                    segLength = distanceToArm - 8f;
                    drawLine = false;
                }
                distanceToArm = 12f / distanceToArm;
                directionToArm *= distanceToArm;
                armPositionCorrected += directionToArm;
                directionToArm = center - armPositionCorrected;
                if (alsoDistanceToArm > 12f)
                {
                    float dirBump = 0.3f;
                    float dir = Math.Abs(velocity.X) + Math.Abs(velocity.Y);
                    if (dir > curveVar1)
                    {
                        dir = curveVar1;
                    }
                    dir = 1f - dir / curveVar1;
                    dirBump *= dir;
                    dir = alsoDistanceToArm / curveVar2;
                    if (dir > 1f)
                    {
                        dir = 1f;
                    }
                    dirBump *= dir;
                    if (dirBump < 0f)
                    {
                        dirBump = 0f;
                    }
                    dir = 1f;
                    dirBump *= dir;
                    if (directionToArm.Y > 0f)
                    {
                        directionToArm.Y *= 1f + dirBump;
                        directionToArm.X *= 1f - dirBump;
                    }
                    else
                    {
                        dir = Math.Abs(velocity.X) / 3f;
                        if (dir > 1f)
                        {
                            dir = 1f;
                        }
                        dir -= 0.5f;
                        dirBump *= dir;
                        if (dirBump > 0f)
                        {
                            dirBump *= 2f;
                        }
                        directionToArm.Y *= 1f + dirBump;
                        directionToArm.X *= 1f - dirBump;
                    }
                }
                float rotation = directionToArm.ToRotation() - MathHelper.PiOver2;
                if (!drawLine)
                {
                    lineFrame.Height = (int)segLength;
                }
                Color lineColor = Lighting.GetColor(center.ToTileCoordinates());
                Main.EntitySpriteDraw(fishingLine, armPositionCorrected - Main.screenPosition, lineFrame, lineColor, rotation, lineOrigin, 1f, SpriteEffects.None);
            }

            #region Segment calculation...
            Vector2 projOrigin = Projectile.Size / 2f;
            float windSpeed = Math.Abs(Main.WindForVisuals);
            float windOffset = MathHelper.Lerp(0.5f, 1f, windSpeed);
            float windStrength = windSpeed;
            if (directionToArm.Y >= -0.02f && directionToArm.Y < 1f)
            {
                windStrength = Utils.GetLerpValue(0.2f, 0.5f, windSpeed, clamped: true);
            }
            int segPadding = physicsIterations;
            int trueSegAmount = segmentAmount + 1;

            List<Vector2> list = new List<Vector2>();
            Vector2 firstSegmentPos = new Vector2(windOffset * (float)segmentSpacingAmtButLess * (float)Projectile.spriteDirection, (float)Math.Sin(Main.timeForVisualEffects / 300.0 * MathHelper.TwoPi) * windStrength) * 2f;
            float xOff = yOffset;
            float yOff = xOffset;
            Vector2 segmentPosition = Projectile.Center + new Vector2(((float)headTexture.Width * 0.5f + xOff) * (float)Projectile.spriteDirection, yOff).RotatedBy(Projectile.rotation + rotPosOff);
            list.Add(segmentPosition);

            int currentCompletion = segPadding;
            int loopNumber = 1;
            while (currentCompletion < trueSegAmount * segPadding)
            {
                Vector2 newPosition = Projectile.oldPos[currentCompletion];
                if (newPosition.X == 0f && newPosition.Y == 0f)
                {
                    list.Add(segmentPosition);
                }
                else
                {
                    newPosition += projOrigin + new Vector2(((float)headTexture.Width * 0.5f + xOff) * (float)Projectile.oldSpriteDirection[currentCompletion], yOff).RotatedBy(Projectile.oldRot[currentCompletion] + rotPosOff);
                    newPosition += firstSegmentPos * (loopNumber + 1);
                    Vector2 directionToPrevPos = segmentPosition - newPosition;
                    float distanceToPrevPos = directionToPrevPos.Length();
                    if (distanceToPrevPos > segmentSpacingAmt)
                    {
                        directionToPrevPos *= segmentSpacingAmt / distanceToPrevPos;
                    }
                    newPosition = segmentPosition - directionToPrevPos;
                    list.Add(newPosition);
                    segmentPosition = newPosition;
                }
                currentCompletion += segPadding;
                loopNumber++;
            }
            #endregion

            // Draw line between segments
            if (drawLineBetween)
            {
                Rectangle lineRect = fishingLine.Frame();
                for (int j = list.Count - 2; j >= 0; j--)
                {
                    Vector2 linePos = list[j];
                    Vector2 dirToCurrent = list[j + 1] - linePos;
                    float distToCurrent = dirToCurrent.Length();
                    if (!(distToCurrent < 2f))
                    {
                        float lineRotation = dirToCurrent.ToRotation() - MathHelper.PiOver2;
                        Main.EntitySpriteDraw(fishingLine, linePos - Main.screenPosition, lineRect, projectileColor, lineRotation, lineOrigin, new Vector2(1f, distToCurrent / (float)lineRect.Height), SpriteEffects.None);
                    }
                }
            }
            // Draw segments
            float finalRot = 0;
            for (int j = list.Count - 2; j >= 0; j--)
            {
                bool tail = j == list.Count - 2;
                string suffix = tail ? "TailS" : "BodyS";
                Texture2D segmentTex = ModContent.Request<Texture2D>("CalamityMod/NPCs/DevourerofGods/DevourerofGods" + suffix).Value;
                Vector2 segmentOrigin = tail ? new Vector2(segmentTex.Width / 2, segmentTex.Height * 0.3f) : segmentTex.Size() / 2;

                Vector2 currentPos = list[j];
                Vector2 nextPos = list[j + 1];
                Vector2 nextToCurrent = nextPos - currentPos;

                float segmentRotation = nextToCurrent.ToRotation() - MathHelper.PiOver2;

                if (j == 0)
                {
                    finalRot = segmentRotation;
                }

                Main.EntitySpriteDraw(segmentTex, nextPos - Main.screenPosition, null, projectileColor, segmentRotation, segmentOrigin, Projectile.scale, effects);
            }
            
            Main.EntitySpriteDraw(headTexture, headPosition, null, projectileColor, finalRot, headOrigin, Projectile.scale, effects);
            return false;
        }
    }
}