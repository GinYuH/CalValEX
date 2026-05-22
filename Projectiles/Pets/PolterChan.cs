using System;
using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Projectiles.Pets
{
    public class PolterChan : ModFlyingPet
    {
        public override bool FacesLeft => false;

        public override float TeleportThreshold => 1440f;

        public override Vector2 FlyingOffset => new(68f * -Main.player[Projectile.owner].direction, -50f);

        public override void SetStaticDefaults()
        {
            PetSetStaticDefaults(lightPet: false);
            // DisplayName.SetDefault("Polter-Chan");
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            PetSetDefaults();
            Projectile.width = 82;
            Projectile.height = 64;
            Projectile.ignoreWater = true;
            Projectile.GetGlobalProjectile<CalValEXGlobalProjectile>().isCalValPet = true;
        }

        public override void Animation(int state)
        {
            Mod infernum;
            Mod armasortof;
            Mod cplus;
            ModLoader.TryGetMod("InfernumMode", out infernum);
            ModLoader.TryGetMod("EfficientNohits", out armasortof);
            bool ida = false;
            ModLoader.TryGetMod("CalValPlus", out cplus);

            if( armasortof != null )
            {
                Type playerType = armasortof.Code.GetType("EfficientNohits.EffPlayer");
                var field = playerType.GetField("InstantDeathAlways");
                ida = (bool)field.GetValue(null);
            }

            //MAID mode
            if (CalValEX.CalamityActive && !CalValEXConfig.Instance.Polterskin && ((Main.masterMode && (bool)CalValEX.Calamity.Call("GetDifficultyActive", "death") && (infernum != null && (bool)infernum.Call("GetInfernumActive")) && (armasortof != null && ida )) || cplus != null))
            {
                if (Projectile.frameCounter++ > 8)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                    if (Projectile.frame >= 8)
                        Projectile.frame = 4;
                }
            }
            else
            {
                if (Projectile.frameCounter++ % 8 == 7)
                {
                    Projectile.frame++;
                }
                if (Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void PostDraw(Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
            string glowmaskTexture = (modPlayer.signutTrans || modPlayer.signutForce) && !CalValEXConfig.Instance.Polterskin ? "CalValEX/Projectiles/Pets/EvilPolterChan_Glow" : "CalValEX/Projectiles/Pets/PolterChan_Glow";
            SimpleGlowmask(Main.spriteBatch, glowmaskTexture);
        }

        public override void PetFunctionality(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();


            if (player.dead)
                modPlayer.mChan = false;

            if (modPlayer.mChan)
                Projectile.timeLeft = 2;
        }
    }
}