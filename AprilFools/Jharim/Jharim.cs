using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Utilities;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using Terraria.Localization;
using CalValEX.AprilFools.Fanny;

namespace CalValEX.AprilFools.Jharim
{
    [AutoloadHead]
    public class Jharim : ModNPC
    {
        private bool MELDOSAURUSED;
        private int textcounter;
        public int framebuffer = 0;
        public int framecounter = 0;
        public bool firinglaser = false;
        public bool lasercheck = false;
        public int shopnum = 1;
        public static int maxshops = 18;
        public List<int> cvitems = new() { };
        Vector2 jharimpos;

        private static int ShimmerHeadIndex;
        private static Profiles.StackedNPCProfile NPCProfile;

        public override void Load()
        {
            // Adds our Shimmer Head to the NPCHeadLoader.
            ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, "CalValEX/AprilFools/Jharim/Jharim_Shimmer_Head");
        }

        public override void SetStaticDefaults()
        {
           // DisplayName.SetDefault("Jungle Tyrant");
            Main.npcFrameCount[NPC.type] = 23;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;

            NPCID.Sets.ShimmerTownTransform[Type] = true; // Allows for this NPC to have a different texture after touching the Shimmer liquid.


            // This creates a "profile" for ExamplePerson, which allows for different textures during a party and/or while the NPC is shimmered.
            NPCProfile = new Profiles.StackedNPCProfile(
                new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture),
                new Profiles.DefaultNPCProfile("CalValEX/AprilFools/Jharim/Jharim_Shimmer", ShimmerHeadIndex)
            );
            NPC.Happiness
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Like) // Example Person prefers the jungle.
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Hate) // Example Person dislikes the ocean.
                .SetBiomeAffection<MushroomBiome>(AffectionLevel.Love) // Example Person likes the shroom biome.
                .SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Pirate, AffectionLevel.Like) // Likes living near the pirate.
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Dislike) // Dislikes living near the tax collector.
            ;
            if (CalValEX.CalamityActive)
                NPC.Happiness.SetNPCAffection(CalValEX.CalamityNPC("WITCH"), AffectionLevel.Hate); // Hates living near scal.

            if (!CalValEX.AprilFoolMonth)
            {
                NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
                {
                    Hide = true
                };
                NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            }
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 1;
            NPC.defense = 150;
            NPC.lifeMax = 250000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.PartyGirl;
        }
        public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
        {
            if (CalValEX.AprilFoolMonth || !CalValEX.CalamityActive)
            {
                bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
                Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
                new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Bestiary")),
            });
            }
        }
        private void EdgyTalk(string text, Color color, bool combatText = false)
        {
            if (combatText)
            {
                CombatText.NewText(NPC.getRect(), color, text, true);
            }
            else
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(text, color);
                }
                else if (Main.netMode == NetmodeID.Server)
                {
                    //NetMessage.BroadcastChatMessage(NetworkText.FromKey(text), color);
                }
            }
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override void AI()
        {
            if (!CalValEX.AprilFoolMonth && CalValEX.CalamityActive && !Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().Blok)
            {
                NPC.active = false;
            }
            if (!CalValEXWorld.jharim)
            {
                CalValEXWorld.jharim = true;
            }
            if (CalValEX.CalamityActive)
            {
                if (NPC.AnyNPCs(CalValEX.CalamityNPC("WITCH")) && !MELDOSAURUSED && NPC.life < NPC.lifeMax * 0.15f)
                {
                    if (!lasercheck)
                    {
                        lasercheck = true;
                        firinglaser = true;
                    }
                    for (int x = 0; x < Main.maxNPCs; x++)
                    {
                        NPC npc3 = Main.npc[x];
                        if (npc3.type == CalValEX.CalamityNPC("WITCH"))
                        {
                            if (npc3.active)
                            {
                                jharimpos.X = npc3.Center.X;
                                jharimpos.Y = npc3.Center.Y;
                                if (npc3.position.X - NPC.position.X >= 0)
                                {
                                    NPC.direction = 1;
                                }
                                else
                                {
                                    NPC.direction = -1;
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
                        Vector2 position = NPC.Center;
                        position.X = NPC.Center.X;
                        Vector2 direction = jharimpos - position;
                        direction.Normalize();
                        float speed = 10f;
                        int type = CalValEX.CalamityActive ? Mod.Find<ModProjectile>("JharimLaser").Type : 0;
                        int damage = 6666666;
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
                        firinglaser = false;
                        NPC.defense = 999;
                    }
                }
            }
            if (MELDOSAURUSED)
            {
                NPC.dontTakeDamage = true;
                NPC.dontTakeDamageFromHostiles = true;
                textcounter++;
                NPC.velocity.X *= 0.04f;
                if (textcounter == 1)
                {
                    EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.EdgyTalk1"), Color.White, true);
                }
                else if (textcounter == 120)
                {
                    EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.EdgyTalk2"), Color.White, true);
                }
                else if (textcounter == 240 || textcounter == 280 || textcounter == 300 || textcounter == 310 || textcounter == 305 || textcounter == 310 || textcounter == 315 || textcounter == 320)
                {
                    EdgyTalk(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.EdgyTalk3"), Color.DarkGreen, true);
                }
                if (textcounter == 360)
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCHit1, NPC.position);
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Meldosaurus.Meldosaurus>());
                    NPC.active = false;
                }

            }
        }

        public override List<string> SetNPCNameList() => new() { Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.NPCName") };
        public override ITownNPCProfile TownNPCProfile()
        {
            return NPCProfile;
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override string GetChat()
        {
            CalValEXGlobalNPC.jharim = NPC.whoAmI;
            Player player = Main.player[Main.myPlayer];
            CalValEXPlayer CalValEXPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (!MELDOSAURUSED)
            {
                if (NPC.homeless)
                {
                    switch (Main.rand.Next(3))
                    {
                        case 0:
                            return Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.homeless1");

                        case 1:
                            return Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.homeless2");

                        default:
                            return Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.homeless3");
                    }
                }

                WeightedRandom<string> dialogue = new();

                if ((NPC.AnyNPCs(NPCID.LunarTowerNebula) || NPC.AnyNPCs(NPCID.LunarTowerVortex) || NPC.AnyNPCs(NPCID.LunarTowerStardust) || NPC.AnyNPCs(NPCID.LunarTowerSolar)) && Main.rand.NextFloat() < 0.25f)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.LunarTower"));
                }

                if (CalValEX.CalamityActive)
                {
                    int Cal = NPC.FindFirstNPC(CalValEX.CalamityNPC("WITCH"));
                    if (Cal >= 0)
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.WITCH"));
                    }

                    int SEAHOE = NPC.FindFirstNPC(CalValEX.CalamityNPC("SEAHOE"));
                    if (SEAHOE >= 0)
                    {
                        if (Cal >= 0)
                            dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.SEAHOE1"));
                        else
                            dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.SEAHOE2"));
                    }

                    if (NPC.AnyNPCs(CalValEX.CalamityNPC("Draedon")))
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Draedon"));
                    }

                    if (NPC.AnyNPCs(CalValEX.CalamityNPC("THELORDE")))
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.THELORDE"));
                    }

                    if (NPC.AnyNPCs(CalValEX.CalamityNPC("Yharon")))
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Yharon"));
                    }

                    Mod clamMod = CalValEX.Calamity;
                    if (((bool)clamMod.Call("GetBossDowned", "supremecalamitas")) && ((bool)clamMod.Call("GetBossDowned", "exomechs")))
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.GetBossDowned1"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.GetBossDowned2"));
                    }

                    if (player.ownedProjectileCounts[CalValEX.CalamityProjectile("WaterElementalMinion")] > 0)
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.WaterElementalMinion"));
                    }
                }

                if (Main.eclipse)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.eclipse1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.eclipse2"));
                    
                }

                if (CalValEX.CalamityActive)
                {
                    if ((bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush"))
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.bossrush1"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.bossrush2"));
                    }

                    if ((bool)CalValEX.Calamity.Call("AcidRainActive"))
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.AcidRainActive1"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.AcidRainActive2"));
                    }
                }

                if (BirthdayParty.PartyIsUp)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.PartyIsUp1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.PartyIsUp2"));
                }

                if (!Main.bloodMoon)
                {
                    if (Main.dayTime)
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Day1"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Day2"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Day3"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Day4"));
                        
                    }
                    else
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Night1"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Night2"));
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.Night3"));
                    }
                }
                else
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.bloodMoon1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.bloodMoon2"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.bloodMoon3"));
                }
                return dialogue;
            }
            else
            {
                return Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.Chat.0");
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (!MELDOSAURUSED)
            {
                button = CalValEX.CalamityActive ? Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.ChatButtons0") : Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.ChatButtons1") + shopnum;
                button2 = CalValEX.CalamityActive ? Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.ChatButtons3") : Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.ChatButtons2");
            }
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (!MELDOSAURUSED)
            {
                if (firstButton)
                {
                    if (CalValEX.CalamityActive)
                        shopName = "Jharim";
                    else
                        shopName = "Jharim" + shopnum;
                }
                else if (!firstButton)
                {
                    if (CalValEX.CalamityActive)
                    {
                        if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                        {
                            if ((bool)CalValEX.Calamity.Call("GetBossDowned", "scal") && (bool)CalValEX.Calamity.Call("GetBossDowned", "draedon"))
                            {
                                NPC.active = false;
                                NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<Fogbound>());
                                Main.NewText(Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.ButtonClicked1"), Color.Gray);
                                NPC.HitEffect();
                            }
                            else
                            {
                                Main.npcChatText = Language.GetTextValue("Mods.CalValEX.NPCs.Jharim.ButtonClicked2");
                            }
                        }
                    }
                    else
                    {
                        if (shopnum < maxshops)
                            shopnum++;
                        else
                            shopnum = 1;
                    }
                }
            }
        }
        public override void AddShops()
        {
            if (CalValEX.CalamityActive)
            {
                var blockShop = new NPCShop(Type, "Jharim");
                blockShop.Add(ItemType<AmogusItem>());
                blockShop.Add(ItemType<FriendExtinguisher>());
                blockShop.Add(ItemType<PaintedGoggles>());
                blockShop.Add(ItemType<Fool>());
                blockShop.Add(ItemType<FoolEX>());
                blockShop.Register();
            }
            else
            {
                for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
                {
                    Item item = ContentSamples.ItemsByType[i];
                    // if the item is from CalVal, add it to the shop
                    if ((item.ModItem?.Mod ?? null) == CalValEX.instance && item.Name != "Compensation")
                    {
                        cvitems.Add(i);
                    }
                }

                int shopCount = (int)Math.Ceiling(cvitems.Count / 39f);
                maxshops = shopCount;

                int progress = 0;

                for (int s = 1; s < maxshops + 1; s++)
                {
                    var shope = new NPCShop(Type, "Jharim" + s);
                    for (int i = 0 + progress; i < 39 + progress; i++)
                    {
                        if (i < cvitems.Count)
                        {
                            Item item = ContentSamples.ItemsByType[cvitems[i]];
                            item.value = DecidePrice(item);
                            shope.Add(item);
                        }
                    }
                    progress += 39;
                    shope.Register();
                }
            }
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {
        }

        public static int DecidePrice(Item item)
        {
            int price = 0;
            // Ore-HM
            if (item.rare < ItemRarityID.LightRed)
            {
                price = Item.buyPrice(gold: 20);
                // Blocks
                if ((item.createTile > TileID.Dirt || item.createWall > 0) && item.rare == ItemRarityID.White)
                {
                    price /= 1000;
                }
            }
            // Post-ML
            else if (item.rare > ItemRarityID.Red)
            {
                price = Item.buyPrice(gold: 60);
            }
            // Hardmode
            else
            {
                price = Item.buyPrice(gold: 40);
            }
            return price;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (!CalValEXConfig.Instance.TownNPC)
            {
                if (CalValEX.CalamityActive && CalValEX.AprilFoolWeek && CalValEXWorld.jharim)
                {
                    return true;
                }
                else if (!CalValEX.CalamityActive)
                {
                    // If Fables is enabled, only move in after defeating all 3 bosses
                    if (CalValEX.FablesActive)
                    {
                        return Main.hardMode;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)NPC.whoAmI);
                packet.Send();
            }
            else
            {
                StatueTeleport();
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JharimGore").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JharimGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JharimGore3").Type, 1f);
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
                Dust.NewDustPerfect(NPC.Center + position, DustID.Adamantite, Vector2.Zero).noGravity = true;
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

        [JITWhenModsEnabled("CalamityMod")]
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            int prod = CalValEX.CalamityActive ? Mod.Find<ModProjectile>("JharimLaser").Type : 0;
            if (!MELDOSAURUSED)
                projType = prod;
            else
                projType = ProjectileID.None;
            attackDelay = 1;
            return;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            randomOffset = 2f;
            multiplier = 24f;
        }

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (!NPC.IsShimmerVariant)
            {
                if (CalValEX.CalamityActive)
                {
                    if (projectile.type == CalValEX.CalamityProjectile("CosmicFire"))
                    {
                        MELDOSAURUSED = true;
                    }
                }
                else if (projectile.type == ProjectileID.VortexBeaterRocket)
                {
                    MELDOSAURUSED = true;
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
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

                Texture2D deusheadsprite = (Request<Texture2D>("CalValEX/AprilFools/Meldosaurus/JharimsBane").Value);

                int deusheadheight = framecounter * (deusheadsprite.Height / 6);

                Rectangle deusheadsquare = new(0, deusheadheight, deusheadsprite.Width, deusheadsprite.Height / 6);
                Color deusheadalpha = NPC.GetAlpha(drawColor);
                spriteBatch.Draw(deusheadsprite, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), deusheadsquare, deusheadalpha, NPC.rotation, Utils.Size(deusheadsquare) / 2f, NPC.scale, SpriteEffects.None, 0);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}