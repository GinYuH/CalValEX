using CalValEX.Items;
using CalValEX.Items.Pets;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Projectiles.NPCs;
using System.Collections.Generic;
using Terraria.GameContent.Personalities;
using Terraria.Utilities;
using CalValEX.Items.Pets.TownPets;
using static CalValEX.CalValEXConditions;

namespace CalValEX.NPCs.Oracle
{
    [AutoloadHead]
    public class OracleNPC : ModNPC
    {
        public override string Texture => "CalValEX/NPCs/Oracle/OracleNPC";

        public const string ShopName = "Shop";

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 23;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Like) // Loves living near the dryad.
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Dislike) // Hates living near the demolitionist.
            ;
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 15;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Mechanic;
        }

        public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
                Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Bestiary")),
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (!CalValEXConfig.Instance.TownNPC)
            {
                for (int k = 0; k < Main.maxPlayers; k++)
                {
                    Player player = Main.player[k];
                    if (!player.active)
                        continue;

                    if (player.miscEquips[0].type != ItemID.None || player.miscEquips[1].type != ItemID.None)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override List<string> SetNPCNameList() => new List<string>()
        {
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName1"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName2"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName3"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName4"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName5"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName6"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName7"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName8"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName9"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName10"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName11"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName12"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName13"),
            Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.NPCName14")
        };


        public override string GetChat()
        {
            if (NPC.homeless)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        return Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.homeless1");

                    default:
                        return Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.homeless2");
                }
            }

            WeightedRandom<string> dialogue = new();

            if (Main.player[Main.myPlayer].miscEquips[0].type != ItemID.None || Main.player[Main.myPlayer].miscEquips[1].type != ItemID.None)
            {
                if (!Main.bloodMoon)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.miscEquips1") + Main.player[Main.myPlayer].name + Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.miscEquips11"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.miscEquips2"));
                }
                else
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.miscEquips3"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.miscEquips4"));
                }
            }

            if (Main.hardMode)
            {
                if (!CalValEXWorld.astro)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.astro1"));
                }
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.astro2"));
            }

            if (CalValEX.CalamityActive)
            {
                if (NPC.AnyNPCs(CalValEX.CalamityNPC("Dragonfolly")))
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Bumblefuck"));
                }

                int WITCH = NPC.FindFirstNPC(CalValEX.CalamityNPC("WITCH"));
                if (WITCH >= 0)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.WITCH"));
                }
            }

            int wizard = NPC.FindFirstNPC(NPCID.Wizard);
            if (wizard >= 0)
            {
                dialogue.Add(Main.npc[wizard].GivenName + Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Wizard1"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Wizard2") + Main.npc[wizard].GivenName + Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Wizard22"));
            }

            if (!Main.expertMode)
            {
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.expertMode1"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.expertMode2"));
            }

            if (Main.eclipse)
            {
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.eclipse1"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.eclipse2") + Main.player[Main.myPlayer].name + Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.eclipse2"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.eclipse3"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.eclipse4"));
            }

            if (BirthdayParty.PartyIsUp)
            {
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.PartyIsUp1"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.PartyIsUp2"));
            }

            if (!Main.bloodMoon)
            {
                if (Main.dayTime)
                {
                    if (!CalValEXWorld.ninja)
                    {
                        dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.ninja"));
                    }
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Day1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Day2"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Day3"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Day4") + Main.player[Main.myPlayer].name + Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Day44"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Day5"));
                }
                else
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Night1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Night2"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Night3"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Night4"));                    
                }
            }
            else
            {
                if (Main.rand.NextFloat() < 0.01f)
                {
                    //pet becomes angery at you for like, a minute (or you go far away from the npc)
                    OracleGlobalNPC.playerTarget = Main.player[Main.myPlayer].whoAmI;
                    OracleGlobalNPC.playerTargetTimer = Main.rand.Next(3600, 7201); //1 to 2 minutes
                    return Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.Next");
                }

                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.bloodMoon1"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.bloodMoon2"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.bloodMoon3"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.bloodMoon4") + Main.player[Main.myPlayer].name + Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.bloodMoon44"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.bloodMoon5"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.Chat.bloodMoon6"));
            }
            return dialogue;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.ChatButtons");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Pets";
            }
            else
            {
                if (Main.myPlayer == Main.LocalPlayer.whoAmI)
                {
                    if (!Main.LocalPlayer.GetModPlayer<OraclePlayer>().playerHasGottenBag)
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.ButtonClicked1");
                        Main.LocalPlayer.GetModPlayer<OraclePlayer>().playerHasGottenBag = true;
                        if (CalValEX.month == 10)
                        {
                            if (Main.rand.NextFloat() < 0.3f)
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoodieBag);
                            }
                            else if (Main.rand.NextFloat() < 0.1f)
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.Present);
                            }
                            else
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemType<MysteryPainting>());
                            }
                        }
                        else if (CalValEX.month == 12)
                        {
                            if (Main.rand.NextFloat() < 0.1f)
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoodieBag);
                            }
                            else if (Main.rand.NextFloat() < 0.3f)
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.Present);
                            }
                            else
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemType<MysteryPainting>());
                            }
                        }
                        else
                        {
                            if (Main.rand.NextFloat() < 0.2f)
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoodieBag);
                            }
                            else if (Main.rand.NextFloat() < 0.2f)
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.Present);
                            }
                            else
                            {
                                Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemType<MysteryPainting>());
                            }
                        }
                    }
                    else
                    {
                        Main.npcChatText = Language.GetTextValue("Mods.CalValEX.NPCs.OracleNPC.ButtonClicked2");
                    }
                }
            }
        }

        public static List<(string, int, int, Condition, string)> shopEntries = new();
        public override void AddShops()
        {
            shopEntries.Add(("Pets", ItemType<DoggoCollar>(), Item.buyPrice(0, 1, 50, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Pets", ItemType<BambooStick>(), Item.buyPrice(0, 2, 0, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Pets", ItemType<RuinedBandage>(), Item.buyPrice(0, 2, 50, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Pets", ItemType<Cube>(), Item.buyPrice(0, 3, 0, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Pets", ItemType<AerialiteBubble>(), Item.buyPrice(0, 5, 50, 0), evil, ""));
            shopEntries.Add(("Pets", ItemType<SmolEldritchHoodie>(), Item.buyPrice(0, 10, 0, 0), Condition.DownedPlantera, ""));
            shopEntries.Add(("Pets", ItemType<UglyTentacle>(), Item.buyPrice(0, 20, 0, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Pets", ItemType<BubbleGum>(), Item.buyPrice(0, 1, 0, 0), acid, ""));
            shopEntries.Add(("Pets", ItemType<MeatyWormTumor>(), Item.buyPrice(0, 20, 0, 0), hive, ""));
            shopEntries.Add(("Pets", ItemType<Corruppuccino>(), Item.buyPrice(0, 20, 0, 0), perf, ""));
            shopEntries.Add(("Pets", ItemType<CooperShortsword>(), Item.buyPrice(0, 10, 0, 0), cirno, ""));
            shopEntries.Add(("Pets", ItemType<BrimberryItem>(), Item.buyPrice(0, 15, 0, 0), brim, ""));
            shopEntries.Add(("Pets", ItemType<SuspiciousLookingGBC>(), Item.buyPrice(0, 20, 0, 0), cala, ""));
            shopEntries.Add(("Pets", ItemType<DeepseaLantern>(), Item.buyPrice(0, 20, 0, 0), cala, ""));
            shopEntries.Add(("Pets", ItemType<TheDragonball>(), Item.buyPrice(0, 75, 0, 0), birb, ""));
            shopEntries.Add(("Pets", ItemType<ProfanedChewToy>(), Item.buyPrice(0, 80, 0, 0), prov, ""));
            shopEntries.Add(("Pets", ItemType<SuspiciousLookingChineseCrown>(), Item.buyPrice(0, 95, 0, 0), siggy, ""));
            shopEntries.Add(("Pets", ItemType<Items.LightPets.CosmicBubble>(), Item.buyPrice(1, 0, 0, 0), dog, ""));
            shopEntries.Add(("Pets", ItemType<NuggetinaBiscuit>(), Item.buyPrice(1, 10, 0, 0), yharon, ""));
            shopEntries.Add(("Pets", ItemType<NuggetLicense>(), Item.buyPrice(1, 10, 0, 0), nugget, ""));

            var blockShop = new NPCShop(Type, "Pets");

            for (int i = 0; i < shopEntries.Count; i++)
            {
                var shop = blockShop;
                if (shopEntries[i].Item2 > -1)
                {
                    blockShop.Add(shopEntries[i].Item2, shopEntries[i].Item4);
                }
            }
            blockShop.Register();
        }

        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Item item = items[i];
                // Skip 'air' items and null items.
                if (item == null || item.type == ItemID.None)
                {
                    continue;
                }

                for (int j = 0; j < shopEntries.Count; j++)
                {
                    if (shopEntries[j].Item2 > -1)
                    {
                        if (item.type == shopEntries[j].Item2)
                        {
                            item.shopCustomPrice = shopEntries[j].Item3;
                        }
                    }
                }
            }
        }

        public override bool CanGoToStatue(bool toKingStatue)
        {
            return false;
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
                Dust.NewDustPerfect(NPC.Center + position, 50, Vector2.Zero).noGravity = true;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 13;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 20;
            randExtraCooldown = 20;
        }

        private int OracleWeapon = 0;

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            if (Main.rand.NextBool(7))
            {
                projType = ProjectileType<OracleNPC_8Ball>();
                attackDelay = 1;
                OracleWeapon = 1;
                return;
            }
            else
            {
                projType = ProjectileType<OracleNPC_Cards>();
                attackDelay = 1;
                OracleWeapon = 0;
            }
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            randomOffset = 2f;

            if (OracleWeapon == 0)
            {
                multiplier = 24f;
            }
            if (OracleWeapon == 1)
            {
                multiplier = 10f;
            }
        }

        public override bool PreAI()
        {
            OracleGlobalNPC.oracleNPC = NPC.whoAmI;
            return true;
        }

        public override void AI()
        {
            if (CalValEXConfig.Instance.TownNPC)
            {
                NPC.active = false;
            }
            bool iExist = false;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].type == ProjectileType<OracleNPCPet_Pet>() && Main.projectile[i].active)
                    iExist = true;
            }

            Vector2 position = NPC.position;
            position.Y -= 16f;

            if (!iExist)
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), position, NPC.velocity, ProjectileType<OracleNPCPet_Pet>(), NPC.damage, 0f, Main.myPlayer);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OracleNPC").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OracleNPC2").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OracleNPC3").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("OracleNPC4").Type, 1f);
            }
        }
    }
}