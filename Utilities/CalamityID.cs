using CalValEX.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.CalamityID
{
    public class CalamityID
    {
        public static int NPCRelation(string calamity, string fables, int vanilla)
        {
            if (CalValEX.FablesActive && CalValEXConfig.Instance.UseFables)
            {
                return CalValEX.Fables.Find<ModNPC>(fables).Type;
            }
            else if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModNPC>(calamity).Type;
            }
            return vanilla;
        }
        public static int NPCRelation(string calamity, int vanilla)
        {
            if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModNPC>(calamity).Type;
            }
            return vanilla;
        }
        public static int ItemRelation(string calamity, string fables, int vanilla)
        {
            if (CalValEX.FablesActive && CalValEXConfig.Instance.UseFables)
            {
                return CalValEX.Fables.Find<ModItem>(fables).Type;
            }
            else if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModItem>(calamity).Type;
            }
            return vanilla;
        }
        public static int ItemRelation(string calamity, int vanilla)
        {
            if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModItem>(calamity).Type;
            }
            return vanilla;
        }
        public static int RarityRelation(string calamity, int vanilla)
        {
            if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModRarity>(calamity).Type;
            }
            return vanilla;
        }
        public static int ProjectileRelation(string calamity, int vanilla)
        {
            if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModProjectile>(calamity).Type;
            }
            return vanilla;
        }
        public static int DustRelation(string calamity, int vanilla)
        {
            if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModDust>(calamity).Type;
            }
            return vanilla;
        }
        public static int TileRelation(string calamity, int vanilla)
        {
            if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModTile>(calamity).Type;
            }
            return vanilla;
        }
        public static int WallRelation(string calamity, int vanilla)
        {
            if (CalValEX.CalamityActive)
            {
                return CalValEX.Calamity.Find<ModWall>(calamity).Type;
            }
            return vanilla;
        }
    }
}

namespace CalValEX.CalamityID
{
    public class CalNPCID : ModSystem
    {
        #region Minibosses
        public static int GiantClam;
        #endregion

        #region Bosses
        public static int DesertScourge;
        public static int Crabulon;
        public static int HiveMind;
        public static int Perforators;
        public static int SlimeGod;
        public static int Cryogen;
        public static int AquaticScourge;
        public static int BrimstoneElemental;
        public static int CalamitasClone;
        public static int Anahita;
        public static int Leviathan;
        public static int AstrumAureus;
        public static int PlaguebringerGoliath;
        public static int Ravager;
        public static int AstrumDeus;
        public static int AstrumDeusBody;
        public static int AstrumDeusTail;
        public static int GuardianCommander;
        public static int GuardianHealer;
        public static int GuardianDefender;
        public static int Bumblebirb;
        public static int Providence;
        public static int StormWeaver;
        public static int CeaselessVoid;
        public static int Signus;
        public static int Polterghast;
        public static int OldDuke;
        public static int DevourerofGods;
        public static int Yharon;
        public static int Draedon;
        public static int Ares;
        public static int Apollo;
        public static int Artemis;
        public static int Thanatos;
        public static int SupremeCalamitas;
        public static int PrimordialWyrm;
        #endregion

        public static int WITCH;
        public static int DILF;
        public static int SEAHOE;
        public static int THIEF;
        public static int YHARM;

        public override void PostSetupContent()
        {
            GiantClam = CalamityID.NPCRelation("GiantClam", NPCID.EyeofCthulhu);
            DesertScourge = CalamityID.NPCRelation("DesertScourgeHead", "DesertScourge", NPCID.EyeofCthulhu);
            Crabulon = CalamityID.NPCRelation("Crabulon", "Crabulon", NPCID.BrainofCthulhu);
            HiveMind = CalamityID.NPCRelation("HiveMind", NPCID.EaterofWorldsHead);
            Perforators = CalamityID.NPCRelation("PerforatorHive", NPCID.BrainofCthulhu);
            SlimeGod = CalamityID.NPCRelation("SlimeGodCore", NPCID.SkeletronHead);
            Cryogen = CalamityID.NPCRelation("Cryogen", NPCID.TheDestroyer);
            AquaticScourge = CalamityID.NPCRelation("AquaticScourgeHead", NPCID.Spazmatism);
            BrimstoneElemental = CalamityID.NPCRelation("BrimstoneElemental", NPCID.SkeletronPrime);
            CalamitasClone = CalamityID.NPCRelation("CalamitasClone", NPCID.Retinazer);
            Anahita = CalamityID.NPCRelation("Anahita", NPCID.Plantera);
            Leviathan = CalamityID.NPCRelation("Leviathan", NPCID.Plantera);
            AstrumAureus = CalamityID.NPCRelation("AstrumAureus", NPCID.Plantera);
            PlaguebringerGoliath = CalamityID.NPCRelation("PlaguebringerGoliath", NPCID.Golem);
            Ravager = CalamityID.NPCRelation("RavagerBody", NPCID.Golem);
            AstrumDeus = CalamityID.NPCRelation("AstrumDeusHead", NPCID.CultistBoss);
            AstrumDeusBody = CalamityID.NPCRelation("AstrumDeusBody", NPCID.CultistBoss);
            AstrumDeusTail = CalamityID.NPCRelation("AstrumDeusTail", NPCID.CultistBoss);
            GuardianCommander = CalamityID.NPCRelation("ProfanedGuardianCommander", NPCID.QueenSlimeBoss);
            GuardianHealer = CalamityID.NPCRelation("ProfanedGuardianHealer", NPCID.QueenSlimeBoss);
            GuardianDefender = CalamityID.NPCRelation("ProfanedGuardianDefender", NPCID.QueenSlimeBoss);
            Bumblebirb = CalamityID.NPCRelation("Bumblefuck", NPCID.DD2Betsy);
            Providence = CalamityID.NPCRelation("Providence", NPCID.HallowBoss);
            StormWeaver = CalamityID.NPCRelation("StormWeaverHead", NPCID.TheDestroyer);
            CeaselessVoid = CalamityID.NPCRelation("CeaselessVoid", NPCID.Spazmatism);
            Signus = CalamityID.NPCRelation("Signus", NPCID.SkeletronPrime);
            Polterghast = CalamityID.NPCRelation("Polterghast", NPCID.Plantera);
            OldDuke = CalamityID.NPCRelation("OldDuke", NPCID.DukeFishron);
            DevourerofGods = CalamityID.NPCRelation("DevourerofGodsHead", NPCID.MoonLordCore);
            Yharon = CalamityID.NPCRelation("Yharon", NPCID.DukeFishron);
            Draedon = CalamityID.NPCRelation("Draedon", NPCID.TheDestroyer);
            Ares = CalamityID.NPCRelation("AresBody", NPCID.SkeletronPrime);
            Apollo = CalamityID.NPCRelation("Apollo", NPCID.Spazmatism);
            Artemis = CalamityID.NPCRelation("Artemis", NPCID.Retinazer);
            Thanatos = CalamityID.NPCRelation("ThanatosHead", NPCID.TheDestroyer);
            SupremeCalamitas = CalamityID.NPCRelation("SupremeCalamitas", NPCID.CultistBoss);
            PrimordialWyrm = CalamityID.NPCRelation("PrimordialWyrmHead", NPCID.MoonLordCore);
            WITCH = CalamityID.NPCRelation("WITCH", NPCID.WitchDoctor);
            SEAHOE = CalamityID.NPCRelation("SEAHOE", NPCID.Angler);
            DILF = CalamityID.NPCRelation("DILF", NPCID.Wizard);
            THIEF = CalamityID.NPCRelation("THIEF", NPCID.GoblinTinkerer);
            //YHARM = CalamityID.NPCRelation("YHARM", NPCID.Wizard);
        }
    }
    public class CalItemID : ModSystem
    {
        #region Bags
        public static int StarterBag;// => CalamityID.ItemRelation("StarterBag", ItemID.KingSlimeBossBag);
        public static int DesertScourgeBag;// => CalamityID.ItemRelation("DesertScourgeBag", ItemID.EyeOfCthulhuBossBag);
        public static int CrabulonBag;// => CalamityID.ItemRelation("CrabulonBag", ItemID.BrainOfCthulhuBossBag);
        public static int HiveMindBag;// => CalamityID.ItemRelation("HiveMindBag", ItemID.EaterOfWorldsBossBag);
        public static int PerforatorBag;// => CalamityID.ItemRelation("PerforatorBag", ItemID.BrainOfCthulhuBossBag);
        public static int SlimeGodBag;// => CalamityID.ItemRelation("SlimeGodBag", ItemID.SkeletronBossBag);
        public static int CryogenBag;// => CalamityID.ItemRelation("CryogenBag", ItemID.DestroyerBossBag);
        public static int AquaticScourgeBag;// => CalamityID.ItemRelation("AquaticScourgeBag", ItemID.TwinsBossBag);
        public static int BrimstoneElementalBag;// => CalamityID.ItemRelation("BrimstoneWaifuBag", ItemID.SkeletronPrimeBossBag);
        public static int CalamitasCloneBag;// => CalamityID.ItemRelation("CalamitasCloneBag", ItemID.TwinsBossBag);
        public static int LeviathanBag;// => CalamityID.ItemRelation("LeviathanBag", ItemID.PlanteraBossBag);
        public static int AstrumAureusBag;// => CalamityID.ItemRelation("AstrumAureusBag", ItemID.PlanteraBossBag);
        public static int PlaguebringerGoliathBag;// => CalamityID.ItemRelation("PlaguebringerGoliathBag", ItemID.GolemBossBag);
        public static int RavagerBag;// => CalamityID.ItemRelation("RavagerBag", ItemID.GolemBossBag);
        public static int AstrumDeusBag;// => CalamityID.ItemRelation("AstrumDeusBag", ItemID.BossBagBetsy);
        public static int BumbleBag;// => CalamityID.ItemRelation("DragonfollyBag", ItemID.BossBagBetsy);
        public static int ProvidenceBag;// => CalamityID.ItemRelation("ProvidenceBag", ItemID.FairyQueenBossBag);
        public static int StormWeaverBag;// => CalamityID.ItemRelation("StormWeaverBag", ItemID.DestroyerBossBag);
        public static int CeaselessVoidBag;// => CalamityID.ItemRelation("CeaselessVoidBag", ItemID.TwinsBossBag);
        public static int SignusBag;// => CalamityID.ItemRelation("SignusBag", ItemID.SkeletronPrimeBossBag);
        public static int PolterghastBag;// => CalamityID.ItemRelation("PolterghastBag", ItemID.PlanteraBossBag);
        public static int OldDukeBag;// => CalamityID.ItemRelation("OldDukeBag", ItemID.FishronBossBag);
        public static int DevourerofGodsBag;// => CalamityID.ItemRelation("DevourerofGodsBag", ItemID.MoonLordBossBag);
        public static int YharonBag;// => CalamityID.ItemRelation("YharonBag", ItemID.FishronBossBag);
        public static int DraedonBox;// => CalamityID.ItemRelation("DraedonBag", ItemID.MoonLordBossBag);
        public static int SupremeCalamitasCoffer;// => CalamityID.ItemRelation("CalamitasCoffer", ItemID.MoonLordBossBag);
        #endregion

        #region Crates
        public static int SulphurousCrate;
        public static int SunkenCrate;
        public static int AstralCrate;
        public static int HydrothermalCrate;
        public static int PrismCrate;
        public static int MonolithCrate;
        #endregion
        public static int AquaticHeart;
        public static int Bloodstone;
        public static int CeremonialUrn;
        public static int MeldConstruct;
        public static int DormantBrimseeker;

        public override void PostSetupContent()
        {
            StarterBag = CalamityID.ItemRelation("StarterBag", ItemID.KingSlimeBossBag);
            DesertScourgeBag = CalamityID.ItemRelation("DesertScourgeBag", "DesertScourgeTreasureBag", ItemID.EyeOfCthulhuBossBag);
            CrabulonBag = CalamityID.ItemRelation("CrabulonBag", "CrabulonTreasureBag", ItemID.BrainOfCthulhuBossBag);
            HiveMindBag = CalamityID.ItemRelation("HiveMindBag", ItemID.EaterOfWorldsBossBag);
            PerforatorBag = CalamityID.ItemRelation("PerforatorBag", ItemID.BrainOfCthulhuBossBag);
            SlimeGodBag = CalamityID.ItemRelation("SlimeGodBag", ItemID.SkeletronBossBag);
            CryogenBag = CalamityID.ItemRelation("CryogenBag", ItemID.DestroyerBossBag);
            AquaticScourgeBag = CalamityID.ItemRelation("AquaticScourgeBag", ItemID.TwinsBossBag);
            BrimstoneElementalBag = CalamityID.ItemRelation("BrimstoneWaifuBag", ItemID.SkeletronPrimeBossBag);
            CalamitasCloneBag = CalamityID.ItemRelation("CalamitasCloneBag", ItemID.TwinsBossBag);
            LeviathanBag = CalamityID.ItemRelation("LeviathanBag", ItemID.PlanteraBossBag);
            AstrumAureusBag = CalamityID.ItemRelation("AstrumAureusBag", ItemID.PlanteraBossBag);
            PlaguebringerGoliathBag = CalamityID.ItemRelation("PlaguebringerGoliathBag", ItemID.GolemBossBag);
            RavagerBag = CalamityID.ItemRelation("RavagerBag", ItemID.GolemBossBag);
            AstrumDeusBag = CalamityID.ItemRelation("AstrumDeusBag", ItemID.BossBagBetsy);
            BumbleBag = CalamityID.ItemRelation("DragonfollyBag", ItemID.BossBagBetsy);
            ProvidenceBag = CalamityID.ItemRelation("ProvidenceBag", ItemID.FairyQueenBossBag);
            StormWeaverBag = CalamityID.ItemRelation("StormWeaverBag", ItemID.DestroyerBossBag);
            CeaselessVoidBag = CalamityID.ItemRelation("CeaselessVoidBag", ItemID.TwinsBossBag);
            SignusBag = CalamityID.ItemRelation("SignusBag", ItemID.SkeletronPrimeBossBag);
            PolterghastBag = CalamityID.ItemRelation("PolterghastBag", ItemID.PlanteraBossBag);
            OldDukeBag = CalamityID.ItemRelation("OldDukeBag", ItemID.FishronBossBag);
            DevourerofGodsBag = CalamityID.ItemRelation("DevourerofGodsBag", ItemID.MoonLordBossBag);
            YharonBag = CalamityID.ItemRelation("YharonBag", ItemID.FishronBossBag);
            DraedonBox = CalamityID.ItemRelation("DraedonBag", ItemID.MoonLordBossBag);
            SupremeCalamitasCoffer = CalamityID.ItemRelation("CalamitasCoffer", ItemID.MoonLordBossBag);
            SulphurousCrate = CalamityID.ItemRelation("SulphurousCrate", ItemID.OceanCrateHard);
            SunkenCrate = CalamityID.ItemRelation("SunkenCrate", ItemID.OasisCrateHard);
            AstralCrate = CalamityID.ItemRelation("AstralCrate", ItemID.FloatingIslandFishingCrateHard);
            HydrothermalCrate = CalamityID.ItemRelation("HydrothermalCrate", ItemID.OceanCrateHard);
            PrismCrate = CalamityID.ItemRelation("PrismCrate", ItemID.OasisCrateHard);
            MonolithCrate = CalamityID.ItemRelation("MonolithCrate", ItemID.FloatingIslandFishingCrateHard);
            AquaticHeart = CalamityID.ItemRelation("AquaticHeart", ItemID.NeptunesShell);
            Bloodstone = CalamityID.ItemRelation("Bloodstone", ItemID.FragmentNebula);
            MeldConstruct = CalamityID.ItemRelation("MeldConstruct", ItemID.FragmentNebula);
            CeremonialUrn = CalamityID.ItemRelation("CeremonialUrn", ItemID.NebulaPickaxe);
            DormantBrimseeker = CalamityID.ItemRelation("DormantBrimseeker", ItemID.Milkshake);
        }
    }

    public class CalRarityID : ModSystem
    {
        public static int Turquoise;
        public static int PureGreen;
        public static int DarkBlue;
        public static int Violet;
        public static int HotPink;
        public static int CalamityRed;
        public static int DarkOrange;

        public override void PostSetupContent()
        {
            Turquoise = CalamityID.RarityRelation("Turquoise", ModContent.RarityType<Turquoise>());
            PureGreen = CalamityID.RarityRelation("PureGreen", ModContent.RarityType<PureGreen>());
            DarkBlue = CalamityID.RarityRelation("DarkBlue", ModContent.RarityType<DarkBlue>());
            Violet = CalamityID.RarityRelation("Violet", ModContent.RarityType<Violet>());
            HotPink = CalamityID.RarityRelation("HotPink", ModContent.RarityType<HotPink>());
            CalamityRed = CalamityID.RarityRelation("CalamityRed", ModContent.RarityType<CalamityRed>());
            DarkOrange = CalamityID.RarityRelation("DarkOrange", ModContent.RarityType<DarkOrange>());
        }
    }

    public class CalProjectileID : ModSystem
    {
        public static int MeldFlames;
        public static int AstrageldonLaser;
        public static int AstrageldonMinion;
        public static int PristineFire;
        public static int PristineAlt;
        public static int BabyFolly;
        public static int TitaniumShuriken;
        public static int WulfrumDroid;
        public static int AstralSpray;
        public static int BrimstoneMonster;
        public override void PostSetupContent()
        {
            MeldFlames = CalamityID.ProjectileRelation("CosmicFire", ProjectileID.VortexBeaterRocket);
            AstrageldonLaser = CalamityID.ProjectileRelation("AstrageldonLaser", ProjectileID.GreenLaser);
            AstrageldonMinion = CalamityID.ProjectileRelation("AstrageldonSummon", ProjectileID.GreenLaser);
            PristineFire = CalamityID.ProjectileRelation("PristineFire", ProjectileID.Fireball);
            PristineAlt = CalamityID.ProjectileRelation("PristineSecondary", ProjectileID.Fireball);
            BabyFolly = CalamityID.ProjectileRelation("MiniatureFolly", ProjectileID.RocketFireworkRed);
            WulfrumDroid = CalamityID.ProjectileRelation("WulfrumDroid", ProjectileID.UFOMinion);
            TitaniumShuriken = CalamityID.ProjectileRelation("TitaniumShurikenProjectile", ProjectileID.Shuriken);
            AstralSpray = CalamityID.ProjectileRelation("AstralSpray", -1);
            BrimstoneMonster = CalamityID.ProjectileRelation("BrimstoneMonster", -1);
        }
    }
    public class CalDustID : ModSystem
    {
        public static int AstralOrange;
        public override void PostSetupContent()
        {
            if (!Main.dedServ)
                AstralOrange = CalamityID.DustRelation("AstralOrange", DustID.AmberBolt);
            else
                AstralOrange = DustID.AmberBolt;
        }
    }
    public class CalTileID : ModSystem
    {
        public static int AstralMonolith;
        public static int AstralGrass;
        public static int AstralDirt;
        public static int AstralClay;
        public static int AstralSnow;
        public override void PostSetupContent()
        {
            AstralMonolith = CalamityID.TileRelation("AstralMonolith", -1);
            AstralGrass = CalamityID.TileRelation("AstralGrass", -1);
            AstralDirt = CalamityID.TileRelation("AstralDirt", -1);
            AstralClay = CalamityID.TileRelation("AstralClay", -1);
            AstralSnow = CalamityID.TileRelation("AstralSnow", -1);
        }
    }
    public class CalWallID : ModSystem
    {
        public static int AstralDirtWall;
        public override void PostSetupContent()
        {
            AstralDirtWall = CalamityID.WallRelation("AstralDirtWall", -1);
        }
    }
}