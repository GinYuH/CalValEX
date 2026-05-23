using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using CalamityMod.World;
//using CalamityMod;
using System;

namespace CalValEX.AprilFools.Meldosaurus
{
    public class MeldWyrmHead : ModNPC

	{
		public override void SetStaticDefaults()
		{
			if (!CalValEX.AprilFoolMonth)
			{
				NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
				{
					Hide = true
				};
				NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
			}
		}
		public override void SetDefaults()
		{
			NPC.damage = 80;
			NPC.width = 80;
			NPC.height = 60;
			NPC.defense = 22;
			NPC.lifeMax = 10000;
			NPC.aiStyle = -1;
			Main.npcFrameCount[NPC.type] = 1;
			NPC.knockBackResist = 0f;
			NPC.lavaImmune = true;
			NPC.behindTiles = false;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.netAlways = true;
            NPC.noGravity = true;
            if (CalValEX.CalamityActive)
			{
				CalValEX.Calamity.Call("SetDefenseDamageNPC", true);
				CalValEX.Calamity.Call("SetDamageReductionSpecific", 0.2f);
			}
		}
		public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
		{
			if (CalValEX.AprilFoolMonth)
			{
				bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
				Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.MeldWyrmHead")
				//("A parasitic tapewyrm that saught refuge inside of the alien dinosaur. It is likely that similar creatures carve out the insides of the mysterious pillars."),
			});
			}
		}
		public override void AI()
		{
			if (CalValEXGlobalNPC.meldodon <= -1)
            {
				NPC.HitEffect(0, NPC.lifeMax * 55, true);
            }
			NPC meldosaurus = Main.npc[CalValEXGlobalNPC.meldodon];
            // Create segments on the first frame.
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.localAI[0] == 0f)
			{
				SpawnSegments();
				NPC.localAI[0] = 1f;
			}
			NPC.ai[0] += 3;
			double deg = 180 * NPC.ai[1] + NPC.ai[0];
			double rad = deg * (Math.PI / 180);
			float hyposx = meldosaurus.Center.X - (int)(Math.Cos(rad) * 360) - NPC.width / 2;
			float hyposy = meldosaurus.Center.Y - (int)(Math.Sin(rad) * 360) - NPC.height / 2;
			Vector2 idealpos = new(hyposx, hyposy);
			Dust.NewDustDirect(idealpos, 1, 1, DustID.Stone);

			NPC.velocity = new Vector2(hyposx, hyposy) - NPC.position;

			NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;
		}
		public void SpawnSegments()
		{
			int previousSegment = NPC.whoAmI;
			for (int i = 0; i < 15; i++)
			{
				int nextSegmentIndex;
				if (i < 15 - 1)
					nextSegmentIndex = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MeldWyrmBody>(), NPC.whoAmI);
				else 
					nextSegmentIndex = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MeldWyrmTail>(), NPC.whoAmI);
				Main.npc[nextSegmentIndex].realLife = NPC.whoAmI;
				Main.npc[nextSegmentIndex].ai[2] = NPC.whoAmI;
				Main.npc[nextSegmentIndex].ai[1] = previousSegment;
				Main.npc[previousSegment].ai[0] = nextSegmentIndex;

				NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, nextSegmentIndex, 0f, 0f, 0f, 0);

				previousSegment = nextSegmentIndex;
			}
		}
	}
}