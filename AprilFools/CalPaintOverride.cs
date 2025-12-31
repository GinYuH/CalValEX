using CalamityMod;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.NPCs.Yharon;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.AprilFools
{
    [ExtendsFromMod("CalamityMod")]
    public class CalPaintOverride : GlobalNPC
    {
        public int frame;
        public int frameX;
        public int frameCounter;

        public static Dictionary<int, int> originalFC = new Dictionary<int, int>();
        public static Dictionary<string, Asset<Texture2D>> originalTX = new Dictionary<string, Asset<Texture2D>>();

        public override bool InstancePerEntity => true;

		public const string nothing = "CalValEX/Items/Equips/Shields/Invishield_Shield";
		public const string path = "CalValEX/AprilFools/CalpaintTextureOverride/";

        public static FieldInfo anahitaChargeField;
        public static FieldInfo proviCocoonField;

        // Only if April Fools config is on
        // Only if it's April 1/GFB April1Week and the texture override isnt enabled OR the texture override is set to true
        // Only if Infernum Mode and DLC EMode aren't enabled
        // Only in singleplayer
        public static bool TexturesActive => 
            CalValEX.CalamityActive &&
            CalValEXConfig.Instance.AprilFoolsContent && 
            (((DateTime.Now.Month == 4 && (DateTime.Now.Day == 1 || DateTime.Now.Day == 2)) || (Main.zenithWorld && DateTime.Now.Month == 4 && DateTime.Now.Day <= 7) && CalValEXWorld.paintEnabled == 0) || CalValEXWorld.paintEnabled == 2) &&
            !CalValEX.InfernumActive() &&
            !CalValEX.CalEModeActive() &&
            Main.netMode == NetmodeID.SinglePlayer;

        // CHECKLIST
        /*
         * Item - Done
         * Desert Scourge - Done
         * Crabulon - Done
         * Hive - Done
         * Perf - Done
         * Slime God - Done
         * Cryogen - Done
         * Aqua - Done
         * Brim - Done
         * Clone - Done
         * Levi - Done
         * Aureus - Done
         * PBG - Done
         * Ravager - Done
         * Deus - Done
         * Guard - Done
         * Birb - Done
         * Prov - Done
         * Sig - Done
         * Void - Done
         * Weaver - Done
         * Polter - Done
         * Duke - Done
         * DoG - Done
         * Yharon - Done
         * Artemis - Done
         * Apollo - Done
         * Thanatos - Done
         * Ares - Done
         * Draedon - Done
         * Scal - Done
         * Wyrm - Done
         */

        public override void Load()
        {
            anahitaChargeField = typeof(Anahita).GetField("forceChargeFrames", BindingFlags.NonPublic | BindingFlags.Instance);
            proviCocoonField = typeof(Providence).GetField("useDefenseFrames", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public override void FindFrame(NPC npc, int frameHeight)
		{
			if (TexturesActive)
			{
                if (npc.type == ModContent.NPCType<HiveMind>())
                {
                    AnimateNPC(3);
				}
				if (npc.type == ModContent.NPCType<PerforatorHive>())
				{
					AnimateNPC(3);
				}
				if (npc.type == ModContent.NPCType<Polterghast>())
				{
					AnimateNPC(4);
				}
				if (npc.type == ModContent.NPCType<SlimeGodCore>())
				{
					AnimateNPC(6);
				}
				if (npc.type == ModContent.NPCType<BrimstoneElemental>())
				{
                    // cocoon
                    if (!(npc.ai[0] <= 2f || npc.ai[0] == 3f || npc.ai[0] == 5f))
                    {
                        AnimateNPC(4, start: 5, end: 8);
                    }
                    // no cocoon
                    else
                    {
                        AnimateNPC(4, end: 4);
                    }
				}
				if (npc.type == ModContent.NPCType<AstrumAureus>() || npc.type == ModContent.NPCType<EbonianPaladin>() || npc.type == ModContent.NPCType<SplitEbonianPaladin>() || npc.type == ModContent.NPCType<CrimulanPaladin>() || npc.type == ModContent.NPCType<SplitCrimulanPaladin>())
				{
					frame = npc.velocity.Y == 0 ? 0 : 1;
				}
                if (npc.type == ModContent.NPCType<CalamitasClone>())
                {
                    AnimateNPC(7);
                }
                if (npc.type == ModContent.NPCType<Providence>())
                {
                    Providence proviNPC = npc.ModNPC<Providence>();
                    bool proviCocooning = (bool)proviCocoonField.GetValue(proviNPC);
                    if (proviCocooning)
                    {
                        frame = 3;
                    }
                    else
                    {
                        AnimateNPC(2);
                    }
                }
                if (npc.type == ModContent.NPCType<Anahita>())
                {
                    Anahita anaNPC = npc.ModNPC<Anahita>();
                    bool forceChargeFrames = (bool)anahitaChargeField.GetValue(anaNPC);
                    if (npc.ai[0] > 2f || forceChargeFrames)
                        AnimateNPC(2);
                }
                if (npc.type == ModContent.NPCType<Polterghast>())
                {
                    AnimateNPC(3);
                }
                if (npc.type == ModContent.NPCType<SupremeCalamitas>())
                {
                    bool p2 = npc.ai[0] >= 3f && (npc.life > npc.lifeMax * 0.01);
                    AnimateNPC(p2 ? 3 : 4);
                    if (!p2)
                        npc.rotation = npc.DirectionTo(Main.player[npc.target].Center).ToRotation() + MathHelper.PiOver2;
                    else
                        npc.rotation = 0;

                }
            }
        }

        public void AnimateNPC(int frameCount, bool twoColumn = false, int start = 0, int end = -1)
        {
            frameCounter++;
            if (frameCounter % 6 == 0)
            {
                frame++;
            }
            int maxFrame = frameCount;
            if (end != - 1)
            {
                maxFrame = end;
            }
            if (frame > maxFrame || frame < start)
            {
                frame = start;
                if (twoColumn)
                {
                    frameX = frameX == 0 ? 1 : 0;
                }
            }
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (TexturesActive)
			{
				if (npc.type == ModContent.NPCType<BrimstoneElemental>())
				{
					BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 10);
					return false;
				}
				if (npc.type == ModContent.NPCType<CryogenShield>())
				{
					BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 1);
					return false;
				}
                if (npc.type == ModContent.NPCType<HiveMind>() && npc.life / (float)npc.lifeMax < 0.8f)
                {
                    BasicDrawNPC(ref spriteBatch, HiveMind.Phase2Texture, npc, ModContent.NPCType<HiveMind>(), npc.GetAlpha(drawColor), 6);
                    return false;
				}
				if (npc.type == ModContent.NPCType<EbonianPaladin>() || npc.type == ModContent.NPCType<SplitEbonianPaladin>() || npc.type == ModContent.NPCType<CrimulanPaladin>() || npc.type == ModContent.NPCType<SplitCrimulanPaladin>() || npc.type == ModContent.NPCType<AstrumAureus>())
				{
					BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 2);
                    if (npc.type == ModContent.NPCType<AstrumAureus>())
                    {
                        BasicDrawNPC(ref spriteBatch, AstrumAureus.Texture_Glow, npc, npc.type, Color.White, 2);
                    }
					return false;
				}
				if (npc.type == ModContent.NPCType<PerforatorHive>())
				{
					BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 4);
					BasicDrawNPC(ref spriteBatch, PerforatorHive.GlowTexture, npc, npc.type, Color.White, 4);
					return false;
                }
                if (npc.type == ModContent.NPCType<CalamitasClone>())
                {
                    BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 8);
                    BasicDrawNPC(ref spriteBatch, CalamitasClone.GlowTexture, npc, npc.type, Color.White, 8);
                    return false;
                }
                // This gets rid of the shield, but since SCal is already armored with the old sprite, she doesn't need it anymore lol
                if (npc.type == ModContent.NPCType<SupremeCalamitas>())
                {
                    bool p2 = npc.ai[0] >= 3f && (npc.life > npc.lifeMax * 0.01);
                    BasicDrawNPC(ref spriteBatch, p2 ? ModContent.Request<Texture2D>(path + "SCal2") : TextureAssets.Npc[npc.type], npc, ModContent.NPCType<SupremeCalamitas>(), npc.GetAlpha(drawColor), p2 ? 4 : 5);
                    return false;
                }
                if (npc.type == ModContent.NPCType<Polterghast>())
                {
                    BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, ModContent.NPCType<Polterghast>(), npc.GetAlpha(drawColor), 4, true, false);
                    BasicDrawNPC(ref spriteBatch, Polterghast.Texture_Glow, npc, ModContent.NPCType<Polterghast>(), Color.White, 4, true, false);
                    BasicDrawNPC(ref spriteBatch, Polterghast.Texture_Glow2, npc, ModContent.NPCType<Polterghast>(), Color.White, 4, true, false);
                    return false;
                }
                if (npc.type == ModContent.NPCType<Anahita>())
                {
                    Anahita anaNPC = npc.ModNPC<Anahita>();
                    bool forceChargeFrames = (bool)anahitaChargeField.GetValue(anaNPC);
                    if (npc.ai[0] > 2f || forceChargeFrames)
                    {
                        BasicDrawNPC(ref spriteBatch, Anahita.ChargeTexture, npc, ModContent.NPCType<Anahita>(), npc.GetAlpha(drawColor), 3, true, true);
                        return false;
                    }
                }
                if (npc.type == ModContent.NPCType<Providence>())
                {
                    Color fire = !Main.dayTime ? Color.Cyan : Color.Orange;
                    Color crystal = !Main.dayTime ? Color.Cyan : Color.Magenta;
                    BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, ModContent.NPCType<Providence>(), npc.GetAlpha(drawColor), 4, true, true);
                    BasicDrawNPC(ref spriteBatch, Providence.TextureNight_Glow, npc, ModContent.NPCType<Providence>(), fire, 4, true, true);
                    BasicDrawNPC(ref spriteBatch, Providence.TextureNight_Glow_2, npc, ModContent.NPCType<Providence>(), crystal, 4, true, true);
                    return false;                    
                }
                if (npc.type == ModContent.NPCType<AresGaussNuke>())
                {
                    BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 1, true, false);
                    Color c = Color.White;
                    if ((npc.Calamity().newAI[2] < 144f) && npc.Calamity().newAI[0] == 1)
                    {
                        c = Color.Red;
                    }
                    BasicDrawNPC(ref spriteBatch, AresGaussNuke.GlowTexture, npc, npc.type, c, 1, true, false);
                    return false;
                }
                if (npc.type == ModContent.NPCType<AresTeslaCannon>())
                {
                    BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 1, false, false);
                    Color c = Color.White;
                    if ((npc.Calamity().newAI[2] < 144f) && npc.Calamity().newAI[0] == 1)
                    {
                        c = Color.Red;
                    }
                    BasicDrawNPC(ref spriteBatch, AresTeslaCannon.GlowTexture, npc, npc.type, c, 1, false, false);
                    return false;
                }
                if (npc.type == ModContent.NPCType<AresPlasmaFlamethrower>())
                {
                    BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 1, true, false);
                    Color c = Color.White;
                    if ((npc.Calamity().newAI[2] < 144f) && npc.Calamity().newAI[0] == 1)
                    {
                        c = Color.Red;
                    }
                    BasicDrawNPC(ref spriteBatch, AresPlasmaFlamethrower.GlowTexture, npc, npc.type, c, 1, true, false);
                    return false;
                }
                if (npc.type == ModContent.NPCType<AresLaserCannon>())
                {
                    BasicDrawNPC(ref spriteBatch, TextureAssets.Npc[npc.type], npc, npc.type, npc.GetAlpha(drawColor), 1, false, false);
                    Color c = Color.White;
                    if ((npc.Calamity().newAI[2] < 144f) && npc.Calamity().newAI[0] == 1)
                    {
                        c = Color.Red;
                    }
                    BasicDrawNPC(ref spriteBatch, AresLaserCannon.GlowTexture, npc, npc.type, c, 1, false, false);
                    return false;
                }
            }
            return true;
        }

        public static void ReplaceSprite(string key, ref Asset<Texture2D> original, string newsprite, bool firstLoad = false, bool revert = false)
        {
            if (firstLoad)
            {
                originalTX.Add(key, original);
                original = originalTX[key];
            }
            if (revert)
            {
                originalTX.TryGetValue(key, out Asset<Texture2D> oldTX);
                original = oldTX;
            }
            else
            {
                original = ModContent.Request<Texture2D>(newsprite, AssetRequestMode.ImmediateLoad);
            }
        }

        public static void BasicDrawNPC(ref SpriteBatch spriteBatch, Asset<Texture2D> texture, NPC npc, int type, Color color, int frameCount, bool flip = false, bool left = false)
        {
            SpriteEffects fx = SpriteEffects.None;
            if (flip)
            {
                if (left)
                {
                    fx = npc.direction != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                }
                else
                {
                    fx = npc.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                }
            }
            spriteBatch.Draw(texture.Value, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), texture.Frame(1, frameCount, 0, npc.GetGlobalNPC<CalPaintOverride>().frame), color, npc.rotation, new Vector2(texture.Width(), texture.Height() / frameCount) / 2f, npc.scale, fx, 0f);   
        }

        public static void DoubleDrawNPC(ref SpriteBatch spriteBatch, Asset<Texture2D> texture, NPC npc, int type, Color color, int frameCount, bool flip = false)
        {
            if (npc.type == type)
            {
                SpriteEffects fx = SpriteEffects.None;
                if (flip)
                {
                    fx = npc.spriteDirection != 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                }
                spriteBatch.Draw(texture.Value, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), texture.Frame(2, frameCount, npc.GetGlobalNPC<CalPaintOverride>().frameX, npc.GetGlobalNPC<CalPaintOverride>().frame), color, npc.rotation, new Vector2(texture.Width() / 2, texture.Height() / frameCount) / 2f, npc.scale, fx, 0f);
            }
        }

        public static void ReplaceFrameCount(int type, int count, bool firstLoad = false, bool revert = false)
        {
            if (firstLoad && !originalFC.ContainsKey(type))
            {
                originalFC.Add(type, Main.npcFrameCount[type]);
            }
            if (revert)
            {
                originalFC.TryGetValue(type, out int oldFC);
                Main.npcFrameCount[type] = oldFC;
            }
            else
            {
                Main.npcFrameCount[type] = count;
            }
        }

        // WEEEEEEEEEELCOME TO HEEEEEEEEEEEEEEELLLLLLLLLLLLL
        [JITWhenModsEnabled("CalamityMod")]
        public static void ReplaceWithPaint(bool isFirstLoad = false, bool revert = false)
        {
            ReplaceFrameCount(ModContent.NPCType<AstrumAureus>(), 2, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<BrimstoneElemental>(), 10, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<WildBumblebirb>(), 6, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<DraconicSwarmer>(), 6, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<CalamitasClone>(), 10, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<Cataclysm>(), 1, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<Catastrophe>(), 1, isFirstLoad, revert);

            // wotg replaces CV with a custom rift
            if (CalValEX.instance.wotg == null)
                ReplaceFrameCount(ModContent.NPCType<CeaselessVoid>(), 5, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<ThanatosHead>(), 2, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ThanatosBody1>(), 2, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ThanatosBody2>(), 2, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ThanatosTail>(), 2, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<HiveMind>(), 6, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<Anahita>(), 5, isFirstLoad, revert);

			ReplaceFrameCount(ModContent.NPCType<PerforatorHive>(), 4, isFirstLoad, revert);

			ReplaceFrameCount(ModContent.NPCType<PlaguebringerGoliath>(), 8, isFirstLoad, revert);

			ReplaceFrameCount(ModContent.NPCType<Polterghast>(), 4, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<PolterghastHook>(), 1, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<ProfanedGuardianCommander>(), 1, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ProfanedGuardianDefender>(), 1, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ProfanedGuardianHealer>(), 1, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ProvSpawnOffense>(), 1, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ProvSpawnDefense>(), 1, isFirstLoad, revert);
            ReplaceFrameCount(ModContent.NPCType<ProvSpawnHealer>(), 1, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<SlimeGodCore>(), 6, isFirstLoad, revert);
			ReplaceFrameCount(ModContent.NPCType<EbonianPaladin>(), 2, isFirstLoad, revert);
			ReplaceFrameCount(ModContent.NPCType<CrimulanPaladin>(), 2, isFirstLoad, revert);
			ReplaceFrameCount(ModContent.NPCType<SplitEbonianPaladin>(), 2, isFirstLoad, revert);
			ReplaceFrameCount(ModContent.NPCType<SplitCrimulanPaladin>(), 2, isFirstLoad, revert);

            ReplaceFrameCount(ModContent.NPCType<SupremeCalamitas>(), 5, isFirstLoad, revert);

            // Aqua Scourge
            ReplaceSprite("ASH", ref TextureAssets.Npc[ModContent.NPCType<AquaticScourgeHead>()], path + "AquaticScourgeHead", isFirstLoad, revert);
			ReplaceSprite("ASB", ref TextureAssets.Npc[ModContent.NPCType<AquaticScourgeBody>()], path + "AquaticScourgeBody", isFirstLoad, revert);
			ReplaceSprite("ASB2", ref TextureAssets.Npc[ModContent.NPCType<AquaticScourgeBodyAlt>()], path + "AquaticScourgeBody", isFirstLoad, revert);
			ReplaceSprite("AST", ref TextureAssets.Npc[ModContent.NPCType<AquaticScourgeTail>()], path + "AquaticScourgeTail", isFirstLoad, revert);

            // Astrum Aureus
            ReplaceSprite("Oreo", ref TextureAssets.Npc[ModContent.NPCType<AstrumAureus>()], path + "Astrageldon", isFirstLoad, revert);
            ReplaceSprite("OreoG", ref AstrumAureus.Texture_Glow, path + "AstrageldonGlow", isFirstLoad, revert);

            // Astrum Deus
            ReplaceSprite("DeusH", ref TextureAssets.Npc[ModContent.NPCType<AstrumDeusHead>()], path + "AstrumDeusHead", isFirstLoad, revert);
            ReplaceSprite("DeusHG1", ref AstrumDeusHead.TextureGlow1, path + "AstrumDeusHeadGlow", isFirstLoad, revert);
            ReplaceSprite("DeusHG2", ref AstrumDeusHead.TextureGlow2, path + "AstrumDeusHeadGlow", isFirstLoad, revert);
            ReplaceSprite("DeusHG3", ref AstrumDeusHead.TextureGlow3, path + "AstrumDeusHeadGlow", isFirstLoad, revert);
            ReplaceSprite("DeusHG4", ref AstrumDeusHead.TextureGlow4, path + "AstrumDeusHeadGlow", isFirstLoad, revert);
            ReplaceSprite("DeusB", ref TextureAssets.Npc[ModContent.NPCType<AstrumDeusBody>()], path + "AstrumDeusBody", isFirstLoad, revert);
            ReplaceSprite("DeusB2", ref AstrumDeusBody.AltTexture, path + "AstrumDeusBody2", isFirstLoad, revert);
            ReplaceSprite("DeusBG1", ref AstrumDeusBody.TextureGlow1, path + "AstrumDeusBodyGlow", isFirstLoad, revert);
            ReplaceSprite("DeusBG2", ref AstrumDeusBody.TextureGlow2, path + "AstrumDeusBodyGlow", isFirstLoad, revert);
            ReplaceSprite("DeusBG3", ref AstrumDeusBody.TextureGlow3, path + "AstrumDeusBodyGlow", isFirstLoad, revert);
            ReplaceSprite("DeusBG4", ref AstrumDeusBody.TextureGlow4, path + "AstrumDeusBodyGlow", isFirstLoad, revert);
            ReplaceSprite("DeusB2G1", ref AstrumDeusBody.TextureAltGlow1, path + "AstrumDeusBody2Glow", isFirstLoad, revert);
            ReplaceSprite("DeusB2G2", ref AstrumDeusBody.TextureAltGlow2, path + "AstrumDeusBody2Glow", isFirstLoad, revert);
            ReplaceSprite("DeusT", ref TextureAssets.Npc[ModContent.NPCType<AstrumDeusTail>()], path + "AstrumDeusTail", isFirstLoad, revert);
            ReplaceSprite("DeusTG1", ref AstrumDeusTail.GlowTexture, path + "AstrumDeusTailGlow", isFirstLoad, revert);
            ReplaceSprite("DeusTG2", ref AstrumDeusTail.GlowTexture2, path + "AstrumDeusTailGlow", isFirstLoad, revert);

            // Brimstone
            ReplaceSprite("Br3mmy", ref TextureAssets.Npc[ModContent.NPCType<BrimstoneElemental>()], path + "BrimstoneElemental", isFirstLoad, revert);

			// Bumblebirb
			ReplaceSprite("Birb", ref TextureAssets.Npc[ModContent.NPCType<Dragonfolly>()], path + "Bumblebirb", isFirstLoad, revert);
            ReplaceSprite("BirbGlow", ref Dragonfolly.GlowTexture, path + "BumblebirbGlow", isFirstLoad, revert);
            ReplaceSprite("BirbBaby", ref TextureAssets.Npc[ModContent.NPCType<DraconicSwarmer>()], path + "Bumblebirb", isFirstLoad, revert);
            ReplaceSprite("BirbBaby2", ref TextureAssets.Npc[ModContent.NPCType<WildBumblebirb>()], path + "Bumblebirb", isFirstLoad, revert);

            // Calamitas Clone
            ReplaceSprite("Calclone", ref TextureAssets.Npc[ModContent.NPCType<CalamitasClone>()], path + "CalamitasClone", isFirstLoad, revert);
            ReplaceSprite("Calclone2", ref CalamitasClone.GlowTexture, path + "CalamitasCloneGlow", isFirstLoad, revert);
            ReplaceSprite("Cataclysm", ref TextureAssets.Npc[ModContent.NPCType<Cataclysm>()], path + "CataclysmClone", isFirstLoad, revert);
            ReplaceSprite("Cataclysm2", ref Cataclysm.GlowTexture, nothing, isFirstLoad, revert);
            ReplaceSprite("Catastrophe", ref TextureAssets.Npc[ModContent.NPCType<Catastrophe>()], path + "CatastropheClone", isFirstLoad, revert);
            ReplaceSprite("Catastrophe2", ref Catastrophe.GlowTexture, nothing, isFirstLoad, revert);

            // Ceaseless Void
            // wotg replaces CV with a custom rift
            if (CalValEX.instance.wotg == null)
            {
                ReplaceSprite("CV", ref TextureAssets.Npc[ModContent.NPCType<CeaselessVoid>()], path + "CeaselessVoid", isFirstLoad, revert);
                ReplaceSprite("CVGlow", ref CeaselessVoid.GlowTexture, nothing, isFirstLoad, revert);
            }

			// Crabulon
			ReplaceSprite("Carb", ref TextureAssets.Npc[ModContent.NPCType<Crabulon>()], path + "Crabulon", isFirstLoad, revert);
			ReplaceSprite("Carb2", ref Crabulon.AltTexture, path + "CrabulonWalk", isFirstLoad, revert);
			ReplaceSprite("Carb3", ref Crabulon.AttackTexture, path + "CrabulonSmash", isFirstLoad, revert);
			ReplaceSprite("CarbG", ref Crabulon.Texture_Glow, path + "Crabulon_Glow", isFirstLoad, revert);
			ReplaceSprite("Carb2G", ref Crabulon.AltTexture_Glow, path + "CrabulonWalk_Glow", isFirstLoad, revert);
			ReplaceSprite("Carb3G", ref Crabulon.AttackTexture_Glow, path + "CrabulonSmash_Glow", isFirstLoad, revert);

			// Cryogen
			ReplaceSprite("Cirno1", ref TextureAssets.Npc[ModContent.NPCType<Cryogen>()], path + "Cryogen", isFirstLoad, revert);
            ReplaceSprite("Cirno2", ref Cryogen.Phase2Texture, path + "Cryogen2", isFirstLoad, revert);
            ReplaceSprite("Cirno3", ref Cryogen.Phase3Texture, path + "Cryogen3", isFirstLoad, revert);
            ReplaceSprite("Cirno4", ref Cryogen.Phase4Texture, path + "Cryogen4", isFirstLoad, revert);
            ReplaceSprite("Cirno5", ref Cryogen.Phase5Texture, path + "Cryogen5", isFirstLoad, revert);
            ReplaceSprite("Cirno6", ref Cryogen.Phase6Texture, path + "Cryogen6", isFirstLoad, revert);
			ReplaceSprite("CirnoWings", ref TextureAssets.Npc[ModContent.NPCType<CryogenShield>()], path + "CryogenShield", isFirstLoad, revert);

			// Desert Scourge
			ReplaceSprite("DS", ref TextureAssets.Npc[ModContent.NPCType<DesertScourgeHead>()], path + "DesertScourgeHead", isFirstLoad, revert);
			ReplaceSprite("DS2", ref TextureAssets.Npc[ModContent.NPCType<DesertScourgeBody>()], path + "DesertScourgeBody", isFirstLoad, revert);
            ReplaceSprite("DS3", ref DesertScourgeBody.BodyTexture2, path + "DesertScourgeBody2", isFirstLoad, revert);
            ReplaceSprite("DS4", ref DesertScourgeBody.BodyTexture3, path + "DesertScourgeBody3", isFirstLoad, revert);
            ReplaceSprite("DS5", ref DesertScourgeBody.BodyTexture4, path + "DesertScourgeBody4", isFirstLoad, revert);
            ReplaceSprite("DS6", ref TextureAssets.Npc[ModContent.NPCType<DesertScourgeTail>()], path + "DesertScourgeTail", isFirstLoad, revert);
            // Desert Nuisance
			ReplaceSprite("DN1", ref TextureAssets.Npc[ModContent.NPCType<DesertNuisanceHead>()], path + "DesertScourgeHead", isFirstLoad, revert);
			ReplaceSprite("DN2", ref TextureAssets.Npc[ModContent.NPCType<DesertNuisanceBody>()], path + "DesertScourgeBody", isFirstLoad, revert);
            ReplaceSprite("DN3", ref DesertNuisanceBody.BodyTexture2, path + "DesertScourgeBody2", isFirstLoad, revert);
            ReplaceSprite("DN4", ref DesertNuisanceBody.BodyTexture3, path + "DesertScourgeBody3", isFirstLoad, revert);
            ReplaceSprite("DN5", ref DesertNuisanceBody.BodyTexture4, path + "DesertScourgeBody4", isFirstLoad, revert);
            ReplaceSprite("DN6", ref TextureAssets.Npc[ModContent.NPCType<DesertNuisanceTail>()], path + "DesertScourgeTail", isFirstLoad, revert);
            // Baby nuisance
			ReplaceSprite("DNY1", ref TextureAssets.Npc[ModContent.NPCType<DesertNuisanceHeadYoung>()], path + "DesertScourgeHead", isFirstLoad, revert);
			ReplaceSprite("DNY2", ref TextureAssets.Npc[ModContent.NPCType<DesertNuisanceBodyYoung>()], path + "DesertScourgeBody", isFirstLoad, revert);
            ReplaceSprite("DNY3", ref DesertNuisanceBodyYoung.BodyTexture2, path + "DesertScourgeBody2", isFirstLoad, revert);
            ReplaceSprite("DNY4", ref DesertNuisanceBodyYoung.BodyTexture3, path + "DesertScourgeBody3", isFirstLoad, revert);
            ReplaceSprite("DNY5", ref DesertNuisanceBodyYoung.BodyTexture4, path + "DesertScourgeBody4", isFirstLoad, revert);
            ReplaceSprite("DNY6", ref TextureAssets.Npc[ModContent.NPCType<DesertNuisanceTailYoung>()], path + "DesertScourgeTail", isFirstLoad, revert);

            // Exo Mechs
            // Draedon
            ReplaceSprite("Draedon", ref TextureAssets.Npc[ModContent.NPCType<Draedon>()], path + "Draedon", isFirstLoad, revert);
            ReplaceSprite("DraedonG", ref Draedon.Texture_Glow, path + "DraedonGlow", isFirstLoad, revert);
            ReplaceSprite("HoloDraedon", ref Draedon.HoloTexture, path + "HologramDraedon", isFirstLoad, revert);
            // Ares
            ReplaceSprite("AresBody", ref TextureAssets.Npc[ModContent.NPCType<AresBody>()], path + "AresBody", isFirstLoad, revert);
            ReplaceSprite("AresBodyGlow", ref AresBody.GlowTexture, path + "AresBodyGlow", isFirstLoad, revert);
            ReplaceSprite("Ares1", ref AresBody.ArmBottomConnectorTexture, path + "AresBottomArmConnector", isFirstLoad, revert);
            ReplaceSprite("Ares2", ref AresBody.ArmBottomShoulderTexture, path + "AresBottomArmShoulder", isFirstLoad, revert);
            ReplaceSprite("Ares3", ref AresBody.ArmBottomTexture, path + "AresBottomArm1", isFirstLoad, revert);
            ReplaceSprite("Ares4", ref AresBody.ArmBottomTexture2, path + "AresBottomArm2", isFirstLoad, revert);
            ReplaceSprite("Ares5", ref AresBody.ArmSegmentTexture, path + "AresTopArmSegment", isFirstLoad, revert);
            ReplaceSprite("Ares6", ref AresBody.ArmTopShoulderTexture, path + "AresTopArmShoulder", isFirstLoad, revert);
            ReplaceSprite("Ares7", ref AresBody.ArmTopTexture, path + "AresTopArm1", isFirstLoad, revert);
            ReplaceSprite("Ares8", ref AresBody.ArmTopTexture2, path + "AresTopArm2", isFirstLoad, revert);
            ReplaceSprite("Ares9", ref AresBody.ArmBottomShoulderTexture_Glow, nothing, isFirstLoad, revert);
            ReplaceSprite("Ares10", ref AresBody.ArmBottomTexture_Glow, path + "AresBottomArm1Glow", isFirstLoad, revert);
            ReplaceSprite("Ares11", ref AresBody.ArmBottomTexture2_Glow, nothing, isFirstLoad, revert);
            ReplaceSprite("Ares12", ref AresBody.ArmSegmentTexture_Glow, path + "AresTopArmSegmentGlow", isFirstLoad, revert);
            ReplaceSprite("Ares13", ref AresBody.ArmTopShoulderTexture_Glow, path + "AresTopArmShoulderGlow", isFirstLoad, revert);
            ReplaceSprite("Ares14", ref AresBody.ArmTopTexture2_Glow, path + "AresTopArm2Glow", isFirstLoad, revert);
            ReplaceSprite("AresAmongauss", ref TextureAssets.Npc[ModContent.NPCType<AresGaussNuke>()], path + "AresGaussCannon", isFirstLoad, revert);
            ReplaceSprite("AresAmongaussGlow", ref AresGaussNuke.GlowTexture, path + "AresGaussCannonGlow", isFirstLoad, revert);
            ReplaceSprite("AresTea", ref TextureAssets.Npc[ModContent.NPCType<AresTeslaCannon>()], path + "AresTeslaCannon", isFirstLoad, revert);
            ReplaceSprite("AresTeaGlow", ref AresTeslaCannon.GlowTexture, path + "AresTeslaCannonGlow", isFirstLoad, revert);
            ReplaceSprite("AresLahzar", ref TextureAssets.Npc[ModContent.NPCType<AresLaserCannon>()], path + "AresLaserCannon", isFirstLoad, revert);
            ReplaceSprite("AresLahzarGlow", ref AresLaserCannon.GlowTexture, path + "AresLaserCannonGlow", isFirstLoad, revert);
            ReplaceSprite("AresPlazma", ref TextureAssets.Npc[ModContent.NPCType<AresPlasmaFlamethrower>()], path + "AresPlasmaCannon", isFirstLoad, revert);
            ReplaceSprite("AresPlazmaGlow", ref AresPlasmaFlamethrower.GlowTexture, path + "AresPlasmaCannonGlow", isFirstLoad, revert);
            // Artemis
            ReplaceSprite("Artemis", ref TextureAssets.Npc[ModContent.NPCType<Artemis>()], path + "Artemis", isFirstLoad, revert);
            ReplaceSprite("ArtemisG", ref Artemis.GlowTexture, path + "ArtemisGlow", isFirstLoad, revert);
            // Apollo
            ReplaceSprite("Apollo", ref TextureAssets.Npc[ModContent.NPCType<Apollo>()], path + "Apollo", isFirstLoad, revert);
            ReplaceSprite("ApolloG", ref Apollo.GlowTexture, path + "ApolloGlow", isFirstLoad, revert);
            // Thanatos
            ReplaceSprite("ThanatosHead", ref TextureAssets.Npc[ModContent.NPCType<ThanatosHead>()], path + "ThanatosHead", isFirstLoad, revert);
            ReplaceSprite("ThanatosHeadG", ref ThanatosHead.GlowTexture, path + "ThanatosHeadGlow", isFirstLoad, revert);
            ReplaceSprite("ThanatosBody", ref TextureAssets.Npc[ModContent.NPCType<ThanatosBody1>()], path + "ThanatosBody", isFirstLoad, revert);
            ReplaceSprite("ThanatosBodyG", ref ThanatosBody1.GlowTexture, path + "ThanatosBodyGlow", isFirstLoad, revert);
            ReplaceSprite("ThanatosBody2", ref TextureAssets.Npc[ModContent.NPCType<ThanatosBody2>()], path + "ThanatosBody2", isFirstLoad, revert);
            ReplaceSprite("ThanatosBody2G", ref ThanatosBody2.GlowTexture, path + "ThanatosBody2Glow", isFirstLoad, revert);
            ReplaceSprite("ThanatosTail", ref TextureAssets.Npc[ModContent.NPCType<ThanatosTail>()], path + "ThanatosTail", isFirstLoad, revert);
            ReplaceSprite("ThanatosTailG", ref ThanatosTail.GlowTexture, path + "ThanatosTailGlow", isFirstLoad, revert);

            // Hive Mind
            ReplaceSprite("Me", ref TextureAssets.Npc[ModContent.NPCType<HiveMind>()], path + "HiveMind", isFirstLoad, revert);
            ReplaceSprite("Me2", ref HiveMind.Phase2Texture, path + "HiveMindP2", isFirstLoad, revert);

            // Leviathan
            ReplaceSprite("Ana", ref TextureAssets.Npc[ModContent.NPCType<Anahita>()], path + "Siren", isFirstLoad, revert);
            ReplaceSprite("Banana", ref Anahita.ChargeTexture, path + "SirenCharge", isFirstLoad, revert);
            ReplaceSprite("Levi", ref TextureAssets.Npc[ModContent.NPCType<Leviathan>()], path + "Leviathan", isFirstLoad, revert);
            ReplaceSprite("LeviAlt", ref Leviathan.AttackTexture, path + "LeviathanAttack", isFirstLoad, revert);

            // Old Duke
            ReplaceSprite("Bad", ref TextureAssets.Npc[ModContent.NPCType<OldDuke>()], path + "OldDuke", isFirstLoad, revert);
            ReplaceSprite("BadGlow", ref OldDuke.GlowTexture, nothing, isFirstLoad, revert);

            // Perforators
            ReplaceSprite("Perf", ref TextureAssets.Npc[ModContent.NPCType<PerforatorHive>()], path + "PerforatorHive", isFirstLoad, revert);
			ReplaceSprite("PerfGlow", ref PerforatorHive.GlowTexture, path + "PerforatorHive_Glow", isFirstLoad, revert);
			// Large Perforator
			ReplaceSprite("PerfLH", ref TextureAssets.Npc[ModContent.NPCType<PerforatorHeadLarge>()], path + "PerforatorLargeHead", isFirstLoad, revert);
			ReplaceSprite("PerfLB", ref TextureAssets.Npc[ModContent.NPCType<PerforatorBodyLarge>()], path + "PerforatorLargeBody", isFirstLoad, revert);
			ReplaceSprite("PerfLT", ref TextureAssets.Npc[ModContent.NPCType<PerforatorTailLarge>()], path + "PerforatorLargeTail", isFirstLoad, revert);
			ReplaceSprite("PerfLB2", ref PerforatorBodyLarge.AltTexture, path + "PerforatorLargeBody2", isFirstLoad, revert);
			ReplaceSprite("PerfLHG", ref PerforatorHeadLarge.GlowTexture, path + "PerforatorLargeHead_Glow", isFirstLoad, revert);
			ReplaceSprite("PerfLBG", ref PerforatorBodyLarge.Texture_Glow, nothing, isFirstLoad, revert);
			ReplaceSprite("PerfLBG2", ref PerforatorBodyLarge.AltTexture_Glow, nothing, isFirstLoad, revert);
			ReplaceSprite("PerfLTG", ref PerforatorTailLarge.GlowTexture, nothing, isFirstLoad, revert);
			// Medium Perforator
			ReplaceSprite("PerfMH", ref TextureAssets.Npc[ModContent.NPCType<PerforatorHeadMedium>()], path + "PerforatorMediumHead", isFirstLoad, revert);
			ReplaceSprite("PerfMB", ref TextureAssets.Npc[ModContent.NPCType<PerforatorBodyMedium>()], path + "PerforatorMediumBody", isFirstLoad, revert);
			ReplaceSprite("PerfMT", ref TextureAssets.Npc[ModContent.NPCType<PerforatorTailMedium>()], path + "PerforatorMediumTail", isFirstLoad, revert);
			ReplaceSprite("PerfMHG", ref PerforatorHeadMedium.GlowTexture, path + "PerforatorMediumHead_Glow", isFirstLoad, revert);
			ReplaceSprite("PerfMBG", ref PerforatorBodyMedium.GlowTexture, path + "PerforatorMediumBodyGlow", isFirstLoad, revert);
			ReplaceSprite("PerfMTG", ref PerforatorTailMedium.GlowTexture, nothing, isFirstLoad, revert);
			// Small Perforator
			ReplaceSprite("PerfSH", ref TextureAssets.Npc[ModContent.NPCType<PerforatorHeadSmall>()], path + "PerforatorSmallHead", isFirstLoad, revert);
			ReplaceSprite("PerfSB", ref TextureAssets.Npc[ModContent.NPCType<PerforatorBodySmall>()], path + "PerforatorSmallBody", isFirstLoad, revert);
			ReplaceSprite("PerfST", ref TextureAssets.Npc[ModContent.NPCType<PerforatorTailSmall>()], path + "PerforatorSmallTail", isFirstLoad, revert);
			ReplaceSprite("PerfSHG", ref PerforatorHeadSmall.GlowTexture, path + "PerforatorSmallHead_Glow", isFirstLoad, revert);
			ReplaceSprite("PerfSBG", ref PerforatorBodySmall.GlowTexture, nothing, isFirstLoad, revert);
			ReplaceSprite("PerfSTG", ref PerforatorTailSmall.GlowTexture, nothing, isFirstLoad, revert);
			
            // Plaguebringer Goliath
            ReplaceSprite("PBG", ref TextureAssets.Npc[ModContent.NPCType<PlaguebringerGoliath>()], path + "PBG", isFirstLoad, revert);
            ReplaceSprite("PBGGlow", ref PlaguebringerGoliath.Texture_Glow, path + "PBGGlow", isFirstLoad, revert);
            ReplaceSprite("PBGC", ref PlaguebringerGoliath.ChargeTexture, path + "PBGCharge", isFirstLoad, revert);
            ReplaceSprite("PBGCGlow", ref PlaguebringerGoliath.ChargeTexture_Glow, path + "PBGChargeGlow", isFirstLoad, revert);

            // Polterghast
            ReplaceSprite("Polt", ref TextureAssets.Npc[ModContent.NPCType<Polterghast>()], path + "Polterghast", isFirstLoad, revert);
			ReplaceSprite("Necro", ref TextureAssets.Npc[ModContent.NPCType<PolterPhantom>()], path + "Polterghast", isFirstLoad, revert);
			ReplaceSprite("PoltGlow", ref Polterghast.Texture_Glow, path + "Polterghast_Glow", isFirstLoad, revert);
			ReplaceSprite("EvilPoltGlow", ref Polterghast.Texture_Glow2, nothing, isFirstLoad, revert);
			ReplaceSprite("PoltHook", ref TextureAssets.Npc[ModContent.NPCType<PolterghastHook>()], path + "PolterghastHook", isFirstLoad, revert);
			ReplaceSprite("PoltHookGlow", ref PolterghastHook.GlowTexture, path + "PolterghastHook_Glow", isFirstLoad, revert);
			ReplaceSprite("PoltChain", ref PolterghastHook.ChainTexture, path + "PolterghastChain", isFirstLoad, revert);
			ReplaceSprite("NecroGlow", ref PolterPhantom.GlowTexture, path + "Polterghast_Glow", isFirstLoad, revert);

            // Primordial Wyrm
            ReplaceSprite("WyrmHead", ref TextureAssets.Npc[ModContent.NPCType<PrimordialWyrmHead>()], path + "PrimordialWyrmHead", isFirstLoad, revert);
            ReplaceSprite("WyrmHeadG", ref PrimordialWyrmHead.GlowTexture, path + "PrimordialWyrmHeadGlow", isFirstLoad, revert);
            ReplaceSprite("WyrmBody", ref TextureAssets.Npc[ModContent.NPCType<PrimordialWyrmBody>()], path + "PrimordialWyrmBody", isFirstLoad, revert);
            ReplaceSprite("WyrmBodyG", ref PrimordialWyrmBody.GlowTexture, path + "PrimordialWyrmBodyGlow", isFirstLoad, revert);
            ReplaceSprite("WyrmBody2", ref TextureAssets.Npc[ModContent.NPCType<PrimordialWyrmBodyAlt>()], path + "PrimordialWyrmBody2", isFirstLoad, revert);
            ReplaceSprite("WyrmBody2G", ref PrimordialWyrmBodyAlt.GlowTexture, nothing, isFirstLoad, revert);
            ReplaceSprite("WyrmTail", ref TextureAssets.Npc[ModContent.NPCType<PrimordialWyrmTail>()], path + "PrimordialWyrmTail", isFirstLoad, revert);
            ReplaceSprite("WyrmTailG", ref PrimordialWyrmTail.GlowTexture, path + "PrimordialWyrmTailGlow", isFirstLoad, revert);

            // Profaned Guardians
            ReplaceSprite("DonutOffense", ref TextureAssets.Npc[ModContent.NPCType<ProfanedGuardianCommander>()], path + "ProfanedGuardianCommander", isFirstLoad, revert);
			ReplaceSprite("DonutDefense", ref TextureAssets.Npc[ModContent.NPCType<ProfanedGuardianDefender>()], path + "ProfanedGuardianDefender", isFirstLoad, revert);
			ReplaceSprite("DonutHealer", ref TextureAssets.Npc[ModContent.NPCType<ProfanedGuardianHealer>()], path + "ProfanedGuardianHealer", isFirstLoad, revert);
			ReplaceSprite("DunkinOffense", ref TextureAssets.Npc[ModContent.NPCType<ProvSpawnOffense>()], path + "ProfanedGuardianCommander", isFirstLoad, revert);
			ReplaceSprite("DunkinDefense", ref TextureAssets.Npc[ModContent.NPCType<ProvSpawnHealer>()], path + "ProfanedGuardianHealer", isFirstLoad, revert);
			ReplaceSprite("DunkinHealer", ref TextureAssets.Npc[ModContent.NPCType<ProvSpawnDefense>()], path + "ProfanedGuardianDefender", isFirstLoad, revert);
			ReplaceSprite("DonutOffenseG", ref ProfanedGuardianCommander.Texture_Glow, path + "ProfanedGuardianCommanderGlow", isFirstLoad, revert);
			ReplaceSprite("DonutOffenseGN", ref ProfanedGuardianCommander.TextureNight_Glow, path + "ProfanedGuardianCommanderGlow", isFirstLoad, revert);
			ReplaceSprite("DonutDefenseG", ref ProfanedGuardianDefender.Texture_Glow, nothing, isFirstLoad, revert);
			ReplaceSprite("DonutDefenseGN", ref ProfanedGuardianDefender.TextureNight_Glow, nothing, isFirstLoad, revert);
			ReplaceSprite("DonutHealerG", ref ProfanedGuardianHealer.Texture_Glow, nothing, isFirstLoad, revert);
			ReplaceSprite("DonutHealerGN", ref ProfanedGuardianHealer.TextureNight_Glow, nothing, isFirstLoad, revert);
			ReplaceSprite("DonutHealerCrystal", ref ProfanedGuardianHealer.Texture_Glow2, path + "ProfanedGuardianCommanderGlow", isFirstLoad, revert);

            // Providence...
            #region providence
            ReplaceSprite("Prov", ref TextureAssets.Npc[ModContent.NPCType<Providence>()], path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov2", ref Providence.TextureAlt, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov3", ref Providence.TextureAttack, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov4", ref Providence.TextureAttackAlt, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov5", ref Providence.TextureDefense, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov6", ref Providence.TextureDefenseAlt, path + "Providence", isFirstLoad, revert);

			ReplaceSprite("Prov7", ref Providence.TextureNight, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov8", ref Providence.TextureAltNight, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov9", ref Providence.TextureAttackNight, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov10", ref Providence.TextureAttackAltNight, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov11", ref Providence.TextureDefenseNight, path + "Providence", isFirstLoad, revert);
			ReplaceSprite("Prov12", ref Providence.TextureDefenseAltNight, path + "Providence", isFirstLoad, revert);

			ReplaceSprite("Prov13", ref Providence.Texture_Glow, path + "ProvidenceFire", isFirstLoad, revert);
			ReplaceSprite("Prov14", ref Providence.TextureAlt_Glow, path + "ProvidenceFire", isFirstLoad, revert);
			ReplaceSprite("Prov15", ref Providence.TextureAttack_Glow, path + "ProvidenceFire", isFirstLoad, revert);
			ReplaceSprite("Prov16", ref Providence.TextureAttackAlt_Glow, path + "ProvidenceFire", isFirstLoad, revert);
			ReplaceSprite("Prov17", ref Providence.TextureDefense_Glow, path + "ProvidenceFire", isFirstLoad, revert);
			ReplaceSprite("Prov18", ref Providence.TextureDefenseAlt_Glow, path + "ProvidenceFire", isFirstLoad, revert);

			ReplaceSprite("Prov19", ref Providence.TextureNight_Glow, path + "ProvidenceFireNight", isFirstLoad, revert);
			ReplaceSprite("Prov20", ref Providence.TextureAltNight_Glow, path + "ProvidenceFireNight", isFirstLoad, revert);
			ReplaceSprite("Prov21", ref Providence.TextureAttackNight_Glow, path + "ProvidenceFireNight", isFirstLoad, revert);
			ReplaceSprite("Prov22", ref Providence.TextureAttackAltNight_Glow, path + "ProvidenceFireNight", isFirstLoad, revert);
			ReplaceSprite("Prov23", ref Providence.TextureDefenseNight_Glow, path + "ProvidenceFireNight", isFirstLoad, revert);
			ReplaceSprite("Prov24", ref Providence.TextureDefenseAltNight_Glow, path + "ProvidenceFireNight", isFirstLoad, revert);

			ReplaceSprite("Prov25", ref Providence.Texture_Glow_2, path + "ProvidenceCrystal", isFirstLoad, revert);
			ReplaceSprite("Prov26", ref Providence.TextureAlt_Glow_2, path + "ProvidenceCrystal", isFirstLoad, revert);
			ReplaceSprite("Prov27", ref Providence.TextureAttack_Glow_2, path + "ProvidenceCrystal", isFirstLoad, revert);
			ReplaceSprite("Prov28", ref Providence.TextureAttackAlt_Glow_2, path + "ProvidenceCrystal", isFirstLoad, revert);
			ReplaceSprite("Prov29", ref Providence.TextureDefense_Glow_2, path + "ProvidenceCrystal", isFirstLoad, revert);
			ReplaceSprite("Prov30", ref Providence.TextureDefenseAlt_Glow_2, path + "ProvidenceCrystal", isFirstLoad, revert);

			ReplaceSprite("Prov31", ref Providence.TextureNight_Glow_2, path + "ProvidenceCrystalNight", isFirstLoad, revert);
			ReplaceSprite("Prov32", ref Providence.TextureAltNight_Glow_2, path + "ProvidenceCrystalNight", isFirstLoad, revert);
			ReplaceSprite("Prov33", ref Providence.TextureAttackNight_Glow_2, path + "ProvidenceCrystalNight", isFirstLoad, revert);
			ReplaceSprite("Prov34", ref Providence.TextureAttackAltNight_Glow_2, path + "ProvidenceCrystalNight", isFirstLoad, revert);
			ReplaceSprite("Prov35", ref Providence.TextureDefenseNight_Glow_2, path + "ProvidenceCrystalNight", isFirstLoad, revert);
			ReplaceSprite("Prov36", ref Providence.TextureDefenseAltNight_Glow_2, path + "ProvidenceCrystalNight", isFirstLoad, revert);
            #endregion

            // Ravager
            ReplaceSprite("Ravager", ref TextureAssets.Npc[ModContent.NPCType<RavagerBody>()], path + "RavagerBody", isFirstLoad, revert);
            ReplaceSprite("RavagerHead", ref TextureAssets.Npc[ModContent.NPCType<RavagerHead>()], path + "RavagerHead", isFirstLoad, revert);
            ReplaceSprite("RavagerHead2", ref TextureAssets.Npc[ModContent.NPCType<RavagerHead2>()], path + "RavagerHead2", isFirstLoad, revert);
            ReplaceSprite("RavagerHandL", ref TextureAssets.Npc[ModContent.NPCType<RavagerClawLeft>()], path + "RavagerClawLeft", isFirstLoad, revert);
            ReplaceSprite("RavagerHandR", ref TextureAssets.Npc[ModContent.NPCType<RavagerClawRight>()], path + "RavagerClawRight", isFirstLoad, revert);
            ReplaceSprite("RavagerLegL", ref TextureAssets.Npc[ModContent.NPCType<RavagerLegLeft>()], path + "RavagerLegLeft", isFirstLoad, revert);
            ReplaceSprite("RavagerLegR", ref TextureAssets.Npc[ModContent.NPCType<RavagerLegRight>()], path + "RavagerLegRight", isFirstLoad, revert);
            ReplaceSprite("RavagerChain", ref RavagerBody.ChainTexture, path + "RavagerChain", isFirstLoad, revert);
            ReplaceSprite("RavagerGlow", ref RavagerBody.GlowTexture, nothing, isFirstLoad, revert);

            // Signus
            ReplaceSprite("Signut", ref TextureAssets.Npc[ModContent.NPCType<Signus>()], path + "Signus", isFirstLoad, revert);
            ReplaceSprite("SignutG", ref Signus.Texture_Glow, path + "SignusGlow", isFirstLoad, revert);
            ReplaceSprite("Signut2", ref Signus.AltTexture, path + "Signus2", isFirstLoad, revert);
            ReplaceSprite("Signut2G", ref Signus.AltTexture_Glow, path + "Signus2Glow", isFirstLoad, revert);
            ReplaceSprite("Signut3", ref Signus.AltTexture2, path + "Signus3", isFirstLoad, revert);
            ReplaceSprite("Signut3G", ref Signus.AltTexture2_Glow, path + "Signus3Glow", isFirstLoad, revert);

            // Slime God
            ReplaceSprite("Elizabeth", ref TextureAssets.Npc[ModContent.NPCType<SlimeGodCore>()], path + "SlimeGodCore", isFirstLoad, revert);
			ReplaceSprite("Cor", ref TextureAssets.Npc[ModContent.NPCType<EbonianPaladin>()], path + "EbonianPaladin", isFirstLoad, revert);
			ReplaceSprite("Crim", ref TextureAssets.Npc[ModContent.NPCType<CrimulanPaladin>()], path + "CrimulanPaladin", isFirstLoad, revert);
			ReplaceSprite("CorS", ref TextureAssets.Npc[ModContent.NPCType<SplitEbonianPaladin>()], path + "EbonianPaladin", isFirstLoad, revert);
			ReplaceSprite("CrimS", ref TextureAssets.Npc[ModContent.NPCType<SplitCrimulanPaladin>()], path + "CrimulanPaladin", isFirstLoad, revert);

            // Storm Weaver
            ReplaceSprite("WeavHead", ref TextureAssets.Npc[ModContent.NPCType<StormWeaverHead>()], path + "StormWeaverHead", isFirstLoad, revert);
            ReplaceSprite("WeavBody", ref TextureAssets.Npc[ModContent.NPCType<StormWeaverBody>()], path + "StormWeaverBody", isFirstLoad, revert);
            ReplaceSprite("WeavTail", ref TextureAssets.Npc[ModContent.NPCType<StormWeaverTail>()], path + "StormWeaverTail", isFirstLoad, revert);
            ReplaceSprite("WeavHead2", ref StormWeaverHead.Phase2Texture, path + "StormWeaverHead2", isFirstLoad, revert);
            ReplaceSprite("WeavBody2", ref StormWeaverBody.Phase2Texture, path + "StormWeaverBody2", isFirstLoad, revert);
            ReplaceSprite("WeavTail2", ref StormWeaverTail.Phase2Texture, path + "StormWeaverTail2", isFirstLoad, revert);
            ReplaceSprite("WeavTailGlow", ref StormWeaverTail.GlowTexture, path + "StormWeaverTailGlow", isFirstLoad, revert);

            // Supreme Calamitas
            ReplaceSprite("SCal", ref TextureAssets.Npc[ModContent.NPCType<SupremeCalamitas>()], path + "SCal", isFirstLoad, revert);
            ReplaceSprite("Scatas", ref TextureAssets.Npc[ModContent.NPCType<SupremeCatastrophe>()], path + "SCatastrophe", isFirstLoad, revert);
            ReplaceSprite("ScatasG", ref SupremeCatastrophe.GlowTexture, path + "SCatastropheGlow", isFirstLoad, revert);
            ReplaceSprite("Scatac", ref TextureAssets.Npc[ModContent.NPCType<SupremeCataclysm>()], path + "SCataclysm", isFirstLoad, revert);
            ReplaceSprite("ScatacG", ref SupremeCataclysm.GlowTexture, path + "SCataclysmGlow", isFirstLoad, revert);
            ReplaceSprite("Sep", ref TextureAssets.Npc[ModContent.NPCType<SepulcherHead>()], path + "SepulcherHead", isFirstLoad, revert);
            ReplaceSprite("Sep2", ref TextureAssets.Npc[ModContent.NPCType<SepulcherBody>()], path + "SepulcherBody", isFirstLoad, revert);
            ReplaceSprite("Sep3", ref TextureAssets.Npc[ModContent.NPCType<SepulcherBodyEnergyBall>()], path + "SepulcherBodyEnergyBall", isFirstLoad, revert);
            ReplaceSprite("Sep4", ref TextureAssets.Npc[ModContent.NPCType<SepulcherArm>()], path + "SepulcherArm2", isFirstLoad, revert);
            ReplaceSprite("Sep5", ref TextureAssets.Npc[ModContent.NPCType<SepulcherTail>()], path + "SepulcherTail", isFirstLoad, revert);
            ReplaceSprite("Sep6", ref SepulcherBody.AltTexture, path + "SepulcherBody2", isFirstLoad, revert);
            ReplaceSprite("Sep7", ref SepulcherArm.ForearmTexture, path + "SepulcherArm", isFirstLoad, revert);
            ReplaceSprite("Sep8", ref SepulcherArm.HandTexture, path + "SepulcherHand", isFirstLoad, revert);

            // DoG
            ReplaceSprite("DoG", ref TextureAssets.Npc[ModContent.NPCType<DevourerofGodsHead>()], path + "DogHead", isFirstLoad, revert);
			ReplaceSprite("DoG2", ref TextureAssets.Npc[ModContent.NPCType<DevourerofGodsBody>()],  path + "DogBody", isFirstLoad, revert);
			ReplaceSprite("DoG3", ref TextureAssets.Npc[ModContent.NPCType<DevourerofGodsTail>()], path + "DogTail", isFirstLoad, revert);
            ReplaceSprite("DoGDJ", ref DevourerofGodsHead.GodSlayerDashJawTexture, nothing, isFirstLoad, revert);
            ReplaceSprite("DoGJ", ref DevourerofGodsHead.JawTexture, nothing, isFirstLoad, revert);
            ReplaceSprite("DoGJG", ref DevourerofGodsHead.JawTexture_Glow, nothing, isFirstLoad, revert);
            ReplaceSprite("DoGG", ref DevourerofGodsHead.Texture_Glow_Cyan, path + "DogHeadGlow", isFirstLoad, revert);
			ReplaceSprite("DoGG2", ref DevourerofGodsHead.Texture_Glow_Purple, nothing, isFirstLoad, revert);
			ReplaceSprite("DoG2G", ref DevourerofGodsBody.Texture_Glow, nothing, isFirstLoad, revert);
			ReplaceSprite("DoG2G2", ref DevourerofGodsTail.Texture_Glow_Cyan, path + "DogTailGlow", isFirstLoad, revert);
			ReplaceSprite("DoG3G", ref DevourerofGodsTail.Texture_Glow_Purple, nothing, isFirstLoad, revert);
			// P2
			ReplaceSprite("DoGST", ref DevourerofGodsHead.TextureP2_Full, path + "DogSHead", isFirstLoad, revert);
            ReplaceSprite("DoGS", ref DevourerofGodsHead.TextureP2, path + "DogSHead", isFirstLoad, revert);
            ReplaceSprite("DoGSJ", ref DevourerofGodsHead.JawTextureP2, nothing, isFirstLoad, revert);
            ReplaceSprite("DoGSJG", ref DevourerofGodsHead.JawTextureP2_Glow, nothing, isFirstLoad, revert);
            ReplaceSprite("DoGS2", ref DevourerofGodsBody.TextureP2, path + "DogSBody", isFirstLoad, revert);
			ReplaceSprite("DoGS3", ref DevourerofGodsTail.TextureP2, path + "DogSTail", isFirstLoad, revert);
			ReplaceSprite("DoGSG", ref DevourerofGodsHead.TextureP2_Glow_Cyan, path + "DogSHeadGlow", isFirstLoad, revert);
			ReplaceSprite("DoGSG2", ref DevourerofGodsHead.TextureP2_Glow_Purple, nothing, isFirstLoad, revert);
			ReplaceSprite("DoGS2G", ref DevourerofGodsBody.TextureP2_Glow_Cyan, nothing, isFirstLoad, revert);
			ReplaceSprite("DoGS2G2", ref DevourerofGodsBody.TextureP2_Glow_Purple, nothing, isFirstLoad, revert);
			ReplaceSprite("DoGS3G", ref DevourerofGodsTail.TextureP2_Glow_Cyan, path + "DogSTailGlow", isFirstLoad, revert);
			ReplaceSprite("DoGS3G2", ref DevourerofGodsTail.TextureP2_Glow_Purple, nothing, isFirstLoad, revert);

			// Yharon
			ReplaceSprite("Yharon", ref TextureAssets.Npc[ModContent.NPCType<Yharon>()], path + "Yharon", isFirstLoad, revert);
			ReplaceSprite("Yhurple", ref Yharon.GlowTexturePurple, path + "YharonShackles", isFirstLoad, revert);
			ReplaceSprite("Yhreen", ref Yharon.GlowTextureGreen, nothing, isFirstLoad, revert);
			ReplaceSprite("Yhorange", ref Yharon.GlowTextureOrange, path + "YharonEyes", isFirstLoad, revert);
		}
    }
}