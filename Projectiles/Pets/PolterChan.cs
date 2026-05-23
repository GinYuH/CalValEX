using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Projectiles.Pets
{
    public class PolterChan : ModFlyingPet
    {
        public override bool FacesLeft => false;

        public override float TeleportThreshold => 1440f;

        public override Vector2 FlyingOffset => new(68f * -Main.player[Projectile.owner].direction, -50f);

        private static Type _effPlayerType;
        private static FieldInfo _instantDeathAlwaysField;

        static PolterChan()
        {
            if (ModLoader.TryGetMod("EfficientNohits", out Mod armasortof))
            {
                _effPlayerType = armasortof.Code.GetType("EfficientNohits.EffPlayer");
                _instantDeathAlwaysField = _effPlayerType?.GetField("InstantDeathAlways");
            }
        }

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
            Mod cplus;
            ModLoader.TryGetMod("InfernumMode", out infernum);
            ModLoader.TryGetMod("CalValPlus", out cplus);

            bool ida = false;
            if (_instantDeathAlwaysField != null)
            {
                ida = (bool)_instantDeathAlwaysField.GetValue(null);
            }

            //MAID mode
            if (CalValEX.CalamityActive && !CalValEXConfig.Instance.Polterskin && ((Main.masterMode && (bool)CalValEX.Calamity.Call("GetDifficultyActive", "death") && (infernum != null && (bool)infernum.Call("GetInfernumActive")) && ida) || cplus != null))
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