using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.AprilFools;
using CalValEX.Items;
using CalValEX.Items.Critters;
using CalValEX.Items.Dyes;
using CalValEX.Items.Equips.Backs;
using CalValEX.Items.Equips.Balloons;
using CalValEX.Items.Equips.Capes;
using CalValEX.Items.Equips.Hats;
using CalValEX.Items.Equips.Legs;
using CalValEX.Items.Equips.Scarves;
using CalValEX.Items.Equips.Shirts;
using CalValEX.Items.Equips.Transformations;
using CalValEX.Items.Equips.Wings;
using CalValEX.Items.LightPets;
using CalValEX.Items.Mounts;
using CalValEX.Items.Mounts.Ground;
using CalValEX.Items.Mounts.Morshu;
using CalValEX.Items.Mounts.LimitedFlight;
using CalValEX.Items.Pets;
using CalValEX.Items.Pets.Elementals;
using CalValEX.Items.Pets.ExoMechs;
using CalValEX.Items.Pets.Scuttlers;
using CalValEX.Items.Tiles.Blocks;
using CalValEX.Items.Tiles.Blocks.Astral;
using CalValEX.Items.Tiles;
using CalValEX.Items.Tiles.Blueprints;
using CalValEX.Items.Tiles.Cages;
using CalValEX.Items.Tiles.FurnitureSets.Arsenal;
using CalValEX.Items.Tiles.FurnitureSets.Astral;
using CalValEX.Items.Tiles.FurnitureSets.Auric;
using CalValEX.Items.Tiles.FurnitureSets.Bloodstone;
using CalValEX.Items.Tiles.FurnitureSets.Phantowax;
using CalValEX.Items.Tiles.Statues;
using CalValEX.Items.Walls;
/*using CalValEX.Items.PetComboItems;
using CalValEX.Items.PetComboItems.PreHardmode;
using CalValEX.Items.PetComboItems.Hardmode;
using CalValEX.Items.PetComboItems.PostMoonLord;*/
using CalValEX.Tiles.MiscFurniture;
using CalValEX.Tiles.FurnitureSets.Auric;
using CalValEX.Tiles.Plants;
using System.Collections.Generic;
using System.Linq;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Mounts.Minecart;
using CalValEX.Items.Plushies;

namespace CalValEX
{
    public class CalValEXRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            #region //April Fools
            {
                Recipe recipe = Recipe.Create(ItemType<JharimHead>());
                recipe.AddIngredient(ItemID.GoldOre);
                recipe.AddTile(TileID.WorkBenches);
                recipe.AddCondition(new Condition("During April Fools week or on the getfixedboi seed.", () => Main.zenithWorld || CalValEX.AprilFoolWeek));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<JharimHead>());
                recipe.AddIngredient(ItemID.PlatinumOre);
                recipe.AddTile(TileID.WorkBenches);
                recipe.AddCondition(new Condition("During April Fools week or on the getfixedboi seed.", () => Main.zenithWorld || CalValEX.AprilFoolWeek));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AprilFools.Meldosaurus.KnowledgeMeldosaurus>());
                recipe.AddIngredient(ItemType<AprilFools.Meldosaurus.MeldosaurusTrophy>());
                recipe.AddTile(TileID.Bookcases);
                recipe.AddCondition(new Condition("During April Fools month on the getfixedboi seed.", () => Main.zenithWorld || CalValEX.AprilFoolMonth));
                recipe.Register();
            }
            #endregion

            #region The few things that don't need Calamity
            {
                Recipe recipe = Recipe.Create(ItemType<OldCassette>());
                recipe.AddIngredient(ItemID.Diamond, 5);
                recipe.AddIngredient(ItemID.ShadowScale, 5);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldCassette>());
                recipe.AddIngredient(ItemID.Diamond, 5);
                recipe.AddIngredient(ItemID.TissueSample, 5);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BlightedAstralDye>());
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddIngredient(ItemType<XenoSolution>());
                recipe.AddTile(TileID.DyeVat);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BoB2>());
                recipe.AddIngredient(ItemType<ChaosBalloon>());
                recipe.AddIngredient(ItemType<Mirballoon>());
                recipe.AddIngredient(ItemType<BoxBalloon>());
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BelladonnaHat>());
                recipe.AddIngredient(ItemID.Vine, 3);
                recipe.AddIngredient(ItemID.JungleSpores, 8);
                recipe.AddIngredient(ItemID.RichMahogany, 5);
                recipe.AddIngredient(ItemID.Silk, 5);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BelladonnaCloak>());
                recipe.AddIngredient(ItemID.Vine, 6);
                recipe.AddIngredient(ItemID.JungleSpores, 15);
                recipe.AddIngredient(ItemID.RichMahogany, 5);
                recipe.AddIngredient(ItemID.Silk, 15);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<TheSeaKingsCoin>());
                recipe.AddIngredient(ItemType<DriedLocket>());
                recipe.AddIngredient(ItemType<MoistLocket>());
                recipe.AddIngredient(ItemType<NuclearFumes>(), 20);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<SunBun>());
                recipe.AddIngredient(ItemID.Bunny);
                recipe.AddIngredient(ItemID.FragmentSolar, 50);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<MiniatureElementalHeart>());
                recipe.AddIngredient(ItemType<RareBrimtulip>());
                recipe.AddIngredient(ItemType<StrangeMusicNote>());
                recipe.AddIngredient(ItemType<CloudCandy>());
                recipe.AddIngredient(ItemType<SmallSandPail>());
                recipe.AddIngredient(ItemType<SmallSandPlushie>());
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PolishedXenomonolith>());
                recipe.AddIngredient(ItemType<AstralTreeWood>());
                recipe.AddTile(TileID.Sawmill);
                recipe.Register();
            }
            #endregion

            #region Register Furniture
            AstralFurniture();
            AuricFurniture();
            BloodstoneFurniture();
            PhantowaxFurniture();
            #endregion

            #region //Cages
            CageRecipe(ItemType<AstJRItem>(), ItemID.Bottle, ItemType<AstJar>());
            CageRecipe(ItemType<BlightolemurItem>(), ItemType<Items.Tiles.Plants.AstralOldPurple>(), ItemType<BleamurPerch>());
            CageRecipe(ItemType<IsopodItem>(), ItemID.Terrarium, ItemType<IsopodTerrarium>(), ItemID.WaterBucket);
            CageRecipe(ItemType<BlinkerItem>(), ItemID.Bottle, ItemType<BlinkerInABottle>());
            CageRecipe(ItemType<CrystalFlyItem>(), ItemID.Bottle, ItemType<CrystalFlyJar>());
            CageRecipe(ItemType<EyedolItem>(), ItemID.Chain, ItemType<EyedolTerrarium>());
            CageRecipe(ItemType<GAstJRItem>(), ItemID.Bottle, ItemType<GAstJar>());
            CageRecipe(ItemType<GoldenIsopodItem>(), ItemID.Terrarium, ItemType<GoldenIsopodTerrarium>(), ItemID.WaterBucket);
            CageRecipe(ItemType<GoldEyedolItem>(), ItemID.Chain, ItemType<GoldEyedolTerrarium>());
            CageRecipe(ItemType<GoldViolemurItem>(), ItemType<Items.Tiles.Plants.MonolithPot>(), ItemType<GoldViolemurMonolith>());
            CageRecipe(ItemType<VaporoflyItem>(), ItemID.Bottle, ItemType<NukeJar>());
            CageRecipe(ItemType<OrthobabItem>(), ItemID.Bottle, ItemType<OrthoJar>());
            CageRecipe(ItemType<PlagueFrogItem>(), ItemID.Terrarium, ItemType<PlagueFrogTerrarium>());
            CageRecipe(ItemType<ProvFlyItem>(), ItemID.Bottle, ItemType<ProvFlyJar>());
            CageRecipe(ItemType<SandTurtleItem>(), ItemID.Terrarium, ItemType<SandTurtleCage>());
            CageRecipe(ItemType<GodSlayerSlugItem>(), ItemID.Terrarium, ItemType<SlugTerrarium>());
            CageRecipe(ItemType<SwearshroomItem>(), ItemID.Terrarium, ItemType<SwearshroomCage>());
            CageRecipe(ItemType<ViolemurItem>(), ItemType<Items.Tiles.Plants.MonolithPot>(), ItemType<ViolemurMonolith>());
            CageRecipe(ItemType<XerocodileItem>(), ItemID.Terrarium, ItemType<XerocodileCage>());
            #endregion

            #region //Statues
            StatueRecipe(ItemType<EyedolItem>(), ItemType<EyedolStatue>());
            StatueRecipe(ItemType<IsopodItem>(), ItemType<IsopodStatue>());
            StatueRecipe(ItemType<VaporoflyItem>(), ItemType<NukeFlyStatue>());
            StatueRecipe(ItemType<PlagueFrogItem>(), ItemType<PlagueFrogStatue>());
            StatueRecipe(ItemType<ViolemurItem>(), ItemType<ViolemurStatue>());

            #endregion
            #region //Wall Block conversion
            WallRecipe(ItemType<AuricBrick>(), ItemType<AuricBrickWall>());
            WallRecipe(ItemType<BlightedEggBlock>(), ItemType<BlightedEggWall>());
            WallRecipe(ItemType<AstralSandstone>(), ItemType<Items.Walls.Astral.AstralSandstoneWall>());
            WallRecipe(ItemType<AstralDirt>(), ItemType<Items.Walls.Astral.AstralDirtWall>());
            WallRecipe(ItemType<AstralHardenedSand>(), ItemType<Items.Walls.Astral.AstralHardenedSandWall>());
            WallRecipe(ItemType<AstralIce>(), ItemType<Items.Walls.Astral.AstralIceWall>());
            WallRecipe(ItemType<FrostflakeBrick>(), ItemType<FrostflakeWall>());
            WallRecipe(ItemType<AstralTreeWood>(), ItemType<Items.Walls.Astral.XenomonolithWall>());
            WallRecipe(ItemType<Xenostone>(), ItemType<Items.Walls.Astral.XenostoneWall>());
            WallRecipe(ItemType<AstralPlating>(), ItemType<AstralPlatingWall>());
            WallRecipe(ItemType<AstralPearlBlock>(), ItemType<AstralPearlWall>());
            WallRecipe(ItemType<ChiseledBloodstone>(), ItemType<ChiseledBloodstoneBrickWall>());
            WallRecipe(ItemType<PhantowaxBlock>(), ItemType<PhantowaxWall>());
            WallRecipe(ItemType<EidolicSlab>(), ItemType<EidolicSlabWall>());
            WallRecipe(ItemType<ShadowBrick>(), ItemType<ShadowBrickWall>());
            WallRecipe(ItemType<PolishedAstralMonolith>(), ItemType<PolishedAstralMonolithWall>());
            WallRecipe(ItemType<PolishedXenomonolith>(), ItemType<PolishedXenomonolithWall>());
            WallRecipe(ItemType<Necrostone>(), ItemType<NecrostoneWall>());
            WallRecipe(ItemType<WulfrumPlating>(), ItemType<WulfrumPanelWall>());
            WallRecipe(ItemType<HallowedBrick>(), ItemType<HallowedBrickWall>());
            WallRecipe(ItemType<Aeroplate>(), ItemType<AeroplateWall>());
            #endregion

            AstralRecipe(ItemType<PolishedXenomonolith>(), ItemType<PolishedAstralMonolith>());
            AstralRecipe(ItemType<PolishedXenomonolithWall>(), ItemType<PolishedAstralMonolithWall>());
            AstralRecipe(ItemType<BlightolemurItem>(), ItemType<ViolemurItem>());
            AstralRecipe(ItemType<BleamurPerch>(), ItemType<ViolemurMonolith>());
            AstralRecipe(ItemType<BlightedEggBlock>(), ItemType<AstralPearlBlock>());
            AstralRecipe(ItemType<BlightedEggWall>(), ItemType<AstralPearlWall>());

            if (!CalValEX.CalamityActive)
                return;
            #region //Misc
            {
                Recipe recipe = Recipe.Create(PlushManager.PlushItems["Polyphemalus"]);
                recipe.AddIngredient(PlushManager.PlushItems["Exotrexia"]);
                recipe.AddIngredient(PlushManager.PlushItems["Cataractacomb"]);
                recipe.AddIngredient(PlushManager.PlushItems["Astigmageddon"]);
                recipe.AddIngredient(PlushManager.PlushItems["Conjunctivirus"]);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BottledSulphurousWater>());
                recipe.AddIngredient(ItemID.Bottle);
                recipe.AddCondition(Condition.NearWater);
                recipe.AddCondition(CalValEXConditions.sulph);
                recipe.Register();
            }
            #endregion
            #region //Critters
            {
                Recipe recipe = Recipe.Create(ItemType<XerocodileItem>());
                recipe.AddIngredient(CalValEX.CalamityItem("Gorecodile"), 10);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            #endregion

            #region //Dyes
            {
                Recipe recipe = Recipe.Create(ItemType<DraedonHologramDye>());
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddIngredient(CalValEX.CalamityItem("DraedonPowerCell"));
                recipe.AddTile(TileID.DyeVat);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<SulphuricDye>());
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddIngredient(CalValEX.CalamityItem("SulfuricScale"));
                recipe.AddTile(TileID.DyeVat);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BlightedAstralPinkDye>());
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddIngredient(ItemType<AstralTreeWood>());
                recipe.AddTile(TileID.DyeVat);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BlightedAstralYellowDye>());
                recipe.AddIngredient(ItemID.BottledWater);
                recipe.AddIngredient(ItemType<AstralTreeWood>());
                recipe.AddTile(TileID.DyeVat);
                recipe.Register();
            }
            #endregion

            #region //Backpacks
            {
                Recipe recipe = Recipe.Create(ItemType<BackpackServer>());
                recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 4);
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 4);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            #endregion

            #region //Balloons
            {
                Recipe recipe = Recipe.Create(ItemType<ApolloBalloon>());
                recipe.AddIngredient(ItemType<ApolloBalloonSmall>());
                recipe.AddIngredient(CalValEX.CalamityItem("ExoPrism"), 8);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ArtemisBalloon>());
                recipe.AddIngredient(ItemType<ArtemisBalloonSmall>());
                recipe.AddIngredient(CalValEX.CalamityItem("ExoPrism"), 8);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ExoTwinsBalloon>());
                recipe.AddIngredient(ItemType<ApolloBalloon>());
                recipe.AddIngredient(ItemType<ArtemisBalloon>());
                recipe.AddIngredient(CalValEX.CalamityItem("MiracleMatter"), 1);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricBalloon>());
                recipe.AddIngredient(ItemID.ShinyRedBalloon);
                recipe.AddIngredient(CalValEX.CalamityItem("AuricBar"), 10);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            #endregion

            #region //Capes
            {
                Recipe recipe = Recipe.Create(ItemType<YharimCape>());
                recipe.AddIngredient(ItemID.MysteriousCape);
                recipe.AddIngredient(ItemID.CrimsonCloak);
                recipe.AddIngredient(ItemID.WinterCape);
                recipe.AddIngredient(ItemID.RedCape);
                recipe.AddIngredient(CalValEX.CalamityItem("AuricBar"), 10);
                recipe.AddTile(TileID.Loom);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<YharimCapeBaby>());
                recipe.AddIngredient(ItemID.HunterCloak);
                recipe.AddIngredient(ItemID.SuperHeroCostume);
                recipe.AddIngredient(ItemID.WinterCape);
                recipe.AddIngredient(ItemID.ClothierJacket);
                recipe.AddIngredient(CalValEX.CalamityItem("AuricBar"), 8);
                recipe.AddTile(TileID.Loom);
                recipe.Register();
            }
            #endregion

            #region //Hats
            {
                Recipe recipe = Recipe.Create(ItemType<Aestheticrown>());
                recipe.AddIngredient(CalValEX.CalamityItem("AerialiteBar"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("SeaPrism"), 4);
                recipe.AddIngredient((ItemID.FallenStar), 2);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodyMaryHat>());
                recipe.AddIngredient(CalValEX.CalamityItem("BloodstoneCore"), 3);
                recipe.AddIngredient((ItemID.TheBrideHat), 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<CosmicCone>());
                recipe.AddIngredient((ItemID.EnchantedSword), 1);
                recipe.AddIngredient(ItemID.MeteoriteBar, 10);
                recipe.AddIngredient((ItemID.Granite), 50);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<FallenPaladinsHelmet>());
                recipe.AddIngredient(CalValEX.CalamityItem("AshesofCalamity"));
                recipe.AddIngredient(CalValEX.CalamityItem("ScoriaBar"));
                recipe.AddIngredient(CalValEX.CalamityItem("CoreofHavoc"));
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<TerminalMask>());
                recipe.AddIngredient(ItemType<Termipebbles>(), 5);
                recipe.AddIngredient(CalValEX.CalamityItem("Rock"));
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<TrueCosmicCone>());
                recipe.AddIngredient(ItemType<CosmicCone>());
                recipe.AddIngredient(ItemType<StonePile>());
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteBar"));
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<SpectralstormHat>());
                recipe.AddIngredient(ItemID.FragmentSolar, 5);
                recipe.AddIngredient(ItemID.Ectoplasm, 8);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            /*{
				Recipe recipe = Recipe.Create(ItemType<Items.Equips.Hats.Draedon.DraedonHelmet>());
				recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 6);
				recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 6);
				recipe.AddTile(TileID.Anvils);
				recipe.Register();
			}*/
            #endregion

            #region //Legs
            /*{
				Recipe recipe = Recipe.Create(ItemType<Items.Equips.Legs.Draedon.DraedonLeggings>());
				recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 8);
				recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 8);
				recipe.AddTile(TileID.Anvils);
				recipe.Register();
			}*/
            {
                Recipe recipe = Recipe.Create(ItemType<FallenPaladinsGreaves>());
                recipe.AddIngredient(CalValEX.CalamityItem("AshesofCalamity"));
                recipe.AddIngredient(CalValEX.CalamityItem("ScoriaBar"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("CoreofHavoc"));
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            #endregion

            #region //Scarves
            {
                Recipe recipe = Recipe.Create(ItemType<UniversalWormScarf>());
                recipe.AddIngredient(ItemType<RapturedWormScarf>());
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteBar"), 20);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            #endregion
            #region //Shirts
            {
                Recipe recipe = Recipe.Create(ItemType<BloodyMaryDress>());
                recipe.AddIngredient(CalValEX.CalamityItem("BloodstoneCore"), 8);
                recipe.AddIngredient((ItemID.TheBrideDress), 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<FallenPaladinsPlateMail>());
                recipe.AddIngredient(CalValEX.CalamityItem("AshesofCalamity"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("ScoriaBar"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("CoreofHavoc"));
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            /*{
				Recipe recipe = Recipe.Create(ItemType<Items.Equips.Shirts.Draedon.DraedonChestplate>());
				recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 12);
				recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 12);
				recipe.AddTile(TileID.Anvils);
				recipe.Register();
			}*/
            #endregion

            #region //Transformations
            {
                Recipe recipe = Recipe.Create(ItemType<ProtoRing>());
                recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 12);
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 12);
                recipe.AddIngredient(ItemID.Wire, 200);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<Signus>());
                recipe.AddIngredient(CalValEX.CalamityItem("SignusMask"));
                recipe.AddIngredient(ItemType<SigCape>());
                recipe.AddIngredient(ItemType<SignusEmblem>());
                recipe.AddIngredient(ItemType<SignusNether>());
                recipe.AddIngredient(CalValEX.CalamityItem("TwistingNether"), 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BurningEye>());
                recipe.AddIngredient(CalValEX.CalamityItem("BrimstoneWaifuMask"));
                recipe.AddIngredient(ItemType<BrimmyBody>());
                recipe.AddIngredient(ItemType<BrimmySpirit>());
                recipe.AddIngredient(CalValEX.CalamityItem("UnholyCore"), 5);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            #endregion

            #region //Wings
            {
                Recipe recipe = Recipe.Create(ItemType<TerminalWings>());
                recipe.AddIngredient(ItemType<Termipebbles>(), 5);
                recipe.AddIngredient(CalValEX.CalamityItem("Rock"));
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<GodspeedBoosters>());
                recipe.AddIngredient(ItemType<PlaguePack>());
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteBar"), 5);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<WulfrumHelipack>());
                recipe.AddIngredient(ItemID.LuckyHorseshoe);
                recipe.AddIngredient(CalValEX.CalamityItem("EnergyCore"), 3);
                recipe.AddIngredient(CalValEX.CalamityItem("WulfrumMetalScrap"), 30);
                recipe.AddRecipeGroup(RecipeGroupID.IronBar, 12);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            #endregion

            #region //Light pets
            {
                Recipe recipe = Recipe.Create(ItemType<DarksunSigil>());
                recipe.AddIngredient(ItemType<CoreofVanity>());
                recipe.AddIngredient(CalValEX.CalamityItem("DarksunFragment"), 20);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<DivineFly>());
                recipe.AddIngredient(ItemType<FROM>());
                recipe.AddIngredient(ItemType<VaporoflyItem>());
                recipe.AddIngredient(ItemType<NuclearFumes>(), 10);
                recipe.AddTile(TileID.LunarCraftingStation);
                ArsenalTG(recipe);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantominaJar>());
                recipe.AddIngredient(ItemID.WispinaBottle);
                recipe.AddIngredient(CalValEX.CalamityItem("RuinousSoul"), 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<SuperstitiousJewel>());
                recipe.AddIngredient(ItemType<DarksunSigil>());
                recipe.AddIngredient(ItemType<HolyTorch>());
                recipe.AddIngredient(ItemType<PhantominaJar>());
                recipe.AddIngredient(ItemType<CoolShades>());
                recipe.AddIngredient(ItemType<ShuttleBalloon>());
                recipe.AddIngredient(CalValEX.CalamityItem("AscendantSpiritEssence"), 3);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<CoreofVanity>());
                recipe.AddIngredient(ItemType<EssenceofDisorder>());
                recipe.AddIngredient(ItemType<EssenceofYeet>());
                recipe.AddIngredient(CalValEX.CalamityItem("CoreofCalamity"), 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            #endregion

            #region //Mounts
            {
                Recipe recipe = Recipe.Create(ItemType<AuricCarKey>());
                recipe.AddIngredient(ItemType<SilvaJeepItem>());
                recipe.AddIngredient(ItemType<GodRiderItem>());
                recipe.AddIngredient(ItemType<BloodstoneCarriageItem>());
                recipe.AddIngredient(ItemType<ProfanedCycleItem>());
                recipe.AddIngredient(CalValEX.CalamityItem("AuricBar"), 5);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneCarriageItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 50);
                recipe.AddIngredient(CalValEX.CalamityItem("BloodstoneCore"), 10);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<GodRiderItem>());
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteBar"), 10);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ProfanedCycleItem>());
                recipe.AddIngredient(ItemType<ProfanedFrame>());
                recipe.AddIngredient(ItemType<ProfanedBattery>());
                recipe.AddIngredient(ItemType<ProfanedWheels>());
                recipe.AddIngredient(CalValEX.CalamityItem("DivineGeode"), 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<SilvaJeepItem>());
                recipe.AddIngredient(CalValEX.CalamityItem("EffulgentFeather"), 20);
                recipe.AddIngredient(CalValEX.CalamityItem("PlantyMush"), 10);
                recipe.AddIngredient(ItemID.GoldBar, 10);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<SilvaJeepItem>());
                recipe.AddIngredient(CalValEX.CalamityItem("EffulgentFeather"), 20);
                recipe.AddIngredient(CalValEX.CalamityItem("PlantyMush"), 10);
                recipe.AddIngredient(ItemID.PlatinumBar, 10);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ShadowShedding>());
                recipe.AddIngredient(CalValEX.CalamityItem("CorruptionEffigy"), 1);
                recipe.AddIngredient(CalValEX.CalamityItem("RottenMatter"), 20);
                recipe.AddIngredient(ItemID.SoulofFlight, 20);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PopUpShop>());
                recipe.AddIngredient(CalValEX.CalamityItem("ThiefsDime"));
                recipe.AddIngredient(ItemID.LargeRuby);
                recipe.AddIngredient(ItemID.RopeCoil, 5);
                recipe.AddIngredient(ItemID.Bomb, 10);
                recipe.AddIngredient(ItemID.Gel, 20);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<DraedonCart>());
                recipe.AddIngredient(ItemID.MinecartMech);
                recipe.AddIngredient(CalValEX.CalamityItem("ExoPrism"), 20);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<YhogoStick>());
                recipe.AddIngredient(ItemID.PogoStick);
                recipe.AddIngredient(CalValEX.CalamityItem("AuricBar"), 20);
                recipe.AddIngredient(CalValEX.CalamityItem("ExoPrism"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("AshesofAnnihilation"), 2);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            #endregion

            #region //Pets
            {
                Recipe recipe = Recipe.Create(ItemType<AuricBottle>());
                recipe.AddIngredient(ItemType<InkyArtifact>());
                recipe.AddIngredient(CalValEX.CalamityItem("Exoblade"));
                recipe.AddIngredient(CalValEX.CalamityItem("SubsumingVortex"));
                recipe.AddIngredient(CalValEX.CalamityItem("VividClarity"));
                recipe.AddIngredient(CalValEX.CalamityItem("HeavenlyGale"));
                recipe.AddIngredient(CalValEX.CalamityItem("CosmicImmaterializer"));
                recipe.AddIngredient(CalValEX.CalamityItem("Photoviscerator"));
                recipe.AddIngredient(CalValEX.CalamityItem("Celestus"));
                recipe.AddIngredient(CalValEX.CalamityItem("MagnomalyCannon"));
                recipe.AddIngredient(CalValEX.CalamityItem("Supernova"));
                recipe.AddIngredient(CalValEX.CalamityItem("AuricBar"), 50);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<InkyArtifact>());
                recipe.AddIngredient(ItemType<InkyPollution>());
                recipe.AddIngredient(CalValEX.CalamityItem("SoulEdge"));
                recipe.AddIngredient(CalValEX.CalamityItem("EidolicWail"));
                recipe.AddIngredient(CalValEX.CalamityItem("CalamarisLament"));
                recipe.AddIngredient(CalValEX.CalamityItem("Valediction"));
                recipe.AddIngredient(CalValEX.CalamityItem("Lumenyl"), 3996);
                recipe.AddIngredient(CalValEX.CalamityItem("DepthCells"), 3996);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<RottingCalamitousArtifact>());
                recipe.AddIngredient(CalValEX.CalamityItem("AshesofCalamity"), 15);
                recipe.AddIngredient(CalValEX.CalamityItem("Cinderplate"), 20);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<DustyBadge>());
                recipe.AddIngredient(CalValEX.CalamityItem("GrandScale"), 1);
                recipe.AddIngredient(ItemID.Bass, 1);
                recipe.AddTile(TileType<BelchingCoralPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BleuBlob>());
                recipe.AddIngredient(ItemType<ShadowCloth>());
                recipe.AddIngredient(ItemType<SlimeDeitysSoul>());
                recipe.AddIngredient(CalValEX.CalamityItem("TwistingNether"), 20);
                recipe.AddIngredient(CalValEX.CalamityItem("PurifiedGel"), 20);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ExtraFluffyFeatherClump>());
                recipe.AddIngredient(ItemType<ExtraFluffyFeather>());
                recipe.AddIngredient(CalValEX.CalamityItem("HolyCollider"));
                recipe.AddIngredient(CalValEX.CalamityItem("BansheeHook"));
                recipe.AddIngredient(CalValEX.CalamityItem("CosmicDischarge"));
                recipe.AddIngredient(CalValEX.CalamityItem("SoulEdge"));
                recipe.AddIngredient(CalValEX.CalamityItem("StreamGouge"));
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<FROM>());
                recipe.AddIngredient(ItemType<BubbleGum>());
                recipe.AddIngredient(ItemType<PlagueFrogItem>());
                recipe.AddIngredient(CalValEX.CalamityItem("InfectedArmorPlating"), 5);
                recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 20);
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 20);
                recipe.AddTile(TileID.MythrilAnvil);
                //recipe.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(3, out Predicate<Recipe> condition), condition);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<HeartoftheSharks>());
                recipe.AddIngredient(ItemType<BubbledFin>());
                recipe.AddIngredient(ItemType<ReaperoidPills>());
                recipe.AddIngredient(ItemType<DustyBadge>());
                recipe.AddIngredient(CalValEX.CalamityItem("GrandScale"), 1);
                recipe.AddIngredient(ItemType<NuclearFumes>(), 15);
                recipe.AddIngredient(CalValEX.CalamityItem("ReaperTooth"), 15);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<RepurposedMonitor>());
                recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 10);
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 10);
                recipe.AddRecipeGroup("AnyPlate", 10);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<IonizedJellyCrystal>());
                recipe.AddIngredient(ItemType<SlimeDeitysSoul>());
                recipe.AddIngredient(CalValEX.CalamityItem("BlightedGel"), 20);
                recipe.AddIngredient(CalValEX.CalamityItem("PurifiedGel"), 20);
                recipe.AddIngredient(ItemID.Gel, 30);
                recipe.AddTile(CalValEX.CalamityTile("StaticRefiner"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<Sirember>());
                recipe.AddIngredient(ItemType<CorrodedCleaver>());
                recipe.AddIngredient(CalValEX.CalamityItem("AnahitaTrophy"));
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteBar"), 20);
                recipe.AddIngredient(ItemType<NuclearFumes>(), 20);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<Finality>());
                recipe.AddIngredient(ItemType<Termipebbles>(), 5);
                recipe.AddIngredient(CalValEX.CalamityItem("Rock"));
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<GodSlayerDoll>());
                recipe.AddIngredient(ItemType<ArmoredScrap>());
                recipe.AddIngredient(ItemType<MirrorMatter>());
                recipe.AddIngredient(ItemType<ShadowCloth>());
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteBar"), 15);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AstraEGGeldon>());
                recipe.AddIngredient(ItemType<Termipebbles>(), 999);
                recipe.AddIngredient(CalValEX.CalamityItem("AstralSlimeBanner"), 30);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AstraEGGeldon>());
                recipe.AddIngredient(CalValEX.CalamityItem("Rock"));
                recipe.AddIngredient(CalValEX.CalamityItem("AstralSlimeBanner"), 30);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BejeweledSpike>());
                recipe.AddIngredient(ItemType<AmberGeode>());
                recipe.AddIngredient(ItemType<AmethystGeode>());
                recipe.AddIngredient(ItemType<CrystalGeode>());
                recipe.AddIngredient(ItemType<DiamondGeode>());
                recipe.AddIngredient(ItemType<EmeraldGeode>());
                recipe.AddIngredient(ItemType<RubyGeode>());
                recipe.AddIngredient(ItemType<SapphireGeode>());
                recipe.AddIngredient(ItemType<TopazGeode>());
                recipe.AddIngredient(CalValEX.CalamityItem("ScuttlersJewel"));
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ExoGemstone>());
                recipe.AddIngredient(ItemType<OminousCore>());
                recipe.AddIngredient(ItemType<GunmetalRemote>());
                recipe.AddIngredient(ItemType<GeminiMarkImplants>());
                recipe.AddIngredient(CalValEX.CalamityItem("MiracleMatter"));
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            #endregion

            #region //Pandemonium Box (LESSSS GOOOOOOO
            /*#region //Prehardmode
			{
				Recipe recipe = Recipe.Create(ItemType<AlarmClock>());
				recipe.AddIngredient(ItemType<Items.Pets.WulfrumController>());
				recipe.AddIngredient(ItemType<WulfrumTransmitter>());
				recipe.AddIngredient(ItemType<RepurposedMonitor>());
				recipe.AddTile(TileType<CalamityMod.Tiles.DraedonStructures.LaboratoryConsole>());
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<NurseryBell>());
				recipe.AddIngredient(ItemType<RottenHotdog>());
				recipe.AddIngredient(ItemType<DecayingFishtail>());
				recipe.AddIngredient(ItemType<TundraBall>());
				recipe.AddIngredient(ItemType<BambooStick>());
				recipe.AddIngredient(ItemType<DoggoCollar>());
				recipe.AddIngredient(ItemType<SunDriedShrimp>());
				recipe.AddTile(TileID.WorkBenches);
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<DustChime>());
				recipe.AddIngredient(ItemType<AerialiteBubble>());
				recipe.AddIngredient(ItemType<CursedLockpick>());
				recipe.AddIngredient(ItemType<UglyTentacle>());
				recipe.AddIngredient(ItemType<Cube>());
				recipe.AddIngredient(ItemType<RuinedBandage>());
				recipe.AddTile(TileID.Anvils);
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<BestInstrument>());
				recipe.AddIngredient(ItemType<ClamHermitMedallion>());
				recipe.AddIngredient(ItemType<ClawShroom>());
				recipe.AddIngredient(ItemType<MeatyWormTumor>());
				recipe.AddIngredient(ItemType<RottenKey>());
				recipe.AddIngredient(ItemType<DespairMask>());
				recipe.AddTile(TileID.TinkerersWorkbench);
				recipe.Register();
			}
		        #endregion

			#region //Hardmode
			{
				Recipe recipe = Recipe.Create(ItemType<HarbingerOfWork>());
				recipe.AddIngredient(ItemType<RoverSpindle>());
				recipe.AddIngredient(ItemType<ConstructionRemote>());
				recipe.AddIngredient(ItemType<SuspiciousLookingGBC>());
				recipe.AddIngredient(ItemType<AstralInfectedIcosahedron>());
				recipe.AddIngredient(ItemType<PlaguebringerPowerCell>());
				recipe.AddIngredient(ItemType<AstralBinoculars>());
				recipe.AddTile(TileID.AdamantiteForge);
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<MaladyBells>());
				recipe.AddIngredient(ItemType<CooperShortsword>());
				recipe.AddIngredient(ItemType<HauntedPebble>());
				recipe.AddIngredient(ItemType<SmolEldritchHoodie>());
				recipe.AddIngredient(ItemType<DeepseaLantern>());
				recipe.AddIngredient(ItemType<SlightlyMoistbutalsoSlightlyDryLocket>());
				recipe.AddIngredient(ItemType<Eggstone>());
				recipe.AddIngredient(ItemType<AcidLamp>());
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<SpiritDinerBell>());
				recipe.AddIngredient(ItemType<SkullCluster>());
				recipe.AddIngredient(ItemType<SunBun>());
				recipe.AddIngredient(ItemType<MiniatureElementalHeart>());
				recipe.AddIngredient(ItemType<SpaceJunk>());
				recipe.AddTile(TileID.LunarCraftingStation);
				recipe.Register();
			}
			#endregion

			#region //PostMoonlord
			{
				Recipe recipe = Recipe.Create(ItemType<AltarBell>());
				recipe.AddIngredient(ItemType<TheDragonball>());
				recipe.AddIngredient(ItemType<DivineFly>());
				recipe.AddIngredient(ItemType<FlareRune>());
				recipe.AddIngredient(ItemType<AuricBottle>());
				recipe.AddIngredient(ItemType<Finality>());
				recipe.AddIngredient(ItemType<BejeweledSpike>());
				recipe.AddIngredient(ItemType<ProfanedHeart>());
				recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<TubRune>());
				recipe.AddIngredient(ItemType<AstraEGGeldon>());
				recipe.AddIngredient(ItemType<BleuBlob>());
				recipe.AddIngredient(ItemType<IonizedJellyCrystal>());
				recipe.AddIngredient(ItemType<ProfanedChewToy>());
				recipe.AddIngredient(ItemType<Sirember>());
				recipe.AddIngredient(ItemType<BejeweledSpike>());
				recipe.AddIngredient(ItemType<ProfanedHeart>());
				recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<VaselineBell>());
				recipe.AddIngredient(ItemType<HeartoftheSharks>());
				recipe.AddIngredient(ItemType<CharredChopper>());
				recipe.AddIngredient(ItemType<DocilePheromones>());
				recipe.AddIngredient(ItemType<ExtraFluffyFeatherClump>());
				recipe.AddIngredient(ItemType<ToyScythe>());
				recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<WormBell>());
				recipe.AddIngredient(ItemType<StormMedal>());
				recipe.AddIngredient(ItemType<TheSeaKingsCoin>());
				recipe.AddIngredient(ItemType<Geminga>());
				recipe.AddIngredient(ItemType<CosmicRapture>());
				recipe.AddIngredient(ItemType<SoulShard>());
				recipe.AddIngredient(ItemType<CalamitousSoulArtifact>());
				recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
				recipe.Register();
			}
			{
				Recipe recipe = Recipe.Create(ItemType<WormBell>());
				recipe.AddIngredient(ItemType<AlarmClock>());
				recipe.AddIngredient(ItemType<HarbingerOfWork>());
				recipe.AddIngredient(ItemType<ExoGemstone>());
				//recipe.AddIngredient(ItemType<DRAESDEVITEM!!! COMING SOON!!!>());
				recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
				recipe.Register();
			}
			#endregion

			//Pandemonium Box!
			{
				Recipe recipe = Recipe.Create(ItemType<PandemoniumBox>());
				recipe.AddIngredient(ItemType<NurseryBell>());
				recipe.AddIngredient(ItemType<DustChime>());
				recipe.AddIngredient(ItemType<BestInstrument>());
				recipe.AddIngredient(ItemType<MaladyBells>());
				recipe.AddIngredient(ItemType<SpiritDinerBell>());
				recipe.AddIngredient(ItemType<VaselineBell>());
				recipe.AddIngredient(ItemType<WormBell>());
				recipe.AddIngredient(ItemType<AltarBell>());
				recipe.AddIngredient(ItemType<ScratchedGong>());
				recipe.AddIngredient(ItemType<TubRune>());
				recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
				recipe.Register();
			}*/
            #endregion

            #region //Blocks
            {
                Recipe recipe = Recipe.Create(ItemType<AuricBrick>(), 50);
                recipe.AddIngredient(CalValEX.CalamityItem("AuricOre"));
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AstralBrick>(), 1);
                recipe.AddIngredient(CalValEX.CalamityItem("AstralStone"));
                recipe.AddIngredient(ItemID.StoneBlock);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneBrick>(), 200);
                recipe.AddIngredient(CalValEX.CalamityItem("BloodstoneCore"));
                recipe.AddIngredient(ItemID.StoneBlock, 200);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ChiseledBloodstone>(), 200);
                recipe.AddIngredient(CalValEX.CalamityItem("BloodstoneCore"));
                recipe.AddIngredient(ItemID.StoneBlock, 200);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<EidolicSlab>(), 200);
                recipe.AddIngredient(CalValEX.CalamityItem("SmoothVoidstone"), 200);
                recipe.AddIngredient(CalValEX.CalamityItem("ReaperTooth"));
                recipe.AddIngredient(CalValEX.CalamityItem("Lumenyl"), 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<Necrostone>(), 200);
                recipe.AddIngredient(CalValEX.CalamityItem("FleshyGeode"));
                recipe.AddIngredient(ItemID.StoneBlock, 200);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<Necrostone>(), 200);
                recipe.AddIngredient(CalValEX.CalamityItem("NecromanticGeode"));
                recipe.AddIngredient(ItemID.StoneBlock, 200);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<MeldBlock>(), 50);
                recipe.AddIngredient(CalValEX.CalamityItem("MeldBlob"));
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxBlock>(), 50);
                recipe.AddIngredient(ItemID.ClayBlock, 50);
                recipe.AddIngredient(CalValEX.CalamityItem("Phantoplasm"));
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PolishedAstralMonolith>());
                recipe.AddIngredient(CalValEX.CalamityItem("AstralMonolith"));
                recipe.AddTile(TileID.Sawmill);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ShadowBrick>(), 300);
                recipe.AddIngredient(CalValEX.CalamityItem("AshesofAnnihilation"));
                recipe.AddIngredient(CalValEX.CalamityItem("ExoPrism"));
                recipe.AddIngredient(ItemID.StoneBlock, 300);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ThanatosPlating>(), 250);
                recipe.AddIngredient(CalValEX.CalamityItem("ExoPrism"), 2);
                recipe.AddIngredient(ItemID.StoneBlock, 250);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ThanatosPlatingVent>(), 250);
                recipe.AddIngredient(CalValEX.CalamityItem("ExoPrism"), 2);
                recipe.AddIngredient(ItemID.StoneBlock, 250);
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<WulfrumPlating>(), 50);
                recipe.AddIngredient(CalValEX.CalamityItem("WulfrumMetalScrap"));
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<Booger>(), 50);
                recipe.AddIngredient(CalValEX.CalamityItem("GrandMarquisBait"));
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.Anvils);
                recipe.DisableDecraft();
                recipe.Register();
            }
            #endregion

            #region //Blueprints
            BlueprintRecipe(CalValEX.CalamityItem("AureusCell"), ItemType<AstrumAureusLog>());
            BlueprintRecipe(CalValEX.CalamityItem("EffulgentFeather"), ItemType<BumblebirbLog>());
            BlueprintRecipe(CalValEX.CalamityItem("AshesofCalamity"), ItemType<CalamitasLog>());
            BlueprintRecipe(ItemID.SoulofMight, ItemType<DestroyerLog>());
            BlueprintRecipe(CalValEX.CalamityItem("CosmiliteBar"), ItemType<DogLog>());
            BlueprintRecipe(ItemID.HellstoneBar, ItemType<MurasamaLog>());
            BlueprintRecipe(ItemType<Help>(), ItemType<OrthoceraLog>());
            BlueprintRecipe(CalValEX.CalamityItem("InfectedArmorPlating"), ItemType<PlagueLog>());
            BlueprintRecipe(ItemID.SoulofFright, ItemType<PrimeLog>());
            BlueprintRecipe(ItemID.SoulofSight, ItemType<TwinsLog>());
            BlueprintRecipe(CalValEX.CalamityItem("AuricBar"), ItemType<YharmorLog>());
            #endregion

            #region //Monoliths
            //MonolithRecipe(ItemType <CosmiliteBar>(), ItemType<DimensionalMonolith>(), TileID.LunarCraftingStation);
            //MonolithRecipe(ItemType <DivineGeode>(), ItemType<UnholyMonolith>(), TileID.LunarCraftingStation);
            //MonolithRecipe(ItemType <YharonSoulFragment>(), ItemType<InfernalMonolith>(), TileID.LunarCraftingStation);
            //MonolithRecipe(ItemType <InfectedArmorPlating>(), ItemType<PlagueMonolith>(), TileID.MythrilAnvil);
            //MonolithRecipe(ItemType <AshesofCalamity>(), ItemType<CalamitousMonolith>(), TileID.MythrilAnvil);
            //MonolithRecipe(ItemType<CryonicBar>(), ItemType<AuroraMonolith>(), TileID.MythrilAnvil);
            MonolithRecipe(ItemType<Termipebbles>(), ItemType<TerminusShrine>(), TileID.LunarCraftingStation);
            #endregion

            #region //Block to Block conversions
            WallRecipe(CalValEX.CalamityItem("Bloodstone"), ItemType<BloodstoneWall>());
            BlockToBlockRecipe(ItemType<ThanatosPlating>(), ItemType<ThanatosPlatingVent>(), CalValEX.CalamityTile("DraedonsForge"));
            #endregion

            #region //SSS conversions
            AstralRecipe(ItemType<AstralTreeWood>(), CalValEX.CalamityItem("AstralMonolith"));
            AstralRecipe(ItemType<AstralDirt>(), CalValEX.CalamityItem("AstralDirt"));
            AstralRecipe(ItemType<AstralClay>(), CalValEX.CalamityItem("AstralClay"));
            AstralRecipe(ItemType<AstralSand>(), CalValEX.CalamityItem("AstralSand"));
            AstralRecipe(ItemType<AstralSandstone>(), CalValEX.CalamityItem("AstralSandstone"));
            AstralRecipe(ItemType<AstralHardenedSand>(), CalValEX.CalamityItem("HardenedAstralSand"));
            AstralRecipe(ItemType<Xenostone>(), CalValEX.CalamityItem("AstralStone"));
            AstralRecipe(ItemType<AstralSnow>(), CalValEX.CalamityItem("AstralSnow"));
            AstralRecipe(ItemType<AstralIce>(), CalValEX.CalamityItem("AstralIce"));
            AstralRecipe(ItemType<Items.Walls.Astral.AstralDirtWall>(), CalValEX.CalamityItem("AstralDirtWall"));
            AstralRecipe(ItemType<Items.Walls.Astral.AstralHardenedSandWall>(), CalValEX.CalamityItem("HardenedAstralSandWall"));
            AstralRecipe(ItemType<Items.Walls.Astral.AstralIceWall>(), CalValEX.CalamityItem("AstralIceWall"));
            AstralRecipe(ItemType<Items.Walls.Astral.XenostoneWall>(), CalValEX.CalamityItem("AstralStoneWall"));
            AstralRecipe(ItemType<OldAstralBathtubItem>(), CalValEX.CalamityItem("MonolithBathtub"));
            AstralRecipe(ItemType<OldAstralBedItem>(), CalValEX.CalamityItem("MonolithBed"));
            AstralRecipe(ItemType<OldAstralBookshelfItem>(), CalValEX.CalamityItem("MonolithBookcase"));
            AstralRecipe(ItemType<OldAstralCandelabraItem>(), CalValEX.CalamityItem("MonolithCandelabra"));
            AstralRecipe(ItemType<OldAstralCandleItem>(), CalValEX.CalamityItem("MonolithCandle"));
            AstralRecipe(ItemType<OldAstralChandelierItem>(), CalValEX.CalamityItem("MonolithChandelier"));
            AstralRecipe(ItemType<OldAstralLanternItem>(), CalValEX.CalamityItem("MonolithLantern"));
            AstralRecipe(ItemType<OldAstralLampItem>(), CalValEX.CalamityItem("MonolithLamp"));
            AstralRecipe(ItemType<OldAstralDoorItem>(), CalValEX.CalamityItem("MonolithDoor"));
            AstralRecipe(ItemType<OldAstralDresserItem>(), CalValEX.CalamityItem("MonolithDresser"));
            AstralRecipe(ItemType<OldAstralSinkItem>(), CalValEX.CalamityItem("MonolithSink"));
            AstralRecipe(ItemType<OldAstralSofaItem>(), CalValEX.CalamityItem("MonolithBench"));
            AstralRecipe(ItemType<OldAstralChestItem>(), CalValEX.CalamityItem("MonolithChest"));
            AstralRecipe(ItemType<OldAstralClockItem>(), CalValEX.CalamityItem("MonolithClock"));
            AstralRecipe(ItemType<OldAstralWorkbenchItem>(), CalValEX.CalamityItem("MonolithWorkBench"));
            AstralRecipe(ItemType<OldAstralPianoItem>(), CalValEX.CalamityItem("MonolithPiano"));
            AstralRecipe(ItemType<BlinkerItem>(), CalValEX.CalamityItem("TwinklerItem"));
            AstralRecipe(ItemType<BlinkerInABottle>(), CalValEX.CalamityItem("TwinklerInABottle"));
            AstralRecipe(ItemType<XenoSolution>(), CalValEX.CalamityItem("AstralSolution"));
            #endregion

            #region //Misc furniture
            {
                Recipe recipe = Recipe.Create(ItemType<AuricTrashCan>());
                recipe.AddIngredient(ItemID.TrashCan);
                recipe.AddIngredient(CalValEX.CalamityItem("AuricBar"), 5);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AgedRustGamingTable>());
                recipe.AddIngredient(CalValEX.CalamityItem("RustedPlating"), 15);
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 3);
                recipe.AddIngredient(CalValEX.CalamityItem("DraedonPowerCell"), 6);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<HallowedBrick>(), 50);
                recipe.AddIngredient(CalValEX.CalamityItem("HallowedOre"));
                recipe.AddIngredient(ItemID.StoneBlock, 50);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<CosmiliteChairEX>());
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteChair"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("CosmiliteBar"), 4);
                recipe.AddTile(CalValEX.CalamityTile("CosmicAnvil"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<Hesfine>());
                recipe.AddIngredient(ItemType<Help>());
                recipe.AddIngredient(CalValEX.CalamityItem("OrthoceraBanner"), 2);
                recipe.AddIngredient(ItemType<NuclearFumes>(), 50);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<MoulderingAltarItem>());
                recipe.AddIngredient(ItemID.DemoniteBar, 20);
                recipe.AddIngredient(ItemID.SoulofNight, 15);
                recipe.AddIngredient(CalValEX.CalamityItem("RottenMatter"), 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<RustGamingTable>());
                recipe.AddIngredient(CalValEX.CalamityItem("LaboratoryPlating"), 15);
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 3);
                recipe.AddIngredient(CalValEX.CalamityItem("DraedonPowerCell"), 6);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<RustGamingTable2>());
                recipe.AddIngredient(CalValEX.CalamityItem("LaboratoryPlating"), 15);
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 2);
                recipe.AddIngredient(CalValEX.CalamityItem("DubiousPlating"), 3);
                recipe.AddIngredient(CalValEX.CalamityItem("DraedonPowerCell"), 6);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<SchematicDisplay>());
                recipe.AddIngredient(CalValEX.CalamityItem("MysteriousCircuitry"), 20);
                recipe.AddIngredient(ItemID.Wire, 20);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<TerminusShrine2>());
                recipe.AddIngredient(ItemType<TerminusShrine>());
                recipe.AddIngredient(CalValEX.CalamityItem("Terminus"));
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<TerminusShrine3>());
                recipe.AddIngredient(ItemType<TerminusShrine2>());
                recipe.AddIngredient(CalValEX.CalamityItem("Rock"));
                recipe.AddTile(CalValEX.CalamityTile("DraedonsForge"));
                recipe.AddCondition(new Condition("During April Fools week or on the getfixedboi seed.", () => Main.zenithWorld || CalValEX.AprilFoolWeek));
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<VisceralAltarItem>());
                recipe.AddIngredient(ItemID.CrimtaneBar, 20);
                recipe.AddIngredient(ItemID.SoulofNight, 15);
                recipe.AddIngredient(CalValEX.CalamityItem("BloodSample"), 6);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<FamiliarPot>());
                recipe.AddIngredient(ItemID.ClayBlock, 5);
                recipe.AddIngredient(ItemID.SandBlock, 5);
                recipe.AddTile(TileID.Furnaces);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<FamiliarUrnPot>());
                recipe.AddIngredient(ItemID.ClayBlock, 5);
                recipe.AddIngredient(ItemID.SandBlock, 5);
                recipe.AddTile(TileID.Furnaces);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<FamiliarSkinnyPot>());
                recipe.AddIngredient(ItemID.ClayBlock, 5);
                recipe.AddIngredient(ItemID.SandBlock, 5);
                recipe.AddTile(TileID.Furnaces);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<FamiliarTallPot>());
                recipe.AddIngredient(ItemID.ClayBlock, 5);
                recipe.AddIngredient(ItemID.SandBlock, 5);
                recipe.AddTile(TileID.Furnaces);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BunkerPlatform>(), 2);
                recipe.AddRecipeGroup(RecipeGroupID.Wood);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<WulfrumBeam>(), 2);
                recipe.AddIngredient(CalamityID.CalItemID.DullPlating);
                recipe.AddTile(CalamityID.CalTileID.WulfrumBunkerWorkshop);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<MoldyWood>(), 100);
                recipe.AddRecipeGroup(RecipeGroupID.Wood, 100);
                recipe.AddIngredient(ItemID.GlowingMushroom);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
            #endregion

            #region //Override Auric Toilet recipe
            // TODO: Make this not the worst fucking thing possible jesus christ
            {
                List<Recipe> rec = Main.recipe.ToList();
                rec.Where(x => x.createItem.type == CalValEX.CalamityItem("AuricToilet")).ToList().ForEach(s =>
                {
                    s.requiredItem = new List<Item>();
                    for (int i = 0; i < 5; i++)
                        s.requiredItem.Add(new Item());
                    s.requiredItem[0].SetDefaults(CalValEX.CalamityItem("BotanicChair"), false);
                    s.requiredItem[0].stack = 1;
                    s.requiredItem[1].SetDefaults(ItemType<BloodstoneChairItem>(), false);
                    s.requiredItem[1].stack = 1;
                    s.requiredItem[2].SetDefaults(CalValEX.CalamityItem("CosmiliteChair"), false);
                    s.requiredItem[2].stack = 1;
                    s.requiredItem[3].SetDefaults(CalValEX.CalamityItem("SilvaChair"), false);
                    s.requiredItem[3].stack = 1;
                    s.requiredItem[4].SetDefaults(ItemType<AuricBrick>(), false);
                    s.requiredItem[4].stack = 50;

                    s.requiredTile[0] = TileType<AuricManufacturerPlaced>();
                    s.createItem.SetDefaults(CalValEX.CalamityItem("AuricToilet"), false);
                    s.createItem.stack = 1;
                });
            }
            #endregion

            #region //Orthocera torture
            {
                Recipe recipe = Recipe.Create(CalValEX.CalamityItem("BloodOrb"), 10);
                recipe.AddIngredient(ItemType<Help>());
                recipe.AddTile(TileID.MeatGrinder);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(CalValEX.CalamityItem("HadalStew"));
                recipe.AddIngredient(ItemType<Help>());
                recipe.AddTile(TileID.CookingPots);
                recipe.Register();
            }
            #endregion
        }

        public override void PostAddRecipes()
        {
            ShimmerMeTimbers();
        }

        void ArsenalTG(Recipe recipe)
        {
            //recipe.AddCondition(CalamityMod.CustomRecipes.ArsenalTierGatedRecipe.ConstructRecipeCondition(2, out Predicate<Recipe> condition), condition);
        }

        void ShimmerMeTimbers()
        {
            List<(int, int)> blacklist = new();

            var ci = CalValEX.CalamityItem;
            blacklist.Add((ci("WulfrumPlating"), ci("WulfrumMetalScrap")));
            blacklist.Add((ci("UelibloomBrick"), ci("UelibloomOre")));
            blacklist.Add((ci("OccultBrickItem"), ci("AshesofAnnihilation")));
            blacklist.Add((ci("ExoPlating"), ci("ExoPrism")));
            blacklist.Add((ci("ExoPrismPanel"), ci("ExoPrism")));
            blacklist.Add((ItemType<Necrostone>(), ci("NecromanticGeode")));
            blacklist.Add((ItemType<Necrostone>(), ci("FleshyGeode")));
            blacklist.Add((ItemType<ChiseledBloodstone>(), ci("BloodstoneCore")));
            blacklist.Add((ItemType<EidolicSlab>(), ci("Lumenyl")));
            blacklist.Add((ci("StratusBricks"), ci("Lumenyl")));
            blacklist.Add((ci("CosmiliteBrick"), ci("CosmiliteBar")));
            blacklist.Add((ci("OtherworldlyStone"), ci("DarkPlasma")));
            blacklist.Add((ci("SilvaCrystal"), ci("EffulgentFeather")));
            blacklist.Add((ci("StatigelBlock"), ci("PurifiedGel")));
            blacklist.Add((ItemType<AuricBrick>(), ci("AuricOre")));
            blacklist.Add((ci("HazardChevronPanels"), ci("LaboratoryPanels")));
            blacklist.Add((ci("LaboratoryPipePlating"), ci("RustedPipes")));

            List<int> labList = new();

            labList.Add(ci("LaboratoryPanels"));
            labList.Add(ci("RustedPipes"));
            labList.Add(ci("LaboratoryPlating"));
            labList.Add(ci("RustedPlating"));

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe r = Main.recipe[i];
                for (int j = 0; j < blacklist.Count; j++)
                {
                    if (r.HasResult(blacklist[j].Item1) && r.HasIngredient(blacklist[j].Item2))
                    {
                        r.DisableDecraft();
                    }
                }
                for (int j = 0; j < labList.Count; j++)
                {
                    if (r.HasResult(labList[j]) && r.HasRecipeGroup(RecipeGroupID.IronBar))
                    {
                        r.DisableDecraft();
                    }
                }
            }
        }

        #region //Additional methods
        void PlushRecipe(int throwplush, int placeplush)
        {
            {
                Recipe recipe = Recipe.Create(throwplush);
                recipe.AddIngredient(placeplush);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(placeplush);
                recipe.AddIngredient(throwplush);
                recipe.Register();
            }
        }


        void BlueprintRecipe(int ingredient, int result)
        {
            {
                Recipe recipe = Recipe.Create(result);
                recipe.AddIngredient(ingredient);
                recipe.AddIngredient(CalValEX.CalamityItem("DraedonPowerCell"), 50);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }

        void CageRecipe(int critter, int container, int result, int extra = 0, int extramt = 1)
        {
            Recipe recipe = Recipe.Create(result);
            recipe.AddIngredient(critter);
            recipe.AddIngredient(container);
            if (extra != 0)
                recipe.AddIngredient(extra, extramt);
            recipe.Register();
        }

        void StatueRecipe(int critter, int result)
        {
            Recipe recipe = Recipe.Create(result);
            recipe.AddIngredient(critter, 5);
            recipe.AddIngredient(ItemID.StoneBlock, 50);
            recipe.Register();
        }

        void MonolithRecipe(int ingredient, int result, int station)
        {
            Recipe recipe = Recipe.Create(result);
            recipe.AddIngredient(ingredient, 15);
            recipe.AddTile(station);
            recipe.Register();
        }

        void WallRecipe(int block, int wall)
        {
            {
                Recipe recipe = Recipe.Create(wall, 4);
                recipe.AddIngredient(block);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(block);
                recipe.AddIngredient(wall, 4);
                recipe.AddTile(TileID.WorkBenches);
                recipe.Register();
            }
        }

        void BlockToBlockRecipe(int blockA, int blockB, int station)
        {
            {
                Recipe recipe = Recipe.Create(blockA);
                recipe.AddIngredient(blockB);
                recipe.AddTile(station);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(blockB);
                recipe.AddIngredient(blockA);
                recipe.AddTile(station);
                recipe.Register();
            }
        }

        void AstralRecipe(int blight, int normal)
        {
            {
                Recipe recipe = Recipe.Create(normal);
                recipe.AddIngredient(blight);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(blight);
                recipe.AddIngredient(normal);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
        }
        #endregion

        void AstralFurniture()
        {
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralBathtubItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 14);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralBedItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 15);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralSofaItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 5);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralBookshelfItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 20);
                recipe.AddIngredient(ItemID.Book, 10);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralCandelabraItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 5);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralCandleItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 4);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralChairItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 5);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralChandelierItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 4);
                recipe.AddIngredient(ItemID.Chain, 1);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralChestItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 8);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralClockItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 10);
                recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralDoorItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 6);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralDresserItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 16);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralLampItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 3);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralLanternItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 6);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralPianoItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 15);
                recipe.AddIngredient(ItemID.Bone, 4);
                recipe.AddIngredient(ItemID.Book, 1);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralSinkItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 6);
                recipe.AddIngredient(ItemID.EmptyBucket, 1);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralTableItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 8);
                recipe.AddTile(TileType<StarstruckSynthesizerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<OldAstralWorkbenchItem>());
                recipe.AddIngredient(ItemType<AstralTreeWood>(), 10);
                recipe.Register();
            }
        }

        void AuricFurniture()
        {
            {
                Recipe recipe = Recipe.Create(ItemType<AuricManufacturer>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 20);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricBathtubItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 14);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricBedItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 15);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricSofaItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 5);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricBookshelfItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 20);
                recipe.AddIngredient(ItemID.Book, 10);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricCandelabraItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 5);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricCandleItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 4);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricChairItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 5);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricChandelierItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 4);
                recipe.AddIngredient(ItemID.Chain, 1);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricChestItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 8);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricClockItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 10);
                recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricDoorItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 6);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricDresserItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 16);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricLampItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 3);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricLanternItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 6);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricPianoItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 15);
                recipe.AddIngredient(ItemID.Bone, 4);
                recipe.AddIngredient(ItemID.Book, 1);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricSinkItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 6);
                recipe.AddIngredient(ItemID.EmptyBucket, 1);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricTableItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 8);
                recipe.AddTile(TileType<AuricManufacturerPlaced>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricWorkbenchItem>());
                recipe.AddIngredient(ItemType<AuricBrick>(), 10);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricPlatformItem>(), 2);
                recipe.AddIngredient(ItemType<AuricBrick>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<AuricBrick>());
                recipe.AddIngredient(ItemType<AuricPlatformItem>(), 2);
                recipe.Register();
            }
        }
        void PhantowaxFurniture()
        {
            int plasm = CalValEX.CalamityActive ? CalValEX.CalamityItem("Phantoplasm") : ItemID.Ectoplasm;
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxBathtubItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 14);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxBedItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 15);
                recipe.AddIngredient(plasm, 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxSofaItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 5);
                recipe.AddIngredient(plasm, 2);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxBookshelfItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 20);
                recipe.AddIngredient(ItemID.Book, 10);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxCandelabraItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 5);
                recipe.AddIngredient(plasm, 3);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxCandleItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 4);
                recipe.AddIngredient(plasm, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxChairItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxChandelierItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 4);
                recipe.AddIngredient(plasm, 4);
                recipe.AddIngredient(ItemID.Chain, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxChestItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 8);
                recipe.AddIngredient(plasm, 2);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxClockItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 10);
                recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxDoorItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 6);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxDresserItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 16);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxLampItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 3);
                recipe.AddIngredient(plasm, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxLanternItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 6);
                recipe.AddIngredient(plasm, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxPianoItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 15);
                recipe.AddIngredient(ItemID.Bone, 4);
                recipe.AddIngredient(ItemID.Book, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxSinkItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 6);
                recipe.AddIngredient(plasm, 1);
                recipe.AddIngredient(ItemID.EmptyBucket, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxTableItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 8);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxPlatformItem>(), 2);
                recipe.AddIngredient(ItemType<PhantowaxBlock>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxWorkbenchItem>());
                recipe.AddIngredient(ItemType<PhantowaxBlock>(), 10);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<PhantowaxBlock>());
                recipe.AddIngredient(ItemType<PhantowaxPlatformItem>(), 2);
                recipe.Register();
            }
        }

        void BloodstoneFurniture()
        {
            int blorb = CalValEX.CalamityActive ? CalValEX.CalamityItem("BloodOrb") : ItemID.FragmentNebula;
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneBathtubItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 14);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneBedItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 15);
                recipe.AddIngredient(blorb, 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneSofaItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 5);
                recipe.AddIngredient(blorb, 2);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneBookshelfItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 20);
                recipe.AddIngredient(ItemID.Book, 10);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneCandelabraItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 5);
                recipe.AddIngredient(blorb, 3);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneCandleItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 4);
                recipe.AddIngredient(blorb, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneChairItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 5);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneChandelierItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 4);
                recipe.AddIngredient(blorb, 4);
                recipe.AddIngredient(ItemID.Chain, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneChestItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 8);
                recipe.AddIngredient(blorb, 2);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneClockItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 10);
                recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneDoorItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 6);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneDresserItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 16);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneLampItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 3);
                recipe.AddIngredient(blorb, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneLanternItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 6);
                recipe.AddIngredient(blorb, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstonePianoItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 15);
                recipe.AddIngredient(ItemID.Bone, 4);
                recipe.AddIngredient(ItemID.Book, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneSinkItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 6);
                recipe.AddIngredient(blorb, 1);
                recipe.AddIngredient(ItemID.EmptyBucket, 1);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneTableItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 8);
                recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstoneTableItem>());
                recipe.AddIngredient(ItemType<ChiseledBloodstone>(), 10);
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<BloodstonePlatformItem>(), 2);
                recipe.AddIngredient(ItemType<ChiseledBloodstone>());
                recipe.Register();
            }
            {
                Recipe recipe = Recipe.Create(ItemType<ChiseledBloodstone>());
                recipe.AddIngredient(ItemType<BloodstonePlatformItem>(), 2);
                recipe.Register();
            }
        }
    }
}
