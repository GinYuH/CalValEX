using CalValEX.Items.Tiles.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;

namespace CalValEX.NPCs.Critters
{
    public class AstJR : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
        }
        public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.UIInfoProvider = new Terraria.GameContent.Bestiary.CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type], quickUnlock: true);
            bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
                new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.{Name}")
                //("A sentient glob from the alien environment. Unlike other slimes, it possesses no offensive capabilities."),
            });
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.BabySlime);
            NPC.width = 26;
            NPC.height = 22;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.npcSlots = 0.5f;
            NPC.catchItem = (short)ItemType<AstJRItem>();
            NPC.lavaImmune = false;
            AIType = NPCID.Pinky;
            AnimationType = NPCID.BlueSlime;
            NPC.lifeMax = 100;
            NPC.Opacity = 255;
            NPC.value = 0;
            NPC.chaseable = false;
            Banner = NPC.type;
            BannerItem = ItemType<AstragellySlimeBanner>();
            SpawnModBiomes = [GetInstance<Biomes.AstralBlight>().Type];
            if (CalValEX.CalamityActive)
            {
                NPC.buffImmune[CalValEX.CalamityBuff("AstralInfectionDebuff")] = true;
            }
        }

        public override void AI()
        {
            NPC.spriteDirection = -NPC.direction;
            NPC.TargetClosest(false);
            CVUtils.CritterBestiary(NPC, Type);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.InModBiome(GetInstance<Biomes.AstralBlight>()) && !CalValEXConfig.Instance.CritterSpawns)
            {
                if (spawnInfo.PlayerSafe)
                {
                    return Terraria.ModLoader.Utilities.SpawnCondition.TownCritter.Chance * 0.5f;
                }
                else if (!Main.eclipse && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon)
                {
                    return 0.15f;
                }
            }
            return 0f;
        }
    }
}