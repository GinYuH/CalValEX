using CalValEX.Items;
using CalValEX.Items.Critters;
using CalValEX.Items.Equips.Backs;
using CalValEX.Items.Equips.Balloons;
using CalValEX.Items.Equips.Capes;
using CalValEX.Items.Equips.Hats;
using CalValEX.Items.Equips.Legs;
using CalValEX.Items.Equips.Scarves;
using CalValEX.Items.Equips.Shields;
using CalValEX.Items.Equips.Shirts;
using CalValEX.Items.Equips.Transformations;
using CalValEX.Items.Equips.Wings;
using CalValEX.Items.Hooks;
using CalValEX.Items.LightPets;
using CalValEX.Items.Mounts;
using CalValEX.Items.Mounts.InfiniteFlight;
using CalValEX.Items.Mounts.Ground;
using CalValEX.Items.Mounts.LimitedFlight;
using CalValEX.Items.Mounts.Morshu;
using CalValEX.Items.Pets;
using CalValEX.Items.Pets.Scuttlers;
using CalValEX.Items.Pets.Elementals;
using CalValEX.Items.Pets.ExoMechs;
using CalValEX.Items.Tiles;
using CalValEX.Items.Tiles.Balloons;
using CalValEX.Items.Tiles.Blocks;
using CalValEX.NPCs.Critters;
using CalValEX.Items.Tiles.Plants;
using CalValEX.NPCs.Oracle;
using Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using CalValEX.AprilFools;
using CalValEX.NPCs.JellyPriest;
using CalValEX.CalamityID;
using ReLogic.Content;
using CalValEX.Items.Plushies;
using CalValEX.Items.Equips;
using CalValEX.Tiles.Paintings;
using CalValEX.Tiles.Plants;
using CalamityFables.Core;

namespace CalValEX
{
    public class CalValEXGlobalNPC : GlobalNPC
    {
        public bool bdogeMount;
        public bool geldonSummon;
        public bool junkoReference;
        public bool wolfram;

        public static int meldodon = -1;
        public static int jharim = -1;

        public override bool InstancePerEntity => true;
        public static Texture2D DeusBlightHead;
        public static Texture2D DeusBlightBody;
        public static Texture2D DeusBlightTail;
        public static Texture2D DeusBlightHeadGlow;
        public static Texture2D DeusBlightBodyGlow;
        public static Texture2D DeusBlightBodyAltGlow;
        public static Texture2D DeusBlightTailGlow;
        public static Texture2D PoGod;

        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                DeusBlightHead = ModContent.Request<Texture2D>("CalValEX/NPCs/AstrumDeus/DeusHeadOld", AssetRequestMode.ImmediateLoad).Value;
                DeusBlightBody = ModContent.Request<Texture2D>("CalValEX/NPCs/AstrumDeus/DeusBodyOld", AssetRequestMode.ImmediateLoad).Value;
                DeusBlightTail = ModContent.Request<Texture2D>("CalValEX/NPCs/AstrumDeus/DeusTailOld", AssetRequestMode.ImmediateLoad).Value;
                DeusBlightHeadGlow = ModContent.Request<Texture2D>("CalValEX/NPCs/AstrumDeus/DeusHeadOld_Glow", AssetRequestMode.ImmediateLoad).Value;
                DeusBlightBodyGlow = ModContent.Request<Texture2D>("CalValEX/NPCs/AstrumDeus/DeusBodyOld_Glow", AssetRequestMode.ImmediateLoad).Value;
                DeusBlightBodyAltGlow = ModContent.Request<Texture2D>("CalValEX/NPCs/AstrumDeus/DeusBodyAltOld_Glow", AssetRequestMode.ImmediateLoad).Value;
                DeusBlightTailGlow = ModContent.Request<Texture2D>("CalValEX/NPCs/AstrumDeus/DeusTailOld_Glow", AssetRequestMode.ImmediateLoad).Value;
                PoGod = ModContent.Request<Texture2D>("CalValEX/ExtraTextures/SlimeGod", AssetRequestMode.ImmediateLoad).Value;
            }
        }

        public override void ModifyShop(NPCShop shop)
        {
            Mod alchLite;
            ModLoader.TryGetMod("AlchemistNPCLite", out alchLite);
            Mod alchFull;
            ModLoader.TryGetMod("AlchemistNPC", out alchFull);
            int type = shop.NpcType;

            if (alchLite != null)
            {
                if (type == alchLite.Find<ModNPC>("Musician").Type)
                {
                    shop.Add(ModContent.ItemType<AstralMusicBox>(), CalValEXConditions.oreo);
                }
            }
            if (alchFull != null)
            {
                if (type == alchFull.Find<ModNPC>("Musician").Type)
                {
                    shop.Add(ModContent.ItemType<AstralMusicBox>(), CalValEXConditions.oreo);
                }
            }
            if (CalValEX.CalamityActive)
            {
                if (type == CalNPCID.SEAHOE)
                {
                    shop.Add(ModContent.ItemType<BloodwormScarf>(), CalValEXConditions.boomer);
                }
                if (type == CalNPCID.DILF)
                {
                    shop.Add(ModContent.ItemType<FrostflakeBrick>())
                        .Add(PaintingLoader.paintingItems["Signut"], CalValEXConditions.siggy);
                }
                if (type == CalNPCID.THIEF)
                {
                    shop.Add(ModContent.ItemType<AureicFedora>(), CalValEXConditions.oreo)
                        .Add(ModContent.ItemType<AstrachnidCranium>(), CalValEXConditions.oreo)
                        .Add(ModContent.ItemType<AstrachnidTentacles>(), CalValEXConditions.oreo)
                        .Add(ModContent.ItemType<AstrachnidThorax>(), CalValEXConditions.oreo);
                }
            }
            if (type == NPCID.Truffle)
            {
                shop.Add(ModContent.ItemType<OddMushroomPot>());
            }
            if (type == NPCID.Steampunker)
            {
                shop.Add(ModContent.ItemType<XenoSolution>(), Condition.Hardmode)
                    .Add(ModContent.ItemType<StarstruckSynthesizer>(), Condition.Hardmode);
            }
            if (type == NPCID.Dryad)
            {
                shop.Add(ModContent.ItemType<AstralGrass>(), Condition.Hardmode);
                shop.Add(ModContent.ItemType<CrepuscularMallow>(), Condition.BloodMoon, Condition.CorruptWorld);
                shop.Add(ModContent.ItemType<PaleblotBane>(), Condition.BloodMoon, Condition.CorruptWorld);
                shop.Add(ModContent.ItemType<SpiderPalm>(), Condition.BloodMoon, Condition.CrimsonWorld);
                shop.Add(ModContent.ItemType<MarrowWillow>(), Condition.BloodMoon, Condition.CrimsonWorld);
                shop.Add(ModContent.ItemType<CrepuscularMallow>(), Condition.BloodMoon, Condition.CrimsonWorld, Condition.InGraveyard);
                shop.Add(ModContent.ItemType<PaleblotBane>(), Condition.BloodMoon, Condition.CrimsonWorld, Condition.InGraveyard);
                shop.Add(ModContent.ItemType<SpiderPalm>(), Condition.BloodMoon, Condition.CorruptWorld, Condition.InGraveyard);
                shop.Add(ModContent.ItemType<MarrowWillow>(), Condition.BloodMoon, Condition.CorruptWorld, Condition.InGraveyard);
                shop.Add(ModContent.ItemType<ArcticWisteria>(), Condition.InSnow);
                shop.Add(ModContent.ItemType<EverfrostPine>(), Condition.InSnow);
            }
            if (type == NPCID.Truffle)
            {
                shop.Add(ModContent.ItemType<SwearshroomItem>())
                    .Add(ModContent.ItemType<ShroomiteVisage>(), Condition.DownedPlantera);
            }
            if (type == NPCID.Clothier)
            {
                shop.Add(ModContent.ItemType<PolterMask>(), CalValEXConditions.dungeon)
                    .Add(ModContent.ItemType<Polterskirt>(), CalValEXConditions.dungeon)
                    .Add(ModContent.ItemType<PolterStockings>(), CalValEXConditions.dungeon)
                    .Add(ModContent.ItemType<BanditHat>(), CalValEXConditions.bandit)
                    .Add(ModContent.ItemType<Permascarf>(), CalValEXConditions.perma);
            }
            if (type == NPCID.PartyGirl)
            {
                shop.Add(ModContent.ItemType<TiedMirageBalloon>(), Condition.PlayerCarriesItem(ModContent.ItemType<Mirballoon>()))
                    .Add(ModContent.ItemType<TiedBoxBalloon>(), Condition.PlayerCarriesItem(ModContent.ItemType<BoxBalloon>()))
                    .Add(ModContent.ItemType<TiedChaosBalloon>(), Condition.PlayerCarriesItem(ModContent.ItemType<ChaosBalloon>()))
                    .Add(ModContent.ItemType<TiedBoB2>(), Condition.PlayerCarriesItem(ModContent.ItemType<BoB2>()))
                    .Add(ModContent.ItemType<ChaoticPuffball>(), CalValEXConditions.polt)
                    .Add(ModContent.ItemType<AuricFirework>(), Condition.DownedMoonLord);
            }
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == ModContent.NPCType<OracleNPC>())
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OracleBeanie>(), 1));
            }
            if (!CalValEXConfig.Instance.DisableVanityDrops)
            {
                if (CalValEX.CalamityActive)
                {
                    if (npc.type == CalNPCID.DILF)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Permascarf>(), 1));
                    }
                    if (npc.type == CalNPCID.THIEF)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BanditHat>(), 1));
                    }
                    if (npc.type == CalValEX.CalamityNPC("BoxJellyfish"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BoxBalloon>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Rimehound"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TundraBall>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Rotdog"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RottenHotdog>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("PrismBack"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PrismShell>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Toxicatfish"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DecayingFishtail>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Trasher"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OlTrashtooth>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("DespairStone"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DespairMask>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Scryllar"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScryllianWings>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ScryllarRage"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScryllianWings>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("RepairUnitCritter"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DisrepairUnit>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("SuperDummy"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DummyMask>()));
                    }
                    if (npc.type == CalValEX.CalamityNPC("WulfrumAmplifier"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumTransmitter>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumKeys>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumHelipack>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("WulfrumGyrator"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumBalloon>(), 100));
                    }
                    if (npc.type == CalValEX.CalamityNPC("WulfrumRover"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoverSpindle>(), 1000));
                    }
                    if (npc.type == CalValEX.CalamityNPC("WulfrumHovercraft"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                    }
                    if (npc.type == CalValEX.CalamityNPC("WulfrumDrone"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Sunskater"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EssenceofYeet>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerAmethyst"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AmethystGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerSapphire"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SapphireGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerTopaz"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TopazGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerEmerald"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EmeraldGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerRuby"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RubyGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerDiamond"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DiamondGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerAmber"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AmberGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CrawlerCrystal"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrystalGeode>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ShockstormShuttle"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShuttleBalloon>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new MoonLord(), ModContent.ItemType<ExodiumMoon>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("SulphurousSkater"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AcidLamp>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("AeroSlime"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AeroWings>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("SeaFloaty"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FloatyCarpetItem>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Orthocera"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Help>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Trilobite"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TrilobiteShield>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Bohldohr"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Eggstone>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("FlakCrab"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlakHeadCrab>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("GammaSlime"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GammaHelmet>(), 30));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NuclearFumes>(), 5));
                    }
                    if (npc.type == CalValEX.CalamityNPC("PerennialSlime"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PerennialFlower>(), 20));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PerennialDress>(), 20));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientPerennialFlower>(), 30));
                    }
                    if (npc.type == CalValEX.CalamityNPC("SightseerCollider"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AstralBinoculars>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("HeatSpirit"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EssenceofDisorder>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("BelchingCoral"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CoralMask>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("AnthozoanCrab"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrackedFossil>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Cryon"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cryocap>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cryocoat>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("RenegadeWarlock"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CultistHood>(), 30));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CultistRobe>(), 30));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CultistLegs>(), 30));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ImpiousImmolator"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedChewToy>(), 40));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HolyTorch>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Cnidrion"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SunDriedShrimp>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("EidolonWyrmHead"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CanofWyrms>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("OverloadedSoldier"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UnloadedHelm>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("DevilFish"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DevilfishMask2>(), 20));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DevilfishMask3>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("DevilFishAlt"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DevilfishMask1>(), 20));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DevilfishMask3>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("MirageJelly"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Mirballoon>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OldMirage>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Hadarian"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HadarianTail>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("AstralSlime"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AstraEGGeldon>(), 87000));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Eidolist"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EidoMask>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Eidcape>(), 10));
                    }
                    if (npc.type == NPCID.SandElemental)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SmallSandPail>(), 5));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SmallSandPlushie>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SandyBangles>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ProfanedEnergyBody"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedChewToy>(), 40));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedEnergyHook>(), 20));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedBalloon>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ScornEater"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedChewToy>(), 40));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScornEaterMask>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ChaoticPuffer"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChaosBalloon>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ReaperShark"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReaperSharkArms>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OmegaBlue>(), 40));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReaperoidPills>(), 10));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ColossalSquid"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SquidHat>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OmegaBlue>(), 40));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Horse"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthShield>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthenHelmet>(), 20));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthenBreastplate>(), 20));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EarthenLeggings>(), 20));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ArmoredDiggerHead"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 10000));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ConstructionRemote>(), 4));
                    }
                    if (npc.type == CalValEX.CalamityNPC("PlaguebringerMiniboss"))
                    {
                        //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PlaguebringerPowerCell>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PlaugeWings>(), 15));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 10000));
                    }
                    if (npc.type == CalValEX.CalamityNPC("Mauler"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NuclearFumes>(), 1, 10, 25));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BubbledFin>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MaulerMask>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), PlushManager.PlushItems["Mauler"], 4));
                    }
                    if (npc.type == CalValEX.CalamityNPC("NuclearTerror"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<NuclearFumes>(), 1, 10, 25));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TerrorLegs>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), PlushManager.PlushItems["NuclearTerror"], 4));
                    }
                    if (npc.type == CalNPCID.GiantClam)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClamHermitMedallion>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ClamMask>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), PlushManager.PlushItems["GiantClam"], 4));
                    }
                    if (npc.type == CalValEX.CalamityNPC("ThiccWaifu"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CloudCandy>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CloudWaistbelt>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new FogboundCondition(), ModContent.ItemType<PurifiedFog>(), 20));
                        npcLoot.Add(ItemDropRule.ByCondition(new FogboundCondition2(), ModContent.ItemType<PurifiedFog>(), 999999));
                    }
                    if (npc.type == CalValEX.CalamityNPC("CragmawMire"))
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MawHook>(), 1));
                        npcLoot.Add(ItemDropRule.ByCondition(new PolterDowned(), ModContent.ItemType<NuclearFumes>(), 1, 5, 8));
                        npcLoot.Add(ItemDropRule.ByCondition(new Polteralive(), PlushManager.PlushItems["MireP1"], 4));
                        npcLoot.Add(ItemDropRule.ByCondition(new Polterdead(), PlushManager.PlushItems["MireP2"], 8));
                        npcLoot.Add(ItemDropRule.ByCondition(new Polterdead(), PlushManager.PlushItems["MireP1"], 8));
                    }
                    if (npc.type == CalValEX.CalamityNPC("GreatSandShark"))
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["SandShark"]);
                    }
                    if (npc.type == ModContent.NPCType<Xerocodile>())
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new YharonDowned(), ModContent.ItemType<Termipebbles>()));
                    }
                    if (npc.type == ModContent.NPCType<XerocodileSwim>())
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new YharonDowned(), ModContent.ItemType<Termipebbles>()));
                    }
                    //Scourge
                    if (npc.type == CalNPCID.DesertScourge)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DesertMedallion>(), 5));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SlightlyMoistbutalsoSlightlyDryLocket>(), 7));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DriedLocket>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["DesertScourge"]);
                    }
                    //Crabulon
                    if (npc.type == CalNPCID.Crabulon)
                    {
                        AddBlockDrop(npcLoot, ModContent.ItemType<MushroomCap>());
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ClawShroom>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Crabulon"]);
                    }
                    //Perfs
                    if (npc.type == CalNPCID.Perforators)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SmallWorm>(), 7));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MidWorm>(), 7));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BigWorm>(), 7));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MeatyWormTumor>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Perforator"]);
                    }
                    //Hive Mind
                    if (npc.type == CalNPCID.HiveMind)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Corruppuccino>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["HiveMind"]);
                    }
                    //Slime Gods
                    if (npc.type == CalNPCID.SlimeGod)
                    {
                        AddBlockDrop(npcLoot, CalValEX.CalamityItem("StatigelBlock"));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["SlimeGod"]);
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<IonizedJellyCrystal>(), 50));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SlimeGodMask>(), 7));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SlimeDeitysSoul>(), 3));
                    }
                    //Cryogen
                    if (npc.type == CalNPCID.Cryogen)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CoolShades>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Cryogen"]);
                    }
                    //Aqua
                    if (npc.type == CalNPCID.AquaticScourge)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MoistLocket>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BleachBallItem>(), 4));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["AquaticScourge"]);
                    }
                    //Brimmy
                    if (npc.type == CalNPCID.BrimstoneElemental)
                    {
                        AddBlockDrop(npcLoot, CalValEX.CalamityItem("BrimstoneSlag"));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BrimmySpirit>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<BrimmyBody>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<FoilSpoon>(), 20));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RareBrimtulip>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["BrimstoneElemental"]);
                    }
                    //Clone
                    if (npc.type == CalNPCID.CalamitasClone)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Calacirclet>(), 5));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 10000));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Clone"]);
                    }
                    //Leviathan
                    if (npc.type == CalNPCID.Leviathan)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Levihita(), ModContent.ItemType<FoilAtlantis>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Levihita(), ModContent.ItemType<StrangeMusicNote>(), 40));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["LeviathanEX"]);
                        npcLoot.Add(ItemDropRule.ByCondition(new LevihitaPlushies(), PlushManager.PlushItems["Anahita"], 20));
                    }
                    if (npc.type == CalNPCID.Anahita)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Levihita(), ModContent.ItemType<FoilAtlantis>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Levihita(), ModContent.ItemType<StrangeMusicNote>(), 40));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Anahita"]);
                        npcLoot.Add(ItemDropRule.ByCondition(new LevihitaPlushies(), PlushManager.PlushItems["LeviathanEX"], 20));
                    }
                    //Astrum Aureus
                    if (npc.type == CalNPCID.AstrumAureus)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AureusShield>(), 5));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AstralInfectedIcosahedron>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), ModContent.ItemType<SpaceJunk>()));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["AstrumAureus"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 10000));
                        npcLoot.Add(ItemDropRule.ByCondition(new GeldonDrop(), ModContent.ItemType<SpaceJunk>()));
                    }
                    //PBG
                    if (npc.type == CalNPCID.PlaguebringerGoliath)
                    {
                        AddBlockDrop(npcLoot, CalValEX.CalamityItem("PlaguedContainmentBrick"));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<InfectedController>(), 5));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<PlaguePack>(), 5));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["PlaguebringerGoliath"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 1000));
                    }
                    //Ravager
                    if (npc.type == CalNPCID.Ravager)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

                        notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]{
                        ModContent.ItemType<SkullBalloon>(),
                        ModContent.ItemType<StonePile>(),
                        ModContent.ItemType<RavaHook>(),
                        ModContent.ItemType<SkullCluster>() }));
                        npcLoot.Add(notExpertRule);
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ScavaHook>(), 15));
                        AddBlockDrop(npcLoot, ModContent.ItemType<Necrostone>());
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Ravager"]);
                    }
                    //Deus
                    //PS: FUCK deus lootcode, I pray to anyone who wants to make weakref support for this abomination _ YuH 2022
                    if (npc.type == CalNPCID.AstrumDeus)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                        npcLoot.Add(ItemDropRule.ByCondition(new DeusFUCKBlight(), ModContent.ItemType<AstrumDeusMask>()));
                        npcLoot.Add(ItemDropRule.ByCondition(new DeusFUCKMasorev(), PlushManager.PlushItems["AstrumDeus"], 4));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new DeusFUCK(), ModContent.ItemType<AstBandana>(), 4));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new DeusFUCK(), ModContent.ItemType<Geminga>(), 3));
                        npcLoot.Add(notExpertRule);
                    }
                    //Bumblebirb
                    if (npc.type == CalNPCID.Bumblebirb)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

                        notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]{
                        ModContent.ItemType<Birbhat>(),
                        ModContent.ItemType<FollyWings>(),
                        ModContent.ItemType<DocilePheromones>()}));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new SilvaCrystal(), CalValEX.CalamityItem("SilvaCrystal"), 1, 155, 265));
                        npcLoot.Add(notExpertRule);
                        npcLoot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), ModContent.ItemType<ExtraFluffyFeather>()));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Bumblefuck"]); ;
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 500));
                        npcLoot.Add(ItemDropRule.ByCondition(new DogeDrop(), ModContent.ItemType<ExtraFluffyFeather>()));
                    }
                    //Providence
                    if (npc.type == CalNPCID.Providence)
                    {
                        AddBlockDrop(npcLoot, CalValEX.CalamityItem("ProfanedRock"));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ProviCrystal>(), 4));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ProfanedHeart>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ProvidenceAltar>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new IsNightDrop(), ModContent.ItemType<FlareRune>()));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Providence"]);
                    }
                    //Storm Weaver
                    if (npc.type == CalNPCID.StormWeaver)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new OtherworldlyStoneDrop(), CalValEX.CalamityItem("OtherworldlyStone"), 1, 155, 265));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<StormBandana>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ArmoredScrap>(), 6));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<StormMedal>(), 6));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["StormWeaver"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 250));
                    }
                    //Signus
                    if (npc.type == CalNPCID.Signus)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

                        notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]{
                        ModContent.ItemType<SignusBalloon>(),
                        ModContent.ItemType<SigCape>(),
                        ModContent.ItemType<SignusEmblem>(),
                        ModContent.ItemType<ShadowCloth>(),
                        ModContent.ItemType<SignusNether>()}));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new OtherworldlyStoneDrop(), CalValEX.CalamityItem("OtherworldlyStone"), 1, 155, 265));
                        npcLoot.Add(notExpertRule);
                        npcLoot.Add(ItemDropRule.ByCondition(new JunkoDrop(), ModContent.ItemType<SuspiciousLookingChineseCrown>()));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Signus"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 250));
                    }
                    //CV
                    if (npc.type == CalNPCID.CeaselessVoid)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new OtherworldlyStoneDrop(), CalValEX.CalamityItem("OtherworldlyStone"), 1, 155, 265));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<VoidWings>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<OldVoidWings>(), 15));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<MirrorMatter>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["CeaselessVoid"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 250));
                    }
                    //Polterghast
                    if (npc.type == CalNPCID.Polterghast)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new BlockDrops(), CalValEX.CalamityItem("StratusBricks"), 2, 155, 265));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new BlockDrops(), ModContent.ItemType<PhantowaxBlock>(), 2, 155, 265));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Polterhook>(), 20));
                        npcLoot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), ModContent.ItemType<ToyScythe>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Polterghast"]);
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<ZygoteinaBucket>(), 3));
                    }
                    //Old Duke
                    if (npc.type == CalNPCID.OldDuke)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<OldWings>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CorrodedCleaver>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CharredChopper>(), 6));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["OldDuke"]);
                    }
                    //DoG
                    if (npc.type == CalNPCID.DevourerofGods)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CosmicWormScarf>(), 5));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<RapturedWormScarf>(), 20));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<CosmicRapture>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["DevourerofGodsEX"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 100));
                    }
                    //Yharon
                    if (npc.type == CalNPCID.Yharon)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<YharonShackle>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<YharonsAnklet>(), 10));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<NuggetinaBiscuit>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["YharonEX"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 20));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<Termipebbles>(), 1, 3, 8));
                        npcLoot.Add(ItemDropRule.ByCondition(new RoverDrop(), ModContent.ItemType<RoverSpindle>()));
                    }
                    //Supreme Cal
                    if (npc.type == CalNPCID.SupremeCalamitas)
                    {
                        AddBlockDrop(npcLoot, CalValEX.CalamityItem("OccultBrickItem"));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<GruelingMask>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Calamitas"]);
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientAuricTeslaHelm>(), 10));
                    }
                    //Ares
                    if (npc.type == CalNPCID.Ares)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<Items.Equips.Shirts.AresChestplate.AresChestplate>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<DraedonBody>(), 5));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<DraedonLegs>(), 5));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<OminousCore>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<AncientAuricTeslaHelm>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new ExoPlating(), CalValEX.CalamityItem("ExoPlating"), 1, 155, 265));
                        npcLoot.Add(notExpertRule);
                        npcLoot.Add(ItemDropRule.ByCondition(new ExoPlush(), PlushManager.PlushItems["Ares"]));
                        npcLoot.Add(ItemDropRule.ByCondition(new ExoPlush(), PlushManager.PlushItems["Draedon"], 10));
                    }
                    //Thanatos
                    if (npc.type == CalNPCID.Thanatos)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<XMLightningHook>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<DraedonBody>(), 5));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<DraedonLegs>(), 5));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<GunmetalRemote>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<AncientAuricTeslaHelm>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new ExoPlating(), CalValEX.CalamityItem("ExoPlating"), 1, 155, 265));
                        npcLoot.Add(notExpertRule);
                        npcLoot.Add(ItemDropRule.ByCondition(new ExoPlush(), PlushManager.PlushItems["Thanatos"]));
                        npcLoot.Add(ItemDropRule.ByCondition(new ExoPlush(), PlushManager.PlushItems["Draedon"], 10));
                    }
                    //Apollo
                    if (npc.type == CalNPCID.Apollo)
                    {
                        LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<ArtemisBalloonSmall>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<ApolloBalloonSmall>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<DraedonBody>(), 5));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<DraedonLegs>(), 5));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<GeminiMarkImplants>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new Exodrop(), ModContent.ItemType<AncientAuricTeslaHelm>(), 3));
                        notExpertRule.OnSuccess(ItemDropRule.ByCondition(new ExoPlating(), CalValEX.CalamityItem("ExoPlating"), 1, 155, 265));
                        npcLoot.Add(notExpertRule);
                        npcLoot.Add(ItemDropRule.ByCondition(new ExoPlush(), PlushManager.PlushItems["Artemis"]));
                        npcLoot.Add(ItemDropRule.ByCondition(new ExoPlush(), PlushManager.PlushItems["Apollo"]));
                        npcLoot.Add(ItemDropRule.ByCondition(new ExoPlush(), PlushManager.PlushItems["Draedon"], 10));
                    }
                    //Wyrm
                    if (npc.type == CalNPCID.PrimordialWyrm)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RespirationShrine>()));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulShard>()));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Jared"]);
                    }
                    //Donuts
                    if (npc.type == CalNPCID.GuardianCommander)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedWheels>(), 3));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedCultistMask>(), 5));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedCultistRobes>(), 5));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["ProfanedGuardian"]);
                    }
                    if (npc.type == CalNPCID.GuardianDefender)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedFrame>(), 3));
                    }
                    if (npc.type == CalNPCID.GuardianHealer)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ProfanedBattery>(), 3));
                    }
                }
                //Meldosaurus
                if (npc.type == ModContent.NPCType<AprilFools.Meldosaurus.Meldosaurus>())
                {
                    LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

                    notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, new int[]{
                            ModContent.ItemType<ShadesBane>(),
                            ModContent.ItemType<Nyanthrop>()}));
                    npcLoot.Add(notExpertRule);


                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AprilFools.Meldosaurus.MeldosaurusTrophy>(), 10));
                    npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<AprilFools.Meldosaurus.MeldosaurusMask>(), 7));
                    npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<AprilFools.Meldosaurus.MeldosaurusBag>()));
                    if (CalValEX.CalamityActive)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), ModContent.ItemType<AprilFools.Meldosaurus.MeldosaurusRelic>()));
                    }
                    npcLoot.Add(ItemDropRule.ByCondition(new MeldosaurusDowned(), ModContent.ItemType<AprilFools.Meldosaurus.KnowledgeMeldosaurus>()));
                }
                //Fogbound
                if (npc.type == ModContent.NPCType<Fogbound>())
                {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PurifiedFog>(), 1));
                    if (CalValEX.CalamityActive)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Fogdowned(), Mod.Find<ModItem>("KnowledgeFogbound").Type));
                    }
                }
                Mod CatalystMod;
                Mod Hypnos;
                Mod Infernum;
                ModLoader.TryGetMod("CatalystMod", out CatalystMod);
                ModLoader.TryGetMod("Hypnos", out Hypnos);
                ModLoader.TryGetMod("InfernumMode", out Infernum);
                ModLoader.TryGetMod("CalRemix", out Mod remix);
                ModLoader.TryGetMod("NoxusBoss", out Mod xeroxus);
                if (Hypnos != null)
                {
                    if (npc.type == Hypnos.Find<ModNPC>("HypnosBoss").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Hypnos"]);
                    }
                }
                if (CatalystMod != null)
                {
                    if (npc.type == CatalystMod.Find<ModNPC>("Astrageldon").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Astrageldon"]);
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SpaceJunk>(), 3));
                    }
                }
                if (xeroxus != null)
                {
                    if (npc.type == xeroxus.Find<ModNPC>("NamelessDeityBoss").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(PlushManager.PlushItems["NamelessDeityEX"], 4));
                    }
                    if (npc.type == xeroxus.Find<ModNPC>("AvatarOfEmptiness").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(PlushManager.PlushItems["AvatarEX"], 4));
                    }
                    if (npc.type == xeroxus.Find<ModNPC>("MarsBody").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Mars"]);
                    }
                }
                if (CalValEX.instance.hunt != null)
                {
                    if (npc.type == CalValEX.instance.hunt.Find<ModNPC>("Goozma").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Goozma"]);
                    }
                }
                if (CalValEX.instance.sloome != null)
                {
                    if (npc.type == CalValEX.instance.sloome.Find<ModNPC>("ExoSlimeGod").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Exodygen"]);
                    }
                }
                if (Infernum != null)
                {
                    if (npc.type == Infernum.Find<ModNPC>("BereftVassal").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["BereftVassal"]);
                    }
                    if (npc.type == CalNPCID.CalamitasClone)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Infernum(), PlushManager.PlushItems["Shadow"], 4));
                    }
                }
                if (remix != null)
                {
                    if (npc.type == remix.Find<ModNPC>("Exotrexia").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Exotrexia"]);
                    }
                    if (npc.type == remix.Find<ModNPC>("Astigmageddon").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Astigmageddon"]);
                    }
                    if (npc.type == remix.Find<ModNPC>("Conjunctivirus").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Conjunctivirus"]);
                    }
                    if (npc.type == remix.Find<ModNPC>("Cataractacomb").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Cataractacomb"]);
                    }
                    if (npc.type == remix.Find<ModNPC>("Hypnos").Type)
                    {
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Hypnos"]);
                    }
                }
                if (CalValEX.instance.fables != null)
                {
                    if (npc.type == CalValEX.instance.fables.Find<ModNPC>("SirNautilus").Type)
                    {
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<SeaguardShield>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<DustyGuitar>(), 3));
                        npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<NautilusShell>(), 3));
                        AddPlushDrop(npcLoot, PlushManager.PlushItems["Signathion"]);
                    }
                    if (npc.type == CalValEX.instance.fables.Find<ModNPC>("WulfrumNexus").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumTransmitter>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumKeys>(), 10));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumHelipack>(), 20));
                    }
                    if (npc.type == CalValEX.instance.fables.Find<ModNPC>("WulfrumRoller").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumBalloon>(), 100));
                    }
                    if (npc.type == CalValEX.instance.fables.Find<ModNPC>("WulfrumRover").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoverSpindle>(), 1000));
                    }
                    if (npc.type == CalValEX.instance.fables.Find<ModNPC>("WulfrumMagnetizer").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                    }
                    if (npc.type == CalValEX.instance.fables.Find<ModNPC>("WulfrumMortar").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                    }
                    if (npc.type == CalValEX.instance.fables.Find<ModNPC>("WulfrumGrappler").Type)
                    {
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumController>(), 100));
                        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WulfrumGrappler>(), 20));
                    }
                }
            }

            //Yharexs' Dev Pet (Calamity BABY)
            /*if (CalValEX.CalamityActive && (bool)CalValEX.Calamity.Call("GetDifficultyActive", "death"))
            {
                if (npc.type == CalValEX.CalamityNPC("SupremeCalamitas"))
                {
                    bool didIGetHit = false;
                    for (int i = 0; i < Main.maxPlayers; i++)
                    {
                        Player player = Main.player[i];
                        if (player.active && !player.dead)
                        {
                            if (player.GetModPlayer<CalValEXPlayer>().SCalHits > 0)
                            {
                                didIGetHit = true;
                            }
                        }
                    }

                    if (!didIGetHit)
                    {
                        Item.NewItem(npc.GetSource_FromAI(), npc.getRect(), ModContent.ItemType<AstraEGGeldon>());
                    }
                    else
                    {
                        if (Main.rand.Next(1000) == 0)
                        {
                            Item.NewItem(npc.GetSource_FromAI(), npc.getRect(), ModContent.ItemType<AstraEGGeldon>());
                        }
                    }
                }
            }*/
        }
        public static void AddPlushDrop(NPCLoot loot, int item)
        {
            loot.Add(ItemDropRule.ByCondition(new MasterRevCondition(), item, 4));
        }
        public static void AddBlockDrop(NPCLoot loot, int item)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.ByCondition(new BlockDrops(), item, 1, 55, 265));
            loot.Add(notExpertRule);
        }

        int signuskill;
        private bool signusbackup = false;
        int signusshaker = 0;
        Vector2 jharimpos;
        int calashoot = 0;

        [JITWhenModsEnabled("CalamityMod")]
        public override void AI(NPC npc)
        {
            if (CalValEX.CalamityActive)
            {
                Mod.TryFind("JharimKiller", out ModProjectile brimbuck);
                if (npc.type == CalNPCID.WITCH && (!CalValEXWorld.jharinter || !NPC.downedMoonlord))
                {
                    if (NPC.AnyNPCs(ModContent.NPCType<AprilFools.Jharim.Jharim>()))
                    {
                        for (int x = 0; x < Main.maxNPCs; x++)
                        {
                            NPC npc3 = Main.npc[x];
                            if (npc3.type == ModContent.NPCType<AprilFools.Jharim.Jharim>() && npc3.active)
                            {
                                jharimpos.X = npc3.Center.X;
                                jharimpos.Y = npc3.Center.Y;
                                calashoot++;
                                if (npc3.position.X - npc.position.X >= 0)
                                {
                                    npc.direction = 1;
                                }
                                else
                                {
                                    npc.direction = -1;
                                }
                            }
                        }
                    }
                    if (calashoot >= 5)
                    {
                        Vector2 position = npc.Center;
                        position.X = npc.Center.X + (20f * npc.direction);
                        Vector2 direction = jharimpos - position;
                        direction.Normalize();
                        float speed = 10f;
                        int type = brimbuck.Type;
                        int damage = 6666;
                        Projectile.NewProjectile(npc.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
                        calashoot = 0;
                    }
                }
                if (npc.type == CalNPCID.Signus)
                {
                    if ((npc.ai[0] == -33f || signusbackup) && Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().junsi)
                    {
                        Dust dust;
                        Vector2 position = npc.position;
                        for (int a = 0; a < 3; a++)
                        {
                            dust = Main.dust[Dust.NewDust(position, npc.width, npc.height, DustID.Cloud, 0f, 0f, 0, new Color(255, 255, 255), 1.578947f)];
                            dust.shader = GameShaders.Armor.GetSecondaryShader(131, Main.LocalPlayer);
                        }
                        npc.rotation = 0;
                        npc.direction = -1;
                        npc.spriteDirection = -1;
                        signusshaker++;
                        npc.alpha = 0;
                        npc.velocity.X = 0;
                        npc.velocity.Y = 0;
                        signuskill++;
                        npc.dontTakeDamage = true;
                        for (int k = 0; k < npc.buffImmune.Length; k++)
                        {
                            npc.buffImmune[k] = true;
                        }
                        if (signuskill == 64)
                        {
                            signuskill = 0;
                            npc.knockBackResist = 20f;
                            npc.SimpleStrikeNPC(499999, npc.direction, false, npc.direction * 50, noPlayerInteraction: true);
                            signusbackup = false;
                        }
                        if (signusshaker == 1)
                        {
                            npc.velocity.X = -5;
                        }
                        else if (signusshaker == 2)
                        {
                            npc.velocity.X = 5;
                            signusshaker = 0;
                        }
                    }
                    else
                    {
                        signuskill = 0;
                        signusshaker = 0;
                    }
                    if (!Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().junsi)
                    {
                        signusbackup = false;
                        if (npc.ai[0] == -33f)
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                        }
                    }
                }
            }
        }

        public override bool CheckDead(NPC npc)
        {
            if (CalValEX.CalamityActive)
            {
                if (npc.type == CalNPCID.Signus && !signusbackup && Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().junsi && Main.netMode != NetmodeID.Server)
                {
                    npc.dontTakeDamage = true;
                    npc.ai[0] = -33f;
                    npc.ai[1] = -33f;
                    npc.ai[2] = -33f;
                    npc.ai[3] = -33f;
                    signusbackup = true;
                }
                return true;
            }
            return true;
        }
        
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
             if (npc.aiStyle == NPCAIStyleID.Slime && !NPC.AnyNPCs(ModContent.NPCType<NPCs.TownPets.Slimes.NinjaSlime>()) && Main.rand.NextBool(22))
                {
                    bool titShuriken = CalValEX.CalamityActive ? projectile.type == CalProjectileID.TitaniumShuriken : false;
                    bool boomShuriken = false;
                    if (ModLoader.HasMod("Fargowiltas"))
                    {
                        if (projectile.type == ModLoader.GetMod("Fargowiltas").Find<ModProjectile>("ShurikenProj").Type)
                        {
                            boomShuriken = true;
                        }
                    }
                    if (projectile.type == ProjectileID.Shuriken || titShuriken || boomShuriken)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<NPCs.TownPets.Slimes.NinjaSlime>());
                        if (!CalValEXWorld.ninja)
                        {
                            CalValEXWorld.ninja = true;
                            CalValEXWorld.UpdateWorldBool();
                        }

                        npc.active = false;
                        projectile.Kill();
                    }
                }
            if (CalValEX.CalamityActive)
            {
                if (npc.type == CalNPCID.AstrumAureus)
                {
                    if (projectile.type == CalProjectileID.AstrageldonLaser || projectile.type == CalProjectileID.AstrageldonMinion)
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().geldonSummon = true;
                        if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.TownPets.Slimes.AstroSlime>()))
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                NPC.NewNPC(npc.GetSource_FromThis(), (int)projectile.Center.X, (int)projectile.Center.Y, ModContent.NPCType<NPCs.TownPets.Slimes.AstroSlime>());
                            }
                            if (!CalValEXWorld.astro)
                            {
                                CalValEXWorld.astro = true;
                                CalValEXWorld.UpdateWorldBool();
                            }
                            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item117, projectile.Center);
                            int dustAmt = 16;
                            for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
                            {
                                int assdust = CalDustID.AstralOrange;
                                Vector2 vector6 = Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f;
                                vector6 = vector6.RotatedBy((double)((float)(dustIndex - (dustAmt / 2 - 1)) * MathHelper.TwoPi / (float)dustAmt), default) + projectile.Center;
                                Vector2 vector7 = vector6 - projectile.Center;
                                int dusty = Dust.NewDust(vector6 + vector7, 0, 0, assdust, vector7.X * 1f, vector7.Y * 1f, 100, default, 1.1f);
                                Main.dust[dusty].noGravity = true;
                                Main.dust[dusty].noLight = true;
                                Main.dust[dusty].velocity = vector7;
                            }
                            projectile.Kill();
                        }
                    }
                    else
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().geldonSummon = false;
                    }
                }
                else if (npc.type == CalNPCID.Bumblebirb)
                {
                    if (projectile.type == CalProjectileID.BabyFolly && projectile.DamageType != DamageClass.Ranged)
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().bdogeMount = true;
                    }
                    else
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().bdogeMount = false;
                    }
                }
                else if (npc.type == CalNPCID.Yharon)
                {
                    if (projectile.type == CalProjectileID.WulfrumDroid)
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().wolfram = true;
                    }
                    else
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().wolfram = false;
                    }
                }
                else if (npc.type == CalNPCID.Signus)
                {
                    if (projectile.type == CalProjectileID.PristineFire ||
                        projectile.type == CalProjectileID.PristineAlt)
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().junkoReference = true;
                    }
                    else
                    {
                        npc.GetGlobalNPC<CalValEXGlobalNPC>().junkoReference = false;
                    }
                }
            }
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void BossExclam(NPC npc, int[] types, bool downed, int boss, bool additionalCondition = true)
        {
            if (npc.type == boss && !downed && additionalCondition)
            {
                CalamityMod.NPCs.CalamityGlobalNPC.SetNewShopVariable(types, downed);
            }
        }

        [JITWhenModsEnabled("CalamityMod")]
        public override bool PreKill(NPC npc)
        {
            if (CalValEX.instance.hunt != null)
            {
                if (npc.type == ModLoader.GetMod("CalamityHunt").Find<ModNPC>("Goozma").Type)
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.TownPets.Slimes.Tarr>()))
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            NPC.NewNPC(npc.GetSource_FromThis(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<NPCs.TownPets.Slimes.Tarr>());
                        if (!CalValEXWorld.tar)
                        {
                            CalValEXWorld.tar = true;
                            CalValEXWorld.UpdateWorldBool();
                        }
                    }
                }
            }
            if (CalValEX.CalamityActive)
            {
                int jellyID = ModContent.NPCType<JellyPriestNPC>();
                int oracleID = ModContent.NPCType<OracleNPC>();

                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "giantclam"), CalNPCID.GiantClam);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "crabulon"), CalNPCID.Crabulon);
                BossExclam(npc, new int[] { jellyID }, NPC.downedBoss3, NPCID.SkeletronHead);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "slimegod"), CalNPCID.SlimeGod);
                BossExclam(npc, new int[] { jellyID }, Main.hardMode, NPCID.WallofFlesh);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "cryogen"), CalNPCID.Cryogen);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "aquaticscourge"), CalNPCID.AquaticScourge);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "brimstoneelemental"), CalNPCID.BrimstoneElemental);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "calamitasclone"), CalNPCID.CalamitasClone);
                BossExclam(npc, new int[] { jellyID }, NPC.downedPlantBoss, NPCID.Plantera);
                BossExclam(npc, new int[] { jellyID }, NPC.downedGolemBoss, NPCID.Golem);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "leviathan"),CalNPCID.Anahita, !NPC.AnyNPCs(CalNPCID.Leviathan));
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "leviathan"), CalNPCID.Leviathan, !NPC.AnyNPCs(CalNPCID.Anahita));
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "plaguebringergoliath"), CalNPCID.PlaguebringerGoliath);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "ravager"), CalNPCID.Ravager);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "providence"), CalNPCID.Providence);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "stormweaver"),CalNPCID.StormWeaver);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "ceaselessvoid"), CalNPCID.CeaselessVoid);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "signus"), CalNPCID.Signus);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "polterghast"), CalNPCID.Polterghast);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "oldduke"), CalNPCID.OldDuke);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "devourerofgods"), CalNPCID.DevourerofGods);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "yharon"), CalNPCID.Yharon);
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "exomechs"), CalNPCID.Ares, !NPC.AnyNPCs(CalNPCID.Apollo) && !NPC.AnyNPCs(CalNPCID.Thanatos));
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "exomechs"), CalNPCID.Apollo, !NPC.AnyNPCs(CalNPCID.Ares) && !NPC.AnyNPCs(CalNPCID.Thanatos));
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "exomechs"), CalNPCID.Thanatos, !NPC.AnyNPCs(CalNPCID.Apollo) && !NPC.AnyNPCs(CalNPCID.Ares));
                BossExclam(npc, new int[] { jellyID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "supremecalamitas"), CalNPCID.SupremeCalamitas);

                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "hivemind"), CalNPCID.HiveMind);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "perforator"), CalNPCID.Perforators);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "cryogen"), CalNPCID.Cryogen);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "brimstoneelemental"), CalNPCID.BrimstoneElemental);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "calamitasclone"), CalNPCID.CalamitasClone);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "dragonfolly"), CalNPCID.Bumblebirb);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "signus"), CalNPCID.Signus);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "devourerofgods"), CalNPCID.DevourerofGods);
                BossExclam(npc, new int[] { oracleID }, (bool)CalValEX.Calamity.Call("GetBossDowned", "yharon"), CalNPCID.Yharon);

                BossExclam(npc, new int[] { CalNPCID.SEAHOE }, (bool)CalValEX.Calamity.Call("GetBossDowned", "oldduke"), CalNPCID.OldDuke);
                BossExclam(npc, new int[] { CalNPCID.SEAHOE }, (bool)CalValEX.Calamity.Call("GetBossDowned", "scal"), CalNPCID.SupremeCalamitas);

                BossExclam(npc, new int[] { CalNPCID.DILF }, (bool)CalValEX.Calamity.Call("GetBossDowned", "signus"), CalNPCID.Signus);

                BossExclam(npc, new int[] { CalNPCID.THIEF }, (bool)CalValEX.Calamity.Call("GetBossDowned", "astrumaureus"), CalNPCID.AstrumAureus);

                BossExclam(npc, new int[] { NPCID.PartyGirl }, (bool)CalValEX.Calamity.Call("GetBossDowned", "polterghast"), CalNPCID.Polterghast);

                if (npc.type == CalValEX.CalamityNPC("CrabShroom") && Main.LocalPlayer.HasItem(ModContent.ItemType<PutridShroom>()) && NPC.AnyNPCs(CalNPCID.Crabulon))
                {
                    NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<Swearshroom>());
                }

                if (npc.type == CalNPCID.Signus && junkoReference)
                {
                    Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ModContent.ItemType<SuspiciousLookingChineseCrown>());
                }

                if (npc.type == CalNPCID.Yharon && wolfram)
                {
                    Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ModContent.ItemType<RoverSpindle>());
                }

                if (npc.type == CalNPCID.AstrumAureus && geldonSummon)
                {
                    Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ModContent.ItemType<SpaceJunk>());
                }

                if (npc.type == CalNPCID.Bumblebirb && bdogeMount)
                {
                    Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ModContent.ItemType<ExtraFluffyFeatherClump>());
                }
            }

            int droppedMoney = 0;
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead)
                {
                    if (player.HasBuff(ModContent.BuffType<MorshuBuff>()))
                    {
                        float value = npc.value;
                        value /= 5;
                        if (value < 0)
                        {
                            value = 1;
                        }

                        if (droppedMoney == 0)
                        {
                            while (value > 0)
                            {
                                if (value > 1000000f)
                                {
                                    int platCoins = (int)(value / 1000000f);
                                    if (platCoins > 50 && Main.rand.NextBool(5))
                                    {
                                        platCoins /= Main.rand.Next(3) + 1;
                                    }

                                    if (Main.rand.NextBool(5))
                                    {
                                        platCoins /= Main.rand.Next(3) + 1;
                                    }

                                    value -= 1000000f * platCoins;
                                    Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ItemID.PlatinumCoin, platCoins);
                                    continue;
                                }

                                if (value > 10000f)
                                {
                                    int goldCoins = (int)(value / 10000f);
                                    if (goldCoins > 50 && Main.rand.NextBool(5))
                                    {
                                        goldCoins /= Main.rand.Next(3) + 1;
                                    }

                                    if (Main.rand.NextBool(5))
                                    {
                                        goldCoins /= Main.rand.Next(3) + 1;
                                    }

                                    value -= 10000f * goldCoins;
                                    Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ItemID.GoldCoin, goldCoins);
                                    continue;
                                }

                                if (value > 100f)
                                {
                                    int silverCoins = (int)(value / 100f);
                                    if (silverCoins > 50 && Main.rand.NextBool(5))
                                    {
                                        silverCoins /= Main.rand.Next(3) + 1;
                                    }

                                    if (Main.rand.NextBool(5))
                                    {
                                        silverCoins /= Main.rand.Next(3) + 1;
                                    }

                                    value -= 100f * silverCoins;
                                    Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ItemID.SilverCoin, silverCoins);
                                    continue;
                                }

                                int copperCoins = (int)value;
                                if (copperCoins > 50 && Main.rand.NextBool(5))
                                {
                                    copperCoins /= Main.rand.Next(3) + 1;
                                }

                                if (Main.rand.NextBool(5))
                                {
                                    copperCoins /= Main.rand.Next(4) + 1;
                                }

                                if (copperCoins < 1)
                                {
                                    copperCoins = 1;
                                }

                                value -= copperCoins;
                                Item.NewItem(npc.GetSource_FromAI(), npc.Hitbox, ItemID.CopperCoin, copperCoins);
                            }

                            droppedMoney++;
                        }
                    }
                }
            }
            return true;
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenpos, Color drawColor)
        {
            if (CalValEX.CalamityActive)
            {
                if (Main.LocalPlayer.InModBiome(ModContent.GetInstance<Biomes.AstralBlight>()) || Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().Blok)
                {
                    BasicDrawNPC(ref spriteBatch, DeusBlightHead, npc, CalNPCID.AstrumDeus, npc.GetAlpha(drawColor));
                    BasicDrawNPC(ref spriteBatch, DeusBlightBody, npc, CalNPCID.AstrumDeusBody, npc.GetAlpha(drawColor));
                    BasicDrawNPC(ref spriteBatch, DeusBlightTail, npc, CalNPCID.AstrumDeusTail, npc.GetAlpha(drawColor));
                    BasicDrawNPC(ref spriteBatch, DeusBlightHeadGlow, npc, CalNPCID.AstrumDeus, Color.White);
                    BasicDrawNPC(ref spriteBatch, npc.localAI[3] == 1f ? DeusBlightBodyAltGlow : DeusBlightBodyGlow, npc, CalNPCID.AstrumDeusBody, Color.White);
                    BasicDrawNPC(ref spriteBatch, DeusBlightTailGlow, npc, CalNPCID.AstrumDeusTail, Color.White);
                    if (npc.type == CalNPCID.AstrumDeus || npc.type == CalNPCID.AstrumDeusBody || npc.type == CalNPCID.AstrumDeusTail)
                        return false;
                }
            }
            return true;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenpos, Color drawColor)
        {
            if (CalValEX.CalamityActive)
            {
                if (CalValEX.AprilFoolDay)
                {
                    BasicDrawNPC(ref spriteBatch, PoGod, npc, CalNPCID.SlimeGod, npc.GetAlpha(drawColor));
                }
            }
        }

        public static void BasicDrawNPC(ref SpriteBatch spriteBatch, Texture2D texture, NPC npc, int type, Color color)
        {
            if (npc.type == type)
            {
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), null, color, npc.rotation, texture.Size() / 2f, npc.scale, SpriteEffects.None, 0f);
            }
        }

        public override void BossHeadSlot(NPC npc, ref int index)
        {
            if (CalValEX.CalamityActive)
            {
                if (npc.type == CalNPCID.AstrumDeus)
                {
                    if (Main.LocalPlayer.InModBiome(ModContent.GetInstance<Biomes.AstralBlight>()) || Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().Blok)
                    {
                        index = ModContent.GetModBossHeadSlot("CalValEX/NPCs/AstrumDeus/AstrumDeusMap");
                    }
                }
            }
        }

        //Disable Astral Blight overworld spawns
        [JITWhenModsEnabled("CalamityMod")]
        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo LocalPlayer)
        {
            Player player = LocalPlayer.Player;
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
            bool acid = CalValEX.CalamityActive ? !(bool)CalValEX.Calamity.Call("AcidRainActive") : true;
            bool noevents = acid && !Main.eclipse && !Main.snowMoon && !Main.pumpkinMoon && Main.invasionType == 0 && !player.ZoneTowerSolar && !player.ZoneTowerStardust && !player.ZoneTowerVortex & !player.ZoneTowerNebula && !player.ZoneOldOneArmy;
            Mod cata;
            ModLoader.TryGetMod("CatalystMod", out cata);
            if (!CalValEXConfig.Instance.CritterSpawns)
            {
                if (modPlayer.sBun)
                {
                    pool.Add(NPCID.Bunny, 0.001f);
                }
            }
            if (player.InModBiome(ModContent.GetInstance<Biomes.AstralBlight>()) && noevents)
            {
                pool.Clear();
                if (!CalValEXConfig.Instance.CritterSpawns)
                {
                    pool.Add(ModContent.NPCType<Blightolemur>(), 0.1f);
                    pool.Add(ModContent.NPCType<Blinker>(), 0.1f);
                    pool.Add(ModContent.NPCType<AstJR>(), 0.1f);
                    pool.Add(ModContent.NPCType<GAstJR>(), 0.1f);
                    if (modPlayer.sBun)
                    {
                        pool.Add(NPCID.Bunny, 0.001f);
                    }
                }
            }
        }

        public override void ModifyTypeName(NPC npc, ref string typeName)
        {
            if (!CalValEX.CalamityActive)
                return;

            if (CalValEX.Bumble && !CalValEXConfig.Instance.DragonballName)
            {
                if (npc.type == CalNPCID.Bumblebirb && npc.TypeName == "The Dragonfolly")
                {
                    if (Main.rand.NextFloat() < 0.01f)
                    {
                        typeName = "Bumblebirb";
                    }
                    else
                    {
                        typeName = "Blunderbird";
                    }
                }

                if (npc.type == CalValEX.CalamityNPC("Bumblefuck2") && npc.TypeName == "Draconic Swarmer")
                {
                    if (Main.rand.NextFloat() < 0.01f)
                    {
                        typeName = "Bumblebirb";
                    }
                    else
                    {
                        typeName = "Blunderling";
                    }
                }
            }
        }

        public override void OnKill(NPC npc)
        {
            if (CalValEX.CalamityActive && CalValEX.instance.hunt == null)
            {
                if (NPCID.Sets.IsTownSlime[npc.type] && NPC.AnyNPCs(CalNPCID.SlimeGod))
                {
                    if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.TownPets.Slimes.Tarr>()))
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            NPC.NewNPC(npc.GetSource_Death(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<NPCs.TownPets.Slimes.Tarr>());
                        if (!CalValEXWorld.tar)
                        {
                            CalValEXWorld.tar = true;
                            CalValEXWorld.UpdateWorldBool();
                        }
                    }
                }
            }
        }
    }
}
