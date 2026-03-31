using Terraria;
using Terraria.ModLoader;
using System.IO;
using Terraria.Audio;
using Terraria.ID;

namespace CalValEX.Projectiles.Pets
{
    public class Sirember : ModFlyingPet
    {
        public override float TeleportThreshold => 1440f;

        public override Vector2 FlyingOffset => new(54f * -Main.player[Projectile.owner].direction, -130f);

        //Mod orthoceraDLC = ModLoader.GetMod("CalValPlus");

        public bool orthod = false;
        public bool theterror = false;
        public bool entropy = false;

        int apocalypse = 0;

        public override void SetStaticDefaults()
        {
            PetSetStaticDefaults(lightPet: false);
            // DisplayName.SetDefault("Giant Decapitated Floating Siren Head");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            PetSetDefaults();
            Projectile.width = 112;
            Projectile.height = 112;
            Projectile.ignoreWater = true;
            Projectile.GetGlobalProjectile<CalValEXGlobalProjectile>().isCalValPet = true;
        }

        public override void PostDraw(Color lightColor)
        {
            string glowmaskTexture;
            Player player = Main.player[Projectile.owner];
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
            if (modPlayer.bossded > 0 && !entropy)
            {
                Projectile.alpha = 255;
                glowmaskTexture = "CalValEX/Projectiles/Pets/Siretrum";
            }
            else if (entropy)
            {
                glowmaskTexture = "CalValEX/Items/Equips/Shields/Invishield_Shield";
                Projectile.alpha = 255;
            }
            else
            {
                Projectile.alpha = 0;
                glowmaskTexture = "CalValEX/Items/Equips/Shields/Invishield_Shield";
            }

            SimpleGlowmask(Main.spriteBatch, glowmaskTexture);
        }

        public override void ModifyFlyingValues(ref float flyingSpeed, ref float flyingInertia)
        {
            if (theterror)
            {
                flyingSpeed = 0f;
                flyingInertia = 0f;
            }
        }

        public override void Animation(int state)
        {
            if (!theterror)
            {
                SimpleAnimation(speed: 7);
            }
        }

        public override void PetFunctionality(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.sirember = false;

            if (modPlayer.sirember)
                Projectile.timeLeft = 2;
        }

        public override void CustomBehaviour(Player player, ref int state, float flyingSpeed, float flyingInertia)
        {
            int time1 = 23600;
            int time2 = 24260;
            if (CalValEX.CalamityActive && player.HasBuff(CalValEX.CalamityBuff("AuricRebuke")))
            {
                time1 = 600;
                time2 = 1260;
            }
            else
            {
                time1 = 23600;
                time2 = 24260;
            }

            apocalypse++;

            if (apocalypse >= time1 && apocalypse < time2)
            {
                if (!entropy)
                {
                    SoundEngine.PlaySound(new SoundStyle("CalValEX/Sounds/ReaperEnragedRoar"), Projectile.position);
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SiremberSpook>(), 0, 0, Projectile.owner);
                    Projectile.alpha = 255;
                    entropy = true;
                }

            }
            if (apocalypse >= time2)
            {
                Projectile.alpha = 0;
                for (int x = 0; x < 60; x++)
                {
                    Dust.NewDust(Projectile.Center, 30, 30, DustID.Cloud, 0f, 0f, 0, new Color(255, 255, 255), 1.644737f);
                }
                apocalypse = 0;
                entropy = false;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(orthod);
            writer.Write(theterror);
            writer.Write(entropy);
            writer.Write(apocalypse);
            base.SendExtraAI(writer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            apocalypse = reader.ReadInt32();
            entropy = reader.ReadBoolean();
            theterror = reader.ReadBoolean();
            orthod = reader.ReadBoolean();
            base.ReceiveExtraAI(reader);
        }
    }
}