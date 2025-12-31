using CalValEX.Buffs.Mounts;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Mounts.Minecart
{
    public class DraedonCartMount : ModMount
    {
        public override void SetStaticDefaults()
        {
            MountData.Minecart = true;
            MountData.delegations = new();
            MountData.delegations.MinecartDust = CreateSparkDust;
            MountID.Sets.Cart[ModContent.MountType<DraedonCartMount>()] = true;

            MountData.spawnDust = DustID.RainbowRod;
            MountData.buff = ModContent.BuffType<DraedonCartBuff>();

            // Movement fields.
            MountData.flightTimeMax = 0;
            MountData.fallDamage = 1f;

            // Mechanical Cart in vanilla uses 20f and 0.1f respectively for these fields.
            MountData.runSpeed = 24f;
            MountData.acceleration = 0.2f;

            MountData.jumpHeight = 18;
            MountData.jumpSpeed = 5.4f;
            MountData.blockExtraJumps = true;
            MountData.heightBoost = 14;

            // Drawing fields.
            MountData.playerYOffsets = new int[] { 12, 12, 12 };
            MountData.xOffset = 2;
            MountData.yOffset = -2;
            MountData.bodyFrame = 3;

            // Animation fields.
            MountData.totalFrames = 3;
            MountData.standingFrameCount = 1;
            MountData.standingFrameDelay = 12;
            MountData.standingFrameStart = 0;
            MountData.runningFrameCount = 3;
            MountData.runningFrameDelay = 12;
            MountData.runningFrameStart = 0;
            MountData.flyingFrameCount = 0;
            MountData.flyingFrameDelay = 0;
            MountData.flyingFrameStart = 0;
            MountData.inAirFrameCount = 0;
            MountData.inAirFrameDelay = 0;
            MountData.inAirFrameStart = 0;
            MountData.idleFrameCount = 1;
            MountData.idleFrameDelay = 10;
            MountData.idleFrameStart = 0;
            MountData.idleFrameLoop = false;
            if (Main.netMode != NetmodeID.Server)
            {
                MountData.textureWidth = MountData.backTexture.Width();
                MountData.textureHeight = MountData.backTexture.Height();
            }
        }

        public static void CreateSparkDust(Vector2 dustPosition)
        {
            Vector2 offsetDirection = DelegateMethods.Minecart.rotation.ToRotationVector2();
            offsetDirection *= new Vector2(Main.rand.NextBool().ToDirectionInt(), 1f) * 13f;
            dustPosition += offsetDirection;

            Dust spark = Dust.NewDustPerfect(dustPosition, DustID.TintableDust, Alpha: 222, Scale: 2, newColor: Main.DiscoColor);
            spark.velocity = Vector2.Zero;
            spark.noGravity = true;
        }

        Vector2[] oldPoses = new Vector2[22];

        public override bool Draw(List<DrawData> playerDrawData, int drawType, Player drawPlayer, ref Texture2D texture, ref Texture2D glowTexture, ref Vector2 drawPosition, ref Rectangle frame, ref Color drawColor, ref Color glowColor, ref float rotation, ref SpriteEffects spriteEffects, ref Vector2 drawOrigin, ref float drawScale, float shadow)
        {
            Vector2 offsetDirection = DelegateMethods.Minecart.rotation.ToRotationVector2();
            offsetDirection *= new Vector2(Main.rand.NextBool().ToDirectionInt(), 1f);
            if (CalValEX.CalamityActive)
            {
                if (Main.hasFocus)
                for (int i = oldPoses.Length - 1; i > 0; i--)
                {
                    oldPoses[i] = oldPoses[i - 1];
                }
                oldPoses[0] = Main.LocalPlayer.MountedCenter + new Vector2(-22 * Main.LocalPlayer.direction, 16) + offsetDirection + Main.LocalPlayer.velocity.SafeNormalize(Vector2.Zero);
                RenderTrail(oldPoses);
            }
            return true;
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void RenderTrail(Vector2[] oldposes)
        {
            CalamityMod.Graphics.Primitives.PrimitiveRenderer.RenderTrail(oldposes, new(WidthFunction, ColorFunction), 40);
        }

        internal Color ColorFunction(float completionRatio, Vector2 v)
        {
            return Color.Lerp(Color.White, Main.DiscoColor, completionRatio);
        }

        internal float WidthFunction(float completionRatio, Vector2 v)
        {
            return (1 - completionRatio) * 4;
        }
    }
}