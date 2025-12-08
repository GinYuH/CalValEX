using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Projectiles.Pets.LightPets
{
    public class Enredpet : ModProjectile
    {
        public override string Texture => "CalValEX/Projectiles/Pets/LightPets/CosmicAssistantRing";
        private readonly string CosmicTexture = "CalValEX/Projectiles/Pets/LightPets/CosmicAssistant";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Assistant");
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
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

        public override void PostDraw(Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Texture2D texture = ModContent.Request<Texture2D>(CosmicTexture).Value;
            Rectangle sourceRectangle = new(0, 40 * frame, texture.Width, texture.Height / 6);
            Vector2 origin = new(texture.Width, texture.Height);
            Vector2 position = player.Center + Vector2.UnitY * player.gfxOffY;
            position.Y += 36f;
            position -= Main.screenPosition;
            Main.EntitySpriteDraw(texture, position, sourceRectangle, lightColor, 0f, origin / 2f, 1f, SpriteEffects.None, 0);
        }

        private int frame = 0;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
            if (player.dead)
            {
                modPlayer.Enredpet = false;
            }
            if (modPlayer.Enredpet)
            {
                Projectile.timeLeft = 2;
            }

            Vector2 vectorToOwner = player.Center;
            vectorToOwner.Y -= 64f - player.gfxOffY;

            float value = 8f;

            float velY = MathHelper.Clamp(player.velocity.Y, -value, value);
            float velX = MathHelper.Clamp(player.velocity.X, -value, value);

            vectorToOwner.X += 0.5f * -velX;
            vectorToOwner.Y += 0.5f * -velY;

            Projectile.Center = vectorToOwner;

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                frame++;
                Projectile.frameCounter = 0;
                if (frame > 5)
                    frame = 0;
            }

            Lighting.AddLight(Projectile.Center, new Vector3(1.2f, 0.65882353f, 1.38039216f));
        }
    }
}