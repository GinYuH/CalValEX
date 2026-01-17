using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;

namespace CalValEX.NPCs.Critters
{
    public class XerocodileSwim : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.noGravity = true;
            NPC.width = 20;
            NPC.height = 24;
            NPC.aiStyle = NPCAIStyleID.Piranha;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.lifeMax = 666;
            NPC.HitSound = SoundID.NPCHit50;
            NPC.DeathSound = SoundID.NPCDeath54;
            NPC.npcSlots = 0.25f;
            NPC.catchItem = (short)ItemType<XerocodileItem>();
            NPC.lavaImmune = false;
            Banner = NPCType<Xerocodile>();
            BannerItem = ItemType<Items.Tiles.Banners.XerocodileBanner>();
            AIType = NPCID.Goldfish;
            AnimationType = NPCID.Goldfish;
            NPC.chaseable = false;
        }

        public override void AI()
        {
            CVUtils.CritterBestiary(NPC, NPCType<Xerocodile>());
            if (!NPC.wet)
            {
                NPC.Transform(NPCType<Xerocodile>());
            }
            bool inGarden = false;
            if (CalValEX.instance.wotg != null)
            {
                if ((bool)ModLoader.GetMod("SubworldLibrary").Call("AnyActive", CalValEX.instance.wotg))
                {
                    inGarden = true;
                }
            }
            if (!Main.bloodMoon && !inGarden)
            {
                if (CalValEX.CalamityActive)
                    Item.NewItem(NPC.GetSource_FromAI(), NPC.position, NPC.width, NPC.height, CalValEX.CalamityItem("Gorecodile"), 1, false, 0, false, false);
                NPC.active = false;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!CalValEXConfig.Instance.CritterSpawns)
            {
                if (CalValEX.CalamityActive)
                {
                    if ((bool)CalValEX.Calamity.Call("GetBossDowned", "providence") && Main.bloodMoon)
                    {
                        if (spawnInfo.PlayerSafe)
                        {
                            return Terraria.ModLoader.Utilities.SpawnCondition.WaterCritter.Chance * 0.01f;
                        }
                        else if (!Main.eclipse && !Main.pumpkinMoon && !Main.snowMoon)
                        {
                            return Terraria.ModLoader.Utilities.SpawnCondition.OceanMonster.Chance * 0.015f;
                        }
                    }
                }
                if (CalValEX.instance.wotg != null)
                {
                    if (spawnInfo.Player.InModBiome(CalValEX.instance.wotg.Find<ModBiome>("EternalGardenBiome")))
                    {
                        return Terraria.ModLoader.Utilities.SpawnCondition.WaterCritter.Chance;
                    }
                }
            }
            return 0f;
        }
        public override void HitEffect(NPC.HitInfo hit) {
            if (Main.netMode == NetmodeID.Server)
                return;

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Xerocodile").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Xerocodile2").Type, 1f);
                Gore.NewGore(NPC.GetSource_FromAI(), NPC.position, NPC.velocity, Mod.Find<ModGore>("Xerocodile3").Type, 1f);
            }
        }
    }
}