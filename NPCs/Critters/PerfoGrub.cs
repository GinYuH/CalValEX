using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;
using Microsoft.Xna.Framework.Graphics;

namespace CalValEX.NPCs.Critters
{
    public class PerfoGrub : ModNPC {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 4;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new (0) { Velocity = 1f, Direction = -1 };
        }

        public override void SetDefaults() {
            NPC.CloneDefaults(NPCID.Snail);
            NPC.width = 22;
            NPC.height = 18;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.npcSlots = 0.5f;
            NPC.catchItem = ItemType<PerfoGrubItem>();
            NPC.lavaImmune = false;
            NPC.chaseable = false;
            NPC.friendly = true;
            AIType = NPCID.Snail;
            AnimationType = NPCID.Snail;
            NPC.lifeMax = 5;
            NPC.value = 0;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.UIInfoProvider = new CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type], quickUnlock: true);
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonDesert,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonIce,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.CrimsonUndergroundDesert,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson,
                new FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.{Name}")
                //("A small grub-like parasite, the vile offspring of the Perforators."),
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
            if (spawnInfo.Player.ZoneCrimson && !CalValEXConfig.Instance.CritterSpawns &&
                        !Main.eclipse &
                        !Main.snowMoon &
                        !Main.pumpkinMoon &
                        Main.invasionType == InvasionID.None) {
                return 0.35f;
            }
            return 0f;
        }

        public override void AI()
        {
            CVUtils.CritterBestiary(NPC, Type);
        }
    }
}