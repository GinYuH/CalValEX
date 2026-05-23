using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using CalamityMod.World;
//using CalamityMod;

namespace CalValEX.AprilFools.Meldosaurus
{
    public class MeldWyrmBody : ModNPC
	{
		public override void SetStaticDefaults()
		{
			NPCID.Sets.NPCBestiaryDrawModifiers value = new(0)
			{
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
		}
		public override void SetDefaults()
		{
			NPC.damage = 0;
			NPC.width = 20;
			NPC.height = 20;
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
				CalValEX.Calamity.Call("SetDamageReductionSpecific", 0.2f);
			}
		}

		public override void AI()
		{
			// Die immediately if the ahead segment is invalid.
			if (NPC.ai[1] <= -1f || NPC.realLife <= -1f || !Main.npc[(int)NPC.ai[1]].active)
			{
				if (Main.netMode == NetmodeID.MultiplayerClient)
					return;

				NPC.life = 0;
				NPC.HitEffect();

				NPC.active = false;
				NPC.netUpdate = true;
			}

			// Don't use a boss HP bar.
			NPC aheadSegment = Main.npc[(int)NPC.ai[1]];
			//NPC.GetGlobalNPC<CalamityMod.NPCs.CalamityGlobalNPC>().ShouldCloseHPBar = true;

			// Inherit various attributes from the head segment.
			NPC head = Main.npc[NPC.realLife];
			NPC.Opacity = head.Opacity;
			NPC.chaseable = true;
			NPC.friendly = false;
			NPC.dontTakeDamage = head.dontTakeDamage;
			NPC.damage = NPC.dontTakeDamage ? 0 : NPC.defDamage;

			// Rotate such that each segment approaches the ahead segment's rotation with an interpolant of 0.03. This process is asymptotic.
			Vector2 directionToNextSegment = aheadSegment.Center - NPC.Center;
			if (aheadSegment.rotation != NPC.rotation && head.velocity.Length() > 2f)
				directionToNextSegment = directionToNextSegment.RotatedBy(MathHelper.WrapAngle(aheadSegment.rotation - NPC.rotation) * 0.03f);
			directionToNextSegment = directionToNextSegment.SafeNormalize(Vector2.Zero);

			NPC.rotation = directionToNextSegment.ToRotation() + MathHelper.PiOver2;
			NPC.Center = aheadSegment.Center - directionToNextSegment * NPC.width * NPC.scale * 0.725f;
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;
	}
}