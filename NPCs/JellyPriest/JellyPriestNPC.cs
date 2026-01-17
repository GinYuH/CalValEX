using System;
using System.Collections.Generic;
using System.IO;
using CalValEX.Items.Tiles;
using CalValEX.Items.Tiles.Banners;
using CalValEX.Items.Tiles.Blocks;
using CalValEX.Items.Tiles.Blueprints;
using CalValEX.Items.Tiles.Monoliths;
using CalValEX.Items.Tiles.Plants;
using CalValEX.Items.Tiles.Statues;
using CalValEX.Projectiles.NPCs;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Events;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static CalValEX.CalValEXConditions;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.NPCs.JellyPriest
{
    [AutoloadHead]
    public class JellyPriestNPC : ModNPC
    {
        public int shoptype = 1;

        public const string ShopName = "Shop";

        public override string Texture => "CalValEX/NPCs/JellyPriest/JellyPriestNPC";
        // public override string[] AltTextures => new[] { "CalValEX/NPCs/JellyPriest/JellyPriestNPC_Alt" };

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 25;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
            NPCID.Sets.AttackFrameCount[NPC.type] = 4;
            NPCID.Sets.DangerDetectRange[NPC.type] = 700;
            NPCID.Sets.AttackType[NPC.type] = 0;
            NPCID.Sets.AttackTime[NPC.type] = 90;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;

            NPC.Happiness
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Like) // Example Person prefers the forest.
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike) // Example Person dislikes the snow.
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Love) // Example Person likes the Example Surface Biome
                .SetNPCAffection(NPCID.Painter, AffectionLevel.Like) // Loves living near the dryad.
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Dislike) // Hates living near the demolitionist.
            ;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Bestiary")),
            });
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 15;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Mechanic;
            if (CalValEX.CalamityActive)
            {
                SpawnModBiomes = [CalValEX.CalamityBiome("SulphurousSeaBiome").Type];
                NPC.buffImmune[CalValEX.CalamityBuff("SulphuricPoisoning")] = true;
                NPC.buffImmune[CalValEX.CalamityBuff("Irradiated")] = true;
            }
        }

        private bool jellyspawn = false;

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (CalValEXWorld.rescuedjelly && !CalValEXConfig.Instance.TownNPC)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void AI()
        {
            if (!CalValEXWorld.rescuedjelly)
                CalValEXWorld.rescuedjelly = true;
            NPC.breath += 2;
            if (CalValEXConfig.Instance.TownNPC)
            {
                NPC.active = false;
            }
        }

        public override List<string> SetNPCNameList() => new List<string>()
        {
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName1"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName2"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName3"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName4"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName5"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName6"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName7"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName8"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName9"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName10"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName11"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName12"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName13"),
            Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.NPCName14")
        };

        public override string GetChat()
        {
            Player player = Main.player[Main.myPlayer];
            CalValEXPlayer CalValEXPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (CalValEX.CalamityActive)
                if (NPC.AnyNPCs(CalValEX.CalamityNPC("Anahita")))
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:
                            return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.0");

                        default:
                            return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.00");
                    }
                }

            if (CalValEXPlayer.sirember && CalValEXPlayer.bossded <= 0)
            {
                return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.bossed");
            }

            if (CalValEXPlayer.sirember && CalValEXPlayer.bossded > 0 && NPC.GivenName != "Kuti")
            {
                return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Kuti0");
            }

            if (CalValEXPlayer.sirember && CalValEXPlayer.bossded > 0 && NPC.GivenName == "Kuti")
            {
                return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Kuti1");
            }

            if (NPC.homeless)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.homeless1");

                    default:
                        return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.homeless2");
                }
            }

            WeightedRandom<string> dialogue = new();

            if (CalValEX.CalamityActive)
            {
                int WITCH = NPC.FindFirstNPC(CalValEX.CalamityNPC("WITCH"));
                if (WITCH >= 0)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.WITCH1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.WITCH2"));
                }

                int SEAHOE = NPC.FindFirstNPC(CalValEX.CalamityNPC("SEAHOE"));
                if (SEAHOE >= 0)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.SEAHOE1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.SEAHOE2"));
                }
                if ((bool)CalValEX.Calamity.Call("GetBossDowned", "leviathan") && !NPC.AnyNPCs(CalValEX.CalamityNPC("Anahita")))
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.leviathan1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.leviathan2"));

                }
                if ((bool)CalValEX.Calamity.Call("GetBossDowned", "scal"))
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.scal"));
                }

                if (player.ownedProjectileCounts[CalValEX.CalamityProjectile("WaterElementalMinion")] > 0)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.WaterElementalMinion"));
                }

                if (CalValEXPlayer.SirenHeart)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.SirenHeart"));
                }

                if (player.ownedProjectileCounts[CalValEX.CalamityProjectile("OceanSpirit")] > 0)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.OceanSpirit"));
                }
            }

            if (CalValEXPlayer.babywaterclone)
            {
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.babywaterclone"));
            }

            if (Main.eclipse)
            {
                switch (Main.rand.Next(2))
                {
                    case 0:
                        return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.eclipse1");

                    default:
                        return Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.eclipse2");
                }
            }

            if (BirthdayParty.PartyIsUp)
            {
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.PartyIsUp1"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.PartyIsUp2"));
            }

            if (!Main.bloodMoon)
            {
                if (Main.dayTime)
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Day1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Day2"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Day3"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Day4"));
                }
                else
                {
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Night1"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Night2"));
                    dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.Night3"));
                }
            }
            else
            {
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.bloodMoon1"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.bloodMoon2"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.bloodMoon3"));
                dialogue.Add(Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.Chat.bloodMoon4"));
            }
            return dialogue;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            if (shoptype == 1)
                button = Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.ChatButtons1");
            else if (shoptype == 2)
                button = Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.ChatButtons2");
            else if (shoptype == 3)
                button = Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.ChatButtons3");
            else if (shoptype == 4)
                button = Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.ChatButtons4");
            else
                button = Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.ChatButtons5");
            button2 = Language.GetTextValue("Mods.CalValEX.NPCs.JellyPriestNPC.ChatButtons0");
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                //shop = ShopName;
                {
                    if (shoptype == 1)
                    {
                        shop = "Blocks1";
                    }
                    else if (shoptype == 2)
                    {
                        shop = "Blocks2";
                    }
                    else if (shoptype == 3)
                    {
                        shop = "Furniture";
                    }
                    else
                    {
                        shop = "Plant";
                    }
                }
            }
            else if (!firstButton)
            {
                if (shoptype < 4)
                {
                    shoptype++;
                }
                else
                {
                    shoptype = 1;
                }
            }
        }
        public static List<(string, int, int, Condition, string)> shopEntries = new();

        public override void AddShops()
        {
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("WulfrumPlating"), Item.buyPrice(0, 0, 0, 20), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("LaboratoryPlating"), Item.buyPrice(0, 0, 0, 25), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("LaboratoryPanels"), Item.buyPrice(0, 0, 0, 25), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("HazardChevronPanels"), Item.buyPrice(0, 0, 0, 25), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("RustedPlating"), Item.buyPrice(0, 0, 0, 25), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("LaboratoryPipePlating"), Item.buyPrice(0, 0, 0, 25), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("RustedPipes"), Item.buyPrice(0, 0, 0, 25), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("Acidwood"), Item.buyPrice(0, 0, 5, 0), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("SulphurousSand"), Item.buyPrice(0, 0, 0, 35), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("SulphurousShale"), Item.buyPrice(0, 0, 0, 35), calamity, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("EutrophicSand"), Item.buyPrice(0, 0, 0, 50), clam, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("Navystone"), Item.buyPrice(0, 0, 0, 75), clam, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("SmoothNavystone"), Item.buyPrice(0, 0, 2, 5), clam, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("AbyssGravel"), Item.buyPrice(0, 0, 2, 5), Condition.DownedSkeletron, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("StatigelBlock"), Item.buyPrice(0, 0, 3, 5), sg, ""));
            shopEntries.Add(("Blocks1", ItemType<Booger>(), Item.buyPrice(0, 0, 10, 0), new Condition("Lots of angler quests", () => Main.LocalPlayer.anglerQuestsFinished > 22), ""));
            shopEntries.Add(("Blocks1", ItemType<Aeroplate>(), Item.buyPrice(0, 0, 5, 0), Condition.Hardmode, ""));
            shopEntries.Add(("Blocks1", ItemType<AstralGrass>(), Item.buyPrice(0, 0, 0, 10), Condition.Hardmode, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("AstralMonolith"), Item.buyPrice(0, 0, 2, 5), Condition.Hardmode, ""));
            shopEntries.Add(("Blocks1", ItemType<AstralPearlBlock>(), Item.buyPrice(0, 0, 3, 0), Condition.Hardmode, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("BrimstoneSlag"), Item.buyPrice(0, 0, 4, 0), brim, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("ScorchedRemains"), Item.buyPrice(0, 0, 4, 0), brim, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("PyreMantle"), Item.buyPrice(0, 0, 4, 50), Condition.DownedGolem, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("PyreMantleMolten"), Item.buyPrice(0, 0, 4, 50), Condition.DownedGolem, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("Voidstone"), Item.buyPrice(0, 0, 5, 0), Condition.DownedGolem, ""));
            shopEntries.Add(("Blocks1", CalValEX.CalamityItem("PlaguedContainmentBrick"), Item.buyPrice(0, 0, 5, 0), pb, ""));
            shopEntries.Add(("Blocks1", ItemType<PlagueHiveWand>(), Item.buyPrice(0, 1, 40, 0), pb, ""));
            shopEntries.Add(("Blocks1", ItemType<Items.Walls.PlagueHiveWall>(), Item.buyPrice(0, 0, 0, 10), pb, ""));
            shopEntries.Add(("Blocks1", ItemType<Necrostone>(), Item.buyPrice(0, 0, 10, 0), rav, ""));

            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("UelibloomBrick"), Item.buyPrice(0, 0, 20, 0), prov, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("ProfanedRock"), Item.buyPrice(0, 0, 66, 66), prov, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("ProfanedCrystal"), Item.buyPrice(0, 6, 66, 66), prov, ""));
            shopEntries.Add(("Blocks2", ItemType<ChiseledBloodstone>(), Item.buyPrice(0, 0, 40, 0), prov, ""));
            shopEntries.Add(("Blocks2", ItemType<PhantowaxBlock>(), Item.buyPrice(0, 0, 50, 0), polt, ""));
            shopEntries.Add(("Blocks2", ItemType<EidolicSlab>(), Item.buyPrice(0, 1, 0, 0), polt, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("StratusBricks"), Item.buyPrice(0, 0, 80, 0), polt, ""));
            shopEntries.Add(("Blocks2", ItemType<AzufreSludge>(), Item.buyPrice(0, 0, 50, 0), boomer, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("CosmiliteBrick"), Item.buyPrice(0, 0, 80, 0), dog, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("OtherworldlyStone"), Item.buyPrice(0, 0, 90, 0), dog, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("SilvaCrystal"), Item.buyPrice(0, 1, 0, 0), dog, ""));
            shopEntries.Add(("Blocks2", ItemType<AuricBrick>(), Item.buyPrice(0, 1, 50, 0), yharon, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("ExoPlating"), Item.buyPrice(0, 2, 0, 0), exo, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("ExoPrismPanel"), Item.buyPrice(0, 2, 0, 0), exo, ""));
            shopEntries.Add(("Blocks2", CalValEX.CalamityItem("OccultBrickItem"), Item.buyPrice(0, 2, 0, 0), scal, ""));

            shopEntries.Add(("Furniture", ItemType<C>(), Item.buyPrice(0, 1, 0, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Furniture", ItemType<Items.Tiles.FurnitureSets.Wulfrum.WulfrumGlobe>(), Item.buyPrice(0, 1, 0, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Furniture", CalValEX.CalamityItem("LaboratoryConsoleItem"), Item.buyPrice(0, 2, 50, 0), calamity, ""));
            shopEntries.Add(("Furniture", ItemType<SulphurGeyser>(), Item.buyPrice(0, 0, 10, 0), calamity, ""));
            shopEntries.Add(("Furniture", ItemType<SunkenLamp>(), Item.buyPrice(0, 0, 50, 0), clam, ""));
            shopEntries.Add(("Furniture", ItemType<Knight>(), Item.buyPrice(0, 0, 95, 0), Condition.Hardmode, ""));
            shopEntries.Add(("Furniture", ItemType<DecommissionedDaedalusGolem>(), Item.buyPrice(0, 2, 50, 0), cirno, ""));
            shopEntries.Add(("Furniture", ItemType<AuroraMonolith>(), Item.buyPrice(0, 4, 0, 0), cirno, ""));
            shopEntries.Add(("Furniture", ItemType<CalamitousMonolith>(), Item.buyPrice(0, 5, 0, 0), cala, ""));
            shopEntries.Add(("Furniture", ItemType<VeilBanner>(), Item.buyPrice(0, 4, 0, 0), Condition.DownedPlantera, ""));
            shopEntries.Add(("Furniture", ItemType<JunkArt>(), Item.buyPrice(0, 4, 0, 0), Condition.DownedPlantera, ""));
            shopEntries.Add(("Furniture", ItemType<HeartoftheCommunity>(), Item.buyPrice(0, 2, 0, 0), lev, ""));
            shopEntries.Add(("Furniture", ItemType<ShrineoftheTides>(), Item.buyPrice(0, 5, 0, 0), lev, ""));
            shopEntries.Add(("Furniture", ItemType<AquaticMonolith>(), Item.buyPrice(0, 6, 0, 0), lev, ""));
            shopEntries.Add(("Furniture", ItemType<PlagueMonolith>(), Item.buyPrice(0, 7, 0, 0), pb, ""));
            shopEntries.Add(("Furniture", ItemType<BubbleMachine>(), Item.buyPrice(0, 35, 0, 0), Condition.DownedMartians, ""));
            shopEntries.Add(("Furniture", ItemType<PlagueDialysis>(), Item.buyPrice(0, 1, 50, 0), jun, ""));
            shopEntries.Add(("Furniture", ItemType<Provibust>(), Item.buyPrice(1, 50, 0, 0), prov, ""));
            shopEntries.Add(("Furniture", ItemType<UnholyMonolith>(), Item.buyPrice(0, 40, 0, 0), prov, ""));
            shopEntries.Add(("Furniture", ItemType<Tesla>(), Item.buyPrice(2, 75, 0, 0), weavie, ""));
            shopEntries.Add(("Furniture", ItemType<Evolution>(), Item.buyPrice(1, 75, 0, 0), toaster, ""));
            shopEntries.Add(("Furniture", ItemType<VoidPortal>(), Item.buyPrice(2, 75, 0, 0), toaster, ""));
            shopEntries.Add(("Furniture", ItemType<MireAquarium>(), Item.buyPrice(2, 0, 0, 0), boomer, ""));
            shopEntries.Add(("Furniture", ItemType<SulphuricTank>(), Item.buyPrice(0, 50, 0, 0), boomer, ""));
            shopEntries.Add(("Furniture", ItemType<Help>(), Item.buyPrice(76, 0, 0, 0), boomer, ""));
            shopEntries.Add(("Furniture", ItemType<DimensionalMonolith>(), Item.buyPrice(0, 13, 0, 0), dog, ""));
            shopEntries.Add(("Furniture", ItemType<InfernalMonolith>(), Item.buyPrice(0, 15, 0, 0), yharon, ""));
            shopEntries.Add(("Furniture", ItemType<SamLog>(), Item.buyPrice(5, 0, 0, 0), sammy, ""));
            shopEntries.Add(("Furniture", ItemType<ExoMonolith>(), Item.buyPrice(0, 30, 0, 0), exo, ""));
            shopEntries.Add(("Furniture", ItemType<BrimstoneHeart>(), Item.buyPrice(0, 20, 0, 0), scal, ""));
            shopEntries.Add(("Furniture", ItemType<CalamitasBanner>(), Item.buyPrice(0, 60, 0, 0), scal, ""));
            shopEntries.Add(("Furniture", ItemType<DemonShield>(), Item.buyPrice(0, 60, 0, 0), scal, ""));
            shopEntries.Add(("Furniture", ItemType<Se>(), Item.buyPrice(6, 66, 66, 66), scal, ""));

            shopEntries.Add(("Plant", ItemType<FleshThing>(), Item.buyPrice(0, 0, 60, 0), Condition.NpcIsPresent(NPC.type), ""));
            shopEntries.Add(("Plant", ItemType<Anemone>(), Item.buyPrice(0, 0, 60, 0), clam, ""));
            shopEntries.Add(("Plant", ItemType<BrainCoral>(), Item.buyPrice(0, 0, 60, 0), clam, ""));
            shopEntries.Add(("Plant", ItemType<FanCoral>(), Item.buyPrice(0, 0, 60, 0), clam, ""));
            shopEntries.Add(("Plant", ItemType<SSCoral>(), Item.buyPrice(0, 0, 60, 0), clam, ""));
            shopEntries.Add(("Plant", ItemType<TableCoral>(), Item.buyPrice(0, 0, 60, 0), clam, ""));
            shopEntries.Add(("Plant", ItemType<SulphurousCactus>(), Item.buyPrice(0, 0, 30, 0), acid, ""));
            shopEntries.Add(("Plant", ItemType<SulphurousPlanter>(), Item.buyPrice(0, 0, 40, 0), acid, ""));
            shopEntries.Add(("Plant", ItemType<PottedDecapoditaSprout>(), Item.buyPrice(0, 0, 50, 0), crab, ""));
            shopEntries.Add(("Plant", ItemType<MonolithPot>(), Item.buyPrice(0, 0, 95, 0), Condition.Hardmode, ""));
            shopEntries.Add(("Plant", ItemType<BelchingCoral>(), Item.buyPrice(0, 2, 0, 0), aqua, ""));
            shopEntries.Add(("Plant", ItemType<AstralOldPurple>(), Item.buyPrice(0, 1, 0, 0), ass, ""));
            shopEntries.Add(("Plant", ItemType<AstralOldYellow>(), Item.buyPrice(0, 1, 0, 0), ass, ""));
            shopEntries.Add(("Plant", ItemType<BotanicPot>(), Item.buyPrice(0, 20, 0, 0), prov, ""));
            shopEntries.Add(("Plant", ItemType<HolyStudedBonsai>(), Item.buyPrice(0, 20, 0, 0), prov, ""));
            shopEntries.Add(("Plant", ItemType<BloodstoneRoses>(), Item.buyPrice(0, 20, 0, 0), prov, ""));
            shopEntries.Add(("Plant", ItemType<NetherTree>(), Item.buyPrice(1, 75, 0, 0), siggy, ""));
            shopEntries.Add(("Plant", ItemType<NetherTreeBig>(), Item.buyPrice(2, 75, 0, 0), siggy, ""));
            shopEntries.Add(("Plant", ItemType<EidolonTree>(), Item.buyPrice(0, 50, 0, 0), polt, ""));

            var blockShop = new NPCShop(Type, "Blocks1");
            var blockShop2 = new NPCShop(Type, "Blocks2");
            var furnShop = new NPCShop(Type, "Furniture");
            var plantShop = new NPCShop(Type, "Plant");

            for (int i = 0; i < shopEntries.Count; i++)
            {
                var shop = blockShop;
                if (shopEntries[i].Item2 > -1)
                {
                    if (blockShop.Name == shopEntries[i].Item1)
                    {
                        shop = blockShop;
                    }
                    if (blockShop2.Name == shopEntries[i].Item1)
                    {
                        shop = blockShop2;
                    }
                    if (furnShop.Name == shopEntries[i].Item1)
                    {
                        shop = furnShop;
                    }
                    if (plantShop.Name == shopEntries[i].Item1)
                    {
                        shop = plantShop;
                    }
                    shop.Add(shopEntries[i].Item2, shopEntries[i].Item4);
                }
            }
            blockShop.Register();
            blockShop2.Register();
            furnShop.Register();
            plantShop.Register();
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
                Dust.NewDustPerfect(NPC.Center + position, DustID.Adamantite, Vector2.Zero).noGravity = true;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 9;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 16;
            randExtraCooldown = 16;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileType<InkShot>();
            attackDelay = 1;
            return;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            randomOffset = 2f;
            multiplier = 24f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            /*if (Main.netMode == NetmodeID.Server)
                return;

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JellyPriest").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JellyPriest2").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("JellyPriest3").Type, 1f);
            }*/
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(shoptype);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            shoptype = reader.ReadInt32();
        }
    }
}
