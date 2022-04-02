using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CalValEX.ExtraTextures.ChristmasPets;
using CalValEX.Backgrounds;
using CalValEX.Items;
using CalValEX.Items.Dyes;
using CalValEX.Items.Equips.Balloons;
using CalValEX.Items.Equips.Capes;
using CalValEX.Items.Equips.Hats;
using CalValEX.Items.Equips.Hats.Draedon;
using CalValEX.Items.Equips.Legs;
using CalValEX.Items.Equips.Scarves;
using CalValEX.Items.Equips.Shields;
using CalValEX.Items.Equips.Shirts;
using CalValEX.Items.Equips.Shirts.Draedon;
using CalValEX.Items.Equips.Transformations;
using CalValEX.Items.Equips.Wings;
using CalValEX.Items.Equips.Blanks;
using CalValEX.Items.Hooks;
using CalValEX.Items.LightPets;
using CalValEX.Items.Mounts;
using CalValEX.Items.Mounts.LimitedFlight;
using CalValEX.Items.Mounts.InfiniteFlight;
using CalValEX.Walls;
using CalValEX.Items.Walls.Astral;
using CalValEX.Items.Walls;
using CalValEX.Items.Pets;
using CalValEX.Items.Plushies;
using CalValEX.Items.Pets.Elementals;
using CalValEX.Items.Tiles;
using CalValEX.Items.Tiles.Blocks;
using CalValEX.Items.Tiles.FurnitureSets.Bloodstone;
using CalValEX.Items.Tiles.Plushies;
using CalValEX.NPCs.Oracle;
using CalValEX.NPCs.JellyPriest;
using CalValEX.Items.Tiles.Blocks.Astral;
using CalValEX.Tiles.MiscFurniture;
using CalValEX.Items.Tiles.FurnitureSets.Astral;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalamityMod;

namespace CalValEX
{
    public class CalValEX : Mod
    { 
        public enum MessageType
        {
            SyncOraclePlayer = 0,
            PlayerBagChanged,
            SyncCalValEXPlayer,
            SyncSCalHits
        }

        public static CalValEX instance;
        public Mod herosmod;
        public Mod infernum;
        public const string heropermission = "CalValEX";
        public const string heropermissiondisplayname = "Calamity's Vanities";
        public bool hasPermission;

        public static bool Bumble;
        public static bool Wulfrumset;
        public static bool WulfrumsetReal;
        public static string currentDate;
        public static int day;
        public static int month;

        public Action<Player> careationElement;
        public Action<Item> creationElement = new Action<Item>(item =>
        {
            item.type = ModContent.ItemType<CalamitousSoulArtifact>();
        });
        public Predicate<Item> caldetec;

        public static MethodInfo compactFraming;
        public static MethodInfo brimstoneFraming;

        public override void Load()
        {
            instance = this;
            herosmod = ModLoader.GetMod("HEROsMod");
            infernum = ModLoader.GetMod("InfernumMode");

            DateTime dateTime = DateTime.Now;
            currentDate = dateTime.ToString("dd/MM/yyyy");
            day = dateTime.Day;
            month = dateTime.Month;

            if (Main.dedServ)
                return;
            AddEquipTexture(null, EquipType.Legs, "BloodyMaryDress_Legs", "CalValEX/Items/Equips/Shirts/BloodyMaryDress_Legs");
            //Signus transformation
            AddEquipTexture(new SignusHead(), null, EquipType.Head, "SignusHead", "CalValEX/Items/Equips/Transformations/SignusTrans_Head");
            AddEquipTexture(new SignusBody(), null, EquipType.Body, "SignusBody", "CalValEX/Items/Equips/Transformations/SignusTrans_Body", "CalValEX/Items/Equips/Transformations/SignusTrans_Arms");
            AddEquipTexture(new SignusLegs(), null, EquipType.Legs, "SignusLegs", "CalValEX/Items/Equips/Transformations/SignusTrans_Legs");
            //TinyIbanRobotofDoom
            AddEquipTexture(new TinyIbanRobotOfDoomHead(), null, EquipType.Head, "TinyIbanRobotOfDoomHead", "CalValEX/Items/Equips/Transformations/TinyIbanRobotOfDoom_Head");
            AddEquipTexture(new TinyIbanRobotOfDoomBody(), null, EquipType.Body, "TinyIbanRobotOfDoomBody", "CalValEX/Items/Equips/Transformations/TinyIbanRobotOfDoom_Body", "CalValEX/Items/Equips/Transformations/TinyIbanRobotOfDoom_Arms");
            AddEquipTexture(new TinyIbanRobotOfDoomLegs(), null, EquipType.Legs, "TinyIbanRobotOfDoomLegs", "CalValEX/Items/Equips/Transformations/TinyIbanRobotOfDoom_Legs");
            //Classic Brimmy
            AddEquipTexture(new ClassicBrimmyHead(), null, EquipType.Head, "ClassicBrimmyHead", "CalValEX/Items/Equips/Transformations/ClassicBrimmy_Head");
            AddEquipTexture(new ClassicBrimmyBody(), null, EquipType.Body, "ClassicBrimmyBody", "CalValEX/Items/Equips/Transformations/ClassicBrimmy_Body", "CalValEX/Items/Equips/Transformations/ClassicBrimmy_Arms");
            AddEquipTexture(new ClassicBrimmyLegs(), null, EquipType.Legs, "ClassicBrimmyLegs", "CalValEX/Items/Equips/Transformations/ClassicBrimmy_Legs");
            //Cloud transformation
            AddEquipTexture(new CloudHead(), null, EquipType.Head, "CloudHead", "CalValEX/Items/Equips/Transformations/Cloud_Head");
            AddEquipTexture(new CloudBody(), null, EquipType.Body, "CloudBody", "CalValEX/Items/Equips/Transformations/Cloud_Body", "CalValEX/Items/Equips/Transformations/Cloud_Arms");
            AddEquipTexture(new CloudLegs(), null, EquipType.Legs, "CloudLegs", "CalValEX/Items/Equips/Transformations/Cloud_Legs");
            //Sand transformation
            AddEquipTexture(new SandHead(), null, EquipType.Head, "SandHead", "CalValEX/Items/Equips/Transformations/Sand_Head");
            AddEquipTexture(new SandBody(), null, EquipType.Body, "SandBody", "CalValEX/Items/Equips/Transformations/Sand_Body", "CalValEX/Items/Equips/Transformations/Sand_Arms");
            AddEquipTexture(new SandLegs(), null, EquipType.Legs, "SandLegs", "CalValEX/Items/Equips/Transformations/Sand_Legs");
            //Blanks
            AddEquipTexture(new BlankWings(), null, EquipType.Wings, "BlankWings", "CalValEX/Items/Equips/Shields/Invishield_Shield");

            GameShaders.Armor.BindShader(ModContent.ItemType<DraedonHologramDye>(),
                new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/DraedonHologramDye")),
                    "DraedonHologramDyePass"));        
            GameShaders.Armor.BindShader(ModContent.ItemType<BlightedAstralDye>(),
                new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/BlightedDye")),
                    "BlightedAstralDyePass")).UseColor(0.9882f, 0.7804f, 0.9686f).UseSecondaryColor(0.9098f, 0.5294f, 0.9764f); //UseColor - the rgb code for blight yel. UseSecondaryColor - the rgb code for blight puro.
            //
            GameShaders.Armor.BindShader(ModContent.ItemType<BlightedAstralPinkDye>(),
                new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/BlightedPinkDye")),
                    "BlightedAstralPinkDyePass")).UseColor(0.4431f, 0.3019f, 0.5725f).UseSecondaryColor(0.6235f,0.3647f,0.6666f); //UseColor - the rgb code for blight purple. UseSecondaryColor - the rgb code for blight darker purple.
            GameShaders.Armor.BindShader(ModContent.ItemType<BlightedAstralYellowDye>(),
                new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/BlightedYellowDye")),
                "BlightedAstralYellowDyePass")).UseColor(0.8588f, 0.5725f, 0.1137f).UseSecondaryColor(1f,0.8588f,0.3098f); //UseColor - the rgb code for blight darker yellow. UseSecondaryColor - the rgb code for blight yellow.
            //GameShaders.Armor.BindShader(ModContent.ItemType<BlightedAstralPinkDye>(),
           // new ArmorShaderData(new Ref<Effect>(GetEffect("Effects/AstralBlightPurple")),
              //  "AstralBlightPurplePass")).UseColor(0.8588f, 0.5725f, 0.1137f).UseSecondaryColor(1f, 0.8588f, 0.3098f); //UseColor - the rgb code for blight darker yellow. UseSecondaryColor - the rgb code for blight yellow.



            DraedonHelmetTextureCache.Load();
            DraedonChestplateCache.Load();

            Filters.Scene["CalValEX:AstralBiome"] = new Filter(new AstralSkyData("FilterMiniTower").UseColor(Color.Purple).UseOpacity(0.15f), EffectPriority.VeryHigh);
            SkyManager.Instance["CalValEX:AstralBiome"] = new AstralSky();

            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AstralBlight"), ItemType("AstralMusicBox"), TileType("AstralMusicBoxPlaced"));
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Meldosaurus"), ItemType("MeldosaurusMusicBox"), TileType("MeldosaurusMusicBoxPlaced"));
        }

        public override void Unload()
        {
            instance = null;
            herosmod = null;
            infernum = null;
            hasPermission = false;

            currentDate = null;
            Bumble = false;
            Wulfrumset = false;
            day = -1;
            month = -1;
            compactFraming = null;
            brimstoneFraming = null;

            if (Main.dedServ)
                return;

            //ChristmasTextureChange.Unload();
            DraedonHelmetTextureCache.Unload();
            DraedonChestplateCache.Unload();
        }

        public override void PostSetupContent()
        {
            //Tooltip changes
            Mod cal = ModLoader.GetMod("CalamityMod");
            cal.GetItem("LaboratoryConsoleItem").Tooltip
                .AddTranslation(GameCulture.English, "Can be used to print blueprints");
            cal.GetItem("HeartoftheElements").Tooltip.AddTranslation(GameCulture.English, "The heart of the world\n" +
                "Summons all elementals to protect you\n" + "Equipping this item in a vanity slot summons passive versions of the Elementals");
            cal.GetItem("CryoStone").Tooltip.AddTranslation(GameCulture.English, "One of the ancient relics\n" +
            "Creates a rotating ice shield around you that damages and slows enemies on contact\n" +
            "Equipping the item in a vanity slot summons a harmless ice shield\n" +
            "Can be dyed with the light pet dye slot when equipped in a vanity slot");
            cal.GetItem("EyeoftheStorm").Tooltip.AddTranslation(GameCulture.English, "Summons a cloud elemental to fight for you\n" + "Equipping this item in a vanity slot summons a passive version");
            cal.GetItem("RoseStone").Tooltip.AddTranslation(GameCulture.English, "Summons a brimstone elemental to fight for you\n" + "Equipping this item in a vanity slot summons a passive version");
            cal.GetItem("WifeinaBottle").Tooltip.AddTranslation(GameCulture.English, "Summons a sand elemental to fight for you\n" + "Equipping this item in a vanity slot summons a passive version");
            cal.GetItem("WifeinaBottlewithBoobs").Tooltip.AddTranslation(GameCulture.English, "Summons a sand elemental to heal you\n" + "Equipping this item in a vanity slot summons a passive version\n" +
                ";D");
            cal.GetItem("LureofEnthrallment").Tooltip.AddTranslation(GameCulture.English, "Summons a water elemental to fight for you\n" +
                "The elemental stays above you, shooting water spears, ice mist, and treble clefs at nearby enemies\n" + "Equipping this item in a vanity slot summons a passive version");
            cal.GetItem("MutatedTruffle").Tooltip.AddTranslation(GameCulture.English, "Summons a small Old Duke to fight for you\n" +
                               "When below 50% life, it moves much faster\n" + "Equipping this item in a vanity slot summons a passive version");
            cal.GetItem("FungalClump").Tooltip.AddTranslation(GameCulture.English, "Summons a fungal clump to fight for you\n" +
                       "The clump latches onto enemies and steals their life for you\n" + "Equipping this item in a vanity slot summons a passive version");

            if ((CalValEX.day == 1 && CalValEX.month == 4) || ModLoader.GetMod("CalValPlus") != null)
            {
                cal.GetItem("TheEmpyrean").Tooltip.AddTranslation(GameCulture.English, "70% chance to not consume gel\n"+"Keep away from the jester...");
            }

            cal.Call("MakeItemExhumable", ModContent.ItemType<CalArtifact>(), ModContent.ItemType<CalamitousSoulArtifact>());

            //Census support
            Mod censusMod = ModLoader.GetMod("Census");
            if (censusMod != null)
            {
                censusMod.Call("TownNPCCondition", ModContent.NPCType<OracleNPC>(), "Equip a Pet or Light Pet");
                censusMod.Call("TownNPCCondition", ModContent.NPCType<JellyPriestNPC>(),
                    "Find at the Sulphurous Sea after defeating Acid Rain tier 1");
                censusMod.Call("TownNPCCondition", ModContent.NPCType<AprilFools.Jharim.Jharim>(), "Dropped from the Starter Bag during April Fools");
            }

            //Compact tile framing support
            Type tileFraming = cal.Code.GetType("CalamityMod.TileFraming");

            compactFraming = tileFraming.GetMethod("CompactFraming", BindingFlags.Static | BindingFlags.NonPublic);

            //Compact tile framing support
            Type tileFraming2 = cal.Code.GetType("CalamityMod.TileFraming");

            brimstoneFraming = tileFraming2.GetMethod("BrimstoneFraming", BindingFlags.Static | BindingFlags.NonPublic);

            //Christmas textures
            ChristmasTextureChange.Load();

            //Boss log support
            Mod catal = ModLoader.GetMod("CatalystMod");
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Desert Scourge",
                    new List<int>
                    {
                        ModContent.ItemType<DesertScourgePlush>(), ModContent.ItemType<DesertMedallion>(), 
                        ModContent.ItemType<DriedMandible>(), ModContent.ItemType<SandTooth>()
                    });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Giant Clam",
                    new List<int> { ModContent.ItemType<ClamMask>(), ModContent.ItemType<ClamHermitMedallion>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Crabulon",
                    new List<int> { ModContent.ItemType<CrabulonPlush>(), ModContent.ItemType<ClawShroom>()});
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Hive Mind",
                    new List<int> { ModContent.ItemType<HiveMindPlush>(), ModContent.ItemType<MissingFang>()});
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "The Perforators",
                    new List<int> { ModContent.ItemType<PerforatorPlush>(), 
                        ModContent.ItemType<DigestedWormFood>(), ModContent.ItemType<SmallWorm>(),
                        ModContent.ItemType<MidWorm>(), ModContent.ItemType<BigWorm>()});
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Slime God",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("StatigelBlock") });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Slime God",
                    new List<int> { ModContent.ItemType<SlimeGodMask>(), ModContent.ItemType<SlimeGodPlush>(), ModContent.ItemType<ImpureStick>()});
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Cryogen",
                    new List<int> { ModContent.ItemType<CryogenPlush>(), ModContent.ItemType<CryoStick>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Aquatic Scourge",
                    new List<int> { ModContent.ItemType<AquaticScourgePlush>(), ModContent.ItemType<AquaticHide>() });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Brimstone Elemental",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("BrimstoneSlag") });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Brimstone Elemental",
                    new List<int>
                    {
                        ModContent.ItemType<BrimstoneElementalPlush>(), ModContent.ItemType<BrimmyBody>(),
                        ModContent.ItemType<BrimmySpirit>(), ModContent.ItemType<FoilSpoon>(),
                        ModContent.ItemType<brimtulip>()
                    });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Calamitas",
                    new List<int> { ModContent.ItemType<ClonePlush>(), ModContent.ItemType<Calacirclet>(), ModContent.ItemType<AncientAuricTeslaHelm>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Leviathan",
                    new List<int>
                    {
                        ModContent.ItemType<AnahitaPlush>(), ModContent.ItemType<LeviathanPlush>(),
                        ModContent.ItemType<LeviWings>(), ModContent.ItemType<FoilAtlantis>(), ModContent.ItemType<LeviathanEgg>(),
                        ModContent.ItemType<WetBubble>()
                    });
                //Remove pet from Aureus' boss log if Catalyst is enabled
                if (catal == null)
                {
                    bossChecklist.Call("AddToBossCollection", "CalamityMod", "Astrum Aureus",
                        new List<int>
                        {
                        ModContent.ItemType<AstrumAureusPlush>(), ModContent.ItemType<AureusShield>(), ModContent.ItemType<AstDie>(),
                        ModContent.ItemType<JellyBottle>(), ModContent.ItemType<AncientAuricTeslaHelm>()
                        });
                }
                else
                {
                    bossChecklist.Call("AddToBossCollection", "CalamityMod", "Astrum Aureus",
                        new List<int>
                        {
                        ModContent.ItemType<AstrumAureusPlush>(), ModContent.ItemType<AureusShield>(), ModContent.ItemType<AstDie>(), ModContent.ItemType<AncientAuricTeslaHelm>()
                        });
                }
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Plaguebringer Goliath",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("PlaguedPlate"), ModContent.ItemType<PlagueHiveWand>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Plaguebringer Goliath",
                    new List<int>
                    {
                        ModContent.ItemType<PlaguebringerGoliathPlush>(), ModContent.ItemType<PlaguePack>(),
                        ModContent.ItemType<InfectedController>(), ModContent.ItemType<AncientAuricTeslaHelm>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Ravager",
                    new List<int> { ModContent.ItemType<Necrostone>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Ravager",
                    new List<int>
                    {
                        ModContent.ItemType<RavagerPlush>(), ModContent.ItemType<SkullBalloon>(),
                        ModContent.ItemType<RavaHook>(), ModContent.ItemType<ScavaHook>(),
                        ModContent.ItemType<AncientChoker>()
                    });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Astrum Deus",
                    new List<int> { 
                        ModContent.ItemType<AstrumDeusPlush>(), ModContent.ItemType<AstBandana>(), 
                        ModContent.ItemType<AstralStar>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Profaned Guardians",
                    new List<int>
                    {
                        ModContent.ItemType<ProfanedGuardianPlush>(), ModContent.ItemType<ProfanedFrame>(), 
                        ModContent.ItemType<ProfanedBattery>(), ModContent.ItemType<ProfanedWheels>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Dragonfolly",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("SilvaCrystal") });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Dragonfolly",
                    new List<int>
                    {
                        ModContent.ItemType<BumblefuckPlush>(), ModContent.ItemType<FollyWings>(),
                        ModContent.ItemType<Birbhat>(), ModContent.ItemType<FollyWing>(),
                        ModContent.ItemType<OrbSummon>(), ModContent.ItemType<FluffyFeather>(),
                        ModContent.ItemType<SparrowMeat>(), ModContent.ItemType<FluffyFur>(),
                        ModContent.ItemType<AncientAuricTeslaHelm>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Providence",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("ProfanedRock") });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Providence",
                    new List<int>
                    {
                        ModContent.ItemType<ProvidencePlush>(), ModContent.ItemType<ProviCrystal>(),
                        ModContent.ItemType<ProShard>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Ceaseless Void",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("OccultStone") });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Ceaseless Void",
                    new List<int>
                    {
                        ModContent.ItemType<CeaselessVoidPlush>(), ModContent.ItemType<VoidWings>(),
                        ModContent.ItemType<OldVoidWings>(), ModContent.ItemType<VoidShard>(),
                        ModContent.ItemType<AncientAuricTeslaHelm>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Storm Weaver",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("OccultStone") });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Storm Weaver",
                    new List<int>
                    {
                        ModContent.ItemType<StormWeaverPlush>(), ModContent.ItemType<StormBandana>(),
                        ModContent.ItemType<ShellScrap>(), ModContent.ItemType<WeaverFlesh>(),
                        ModContent.ItemType<AncientAuricTeslaHelm>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Signus",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("OccultStone") });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Signus",
                    new List<int>
                    {
                        ModContent.ItemType<SignusPlush>(), ModContent.ItemType<SignusEmblem>(),
                        ModContent.ItemType<SigCape>(), ModContent.ItemType<SignusNether>(),
                        ModContent.ItemType<SigCloth>(), ModContent.ItemType<SignusBalloon>(), ModContent.ItemType<JunkoHat>(),
                        ModContent.ItemType<AncientAuricTeslaHelm>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Polterghast",
                    new List<int> { ModLoader.GetMod("CalamityMod").ItemType("StratusBricks"), ModContent.ItemType<PhantowaxBlock>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Polterghast",
                    new List<int> {
                        ModContent.ItemType<PolterghastPlush>(),
                        ModContent.ItemType<Polterhook>(), ModContent.ItemType<ToyScythe>() 
                    });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Old Duke",
                    new List<int>
                    {
                        ModContent.ItemType<OldDukePlush>(), ModContent.ItemType<OldWings>(),
                        ModContent.ItemType<CorrodedCleaver>(), ModContent.ItemType<CharredChopper>()
                    });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Devourer of Gods",
                    new List<int>
                    {
                        ModContent.ItemType<DevourerofGodsPlush>(),
                        ModContent.ItemType<CosmicWormScarf>(), ModContent.ItemType<RapturedWormScarf>(),
                        ModContent.ItemType<DogPetItem>(), ModContent.ItemType<AncientAuricTeslaHelm>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Yharon",
                    new List<int> { ModContent.ItemType<Termipebbles>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Yharon",
                    new List<int>
                    {
                        ModContent.ItemType<YharonPlush>(), ModContent.ItemType<JunglePhoenixWings>(),
                        ModContent.ItemType<YharonShackle>(), ModContent.ItemType<NuggetBiscuit>(), ModContent.ItemType<YharonsAnklet>(),
                        ModContent.ItemType<AncientAuricTeslaHelm>(),
                        ModContent.ItemType<DemonshadeHood>(), ModContent.ItemType<DemonshadeRobe>(), ModContent.ItemType<DemonshadePants>()
                    });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Supreme Calamitas",
                    new List<int> { ModContent.ItemType<CalamitasFumo>(), ModContent.ItemType<GruelingMask>(), ModContent.ItemType<AncientAuricTeslaHelm>() });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Exo Mechs",
                    new List<int> { ModContent.ItemType<XMLightningHook>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Exo Mechs",
                    new List<int> { ModContent.ItemType<DraedonBody>(), ModContent.ItemType<DraedonLegs>(), ModContent.ItemType<DraedonPlush>(), ModContent.ItemType<AresPlush>(), ModContent.ItemType<ApolloPlush>(), ModContent.ItemType<ArtemisPlush>(), ModContent.ItemType<ThanatosPlush>(), ModContent.ItemType<AncientAuricTeslaHelm>(), ModContent.ItemType<ArtemisBalloonSmall>(), ModContent.ItemType<ApolloBalloonSmall>(), ModContent.ItemType<Items.Equips.Shirts.AresChestplate.AresChestplate>(), ModContent.ItemType<Items.Pets.ExoMechs.GunmetalRemote>(), ModContent.ItemType<Items.Pets.ExoMechs.GeminiMarkImplants>(), ModContent.ItemType<Items.Pets.ExoMechs.OminousCore>() });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Adult Eidolon Wyrm",
                    new List<int> { ModContent.ItemType<RespirationShrine>()});
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Adult Eidolon Wyrm",
                    new List<int> { ModContent.ItemType<JaredPlush>(), ModContent.ItemType<SoulShard>(), ModContent.ItemType<OmegaBlue>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Acid Rain (Post-AS)",
                    new List<int>
                    {
                        ModContent.ItemType<MawHook>(), ModContent.ItemType<FlakHeadCrab>(),
                        ModContent.ItemType<SkaterEgg>(), ModContent.ItemType<Help>(),
                        ModContent.ItemType<TrilobiteShield>()
                    });
                bossChecklist.Call("AddToBossLoot", "CalamityMod", "Acid Rain (Post-Polter)",
                    new List<int> { ModContent.ItemType<NuclearFumes>() });
                bossChecklist.Call("AddToBossCollection", "CalamityMod", "Acid Rain (Post-Polter)",
                    new List<int>
                    {
                        ModContent.ItemType<MawHook>(), ModContent.ItemType<FlakHeadCrab>(),
                        ModContent.ItemType<SkaterEgg>(), ModContent.ItemType<Help>(), ModContent.ItemType<Items.Mounts.Ground.RadJuice>(), ModContent.ItemType<Items.Equips.Legs.TerrorLegs>(),
                        ModContent.ItemType<TrilobiteShield>(),
                        ModContent.ItemType<GammaHelmet>()
                    });
                //Catalyst Support
                if (catal != null)
                {
                    bossChecklist.Call("AddToBossCollection", "CatalystMod", "Astrageldon",
                           new List<int> { ModContent.ItemType<JellyBottle>(), ModContent.ItemType<Items.Tiles.Plushies.AstrageldonPlush>() });
                }
                if ((month == 4 && day == 1) || ModLoader.GetMod("CalValPlus") != null)
                {
					bossChecklist.Call(new object[12]
				{
				"AddBoss",
				13f,
				ModContent.NPCType<AprilFools.Meldosaurus.Meldosaurus>(),
				this,
				"Meldosaurus",
				(Func<bool>)(() => CalValEXWorld.downedMeldosaurus),
				ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.TheEmpyrean>(),
				new List<int>
				{
					ModLoader.GetMod("CalValEX").ItemType("MeldosaurusMask"),
					ModLoader.GetMod("CalValEX").ItemType("KnowledgeMeldosaurus"),
					ModLoader.GetMod("CalValEX").ItemType("MeldosaurusTrophy"),
					ModLoader.GetMod("CalValEX").ItemType("MeldosaurusMusicBox")
				},
				new List<int>
				{
					ModLoader.GetMod("CalamityMod").ItemType("MeldBlob"),
					ModLoader.GetMod("CalValEX").ItemType("Nyanthrop"),
					ModLoader.GetMod("CalValEX").ItemType("ShadesBane")
				},
				$"Shoot the Jungle Tyrant Town NPC with [i:{ModContent.ItemType<CalamityMod.Items.Weapons.Ranged.TheEmpyrean>()}]",
				"Get melded!",
				null
				});
                }   
            }

            if (ModContent.GetInstance<CalValEXConfig>().DiscordRichPresence)
            {
                try
                {
                    var drp = ModLoader.GetMod("DiscordRP");
                    if (drp != null)
                    {
                        // This discord rich presence stuff is very wacky.
                        // Get in contact with (Discord: nalyddd#9372, Github: NalydddNobel) if you want to change any of this.
                        // (even if you are completely removing this, atleast tell me so I can get rid of my Discord-Developer-Application which hosts these images)
                        drp.Call("AddClient", "929973580178010152", "mod_calvalex");
                        drp.Call("AddBiome", (Func<bool>)(() => Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().ZoneAstral), "Astral Blight",
                            "biome_astralblight", 50f, "mod_calvalex");
                    
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error when adding Discord Rich Presence support!", ex);
                }
            }
        }

        public static void MountNerf(Player player, float reduceDamageBy, float reduceHealthBy)
        {
            bool bossIsAlive = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.boss)
                {
                    bossIsAlive = true;
                }
            }

            if (bossIsAlive && !CalValEXConfig.Instance.GroundMountLol)
            {
                int calculateLife = (int) (player.statLifeMax2 * reduceHealthBy);
                player.statLifeMax2 -= calculateLife;
                player.allDamage -= reduceDamageBy;
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType) reader.ReadByte();
            byte playerNumber;
            OraclePlayer oraclePlayer;
            CalValEXPlayer calValEXPlayer;
            int SCalHits;
            switch (msgType)
            {
                case MessageType.SyncOraclePlayer:
                    playerNumber = reader.ReadByte();
                    oraclePlayer = Main.player[playerNumber].GetModPlayer<OraclePlayer>();
                    oraclePlayer.playerHasGottenBag = reader.ReadBoolean();
                    break;

                case MessageType.PlayerBagChanged:
                    playerNumber = reader.ReadByte();
                    oraclePlayer = Main.player[playerNumber].GetModPlayer<OraclePlayer>();
                    oraclePlayer.playerHasGottenBag = reader.ReadBoolean();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte) MessageType.PlayerBagChanged);
                        packet.Write(playerNumber);
                        packet.Write(oraclePlayer.playerHasGottenBag);
                        packet.Send(-1, playerNumber);
                    }

                    break;

                case MessageType.SyncCalValEXPlayer:
                    playerNumber = reader.ReadByte();
                    calValEXPlayer = Main.player[playerNumber].GetModPlayer<CalValEXPlayer>();
                    SCalHits = reader.ReadInt32();
                    calValEXPlayer.SCalHits = SCalHits;
                    break;

                case MessageType.SyncSCalHits:
                    playerNumber = reader.ReadByte();
                    calValEXPlayer = Main.player[playerNumber].GetModPlayer<CalValEXPlayer>();
                    SCalHits = reader.ReadInt32();
                    calValEXPlayer.SCalHits = SCalHits;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        var packet = GetPacket();
                        packet.Write((byte) MessageType.SyncSCalHits);
                        packet.Write(playerNumber);
                        packet.Write(calValEXPlayer.SCalHits);
                        packet.Send(-1, playerNumber);
                    }

                    break;

                default:
                    Logger.WarnFormat("CalValEX: Unknown Message type: {0}", msgType);
                    break;
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }
            if (Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().ZoneAstral)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/AstralBlight");
                priority = MusicPriority.Environment;
            }
            if ((Main.LocalPlayer.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().enraged || Main.LocalPlayer.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().adrenaline > 0f || Main.LocalPlayer.GetModPlayer<CalamityMod.CalPlayer.CalamityPlayer>().rage > 0f) && (Main.LocalPlayer.HeldItem.type == ModContent.ItemType<CalamityMod.Items.Weapons.Melee.Murasama>()/* || Main.LocalPlayer.HeldItem.type == ModContent.ItemType<CalamityMod.Items.Weapons.Melee.UHFMurasama>() One day my love...*/) && Main.LocalPlayer.controlUseItem && !CalValEXConfig.Instance.Sama)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/Murasama");
                priority = MusicPriority.BossHigh + 1;
            }

        }

        public override void AddRecipeGroups()
        {
            RecipeGroup sand = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Sand"]];
            sand.ValidItems.Add(ModContent.ItemType<AstralSand>());

            RecipeGroup fieref = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Fireflies"]];
            fieref.ValidItems.Add(ModContent.ItemType<Items.Critters.NukeFlyItem>());
            fieref.ValidItems.Add(ModContent.ItemType<Items.Critters.BlinkerItem>());

            RecipeGroup bf = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Butterflies"]];
            bf.ValidItems.Add(ModContent.ItemType<Items.Critters.ProvFlyItem>());
            bf.ValidItems.Add(ModContent.ItemType<Items.Critters.CrystalFlyItem>());

            if (RecipeGroup.recipeGroupIDs.ContainsKey("WingsGroup"))
            {
                int index = RecipeGroup.recipeGroupIDs["WingsGroup"];
                RecipeGroup groupe = RecipeGroup.recipeGroups[index];
                groupe.ValidItems.Add(ModContent.ItemType<WulfrumHelipack>());
                groupe.ValidItems.Add(ModContent.ItemType<AeroWings>());
                groupe.ValidItems.Add(ModContent.ItemType<GodspeedBoosters>());
                groupe.ValidItems.Add(ModContent.ItemType<FollyWings>());
                groupe.ValidItems.Add(ModContent.ItemType<JunglePhoenixWings>());
                groupe.ValidItems.Add(ModContent.ItemType<LeviWings>());
                groupe.ValidItems.Add(ModContent.ItemType<OldVoidWings>());
                groupe.ValidItems.Add(ModContent.ItemType<VoidWings>());
                groupe.ValidItems.Add(ModContent.ItemType<PlaugeWings>());
                groupe.ValidItems.Add(ModContent.ItemType<ScryllianWings>());
                groupe.ValidItems.Add(ModContent.ItemType<TerminalWings>());
            }

            if (RecipeGroup.recipeGroupIDs.ContainsKey("AnyIceBlock"))
            {
                int index = RecipeGroup.recipeGroupIDs["AnyIceBlock"];
                RecipeGroup groupe = RecipeGroup.recipeGroups[index];
                groupe.ValidItems.Add(ModContent.ItemType<AstralIce>());
            }

            RecipeGroup group = new RecipeGroup(() => "Any Plate", new int[]
            {
                ModLoader.GetMod("CalamityMod").ItemType("PlagueContainmentCells"),
                ModLoader.GetMod("CalamityMod").ItemType("Cinderplate"),
                ModLoader.GetMod("CalamityMod").ItemType("Chaosplate"),
                ModLoader.GetMod("CalamityMod").ItemType("Navyplate"),
                ModLoader.GetMod("CalamityMod").ItemType("Elumplate")
            });
            RecipeGroup.RegisterGroup("AnyPlate", group);
        }

        public override void AddRecipes()
        {
            Mod catalyst = ModLoader.GetMod("CatalystMod");
            if (catalyst != null)
            {
                catalyst.Call("itemset_superbossrarity", ModContent.ItemType<AstrageldonPlush>(), true);
                catalyst.Call("itemset_superbossrarity", ModContent.ItemType<AstrageldonPlushThrowable>(), true);
                catalyst.Call("itemset_superbossrarity", ModContent.ItemType<JellyBottle>(), true);
                catalyst.Call("itemset_superbossrarity", ModContent.ItemType<JaredPlush>(), true);
                catalyst.Call("itemset_superbossrarity", ModContent.ItemType<JaredPlushThrowable>(), true);
                catalyst.Call("itemset_superbossrarity", ModContent.ItemType<RespirationShrine>(), true);
                catalyst.Call("itemset_superbossrarity", ModContent.ItemType<SoulShard>(), true);
            }
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            //Irradiated
            ModRecipe recipe = new ModRecipe(this);
            recipe = new ModRecipe(this);
            recipe.AddIngredient(calamityMod.ItemType("GammaSlimeBanner"));
            recipe.AddIngredient(ModContent.ItemType<NuclearFumes>(), 50);
            recipe.AddTile(TileID.Solidifier);
            recipe.SetResult(calamityMod.ItemType("IrradiatedSlimeBanner"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(calamityMod.ItemType("GammaSlimeBanner"));
            recipe.AddIngredient(ModContent.ItemType<NuclearFumes>(), 50);
            recipe.AddTile(calamityMod.TileType("StaticRefiner"));
            recipe.SetResult(calamityMod.ItemType("IrradiatedSlimeBanner"));
            recipe.AddRecipe();
            //Bloodstone wall to Bloodstone
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<BloodstoneWall>(), 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(calamityMod.ItemType("Bloodstone"));
            recipe.AddRecipe();
            //Astral blocks
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralTreeWood>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralMonolith"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralSand>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralSand"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralSandstone>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralSandstone"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralClay>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralClay"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<Xenostone>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralStone"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralDirt>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralDirt"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralHardenedSand>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("HardenedAstralSand"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralIce>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralIce"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralSnow>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralSnow"));
            recipe.AddRecipe();
            //Astral Walls
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralDirtWall>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralDirtWall"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<XenostoneWall>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralStoneWall"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralHardenedSandWall>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("HardenedAstralSandWall"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralSandstoneWall>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralSandstoneWall"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralGrassWall>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralGrassWall"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<AstralIceWall>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralIceWall"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<XenomonolithWall>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("AstralMonolithWall"));
            recipe.AddRecipe();
            //Astral Furniture
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralBathtubItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithBathtub"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralBedItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithBed"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralCandelabraItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithCandelabra"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralCandleItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithCandle"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralLanternItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithLantern"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralLampItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithLamp"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralClockItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithClock"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralSofaItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithBench"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralSinkItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithSink"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralTableItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithTable"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralWorkbenchItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithWorkBench"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralDoorItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithDoor"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralBookshelfItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithBookcase"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralChestItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithChest"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralChandelierItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithChandelier"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralPianoItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithPiano"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<OldAstralDresserItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("MonolithDresser"));
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ModContent.ItemType<Items.Critters.BlinkerItem>());
            recipe.AddTile(ModContent.TileType<StarstruckSynthesizerPlaced>());
            recipe.SetResult(calamityMod.ItemType("TwinklerItem"));
            recipe.AddRecipe();
        }

        public void SetupHerosMod()
        {
            if (herosmod != null)
            {
                herosmod.Call(
                    // Special string
                    "AddPermission",
                    // Permission Name
                    heropermission,
                    // Permission Display Name
                    heropermissiondisplayname);
            }
        }

        public override void PostAddRecipes()
        {
            SetupHerosMod();
        }

        public bool getPermission()
        {
            return hasPermission;
        }
    }
}
