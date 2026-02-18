using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;

namespace CalValEX.NPCs.Critters
{
    public class Vaporofly : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
            Main.npcCatchable[NPC.type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.CountsAsCritter[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 20;
            NPC.CloneDefaults(NPCID.Firefly);
            NPC.catchItem = (short)ItemType<VaporoflyItem>();
            NPC.lavaImmune = false;
            AIType = NPCID.Firefly;
            AnimationType = NPCID.Firefly;
            NPC.lifeMax = 20;
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
                //("An ancient insect species that was revitalized by the radioactive waters."),
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (CalValEX.CalamityActive)
            {
                if (CalValEX.InCalamityBiome(spawnInfo.Player, "SulphurousSeaBiome") && !CalValEXConfig.Instance.CritterSpawns)
                {
                    if (spawnInfo.Player.ZoneOverworldHeight && !Main.bloodMoon)
                    {
                        return Terraria.ModLoader.Utilities.SpawnCondition.OverworldNight.Chance * 0.5f;
                    }
                }
                return 0f;
            }
            return 0f;
        }

        public override void AI()
        {
            CVUtils.CritterBestiary(NPC, Type);
            if (Main.rand.NextFloat() < 0.3421053f)
            {
                Dust dust;
                Vector2 positionLeft = new(NPC.position.X + 9, NPC.position.Y);
                Vector2 positionRight = new(NPC.position.X - 9, NPC.position.Y);
                if (NPC.direction == -1)
                {
                    dust = Main.dust[Dust.NewDust(positionLeft, 0, 0, DustID.Torch, 1f, 1f, 0, new Color(109, 255, 0), 0.5f)];
                    dust.noGravity = true;
                }
                else if (NPC.direction != 0)
                {
                    dust = Main.dust[Dust.NewDust(positionRight, 0, 0, DustID.Torch, 1f, 1f, 0, new Color(109, 255, 0), 0.5f)];
                    dust.noGravity = true;
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            Texture2D texture = Request<Texture2D>("CalValEX/NPCs/Critters/Vaporofly_Glow").Value;
            float vapeframe = 8f / (float)Main.npcFrameCount[NPC.type];
            int vapeheight = (int)((float)(NPC.frame.Y / NPC.frame.Height) * vapeframe) * (texture.Height / 8);

            Rectangle vapesquare = new(0, vapeheight, texture.Width, texture.Height / 8);

            SpriteEffects rainbowy = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                rainbowy = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, vapesquare, NPC.color, NPC.rotation, Utils.Size(vapesquare) / 2f, NPC.scale, rainbowy, 0f);
        }
    }
}