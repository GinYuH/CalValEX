using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;

namespace CalValEX.NPCs.Critters
{
    public class Swearshroom : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 22;
            NPC.height = 22;
            NPC.aiStyle = NPCAIStyleID.Passive;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit26;
            NPC.DeathSound = SoundID.NPCDeath1;

            NPC.catchItem = (short)ItemType<SwearshroomItem>();
            NPC.lavaImmune = false;
            AIType = NPCID.Mouse;
            AnimationType = NPCID.Grubby;
            NPC.npcSlots = 0.25f;
            NPC.dontTakeDamage = true;
            NPC.chaseable = false;
        }

        public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.UIInfoProvider = new Terraria.GameContent.Bestiary.CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type], quickUnlock: true);
            bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
                Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
                new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.{Name}")
                //("The great crustacean's spores are not happy that their host has been destroyed. These wicked fungi have psychopathic tendencies, but are completely harmless."),
            });
        }

        private int nohurt = 120;
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (CalValEX.CalamityActive)
            {
                if ((bool)CalValEX.Calamity.Call("GetBossDowned", "crabulon") && !CalValEXConfig.Instance.CritterSpawns && spawnInfo.Player.ZoneGlowshroom)
                {
                    if (spawnInfo.PlayerSafe)
                    {
                        return Terraria.ModLoader.Utilities.SpawnCondition.UndergroundMushroom.Chance * 0.025f;
                    }
                    else
                    {
                        return Terraria.ModLoader.Utilities.SpawnCondition.UndergroundMushroom.Chance * 0.05f;
                    }
                }
            }
            return 0f;
        }

        public override void AI()
        {
            CVUtils.CritterBestiary(NPC, Type);
            if (--nohurt == 0)
            {
                NPC.dontTakeDamage = false;
                NPC.netUpdate = true;
            }
        }
    }
}