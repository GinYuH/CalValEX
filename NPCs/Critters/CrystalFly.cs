using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;

namespace CalValEX.NPCs.Critters
{
    public class CrystalFly : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 40;
            NPC.aiStyle = NPCAIStyleID.Butterfly;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 0.25f;
            NPC.noGravity = true;

            NPC.catchItem = (short)ItemType<CrystalFlyItem>();
            NPC.lavaImmune = true;
            AIType = NPCID.GoldButterfly;
            AnimationType = NPCID.GoldButterfly;
            NPC.lifeMax = 20;
            NPC.chaseable = false;
            if (CalValEX.CalamityActive)
            {
                NPC.buffImmune[CalValEX.CalamityBuff("HolyFlames")] = true;
            }
        }

        public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.UIInfoProvider = new Terraria.GameContent.Bestiary.CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type], quickUnlock: true);
            bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
                Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
                new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.{Name}")
                //("A butterfly possessing energy from one of two halves of a fiery goddess' power."),
            });
        }

        public override void AI()
        {
            CVUtils.CritterBestiary(NPC, Type);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !NPC.downedMoonlord || CalValEXConfig.Instance.CritterSpawns)
            {
                return 0f;
            }
            return Terraria.ModLoader.Utilities.SpawnCondition.OverworldHallow.Chance * 0.1f;
        }

       
    }
}