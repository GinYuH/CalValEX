using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using Terraria.GameContent;
using System;

namespace CalValEX.Projectiles.Pets
{
    [LegacyName("YharimSquid")]
    public class AbyssalTitan : ModProjectile
    {
        public static Asset<Texture2D> tentacleMid;
        public static Asset<Texture2D> tentacleLow;
        public static Asset<Texture2D> tentacleHigh;
        public static Asset<Texture2D> glow;

        public override void Load()
        {
            tentacleMid = ModContent.Request<Texture2D>(Texture + "_MiddleTentacle");
            tentacleHigh = ModContent.Request<Texture2D>(Texture + "_UpperTentacle");
            tentacleLow = ModContent.Request<Texture2D>(Texture + "_LowerTentacle");
            glow = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 72;
            Projectile.height = 72;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft *= 5;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Texture2D low = tentacleLow.Value;
            Texture2D mid = tentacleMid.Value;
            Texture2D high = tentacleHigh.Value;
            Texture2D glowT = glow.Value;
            Texture2D bloom = ModContent.Request<Texture2D>("CalValEX/ExtraTextures/LargeBloom").Value;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            Main.EntitySpriteDraw(bloom, Projectile.Center - Main.screenPosition, null, Color.Goldenrod, Projectile.rotation, bloom.Size() / 2, Projectile.scale * 0.6f, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();

            Vector2 highOff = new Vector2(-26, 42);
            Vector2 lowOff = new Vector2(-10, 46);

            float sin = MathHelper.ToRadians(10) * MathF.Sin(Main.GlobalTimeWrappedHourly * 2);

            Main.EntitySpriteDraw(high, Projectile.Center - Main.screenPosition + highOff, null, Projectile.GetAlpha(lightColor) * 1.4f, Projectile.rotation + sin, new Vector2(66, 14), Projectile.scale, 0);
            Main.EntitySpriteDraw(high, Projectile.Center - Main.screenPosition + new Vector2(-highOff.X, highOff.Y), null, Projectile.GetAlpha(lightColor) * 1.4f, Projectile.rotation - sin, new Vector2(high.Width - 66, 14), Projectile.scale, SpriteEffects.FlipHorizontally);
            Main.EntitySpriteDraw(low, Projectile.Center - Main.screenPosition + lowOff, null, Projectile.GetAlpha(lightColor) * 1.4f, Projectile.rotation + sin, new Vector2(55, 2), Projectile.scale, 0);
            Main.EntitySpriteDraw(low, Projectile.Center - Main.screenPosition + new Vector2(-lowOff.X, lowOff.Y), null, Projectile.GetAlpha(lightColor) * 1.4f, Projectile.rotation - sin, new Vector2(low.Width - 55, 2), Projectile.scale, SpriteEffects.FlipHorizontally);
            Main.EntitySpriteDraw(mid, Projectile.Center - Main.screenPosition + new Vector2(-1, 36 + MathF.Sin(Main.GlobalTimeWrappedHourly * 2) * -4), null, Projectile.GetAlpha(lightColor) * 1.4f, Projectile.rotation, new Vector2(11, 2), Projectile.scale, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor) * 1.4f, Projectile.rotation, tex.Size() / 2, Projectile.scale, 0);
            Main.EntitySpriteDraw(glowT, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() / 2, Projectile.scale, 0);

            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
            if (player.dead)
            {
                modPlayer.ySquid = false;
            }
            if (modPlayer.ySquid)
            {
                Projectile.timeLeft = 2;
            }

            Vector2 vectorToOwner = player.Center;
            vectorToOwner.Y -= 194f - player.gfxOffY + MathF.Sin(Projectile.ai[0] * 0.02f + 2) * 8;

            float value = 8f;

            float velY = MathHelper.Clamp(player.velocity.Y, -value, value);
            float velX = MathHelper.Clamp(player.velocity.X, -value, value);

            vectorToOwner.X += 0.5f * -velX;
            vectorToOwner.Y += 0.5f * -velY;

            Projectile.Center = vectorToOwner;

            Projectile.ai[0]++;
        }
    }
}