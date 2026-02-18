using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;

namespace CalValEX.NPCs.Critters
{
    public class Orthobab : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.width = 20;
            NPC.height = 24;
            NPC.aiStyle = NPCAIStyleID.Piranha;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 5;
            NPC.HitSound = SoundID.NPCHit38;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 0.25f;
            NPC.catchItem = (short)ItemType<OrthobabItem>();
            NPC.lavaImmune = false;
            AIType = NPCID.Goldfish;
            AnimationType = NPCID.Goldfish;
            NPC.lifeMax = 5;
            NPC.chaseable = false;
            if (CalValEX.CalamityActive)
            {
                SpawnModBiomes = [CalValEX.CalamityBiome("SulphurousSeaBiome").Type];
                NPC.buffImmune[CalValEX.CalamityBuff("SulphuricPoisoning")] = true;
                NPC.buffImmune[CalValEX.CalamityBuff("Irradiated")] = true;
            }
        }

        public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.UIInfoProvider = new Terraria.GameContent.Bestiary.CommonEnemyUICollectionInfoProvider(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[Type], quickUnlock: true);
            bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
                new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.{Name}")
                //("Newer offspring of The Shelled. Despite their bumbling appearance, any one of these may very well grow into something horrific."),
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (CalValEX.CalamityActive)
            {
                if (CalValEX.InCalamityBiome(spawnInfo.Player, "SulphurousSeaBiome") && !CalValEXConfig.Instance.CritterSpawns &&
                        !Main.eclipse &
                        !Main.snowMoon &
                        !Main.pumpkinMoon &
                        Main.invasionType == InvasionID.None)
                {
                    return 0.15f;
                }
            }
            return 0f;
        }

        public override void AI()
        {
            CVUtils.CritterBestiary(NPC, Type);
        }
    }
}