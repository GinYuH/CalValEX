using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using CalamityMod.CalPlayer;

namespace CalValEX.NPCs.JellyPriest
{
    public class JellyPriestBound : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.npcSlots = 5f;
            NPC.width = 18;
            NPC.height = 34;
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            NPC.rarity = 1;
            NPC.dontTakeDamageFromHostiles = true;
        }

        public override bool CanChat()
        {
            return true;
        }

        public override void AI()
        {
            NPC.breath += 2;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && Main.player[i].talkNPC == NPC.whoAmI)
                {
                    CalValEXWorld.rescuedjelly = true;
                    CalValEXWorld.UpdateWorldBool();
                    NPC.Transform(ModContent.NPCType<JellyPriestNPC>());
                    NPC.netUpdate = true;
                    if (!NPC.AnyNPCs(ModContent.NPCType<JellyPriestNPC>()))
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<JellyPriestNPC>());
                        NPC.active = false;
                    }
                    return;
                }
            }
        }

        public override string GetChat()
        {
            return "Greetings, land creature! I rise from this old sea in hopes of traveling and finding a certain deity from the old times, from when the sea was a beautiful reign for many. Do you have any hint about where I could find them?";         
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (CalValEX.CalamityActive)
            {
                if ((bool)CalValEX.Calamity.Call("GetBossDowned", "acidrain") && !CalValEXConfig.Instance.TownNPC && !CalValEXWorld.rescuedjelly && spawnInfo.Player.InModBiome(CalValEX.CalamityBiome("SulphurousSeaBiome")) && !NPC.AnyNPCs(ModContent.NPCType<JellyPriestBound>()) && !NPC.AnyNPCs(ModContent.NPCType<JellyPriestNPC>()))
                {
                    return 0.5f;
                }
                else
                    return 0;
            }
            else
            {
                return 0f;
            }
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return true;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (projectile.friendly)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.DisableCrit();
            //modifiers.Knockback = 0;
            modifiers.SetMaxDamage(1);
            NPC.Transform(ModContent.NPCType<JellyPriestNPC>());
            return;
        }
        public override bool CheckDead()
        {
            NPC.life = 1;
            NPC.active = true;
            NPC.netUpdate = true;
            return false;
        }

        /*public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, mod.GetGoreSlot("Gores/JellyPriest"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, mod.GetGoreSlot("Gores/JellyPriest2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, mod.GetGoreSlot("Gores/JellyPriest3"), 1f);
            }
        }*/
    }
}