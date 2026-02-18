using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework.Graphics;

namespace CalValEX.NPCs.Critters
{
    public class NeuroFly : ModNPC {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 4;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
	        NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new (0) { Velocity = 1f, Direction = -1 };
        }

        public override void SetDefaults() {
            NPC.CloneDefaults(NPCID.BlackDragonfly);
            NPC.width = 26;
            NPC.height = 22;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.npcSlots = 0.5f;
            NPC.catchItem = ItemType<NeuroFlyItem>();
            NPC.lavaImmune = true;
            AIType = NPCID.BlackDragonfly;
            AnimationType = NPCID.BlackDragonfly;
            NPC.lifeMax = 20;
            NPC.value = 0;
            NPC.chaseable = false;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type], quickUnlock: true);
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptDesert,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptIce,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CorruptUndergroundDesert,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCorruption,
                new FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.{Name}")
                //("One of the many creatures assisting the Hive Mind, although not physically connected, they share a psychic connection. Its sole purpose is to spread the virus."),
            });
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers)) {
                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }

            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            if (spawnInfo.Player.ZoneCorrupt && !CalValEXConfig.Instance.CritterSpawns &&
                        !Main.eclipse &
                        !Main.snowMoon &
                        !Main.pumpkinMoon &
                        Main.invasionType == InvasionID.None)
            {
                return 0.35f;
            }
            return 0f;
        }

        public override void AI()
        {
            CVUtils.CritterBestiary(NPC, Type);
            NPC.spriteDirection = -NPC.direction;
            NPC.TargetClosest(false);
        }
    }
}