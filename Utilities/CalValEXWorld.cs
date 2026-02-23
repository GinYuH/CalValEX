using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using CalValEX.Tiles.AstralBlocks;
using System.IO;
using System;
using CalValEX.NPCs.TownPets.Nuggets;
using Terraria.Chat;
using Terraria.Localization;
using Humanizer;
using CalValEX.Tiles.MiscFurniture;
using CalValEX.AprilFools.Fanny;
using CalValEX.Items.Tiles.Blocks;
using CalValEX.AprilFools;
using CalValEX.Tiles.Paintings;

namespace CalValEX
{
    public class CalValEXWorld : ModSystem {
        public static bool CanNugsSpawn() {
            return !(nugget || draco || folly || godnug || mammoth || shadow);
        }

        public static int astralTiles;
        public static int hellTiles;
        public static int jungleTiles;
        public static int labTiles;
        public static int dungeontiles;
        public static bool rescuedjelly;
        public static bool jharim;
        public static bool orthofound;
        public static bool amogus;
        public static bool OneMonolith;
        public static bool TwoMonolith;
        public static bool Rockshrine;
        public static bool RockshrinEX;
        public static bool jharinter;
        public static bool downedMeldosaurus;
        public static bool downedFogbound;
        public static bool masorev;
        public static int fannyOverride; // 0 = auto enabled. 1 = disabled. 2 = manually enabled
        public static int paintEnabled; // 0 = auto enabled. 1 = disabled. 2 = manually enabled
        public static int desertScourgeDeaths;

        // Chickens
        public static bool nugget;
        public static bool draco;
        public static bool folly;
        public static bool godnug;
        public static bool mammoth;
        public static bool shadow;
        private int nugCounter = 1;
        public static bool isThereAHouse;
        public static bool ninja;
        public static bool astro;
        public static bool tar;

        public override void Load()
        {
            Terraria.On_Main.UpdateTime_StartDay += ResetDSDeaths;
        }

        public static void ResetDSDeaths(On_Main.orig_UpdateTime_StartDay orig, ref bool stopEvents)
        {
            desertScourgeDeaths = 0;
            CalValEXWorld.UpdateWorldBool();
            orig(ref stopEvents);
        }

        public override void OnWorldLoad()
        { 
            rescuedjelly = false;
            jharim = false;
            amogus = false;
            orthofound = false;
            Rockshrine = false;
            RockshrinEX = false;
            jharinter = false;
            downedMeldosaurus = false;
            downedFogbound = false;

            nugget = draco = folly = godnug = mammoth = shadow = isThereAHouse = false;
            ninja = false;
            astro = false;
            tar = false;
            fannyOverride = 0;
            desertScourgeDeaths = 0;
            PolterCableTE.UpdateHooks();
        }

        public override void OnWorldUnload()
        {
            rescuedjelly = false;
            jharim = false;
            amogus = false;
            orthofound = false;
            Rockshrine = false;
            RockshrinEX = false;
            jharinter = false;
            downedMeldosaurus = false;
            downedFogbound = false;
            ninja = false;
            astro = false;
            tar = false;
            FannyManager.fannyEnabled = false;
            fannyOverride = 0;
            desertScourgeDeaths = 0;

            nugget = draco = folly = godnug = mammoth = shadow = isThereAHouse = false;
        }

        #region //Flags
        public override void SaveWorldData(TagCompound tag) {
            if (rescuedjelly)
                tag["rescuedjelly"] = true;

            if (jharim)
                tag["jharim"] = true;

            if (orthofound)
                tag["orthofound"] = true;

            if (amogus)
                tag["amogus"] = true;

            if (jharinter)
                tag["jharinter"] = true;

            if (downedMeldosaurus)
                tag["downedMeldosaurus"] = true;

            if (downedFogbound)
                tag["downedFogbound"] = true;

            tag["fanson"] = fannyOverride;

            tag["paintEnabled"] = paintEnabled;

            // Chickens
            if (nugget)
                tag["nugget"] = true;
            if (draco)
                tag["draco"] = true;
            if (folly)
                tag["folly"] = true;
            if (godnug)
                tag["godnug"] = true;
            if (mammoth)
                tag["mammoth"] = true;
            if (shadow)
                tag["shadow"] = true;
            if (ninja)
                tag["ninja"] = true;
            if (astro)
                tag["astro"] = true;
            if (tar)
                tag["tar"] = true;
        }

        public override void LoadWorldData(TagCompound tag) {
            rescuedjelly = tag.ContainsKey("rescuedjelly");
            jharim = tag.ContainsKey("jharim");
            orthofound = tag.ContainsKey("orthofound");
            amogus = tag.ContainsKey("amogus");
            jharinter = tag.ContainsKey("jharinter");
            downedMeldosaurus = tag.ContainsKey("downedMeldosaurus");
            downedFogbound = tag.ContainsKey("downedFogbound");
            paintEnabled = tag.GetInt("paintEnabled");
            fannyOverride = tag.GetInt("fanson");

            nugget = tag.ContainsKey("nugget");
            draco = tag.ContainsKey("draco");
            folly = tag.ContainsKey("folly");
            godnug = tag.ContainsKey("godnug");
            mammoth = tag.ContainsKey("mammoth");
            shadow = tag.ContainsKey("shadow");

            ninja = tag.ContainsKey("ninja");
            astro = tag.ContainsKey("astro");
            tar = tag.ContainsKey("tar");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new();
            flags[0] = rescuedjelly;
            flags[1] = jharim;
            flags[2] = orthofound;
            flags[3] = amogus;
            flags[6] = jharinter;

            BitsByte flags2 = new();
            flags2[0] = downedMeldosaurus;
            flags2[1] = downedFogbound;
            flags2[2] = ninja;
            flags2[3] = astro;
            flags2[4] = tar;

            BitsByte flags3 = new();
            flags3[0] = nugget;
            flags3[1] = draco;
            flags3[2] = folly;
            flags3[3] = godnug;
            flags3[4] = mammoth;
            flags3[5] = shadow;

            writer.Write(flags);
            writer.Write(flags2);
            writer.Write(flags3);
            writer.Write(paintEnabled);
        }
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            rescuedjelly = flags[0];
            jharim = flags[1];
            orthofound = flags[2];
            amogus = flags[3];
            jharinter = flags[6];

            BitsByte flags2 = reader.ReadByte();
            downedMeldosaurus = flags2[0];
            downedFogbound = flags2[1];
            ninja = flags2[2];
            astro = flags2[3];
            tar = flags2[4];

            BitsByte flags3 = reader.ReadByte();
            nugget = flags3[0];
            draco = flags3[1];
            folly = flags3[2];
            godnug  = flags3[3];
            mammoth = flags3[4];
            shadow = flags3[5];

            paintEnabled = reader.ReadInt32();
        }
        #endregion

        #region //Tiles
        public override void ResetNearbyTileEffects()
        {
            astralTiles = 0;
            hellTiles = 0;
            labTiles = 0;
            dungeontiles = 0;
        }

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            // Old Astral tiles
            astralTiles = tileCounts[TileType<AstralDirtPlaced>()] + tileCounts[TileType<AstralGrassPlaced>()] + tileCounts[TileType<XenostonePlaced>()] + tileCounts[TileType<AstralSandPlaced>()] + tileCounts[TileType<AstralHardenedSandPlaced>()] + tileCounts[TileType<AstralSandstonePlaced>()] + tileCounts[TileType<AstralClayPlaced>()] + tileCounts[TileType<AstralIcePlaced>()] + tileCounts[TileType<AstralSnowPlaced>()];
            if (CalValEX.CalamityActive)
            {
                // Hell Lab tiles
                hellTiles = tileCounts[CalValEX.CalamityTile("Chaosplate")];
                // Jungle Lab tiles
                jungleTiles = tileCounts[CalValEX.CalamityTile("PlagueContainmentCells")];
                // Lab tiles
                labTiles = tileCounts[CalValEX.CalamityTile("LaboratoryPlating")] + tileCounts[CalValEX.CalamityTile("LaboratoryPanels")] + tileCounts[CalValEX.CalamityTile("RustedPlating")] + tileCounts[CalValEX.CalamityTile("LaboratoryPipePlating")] + tileCounts[CalValEX.CalamityTile("RustedPipes")];
            }
            //Dungeon tiles
            dungeontiles = tileCounts[TileID.BlueDungeonBrick] + tileCounts[TileID.PinkDungeonBrick] + tileCounts[TileID.GreenDungeonBrick];
        }
        #endregion

        public override void PreUpdateNPCs()
        {
            if (CalValEX.CalamityActive)
            {
                if ((bool)ModLoader.GetMod("CalamityMod").Call("GetDifficultyActive", "revengeance") || Main.masterMode)
                {
                    masorev = true;
                }
                else
                {
                    masorev = false;
                }
            }
            else
            {
                masorev = false;
            }
            bool invasiveAFChange = (DateTime.Now.Month == 4 && DateTime.Now.Day == 1) || (Main.zenithWorld && DateTime.Now.Month == 4 && DateTime.Now.Day <= 7);
            // Requires special logic
            if (invasiveAFChange || fannyOverride == 2)
            {
                if (CalValEXConfig.Instance.AprilFoolsContent)
                    FannyManager.fannyEnabled = true;
                else
                    FannyManager.fannyEnabled = false;
            }
            else if (CalValEXConfig.Instance.AprilFoolsContent)
            {
                FannyManager.fannyEnabled = false;
            }

            // Enable April Fool's content automatically in the drunk seed/gfb if the config is on
            if (Main.drunkWorld && CalValEXConfig.Instance.AprilFoolsContent)
            {
                CalValEX.AprilFoolDay = true;
                CalValEX.AprilFoolWeek = true;
                CalValEX.AprilFoolMonth = true;
            }
            // If the config is off, disable all April Fool's content
            else if (!CalValEXConfig.Instance.AprilFoolsContent)
            {
                CalValEX.AprilFoolDay = false;
                CalValEX.AprilFoolWeek = false;
                CalValEX.AprilFoolMonth = false;
            }
            // Replace sprites with CalPaint on April Fools
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                if (CalValEX.CalamityActive)
                {
                    if (CalPaintOverride.TexturesActive)
                    {
                        if (CalPaintOverride.originalFC.Count <= 0)
                        {
                            CalPaintOverride.ReplaceWithPaint(true);
                        }
                        else if (Main.npcFrameCount[CalamityID.CalNPCID.CalamitasClone] != 10)
                        {
                            CalPaintOverride.ReplaceWithPaint();
                        }
                    }
                    if (!CalPaintOverride.TexturesActive && Main.npcFrameCount[CalamityID.CalNPCID.CalamitasClone] == 10)
                    {
                        CalPaintOverride.ReplaceWithPaint(revert: true);
                    }
                }
            }
            // Call the spawning method
            if (nugget)
                SpawnBitches<NuggetNugget>(nugget);
            if (draco)
                SpawnBitches<DracoNugget>(draco);
            if (folly)
                SpawnBitches<FollyNugget>(folly);
            if (godnug)
                SpawnBitches<GODNugget>(godnug);
            if (mammoth)
                SpawnBitches<MammothNugget>(mammoth);
            if (shadow)
                SpawnBitches<ShadowNugget>(shadow);
        }

        // We use a generic method here so we can pass the ModNPC type as a paremeter, basically saving me from having to wrie this shit 6 times
        public void SpawnBitches<T>(bool nugSpawn) where T : ModNPC {
            Player player = Main.LocalPlayer;

            // Random delay between death/reroll and spawn, measured in frames divided by 100
            var maxCount = Main.rand.Next(9, 108);
            
            nugCounter++;
            // Multiplied by 100 so there's not as many numbers to choose from, it's still a lot but it's better than 900-10800
            if (nugCounter > (maxCount * 100))
                nugCounter = 0;

            if (CanSpawnNow() && !NPC.AnyNPCs(NPCType<T>()) && nugSpawn && isThereAHouse && nugCounter == 0) {
                // We spawn the nug near the player facing them
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int newNug = NPC.NewNPC(Entity.GetSource_TownSpawn(), (int)(player.position.X + (96 * Main.LocalPlayer.direction)), (int)(player.position.Y - 16), NPCType<T>(), 1);
                    NPC nug = Main.npc[newNug];
                    nug.direction = -Main.LocalPlayer.direction;
                    nug.netUpdate = true;

                    // STAY INSIDE THE WORLD
                    nug.position.X = MathHelper.Clamp(nug.position.X, 150f, Main.maxTilesX * 16f - 150f);
                    nug.position.Y = MathHelper.Clamp(nug.position.Y, 150f, Main.maxTilesY * 16f - 150f);

                    if (Main.netMode == NetmodeID.SinglePlayer)
                        Main.NewText(Language.GetTextValue("Mods.CalValEX.NPCs.NuggetNugget.NuggetSpawn").FormatWith(nug.FullName, Main.worldName), 50, 125, 255);
                        //nug.FullName + " has risen from " + Main.worldName + "'s ashes!"
                    else
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.CalValEX.NPCs.NuggetNugget.NuggetSpawn").FormatWith(nug.FullName, Main.worldName)), new Color(50, 125, 255));
                }
            }
        }

        public static void UpdateWorldBool() {
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendData(MessageID.WorldData);
        }

        private static bool CanSpawnNow() {
            if (Main.eclipse || Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0)
                return false;

            if (Main.IsFastForwardingTime())
                return false;

            return Main.dayTime;
        }

        public override void AddRecipeGroups()
        {
            if (CalValEX.CalamityActive)
            {
                RecipeGroup sand = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Sand"]];
                sand.ValidItems.Add(ItemType<Items.Tiles.Blocks.Astral.AstralSand>());
                RecipeGroup fieref = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Fireflies"]];
                fieref.ValidItems.Add(ItemType<Items.Critters.VaporoflyItem>());
                fieref.ValidItems.Add(ItemType<Items.Critters.BlinkerItem>());
                RecipeGroup bf = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Butterflies"]];
                bf.ValidItems.Add(ItemType<Items.Critters.ProvFlyItem>());
                bf.ValidItems.Add(ItemType<Items.Critters.CrystalFlyItem>());
                if (RecipeGroup.recipeGroupIDs.ContainsKey("AnyIceBlock"))
                {
                    int index = RecipeGroup.recipeGroupIDs["AnyIceBlock"];
                    RecipeGroup groupe = RecipeGroup.recipeGroups[index];
                    groupe.ValidItems.Add(ItemType<Items.Tiles.Blocks.Astral.AstralIce>());
                }
                RecipeGroup group = new(() => "Any Plate", new int[]
                {
                CalValEX.CalamityItem("Plagueplate"),
                CalValEX.CalamityItem("Cinderplate"),
                CalValEX.CalamityItem("Chaosplate"),
                CalValEX.CalamityItem("Navyplate"),
                CalValEX.CalamityItem("Elumplate"),
                CalValEX.CalamityItem("Onyxplate"),
                ItemType<Aeroplate>()
                });
                RecipeGroup.RegisterGroup("AnyPlate", group);

                /*RecipeGroup group2 = new RecipeGroup(() => "Any Hardmode Drill", new int[]
                {
                    ItemID.CobaltDrill,
                    ItemID.PalladiumDrill,
                    ItemID.MythrilDrill,
                    ItemID.OrichalcumDrill,
                    ItemID.AdamantiteDrill,
                    ItemID.TitaniumDrill,
                });
                RecipeGroup.RegisterGroup("AnyHardmodeDrill", group2);*/
            }
        }

        public override void PostAddRecipes()
        {
            CalValEX.instance.SetupHerosMod();
            ItemID.Sets.ShimmerTransformToItem[PaintingLoader.paintingItems["Scourgy"]] = PaintingLoader.paintingItems["Scourgie"];
            ItemID.Sets.ShimmerTransformToItem[PaintingLoader.paintingItems["Scourgie"]] = PaintingLoader.paintingItems["Scourgy"];
        }
    }
}