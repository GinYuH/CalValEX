using CalamityMod.CalPlayer;
using CalValEX.AprilFools;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;

namespace CalValEX.AprilFools.Jharim
{
    [AutoloadHead]
    public class Jharim : ModNPC
    {
        private static bool shop1;
        private static bool boss;
        private bool MELDOSAURUSED;
        private int textcounter;
        public int framebuffer = 0;
        public int framecounter = 0;
        public bool firinglaser = false;
        public bool lasercheck = false;
        Vector2 jharimpos;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Tyrant");
            Main.npcFrameCount[npc.type] = 23;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 1;
            npc.defense = 150;
            npc.lifeMax = 250000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.PartyGirl;
        }
        private void EdgyTalk(string text, Color color, bool combatText = false)
        {
            if (combatText)
            {
                CombatText.NewText(npc.getRect(), color, text, true);
            }
            else
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(text, color);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey(text), color);
                }
            }
        }

        public override void AI()
        {
            Mod orthoceraDLC = ModLoader.GetMod("CalValPlus");
            if (CalValEX.month != 4 && orthoceraDLC == null)
            {
            npc.active = false;
            }
            if (NPC.AnyNPCs(NPCType<CalamityMod.NPCs.TownNPCs.WITCH>()) && !MELDOSAURUSED && npc.life < npc.lifeMax * 0.15f)
            {
                if (!lasercheck)
                {
                    lasercheck = true;
                    firinglaser = true;
                }
                for (int x = 0; x < Main.maxNPCs; x++)
                {
                    NPC npc3 = Main.npc[x];
                    if (npc3.type == NPCType<CalamityMod.NPCs.TownNPCs.WITCH>())
                    {
                        if (npc3.active)
                        {
                            jharimpos.X = npc3.Center.X;
                            jharimpos.Y = npc3.Center.Y;
                            if (npc3.position.X - npc.position.X >= 0)
                            {
                                npc.direction = 1;
                            }
                            else
                            {
                                npc.direction = -1;
                            }
                        }
                        else
                        {
                            CalValEXWorld.jharinter = true;
                            lasercheck = false;
                        }
                    }
                }
                if (firinglaser)
                {
                    Vector2 position = npc.Center;
                    position.X = npc.Center.X;
                    Vector2 direction = jharimpos - position;
                    direction.Normalize();
                    float speed = 10f;
                    int type = ProjectileType<JharimLaser>();
                    int damage = 6666666;
                    Projectile.NewProjectile(position, direction * speed, type, damage, 0f, Main.myPlayer);
                    firinglaser = false;
                    npc.defense = 999;
                }
            }
            if (MELDOSAURUSED)
            {
                npc.dontTakeDamage = true;
                npc.dontTakeDamageFromHostiles = true;
                textcounter++;
                npc.velocity.X *= 0.04f;
                if (textcounter == 1)
                {
                    EdgyTalk("GRAHGAHGAH WHY WOULD YOU DO THAT!", Color.White, true);
                }
                else if (textcounter == 120)
                {
                    EdgyTalk("IT    IS GOING TO WAKE UP HELPHELPHELP", Color.White, true);
                }
                else if (textcounter == 240 || textcounter == 280 || textcounter == 300 || textcounter == 310 || textcounter == 305 || textcounter == 310 || textcounter == 315 || textcounter == 320)
                {
                    EdgyTalk("ORULQDORULQDORULQDORULQD", Color.DarkGreen, true);
                }
                if (textcounter == 360)
                {
                    Main.PlaySound(SoundID.NPCHit, npc.position, 1);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<AprilFools.Meldosaurus.Meldosaurus>());
                    npc.active = false;
                }

            }
        }

        public override string TownNPCName()
        {
            return "Jharim";
        }

        public override string GetChat()
        {

            CalValEXGlobalNPC.jharim = npc.whoAmI;
            Player player = Main.player[Main.myPlayer];
            CalValEXPlayer CalValEXPlayer = player.GetModPlayer<CalValEXPlayer>();
            CalamityPlayer calPlayer = player.GetModPlayer<CalamityPlayer>();
            if (!MELDOSAURUSED)
            {
                if (npc.homeless)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            return "Hyuck Hyuck Hyuck, hello I am Jharim the Great Jungle Tyrant. I appear in seek of someone to defeat a great beast for me.";

                        case 1:
                            return "That bag wasn't very comfy. I think I ate a fly in there.";

                        default:
                            return "Hyuck Hyuck Hyuck, I as Great Jungle Tyrant, will aid you on your quest to defeat the Non-Great Jungle Tyrant... by doing nothing!";
                    }
                }

                if (calPlayer.cirrusDress)
                {
                    return "NO NO NOT THE STAR NEVER AGAIN NO NO NO!";
                }

                //Main.NewText("MISC EQUIPS 0 TYPE: " + Main.player[Main.myPlayer].miscEquips[0].type + "|MISC EQUIPS 1 TYPE: " + Main.player[Main.myPlayer].miscEquips[1].type);

                if ((NPC.AnyNPCs(NPCID.LunarTowerNebula) || NPC.AnyNPCs(NPCID.LunarTowerVortex) || NPC.AnyNPCs(NPCID.LunarTowerStardust) || NPC.AnyNPCs(NPCID.LunarTowerSolar)) && Main.rand.NextFloat() < 0.25f)
                {
                    return "These pillars are spookay, and those dark globs some of their friends drop are... I don't want to touch any... especially if its on fire, so please don't shoot me with any fiery weapons made of that stuff.";
                }

                int FAP = NPC.FindFirstNPC((ModLoader.GetMod("CalamityMod").NPCType("FAP")));
                if (FAP >= 0 && Main.rand.NextFloat() < 0.25f)
                {
                    return "Hyu Hyu Hyu... That purple lady stole my booze.";
                }

                int Cal = NPC.FindFirstNPC((ModLoader.GetMod("CalamityMod").NPCType("WITCH")));
                if (Cal >= 0 && Main.rand.NextFloat() < 0.25f)
                {
                    return "GET THAT ACURSED WITCH AWAY FROM ME";
                }

                int SEAHOE = NPC.FindFirstNPC((ModLoader.GetMod("CalamityMod").NPCType("SEAHOE")));
                if (SEAHOE >= 0 && Main.rand.NextFloat() < 0.25f)
                {
                    if (Cal >= 0)
                        return "How is Amidas still alive, I thought that he got burnt by that DUMB witch.";
                    else
                        return "How is Amidas still alive, I thought that he got burnt by Soup Ree Calamitoad.";
                }

                if (NPC.AnyNPCs((ModLoader.GetMod("CalamityMod").NPCType("Draedon"))) && Main.rand.NextFloat() < 0.25f)
                {
                    return "DRAAAAAAAAAEEEEEEEDOOOOOOOOOOOONNNNNNNNNNNNNNNNNNN I KNOW WHAT YOU DID!";
                }

                Mod apoc = ModLoader.GetMod("ApothTestMod");
                if (apoc != null)
                {
                    if (NPC.AnyNPCs((apoc.NPCType("THELORDE"))))
                    {
                        return "IMPOSTER!";
                    }
                }

                if (NPC.AnyNPCs((ModLoader.GetMod("CalamityMod").NPCType("Yharon"))) && Main.rand.NextFloat() < 0.25f)
                {
                    return "Hyuck Hyuck Hyuck, my loyal friend Yharon! I demand you to stop atacking! ... Guess he doesn't recognize me...";
                }
                Mod clamMod = ModLoader.GetMod("CalamityMod");
                if (((bool)clamMod.Call("GetBossDowned", "supremecalamitas")) && ((bool)clamMod.Call("GetBossDowned", "exomechs")) && Main.rand.NextFloat() < 0.25)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "It is time for you to face him.";

                        default:
                            return "The god of the universe... he is willing to face you now.";
                    }
                }

                if ((calPlayer.sirenWaifu || calPlayer.elementalHeart || (CalValEXPlayer.vanityhote && !CalValEXConfig.Instance.HeartVanity) || (CalValEXPlayer.vanitysiren && !CalValEXConfig.Instance.HeartVanity)) && Main.rand.NextFloat() < 0.25f)
                {
                    return "OoooooO Fish Lady, tell me! Where's my fish tacos!";
                }

                if (Main.eclipse)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "Hyuck Hyuck Hyuck, why is it so dark.";

                        default:
                            return "Hmm, I KNOW! I should go and wrestle one of those moths! I've wrestled a lot of moths before!";
                    }
                }

                if (CalamityMod.Events.BossRushEvent.BossRushActive)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "THE SKY IS COLLAPSING, IT'S THE END AGAIN!";

                        default:
                            return "THE MOTH, THEY'LL RECKON HAVOC UPON ME AND FLING ME OFF EXISTENCE AAAAAHHHHHHHHHH";
                    }
                }

                if (CalamityMod.World.CalamityWorld.rainingAcid)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "ribbit.";

                        default:
                            return "I'm feeling kinda drunk right now Hyickup Hyickup. Maybe I should stop drinking highly toxic acid...";
                    }
                }

                if (BirthdayParty.PartyIsUp)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return "Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck Hyuck .";

                        default:
                            return "PARTY TIMEEEEEEEEEEEEE HYUCK HYUCK HYUCK.";
                    }
                }

                if (!Main.bloodMoon)
                {
                    if (Main.dayTime)
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 0:
                                return "Hyuck Hyuck Hyuck, Jharim the Great orders you to get fish for me! ... what do you mean you don't want to!?";

                            case 1:
                                return "Yharim is a faker, I AM THE ONLY JUNGLE TYRANT!";

                            case 2:
                                return "HYuHck HYuHck HYuHck. Why am I spelling like this?";

                            default:
                                return "When I was younger, I tortured a bee for a minute straight then bashed nails into its body. It was funny and got a lot of views on TubeYou.";
                        }
                    }
                    else
                    {
                        switch (Main.rand.Next(4))
                        {
                            case 0:
                                return "It's dark.";

                            case 1:
                                return "Hyuck Hyuck Hyuck, nights remind me of the time that I went to the beach and was almost assassinated!";

                            default:
                                return "I wonder where my pet dog went.";
                        }
                    }
                }
                else
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            return "Run.";

                        case 1:
                            return "The red moon... a cursed thing... Anyways where's my eggos, chop chop little one.";

                        default:
                            return "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA.";
                    }
                }
            }
            else
            {
                return "GAHUGIEGJIWEGUIEUWG";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (!MELDOSAURUSED)
            {
                button = "Shop";
                button2 = "Summon";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (!MELDOSAURUSED)
            {
                if (firstButton)
                {
                    shop = true;
                    {
                        shop1 = true;
                        boss = false;
                    }
                }
                else if (!firstButton)
                {
                    {
                        if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                        {
                            Mod clamMod = ModLoader.GetMod("CalamityMod");
                            if ((bool)clamMod.Call("GetBossDowned", "supremecalamitas") && (bool)clamMod.Call("GetBossDowned", "exomechs"))
                            {
                                npc.active = false;
                                NPC.SpawnOnPlayer(Main.player[Main.myPlayer].whoAmI, mod.NPCType("Fogbound"));
                            }
                            else if ((bool)clamMod.Call("GetBossDowned", "supremecalamitas") && !(bool)clamMod.Call("GetBossDowned", "exomechs"))
                            {
                                Main.npcChatText = "The time is not here yet. He will only appear after the ultimate mechanical horrors have been destroyed.";
                            }
                            else if (!(bool)clamMod.Call("GetBossDowned", "supremecalamitas") && (bool)clamMod.Call("GetBossDowned", "exomechs"))
                            {
                                Main.npcChatText = "The time is not here yet. He will only appear after the great brimstone witch has been bested in combat.";
                            }
                            else
                            {
                                Main.npcChatText = "The time is not here yet. He will only appear after the great brimstone witch and the ultimate mechanical horrors have been defeated.";
                            }
                        }
                    }
                }
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (!MELDOSAURUSED)
            {
                if (shop1)
                {
                    shop.item[nextSlot].SetDefaults(ItemType<AprilFools.AmogusItem>());
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1, 0, 0);
                    nextSlot++;
                }
            }
        }

        public override bool CanGoToStatue(bool toKingStatue)
        {
            return true;
        }

        public override void OnGoToStatue(bool toKingStatue)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)npc.whoAmI);
                packet.Send();
            }
            else
            {
                StatueTeleport();
            }
        }

        public void StatueTeleport()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Main.rand.NextVector2Square(-20, 21);
                if (Math.Abs(position.X) > Math.Abs(position.Y))
                {
                    position.X = Math.Sign(position.X) * 20;
                }
                else
                {
                    position.Y = Math.Sign(position.Y) * 20;
                }
                Dust.NewDustPerfect(npc.Center + position, 50, Vector2.Zero).noGravity = true;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 1;
            knockback = 0f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 100;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (!MELDOSAURUSED)
                projType = ModContent.ProjectileType<JharimLaser>();
            else
                projType = ModLoader.GetMod("CalamityMod").ProjectileType("NobodyKnows");
            attackDelay = 1;
            return;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            randomOffset = 2f;
            multiplier = 24f;
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (projectile.type == ProjectileType<CalamityMod.Projectiles.Ranged.CosmicFire>())
            {
                MELDOSAURUSED = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (MELDOSAURUSED)
            {
                framebuffer++;
                if (framebuffer >= 6)
                {
                    framecounter++;
                    framebuffer = 0;
                }
                if (framecounter > 5)
                {
                    framecounter = 0;
                }

                Texture2D deusheadsprite = (ModContent.GetTexture("CalValEX/AprilFools/Meldosaurus/JharimsBane"));

                int deusheadheight = framecounter * (deusheadsprite.Height / 6);

                Rectangle deusheadsquare = new Rectangle(0, deusheadheight, deusheadsprite.Width, deusheadsprite.Height / 6);
                Color deusheadalpha = npc.GetAlpha(drawColor);
                spriteBatch.Draw(deusheadsprite, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), deusheadsquare, deusheadalpha, npc.rotation, Utils.Size(deusheadsquare) / 2f, npc.scale, SpriteEffects.None, 0f);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}