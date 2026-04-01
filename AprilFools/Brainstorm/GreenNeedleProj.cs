using Terraria;
using Terraria.ModLoader;
using CalValEX.Items;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace CalValEX.AprilFools.Brainstorm
{
    public class GreenNeedleProj : ModProjectile
    {
        public override string Texture => "CalValEX/AprilFools/Brainstorm/GreenNeedleProj1";
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = ProjAIStyleID.Nail;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] == 0)
            {
                Projectile.localAI[1] = 1;
                Projectile.ai[2] = Main.rand.Next(4);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.TryGetGlobalNPC(out CalValEXGlobalNPC cvnpc))
            {
                cvnpc.needleAmt++;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetMaxDamage(1);
            modifiers.DisableCrit();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.localAI[1] == 0)
                return false;
            Texture2D texture = CalValEXGlobalNPC.needles[(int)Projectile.ai[2]].Value;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Projectile.GetAlpha(lightColor), Projectile.rotation + MathHelper.PiOver2, texture.Size() / 2, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}