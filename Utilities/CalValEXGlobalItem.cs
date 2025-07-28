//using CalValEX.Buffs.LightPets;
using CalValEX.Items;
using CalValEX.Items.Equips.Balloons;
using CalValEX.Items.Equips.Hats;
using CalValEX.Items.Equips.Legs;
using CalValEX.Items.Equips.Scarves;
using CalValEX.Items.Equips.Shields;
using CalValEX.Items.Equips.Shirts;
using CalValEX.Items.Equips.Wings;
using CalValEX.Items.Hooks;
using CalValEX.Items.LightPets;
using CalValEX.Items.Mounts.InfiniteFlight;
using CalValEX.Items.Mounts.LimitedFlight;
using CalValEX.Items.Pets;
using CalValEX.Items.Pets.Elementals;
using CalValEX.Items.Tiles;
using CalValEX.Items.Tiles.Blocks;
using CalValEX.Tiles.Blocks;
using CalValEX.Tiles;
using CalValEX.Items.Tiles.Monoliths;
using CalValEX.Items.Tiles.Plants;
using CalValEX.Items.Tiles.Statues;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using System.Linq;
using CalValEX.CalamityID;
using CalValEX.Tiles.Paintings;

namespace CalValEX
{
    public class CalValEXGlobalitem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override void SetDefaults(Item item)
        {
            if (!CalValEX.CalamityActive)
                return;

            MakePlaceable(item, CalItemID.Bloodstone, ModContent.TileType<BloodstonePlaced>());
            MakePlaceable(item, CalItemID.MeldConstruct, ModContent.TileType<MeldConstructPlaced>());
            MakePlaceable(item, CalItemID.CeremonialUrn, ModContent.TileType<CeremonialUrnPlaced>());
            MakePlaceable(item, CalItemID.SupremeCalamitasCoffer, ModContent.TileType<CalamitasCofferPlaced>());
            MakePlaceable(item, CalItemID.DraedonBox, ModContent.TileType<DraedonQuoteonQuoteBagPlaced>());
        }

        public void MakePlaceable(Item item, int theitem, int tile)
        {
            if (item.type == theitem)
            {
                item.useTurn = true;
                item.autoReuse = true;
                item.useAnimation = 15;
                item.useTime = 10;
                item.useStyle = ItemUseStyleID.Swing;
                item.consumable = true;
                item.createTile = tile;
            }
        }

        public override void ArmorSetShadows(Player player, string set)
        {
            if (player.GetModPlayer<CalValEXPlayer>().cassette)
            {
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
			}
		}

		public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
            LeadingConditionRule rule = new(new DropsEnabled());
            LeadingConditionRule rule2 = new(new BlockDrops());
            LeadingConditionRule rule6 = new(new CalamityDay());
            LeadingConditionRule rule7 = new(new ThanatosDowned());
            LeadingConditionRule rule8 = new(new TwinsDowned());
            LeadingConditionRule rule9 = new(new AresDowned());
            LeadingConditionRule rule10 = new(new MidhardmodeDowned());
            LeadingConditionRule rule11 = new(new PolterDowned());
            LeadingConditionRule rule12 = new(new AquaDowned());

            #region bags
            if (item.type == CalItemID.StarterBag)
			{
				itemLoot.Add(rule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<C>())));
                itemLoot.Add(ItemDropRule.ByCondition(new April14(), PaintingLoader.paintingItems["GallusYharus"]));
            }
			else if (item.type == CalItemID.DesertScourgeBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<DesertMedallion>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DriedLocket>(), 5)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SlightlyMoistbutalsoSlightlyDryLocket>(), 100, chanceNumerator: 7)));
			}
			else if (item.type == CalItemID.CrabulonBag)
            {
                itemLoot.Add(rule2.OnSuccess(new CommonDrop(ModContent.ItemType<MushroomCap>(), 1, 205, 335)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ClawShroom>(), 10, chanceNumerator: 3)));
			}
			else if (item.type == CalItemID.HiveMindBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<Corruppuccino>(), 10, chanceNumerator: 3)));
			}
			else if (item.type == CalItemID.PerforatorBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<MeatyWormTumor>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new OneFromOptionsNotScaledWithLuckDropRule(100, 40, ModContent.ItemType<SmallWorm>(), ModContent.ItemType<MidWorm>(), ModContent.ItemType<BigWorm>())));
			}
			else if (item.type == CalItemID.SlimeGodBag)
            {
                AddBlockDrop(ref itemLoot, "StatigelBlock");
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SlimeDeitysSoul>(), 10, chanceNumerator: 3)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SlimeGodMask>(), 7)));
            }
			else if (item.type == CalItemID.CryogenBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<CoolShades>(), 10, chanceNumerator: 3)));
			}
			else if (item.type == CalItemID.AquaticScourgeBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<MoistLocket>(), 10, chanceNumerator: 3)));
			}
			else if (item.type == CalItemID.BrimstoneElementalBag)
            {
                AddBlockDrop(ref itemLoot, "BrimstoneSlag");
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BrimmyBody>(), 5)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BrimmySpirit>(), 5)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<RareBrimtulip>(), 5)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<FoilSpoon>(), 20)));
			}
			else if (item.type == CalItemID.CalamitasCloneBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<Calacirclet>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 1000)));
			}
			else if (item.type == CalItemID.LeviathanBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<AquaticMonolith>(), 100, chanceNumerator: 15)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<LeviWings>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<LeviathanEgg>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<FoilAtlantis>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<StrangeMusicNote>(), 100)));
			}
			else if (item.type == CalItemID.AstrumAureusBag)
			{
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AstralInfectedIcosahedron>(), 5)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AureusShield>(), 5)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 1000)));
			}
			else if (item.type == CalItemID.PlaguebringerGoliathBag)
            {
                AddBlockDrop(ref itemLoot, "PlaguedContainmentBrick");
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 250)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<InfectedController>(), 5)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<PlaguePack>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<PlagueHiveWand>(), 3)));
			}
			else if (item.type == CalItemID.RavagerBag)
			{
				itemLoot.Add(rule2.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Necrostone>(), 1, 205, 335)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SkullCluster>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ScavaHook>(), 100, chanceNumerator: 7)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<RavaHook>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SkullBalloon>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<StonePile>(), 10, chanceNumerator: 3)));
			}
			else if (item.type == CalItemID.AstrumDeusBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<Geminga>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AstBandana>(), 5)));
			}
			else if (item.type == CalItemID.BumbleBag)
            {
                AddBlockDrop(ref itemLoot, "SilvaCrystal", true);
                itemLoot.Add(rule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<FollyWings>(), ModContent.ItemType<Birbhat>(), ModContent.ItemType<DocilePheromones>())));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 200)));
			}
			else if (item.type == CalItemID.ProvidenceBag)
            {
                AddBlockDrop(ref itemLoot, "ProfanedRock");
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ProfanedHeart>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ProviCrystal>(), 10, chanceNumerator: 3)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ProvidenceAltar>(), 10, chanceNumerator: 3)));
            }
			else if (item.type == CalItemID.StormWeaverBag)
            {
                AddBlockDrop(ref itemLoot, "OtherworldlyStone", otherworldlyDrop: true);
                itemLoot.Add(rule.OnSuccess(ItemDropRule.OneFromOptionsNotScalingWithLuckWithX(10, 3, ModContent.ItemType<StormMedal>(), ModContent.ItemType<ArmoredScrap>())));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<StormBandana>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<AncientAuricTeslaHelm>(), 1000, chanceNumerator: 7)));
			}
			else if (item.type == CalItemID.CeaselessVoidBag)
            {
                AddBlockDrop(ref itemLoot, "OtherworldlyStone", otherworldlyDrop: true);

                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<MirrorMatter>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<VoidWings>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<OldVoidWings>(), 20)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<AncientAuricTeslaHelm>(), 1000, chanceNumerator: 7)));
			}
			else if (item.type == CalItemID.SignusBag)
            {
                AddBlockDrop(ref itemLoot, "OtherworldlyStone", otherworldlyDrop: true);
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ShadowCloth>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<SignusEmblem>(), ModContent.ItemType<SignusNether>(), ModContent.ItemType<SignusBalloon>(), ModContent.ItemType<Items.Equips.Capes.SigCape>())));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<AncientAuricTeslaHelm>(), 1000, chanceNumerator: 7)));
			}
			else if (item.type == CalItemID.PolterghastBag)
            {
                AddBlockDrop(ref itemLoot, "StratusBricks");
                itemLoot.Add(rule2.OnSuccess(new CommonDrop(ModContent.ItemType<PhantowaxBlock>(), 2, 205, 335)));

                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Polterhook>(), 10)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ZygoteinaBucket>(), 10, chanceNumerator: 3)));
            }
			else if (item.type == CalItemID.OldDukeBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<OldWings>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<CorrodedCleaver>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<CharredChopper>(), 100, chanceNumerator: 7)));
			}
			else if (item.type == CalItemID.DevourerofGodsBag)
			{
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<CosmicWormScarf>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CosmicRapture>(), 5)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<RapturedWormScarf>(), 100, chanceNumerator: 7)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 100)));
			}
			else if (item.type == CalItemID.YharonBag)
			{
				IItemDropRule demonshade = ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DemonshadeHood>(), 10);
				demonshade.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DemonshadeRobe>()));
				demonshade.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DemonshadePants>()));

				itemLoot.Add(rule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Termipebbles>(), 1, 5, 8)));
				itemLoot.Add(rule6.OnSuccess(new CommonDrop(ModContent.ItemType<YharonsAnklet>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<NuggetinaBiscuit>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<YharonShackle>(), 10, chanceNumerator: 3)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 20)));
				itemLoot.Add(rule.OnSuccess(demonshade));
			}
			else if (item.type == CalItemID.DraedonBox)
			{
				IItemDropRule draedon = ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DraedonBody>(), 7);
				draedon.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DraedonLegs>()));
				IItemDropRule balloons = ItemDropRule.NotScalingWithLuck(ModContent.ItemType<ApolloBalloonSmall>(), 2);
				balloons.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ArtemisBalloonSmall>()));
                AddBlockDrop(ref itemLoot, "ExoPlating");

                itemLoot.Add(rule7.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<XMLightningHook>(), 2)));
				itemLoot.Add(rule7.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Items.Pets.ExoMechs.GunmetalRemote>(), 2)));

				itemLoot.Add(rule8.OnSuccess(balloons));
				itemLoot.Add(rule8.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Items.Pets.ExoMechs.GeminiMarkImplants>(), 2)));

				itemLoot.Add(rule9.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Items.Equips.Shirts.AresChestplate.AresChestplate>(), 2)));
				itemLoot.Add(rule9.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<Items.Pets.ExoMechs.OminousCore>(), 2)));

				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 10)));
				itemLoot.Add(rule.OnSuccess(draedon));
			}
			else if (item.type == CalItemID.SupremeCalamitasCoffer)
            {
                AddBlockDrop(ref itemLoot, "OccultBrickItem");
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AncientAuricTeslaHelm>(), 10)));
				itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<GruelingMask>(), 3)));
			}

			if (ModLoader.HasMod("CatalystMod"))
			{
				if (item.type == ModLoader.GetMod("CatalystMod").Find<ModItem>("AstrageldonBag").Type)
					itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SpaceJunk>(), 10, chanceNumerator: 3)));
			}
			#endregion

			#region crates
            if (item.type == CalItemID.SulphurousCrate || item.type == CalItemID.HydrothermalCrate)
			{
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<AcidGun>(), 100)));
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<CursedLockpick>(), 50)));
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SulphurGeyser>(), 20, 2, 3)));
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SulphurousCactus>(), 20, 1, 3)));
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SulphurousPlanter>(), 25)));
                itemLoot.Add(rule10.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<InkyPollution>(), 50)));
                itemLoot.Add(rule11.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<EidolonTree>(), 40)));
                itemLoot.Add(rule11.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NuclearFumes>(), 10, 2, 11)));
                itemLoot.Add(rule12.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<BelchingCoral>(), 20)));
            }
            else if (item.type == CalItemID.AstralCrate || item.type == CalItemID.MonolithCrate)
            {
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<MonolithPot>(), 100, 1, 1, 3)));
                itemLoot.Add(rule11.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<NetherTree>(), 20)));
            }
            else if (item.type == CalItemID.SunkenCrate || item.type == CalItemID.PrismCrate)
            {
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SSCoral>(), 100, 1, 1, 3)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<Anemone>(), 100, 1, 1, 3)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<TableCoral>(), 100, 1, 1, 3)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<FanCoral>(), 100, 1, 1, 3)));
                itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<BrainCoral>(), 100, 1, 1, 3)));
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SeaCrown>(), 100)));
                itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<SunkenLamp>(), 40)));
            }
            #endregion

            # region fables
            if (CalValEX.FablesActive)
            {
                if (item.type == CalValEX.Fables.Find<ModItem>("DesertScourgeTreasureBag").Type)
                {
                    itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<DesertMedallion>(), 10, chanceNumerator: 3)));
                    itemLoot.Add(rule.OnSuccess(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<DriedLocket>(), 5)));
                    itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<SlightlyMoistbutalsoSlightlyDryLocket>(), 100, chanceNumerator: 7)));
                }
                else if (item.type == CalValEX.Fables.Find<ModItem>("CrabulonTreasureBag").Type)
                {
                    itemLoot.Add(rule2.OnSuccess(new CommonDrop(ModContent.ItemType<MushroomCap>(), 1, 205, 335)));
                    itemLoot.Add(rule.OnSuccess(new CommonDrop(ModContent.ItemType<ClawShroom>(), 10, chanceNumerator: 3)));
                }
            }
            #endregion

            #region spaghetti starter
            /*if (item.type != ModContent.ItemType<StarterBag>())
                return;

            LeadingConditionRule[] names = new LeadingConditionRule[]
            {
                new(new Combine(true, null, new rule, new PlayerNameRule("Jared"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("RamG", "Ramgear"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Bumbledoge", "BumbleDoge", "Bojangles", "Bojeangles"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("William"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Kiwabug"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("YuH", "Yuh", "yuh", "Lilsigtum", "GinYuH", "Lil Sigtum"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Hypera"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Drakudragonx"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Spider", "spider", "Spooktacular", "spooktacular"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Lucca"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Junko"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Lil Junko"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Cooper"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Tess"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Enreden"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Iban", "IbanPlay", "IBlockaroz"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Maple", "Maple", "Maple"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Emerald", "EmeraldXLapis"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Yharex87", "Yharex"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Scarfy", "ScarfyScout", "Krysmun", "DodoNation", "Dodo"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("caligulasAquarium", "caligulas"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Willow", "willowmaine", "bean long"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Potato Person"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Dorira", "Marco"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Hat Enthusiast"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Triangle"))),
                new(new Combine(true, null, new VanityDropsEnabled(), new PlayerNameRule("Brimmy"))),
            };

            IItemDropRule jaredPack = ItemDropRule.Common(ModContent.ItemType<CanofWyrms>());
            jaredPack.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SoulShard>()));

            IItemDropRule bumbledoge = ItemDropRule.Common(ModContent.ItemType<AerialiteBubble>());
            bumbledoge.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ExtraFluffyFeatherClump>()));

            IItemDropRule yuh = ItemDropRule.Common(ModContent.ItemType<FlareRune>());
            yuh.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SmolEldritchHoodie>()));

            IItemDropRule lucca = ItemDropRule.Common(ModContent.ItemType<SuspiciousLookingChineseCrown>());
            lucca.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ToyScythe>()));

            IItemDropRule junko = ItemDropRule.Common(ModContent.ItemType<SuspiciousLookingChineseCrown>());
            junko.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ToyScythe>()));
            junko.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ProfanedBalloon>()));

            IItemDropRule maple = ItemDropRule.Common(ModContent.ItemType<DeepseaLantern>());
            maple.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SwearingShroom>()));
            maple.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FleshThing>()));

            IItemDropRule yhar = ItemDropRule.Common(ModContent.ItemType<SpaceJunk>());
            yhar.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AstraEGGeldon>()));

            IItemDropRule willow = ItemDropRule.Common(ModContent.ItemType<OldMirage>());
            willow.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PerennialFlower>()));
            willow.OnSuccess(ItemDropRule.Common(ModContent.ItemType<VVanities>()));

            IItemDropRule dorira = ItemDropRule.Common(ModContent.ItemType<CharredChopper>());
            dorira.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RapturedWormScarf>()));

            IItemDropRule triangle = ItemDropRule.Common(ModContent.ItemType<BubbledFin>());
            triangle.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IonizedJellyCrystal>()));

            IItemDropRule brimmy = ItemDropRule.Common(ModContent.ItemType<BurningEye>());
            brimmy.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FoilSpoon>()));
            brimmy.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RareBrimtulip>()));

            names[0].OnSuccess(jaredPack);
            names[1].OnSuccess(ItemDropRule.Common(ModContent.ItemType<ToyScythe>()));
            names[2].OnSuccess(bumbledoge);
            names[3].OnSuccess(ItemDropRule.Common(ModContent.ItemType<RuinedBandage>()));
            names[4].OnSuccess(ItemDropRule.Common(ModContent.ItemType<UglyTentacle>()));
            names[5].OnSuccess(yuh);
            names[6].OnSuccess(ItemDropRule.Common(ModContent.ItemType<SunBun>()));
            names[7].OnSuccess(ItemDropRule.Common(ModContent.ItemType<BambooStick>()));
            names[8].OnSuccess(ItemDropRule.Common(ModContent.ItemType<IsopodItem>(), 1, 5, 5));
            names[9].OnSuccess(ItemDropRule.Common(ModContent.ItemType<CosmicRapture>()));
            names[10].OnSuccess(lucca);
            names[11].OnSuccess(junko);
            names[12].OnSuccess(ItemDropRule.Common(ModContent.ItemType<SuspiciousLookingChineseCrown>()));
            names[13].OnSuccess(ItemDropRule.Common(ModContent.ItemType<CooperShortsword>()));
            names[14].OnSuccess(ItemDropRule.Common(ModContent.ItemType<Geminga>()));
            names[15].OnSuccess(ItemDropRule.Common(ModContent.ItemType<CosmicBubble>()));
            names[16].OnSuccess(ItemDropRule.Common(ModContent.ItemType<ProtoRing>()));
            names[17].OnSuccess(maple);
            names[18].OnSuccess(ItemDropRule.Common(ModContent.ItemType<PurifiedFog>()));
            names[19].OnSuccess(yhar);
            names[20].OnSuccess(ItemDropRule.Common(ModContent.ItemType<ExtraFluffyFeather>()));
            names[21].OnSuccess(ItemDropRule.Common(ModContent.ItemType<BleuBlob>()));
            names[22].OnSuccess(willow);
            names[23].OnSuccess(ItemDropRule.Common(ModContent.ItemType<RottenKey>()));
            names[24].OnSuccess(dorira);
            names[25].OnSuccess(ItemDropRule.Common(ModContent.ItemType<InkyArtifact>()));
            names[26].OnSuccess(triangle);
            names[27].OnSuccess(brimmy);*/
            #endregion
        }

        public static void AddBlockDrop(ref ItemLoot itemloot, string type, bool silvaDrop = false, bool otherworldlyDrop = false)
        {
            if (!CalValEX.CalamityActive)
                return;
            LeadingConditionRule blockGeneral = new(new BlockDrops());
            if (otherworldlyDrop)
            {
                blockGeneral = new LeadingConditionRule(new OtherworldlyStoneDrop());
            }
            if (silvaDrop)
            {
                blockGeneral = new LeadingConditionRule(new SilvaCrystal());
            }
            itemloot.Add(blockGeneral.OnSuccess(ItemDropRule.Common(CalValEX.CalamityItem(type), 1, 205, 335)));
        }

		#region drop rules
        private class PlayerNameRule : IItemDropRuleCondition
        {
            public readonly string[] names;

            public PlayerNameRule(params string[] names) => this.names = names;

			public bool CanDrop(DropAttemptInfo info)
			{
                foreach (string s in names)
                    if (info.player.name == s)
                        return true;
				return false;
			}

			public bool CanShowItemDropInUI() => true;

            public string GetConditionDescription() => null;
        }
		private class Combine : IItemDropRuleCondition
        {
            private readonly bool andConditions;
            private readonly IItemDropRuleCondition[] conditions;
            private readonly string description;

            public Combine(bool andConditions = true, string? description = null,
                   params IItemDropRuleCondition[] dropRuleConditions)
            {
                this.andConditions = andConditions;
                this.description = description;
                conditions = dropRuleConditions;
            }

            public bool CanDrop(DropAttemptInfo info) => andConditions
                    ? conditions.All(val => val.CanDrop(info))
                    : conditions.Any(val => val.CanDrop(info));

			public bool CanShowItemDropInUI() => andConditions
					? conditions.All(val => val.CanShowItemDropInUI())
					: conditions.Any(val => val.CanShowItemDropInUI());

			public string GetConditionDescription() => description;
		}
        #endregion
        public override void RightClick(Item item, Player player)
        {
            if (!CalValEX.CalamityActive)
                return;

            if (!CalValEXConfig.Instance.DisableVanityDrops && item.type == CalItemID.StarterBag && player.whoAmI == Main.myPlayer && CalValEX.AprilFoolWeek&& !NPC.AnyNPCs(ModContent.NPCType<AprilFools.Jharim.Jharim>()))
			{
				NPC.NewNPC(player.GetSource_ReleaseEntity(), (int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<AprilFools.Jharim.Jharim>());
			}
		}

        public override void UpdateAccessory(Item item, Player player, bool hide)
        {
            CalValEXPlayer modplayer = player.GetModPlayer<CalValEXPlayer>();
            if (CalValEX.CalamityActive)
            {
                if (item.type == CalItemID.AquaticHeart && !hide)
                {
                    modplayer.SirenHeart = true;
                }
            }
        }

        public override void UpdateVanity(Item item, Player player)
        {
            CalValEXPlayer modplayer = player.GetModPlayer<CalValEXPlayer>();
            if (CalValEX.CalamityActive)
            {
                if (item.type == CalItemID.AquaticHeart)
                {
                    modplayer.SirenHeart = true;
                }
            }
        }
    }
}
