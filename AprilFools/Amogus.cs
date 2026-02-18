using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using CalValEX.Projectiles.Pets;
using Terraria.Chat;
using Terraria.Audio;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.AprilFools
{
    public class Amogus : ModWalkingPet
    {
        int deathcounter = 0;
        int lifecounter = 0;
        int sandblasttimer = 0;
        int mushroom = 0;
        int birb = 1200;
        int minetimer = 0;
        int infernadotimer = 1200;
        int polterdart = 0;
        int mine;
        int raintype;
        int attackphase;
        int attackcounter1;
        int attackcounter2;
        int attackmoment = 0;
        int chargetype = 0;
        public override bool FacesLeft => false;
        public override void SetStaticDefaults()
        {
            PetSetStaticDefaults(lightPet: false);
            // DisplayName.SetDefault("Amogus");
            Main.projFrames[Projectile.type] = 4; //frames
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            PetSetDefaults();
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft *= 5;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            DrawOffsetX = -7;
            DrawOriginOffsetY = 0;
            DrawOffsetX = -7;
            DrawOriginOffsetY = 0;
        }
        public override void ModifyJumpHeight(ref float oneTileHigherAndNotTwoTilesHigher, ref float twoTilesHigher, ref float fourTilesHigher, ref float fiveTilesHigher, ref float anyOtherJump)
        {
            oneTileHigherAndNotTwoTilesHigher = -4f;
            twoTilesHigher = -6f;
            fiveTilesHigher = -8f;
            fourTilesHigher = -7f;
            anyOtherJump = -6.5f;
        }
        public override void Animation(int state)
        {
            switch (state)
            {
                case States.Walking:
                    if (Projectile.velocity.X != 0f)
                    {
                        if (++Projectile.frameCounter > 8)
                        {
                            Projectile.frameCounter = 0;
                            Projectile.frame++;
                            if (Projectile.frame > 3)
                                Projectile.frame = 0;
                        }
                    }
                    else
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame = 0;
                    }
                    break;
                case States.Flying:
                    if (++Projectile.frameCounter > 10)
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame++;
                        if (Projectile.frame > 3)
                            Projectile.frame = 0;
                    }
                    break;
            }
        }
        private void EdgyTalk(string text, Color color, bool combatText = false)
        {
            if (combatText)
            {
                CombatText.NewText(Projectile.getRect(), color, text, true);
            }
            else
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(text, color);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey(text), color);
                }
            }
        }

        public override void PetFunctionality(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
            {
                modPlayer.amogus = false;
                Projectile.active = false;
            }
            if (modPlayer.amogus)
                Projectile.timeLeft = 2;
        }
        public override void CustomBehaviour(Player player, ref int state, float walkingSpeed, float walkingInertia, float flyingSpeed, float flyingInertia)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
            if (deathcounter <= 0 && !player.HasBuff(BuffType<AmogusBuff>()) && !NPC.AnyNPCs(NPCType<Meldosaurus.Meldosaurus>()))
            {
                Projectile.active = false;
            }

            if (NPC.AnyNPCs(NPCType<Meldosaurus.Meldosaurus>()) && deathcounter <= 0)
            {
                Projectile.tileCollide = false;
                state = 5;
            }

            if ((!CalValEX.AprilFoolMonth && state != 4 && !CalValEXWorld.amogus && CalValEX.CalamityActive) || modPlayer.rockhat)
            {
                deathcounter++;
                if (deathcounter >= 300)
                {
                    state = 3;
                    Projectile.timeLeft = 2;
                }
            }
            switch (state)
            {
                case 3:
                    if (CalValEX.CalamityActive)
                    {
                        Vector2 playerpos;
                        playerpos.X = player.position.X + (Main.rand.Next(-256, 256));
                        playerpos.Y = player.position.Y - 256;
                        if (deathcounter >= 900)
                        {
                            sandblasttimer++;

                            if (deathcounter <= 1800)
                            {
                                if (Main.rand.NextBool(2))
                                {
                                    raintype = CalValEX.CalamityProjectile("SandBlast");
                                }
                                else
                                {
                                    raintype = CalValEX.CalamityProjectile("UnstableEbonianGlob");
                                }
                            }
                            else if (deathcounter > 1800 && deathcounter <= 2700)
                            {
                                if (Main.rand.NextBool(2))
                                {
                                    raintype = CalValEX.CalamityProjectile("SCalBrimstoneFireblast");
                                }
                                else
                                {
                                    raintype = CalValEX.CalamityProjectile("AstralLaser");
                                }
                            }
                            else if (deathcounter > 2700 && deathcounter <= 3600)
                            {
                                if (Main.rand.NextBool(2))
                                {
                                    raintype = CalValEX.CalamityProjectile("BrimstoneBarrage");
                                }
                                else
                                {
                                    raintype = CalValEX.CalamityProjectile("ProfanedSpear");
                                }
                            }
                            else if (deathcounter > 3600)
                            {
                                if (Main.rand.NextBool(3))
                                {
                                    raintype = CalValEX.CalamityProjectile("SignusScythe");
                                }
                                else if (Main.rand.NextBool(2))
                                {
                                    raintype = CalValEX.CalamityProjectile("SCalBrimstoneGigablast");
                                }
                                else
                                {
                                    raintype = CalValEX.CalamityProjectile("ApolloRocket");
                                }
                            }

                            if (sandblasttimer >= 10)
                            {
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, 0, 5, raintype, 80, 0f, Main.myPlayer, 0f, 0f);
                                sandblasttimer = 0;
                            }
                        }
                        if (deathcounter >= 1080)
                        {
                            mushroom++;
                            if (mushroom >= 120)
                            {
                                for (int x = 0; x < 10; x++)
                                {
                                    SoundEngine.PlaySound(SoundID.Item11);
                                }
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("OldDukeGore"), 180, 0f, Main.myPlayer, 0f, 0f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("MushBomb"), 90, 0f, Main.myPlayer, 0f, 0f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("IchorBlob"), 180, 0f, Main.myPlayer, 0f, 0f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("MoltenBlob"), 120, 0f, Main.myPlayer, 0f, 0f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("IceRain"), 80, 0f, Main.myPlayer, 0f, 0f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("ShadeNimbusHostile"), 80, 0f, Main.myPlayer, 0f, 0f);
                                if (deathcounter >= 2700)
                                {
                                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("RavagerFlame"), 80, 0f, Main.myPlayer, 0f, 0f);
                                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, Main.rand.Next(-5, 5), Main.rand.Next(-5, -3), CalValEX.CalamityProjectile("PlagueStingerGoliathV2"), 80, 0f, Main.myPlayer, 0f, 0f);
                                }
                                for (int x = 0; x < 20; x++)
                                {
                                    Dust dust;
                                    dust = Main.dust[Dust.NewDust(playerpos, 30, 30, DustID.Cloud, 0f, 0f, 0, new Color(255, 255, 255), 1.644737f)];
                                }
                                mushroom = 0;
                            }
                        }
                        if (deathcounter >= 1400)
                        {
                            birb++;
                            if (birb >= 1200)
                            {
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, 1, 20, CalValEX.CalamityProjectile("BirbAuraFlare"), 0, 0f, Main.myPlayer, 0f, 0f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, 0, 0, CalValEX.CalamityProjectile("DoGBeamPortal"), 0, 0f, Main.myPlayer, 0f, 0f);
                                birb = 0;
                            }
                        }
                        if (deathcounter >= 1000)
                        {
                            if (deathcounter <= 1800)
                            {
                                mine = CalValEX.CalamityProjectile("DeusMine");
                            }
                            else if (deathcounter >= 1800 && deathcounter <= 2700)
                            {
                                mine = CalValEX.CalamityProjectile("SirenSong");
                            }
                            else if (deathcounter >= 2700)
                            {
                                mine = CalValEX.CalamityProjectile("ToxicCloud");
                            }

                            minetimer++;
                            if (minetimer >= 240)
                            {
                                for (int x = 0; x < 8; x++)
                                {
                                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X, Projectile.position.Y, Main.rand.Next(-2, 2), Main.rand.Next(-2, 2), mine, 80, 0f, Main.myPlayer, 0f, 0f);
                                    minetimer = 0;
                                }
                            }
                        }
                        if (deathcounter >= 1800)
                        {
                            infernadotimer++;
                            if (infernadotimer >= 1200)
                            {
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), playerpos.X, playerpos.Y, 0, 20, CalValEX.CalamityProjectile("BigFlare"), 80, 0f, Main.myPlayer, 0f, 0f);
                                infernadotimer = 0;
                            }
                        }
                        if (deathcounter <= 540)
                        {
                            Projectile.velocity.X = 0;
                            Projectile.velocity.Y = 0;
                        }
                        if (deathcounter > 540 && deathcounter <= 900)
                        {
                            Projectile.position.Y = player.position.Y;
                            Projectile.position.X = player.position.X + 256;
                        }
                        if (deathcounter > 900)
                        {
                            Projectile.position.Y = player.position.Y - 256;
                            Projectile.position.X = player.position.X;
                        }
                        if (deathcounter > 540 && deathcounter < 820)
                        {
                            Projectile.scale *= 1.008f;
                            if (Projectile.frameCounter++ > 4)
                            {
                                Projectile.frameCounter = 0;
                                Projectile.frame++;
                                if (Projectile.frame >= 4)
                                    Projectile.frame = 0;
                            }
                        }
                        if (deathcounter == 300)
                        {
                            EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.FightTalk1"), Color.White, true);
                        }
                        if (deathcounter == 420)
                        {
                            EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.FightTalk2"), Color.White, true);
                        }
                        if (deathcounter == 540)
                        {
                            EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.FightTalk3"), Color.White, true);
                            Projectile.rotation = 0;
                        }
                        if (deathcounter == 680)
                        {
                            EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.FightTalk4"), Color.White, true);
                        }
                        if (deathcounter == 820)
                        {
                            EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.FightTalk5"), Color.DarkRed, true);
                            Projectile.alpha = 50;
                            Projectile.rotation = 0;
                        }
                        if (deathcounter >= 4200 && !modPlayer.rockhat)
                        {
                            state = 4;
                        }
                    }
                    break;

                case 4:
                    lifecounter++;
                    deathcounter = 0;
                    if (lifecounter < 420)
                    {
                        Projectile.position.Y = player.position.Y - 256;
                        Projectile.position.X = player.position.X;
                    }
                    else if (lifecounter >= 420 && lifecounter < 680)
                    {
                        Projectile.position.X = player.position.X - 256;
                        Projectile.position.Y = player.position.Y;
                    }
                    if (lifecounter == 300)
                    {
                        EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.CutsceneTalk1"), Color.White, true);
                    }
                    if (lifecounter == 420)
                    {
                        EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.CutsceneTalk2"), Color.White, true);
                    }
                    if (lifecounter == 540)
                    {
                        EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Amogus.CutsceneTalk3"), Color.White, true);
                        CalValEXWorld.amogus = true;
                        CalValEXWorld.UpdateWorldBool();
                    }
                    if (lifecounter == 680)
                    {
                        Projectile.scale = 1;
                        state = 0;
                        deathcounter = 0;
                        lifecounter = 0;
                    }
                    break;
                case 5:
                    {
                        if (!NPC.AnyNPCs(NPCType<Meldosaurus.Meldosaurus>()))
                        {
                            attackcounter1 = 0;
                            attackcounter2 = 0;
                            attackphase = 0;
                            state = 0;
                        }
                        if (attackphase == 0)
                        {
                            attackcounter2--;
                            var thisRect = Projectile.getRect();

                            for (int i = 0; i < Main.maxNPCs; i++)
                            {
                                var npc = Main.npc[i];

                                if (npc != null && npc.active && npc.getRect().Intersects(thisRect) && npc.type == NPCType<Meldosaurus.Meldosaurus>() && attackcounter2 <= 20)
                                {
                                    if (chargetype == 4)
                                    {
                                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, 0, 0, ProjectileID.InfernoFriendlyBlast, 2020, 0, Main.myPlayer);
                                    }
                                    else
                                    {
                                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, 0, 0, ProjectileID.DaybreakExplosion, 1020, 0, Main.myPlayer);
                                    }
                                    attackcounter2 = 40;
                                }
                            }
                            int chargedelay = 30;
                            int chargedist = 10;
                            attackcounter1++;
                            if (chargetype == 0)
                            {
                                if (attackcounter1 <= chargedelay) //Less than or equal to 30
                                {
                                    Projectile.position.X = Main.npc[CalValEXGlobalNPC.meldodon].Center.X - 100;
                                    Projectile.position.Y = Main.npc[CalValEXGlobalNPC.meldodon].Center.Y;
                                }
                                if (attackcounter1 == chargedelay + 1) //Exactly 31
                                {
                                    Projectile.velocity.X = 20;
                                    Projectile.velocity.Y = 0;
                                    attackmoment = 0 + chargedelay + chargedist; //Set the counter to 40
                                    chargetype = 1;
                                }
                            }
                            if (chargetype == 1)
                            {
                                if (attackcounter1 >= attackmoment && attackcounter1 <= attackmoment + chargedelay) //Between 40 and 70
                                {
                                    Projectile.position.Y = Main.npc[CalValEXGlobalNPC.meldodon].Center.Y - 100;
                                    Projectile.position.X = Main.npc[CalValEXGlobalNPC.meldodon].Center.X;
                                }
                                if (attackcounter1 == attackmoment + chargedelay + 1) //71
                                {
                                    Projectile.velocity.Y = 20;
                                    Projectile.velocity.X = 0;
                                    attackmoment = attackmoment + chargedelay + chargedist; //80
                                    chargetype = 2;
                                }
                            }
                            if (chargetype == 2)
                            {
                                if (attackcounter1 >= attackmoment && attackcounter1 <= attackmoment + chargedelay) //Between 80 and 110
                                {
                                    Projectile.position.Y = Main.npc[CalValEXGlobalNPC.meldodon].Center.Y + 100;
                                    Projectile.position.X = Main.npc[CalValEXGlobalNPC.meldodon].Center.X - 100;
                                }
                                if (attackcounter1 == attackmoment + chargedelay + 1) //111
                                {
                                    Projectile.velocity.X = 20;
                                    Projectile.velocity.Y = -20;
                                    attackmoment = attackmoment + chargedelay + chargedist; //120
                                    chargetype = 3;
                                }
                            }
                            if (chargetype == 3)
                            {
                                if (attackcounter1 >= attackmoment && attackcounter1 <= attackmoment + chargedelay) //Between 120 and 150
                                {
                                    Projectile.position.Y = Main.npc[CalValEXGlobalNPC.meldodon].Center.Y - 100;
                                    Projectile.position.X = Main.npc[CalValEXGlobalNPC.meldodon].Center.X + 100;
                                }
                                if (attackcounter1 == attackmoment + chargedelay + 1) //151
                                {
                                    Projectile.velocity.X = -20;
                                    Projectile.velocity.Y = +20;
                                    attackmoment = attackmoment + chargedelay + chargedist; //160
                                    chargetype = 4;
                                }
                            }
                            if (chargetype == 4)
                            {
                                if (attackcounter1 >= attackmoment + chargedist) //170
                                {
                                    chargetype = 0;
                                    attackmoment = 0;
                                    attackcounter1 = 0;
                                    attackcounter2 = 0;
                                    attackphase = 1;
                                }
                            }
                        }
                        if (attackphase == 1)
                        {
                            attackcounter1++;
                            attackcounter2++;
                            int num412 = 1;
                            float num413 = 25f;
                            float num414 = 1.2f;
                            float distanceX = 480f;
                            float yoffset = 0f;

                            Vector2 vector40 = new(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                            float num415 = Main.npc[CalValEXGlobalNPC.meldodon].position.X + (float)(Main.npc[CalValEXGlobalNPC.meldodon].width / 2) + (float)(num412 * distanceX) - vector40.X;
                            float num416 = Main.npc[CalValEXGlobalNPC.meldodon].position.Y + (float)(Main.npc[CalValEXGlobalNPC.meldodon].height / 2) + yoffset - vector40.Y;
                            float num417 = (float)Math.Sqrt(num415 * num415 + num416 * num416);
                            num417 = num413 / num417;
                            num415 *= num417;
                            num416 *= num417;
                            if (Projectile.velocity.X < num415)
                            {
                                Projectile.velocity.X += num414;
                                if (Projectile.velocity.X < 0f && num415 > 0f)
                                {
                                    Projectile.velocity.X += num414;
                                }
                            }
                            else if (Projectile.velocity.X > num415)
                            {
                                Projectile.velocity.X -= num414;
                                if (Projectile.velocity.X > 0f && num415 < 0f)
                                {
                                    Projectile.velocity.X -= num414;
                                }
                            }
                            if (Projectile.velocity.Y < num416)
                            {
                                Projectile.velocity.Y += num414;
                                if (Projectile.velocity.Y < 0f && num416 > 0f)
                                {
                                    Projectile.velocity.Y += num414;
                                }
                            }
                            else if (Projectile.velocity.Y > num416)
                            {
                                Projectile.velocity.Y -= num414;
                                if (Projectile.velocity.Y > 0f && num416 < 0f)
                                {
                                    Projectile.velocity.Y -= num414;
                                }
                            }
                            if (attackcounter1 >= 5)
                            {
                                SoundEngine.PlaySound(SoundID.Item41, Projectile.position);
                                Vector2 position = Projectile.Center;
                                position.X = Projectile.Center.X + (10f * Projectile.direction);
                                Vector2 targetPosition = Main.npc[CalValEXGlobalNPC.meldodon].Center;
                                Vector2 direction = targetPosition - position;
                                direction.Normalize();
                                float speed = 15f;
                                int type = ProjectileID.BulletHighVelocity;
                                int damage = Main.expertMode ? 60 : 95;
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, direction * speed, type, damage, 0f, Main.myPlayer);
                                attackcounter1 = 0;
                            }
                            if (attackcounter2 >= 360)
                            {
                                attackcounter1 = 0;
                                attackcounter2 = 0;
                                attackphase = 2;
                            }
                        }
                        if (attackphase == 2)
                        {
                            attackcounter1++;
                            Projectile.velocity.X = 0;
                            Projectile.velocity.Y = 0;
                            if (attackcounter1 == 60)
                            {
                                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
                                Vector2 position = Projectile.Center;
                                position.X = Projectile.Center.X + (10f * Projectile.direction);
                                Vector2 targetPosition = Main.npc[CalValEXGlobalNPC.meldodon].Center;
                                Vector2 direction = targetPosition - position;
                                direction.Normalize();
                                float speed = 18f;
                                int type = ProjectileID.IchorBullet;
                                int damage = Main.expertMode ? 1702 : 2040;
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, direction * speed, type, damage, 0f, Main.myPlayer);
                            }
                            if (attackcounter1 >= 120)
                            {
                                attackcounter1 = 0;
                                attackphase = 3;
                            }
                        } 
                        if (attackphase == 3)
                        {
                            attackcounter1++;
                            attackcounter2++;
                            int num412 = 1;
                            float num413 = 25f;
                            float num414 = 1.2f;
                            float distanceX = 580f;
                            float yoffset = -300f;

                            Vector2 vector40 = new(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                            float num415 = Main.npc[CalValEXGlobalNPC.meldodon].position.X + (float)(Main.npc[CalValEXGlobalNPC.meldodon].width / 2) + (float)(num412 * distanceX) - vector40.X;
                            float num416 = Main.npc[CalValEXGlobalNPC.meldodon].position.Y + (float)(Main.npc[CalValEXGlobalNPC.meldodon].height / 2) + yoffset - vector40.Y;
                            float num417 = (float)Math.Sqrt(num415 * num415 + num416 * num416);
                            num417 = num413 / num417;
                            num415 *= num417;
                            num416 *= num417;
                            if (Projectile.velocity.X < num415)
                            {
                                Projectile.velocity.X += num414;
                                if (Projectile.velocity.X < 0f && num415 > 0f)
                                {
                                    Projectile.velocity.X += num414;
                                }
                            }
                            else if (Projectile.velocity.X > num415)
                            {
                                Projectile.velocity.X -= num414;
                                if (Projectile.velocity.X > 0f && num415 < 0f)
                                {
                                    Projectile.velocity.X -= num414;
                                }
                            }
                            if (Projectile.velocity.Y < num416)
                            {
                                Projectile.velocity.Y += num414;
                                if (Projectile.velocity.Y < 0f && num416 > 0f)
                                {
                                    Projectile.velocity.Y += num414;
                                }
                            }
                            else if (Projectile.velocity.Y > num416)
                            {
                                Projectile.velocity.Y -= num414;
                                if (Projectile.velocity.Y > 0f && num416 < 0f)
                                {
                                    Projectile.velocity.Y -= num414;
                                }
                            }
                            if (attackcounter1 >= 40)
                            {
                                SoundEngine.PlaySound(SoundID.Item21, Projectile.position);
                                Vector2 position = Projectile.Center;
                                position.X = Projectile.Center.X + (10f * Projectile.direction);
                                Vector2 targetPosition = Main.npc[CalValEXGlobalNPC.meldodon].Center;
                                Vector2 direction = targetPosition - position;
                                direction.Normalize();
                                float speed = 10f;
                                int type = ProjectileID.RainCloudRaining;
                                int damage = Main.expertMode ? 120 : 152;
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, direction * speed, type, damage, 0f, Main.myPlayer);
                                attackcounter1 = 0;
                            }
                            if (attackcounter2 >= 460)
                            {
                                attackcounter1 = 0;
                                attackcounter2 = 0;
                                attackphase = 0;
                            }
                        }
                    }
                    break;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            if (deathcounter >= 820)
            {
                return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, Projectile.alpha);
            }
            else
            {
                return null;
            }
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(deathcounter);
            writer.Write(lifecounter);
            writer.Write(sandblasttimer);
            writer.Write(mushroom);
            writer.Write(birb);
            writer.Write(minetimer);
            writer.Write(infernadotimer);
            writer.Write(polterdart);
            writer.Write(mine);
            writer.Write(raintype);
            writer.Write(attackphase);
            writer.Write(attackcounter1);
            writer.Write(attackcounter2);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            deathcounter = reader.ReadInt32();
            lifecounter = reader.ReadInt32();
            sandblasttimer = reader.ReadInt32();
            mushroom = reader.ReadInt32();
            birb = reader.ReadInt32();
            minetimer = reader.ReadInt32();
            infernadotimer = reader.ReadInt32();
            polterdart = reader.ReadInt32();
            mine = reader.ReadInt32();
            raintype = reader.ReadInt32();
            attackphase = reader.ReadInt32();
            attackcounter1 = reader.ReadInt32();
            attackcounter2 = reader.ReadInt32();
            base.ReceiveExtraAI(reader);
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D deusheadsprite;
            if ((attackphase == 1 || attackphase == 2) && state == 5)
            {
                deusheadsprite = (Request<Texture2D>("CalValEX/AprilFools/AmogusGun").Value);
            }
            else if (attackphase == 0 && state == 5)
            {
                deusheadsprite = (Request<Texture2D>("CalValEX/AprilFools/AmogusKnife").Value);
            }
            else
            {
                deusheadsprite = (Request<Texture2D>("CalValEX/Items/Equips/Shields/Invishield_Shield").Value);
            }

            int deusheadheight = deusheadsprite.Height;

            Rectangle deusheadsquare = new(0, 0, deusheadsprite.Width, deusheadsprite.Height);
            Color deusheadalpha = Projectile.GetAlpha(lightColor);
            Main.EntitySpriteDraw(deusheadsprite, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY + 10), deusheadsquare, deusheadalpha, Projectile.rotation, Utils.Size(deusheadsquare) / 2f, Projectile.scale, Projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }
    }
}
