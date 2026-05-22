using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CalValEX.AprilFools.Meldosaurus
{
    [AutoloadBossHead]
	public class Meldosaurus : ModNPC

	{
		public bool ischarginng;
		public bool fate;
		public int chargecount;
		public int framebuffer = 0;
		public int framecounter = 0;
		public override void SetStaticDefaults()
		{
			NPCID.Sets.TrailingMode[NPC.type] = 2;
			NPCID.Sets.TrailCacheLength[NPC.type] = 21;
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
			NPC.damage = 110;
			NPC.width = 118;
			NPC.height = 84;
			NPC.defense = 10;
			NPC.lifeMax = 100000;
			NPC.boss = true;
			NPC.aiStyle = -1;
			Main.npcFrameCount[NPC.type] = 7;
			NPC.knockBackResist = 0f;
			NPC.value = Item.buyPrice(0, 10, 0, 0);
			NPC.lavaImmune = true;
			NPC.behindTiles = false;
			NPC.noTileCollide = false;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Meldosaurus");
			NPC.netAlways = true;
			if (CalValEX.CalamityActive)
			{
				CalValEX.Calamity.Call("SetDefenseDamageNPC", true);
				CalValEX.Calamity.Call("SetDamageReductionSpecific", 0.1f);
				NPC.lifeMax = (bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance") ? 140000 : 100000;
			}
		}
		public override void SetBestiary(Terraria.GameContent.Bestiary.BestiaryDatabase database, Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry)
		{
			if (CalValEX.AprilFoolMonth)
			{
				bestiaryEntry.Info.AddRange(new Terraria.GameContent.Bestiary.IBestiaryInfoElement[] {
				Terraria.GameContent.Bestiary.BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				new Terraria.GameContent.Bestiary.FlavorTextBestiaryInfoElement($"Mods.CalValEX.Bestiary.{Name}")
				//("Evolution corrupted by the remains of the dark gods. It seeks acceptance from the other celestial creatures to no success."),
			});
			}
		}

		public override void AI()
		{
			bool expert = Main.expertMode;
			bool revenge = false;
			bool death = false;
			bool malice = false;
			if (CalValEX.CalamityActive)
			{
				expert = (bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush") || Main.expertMode;
				revenge = (bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush") || (bool)CalValEX.Calamity.Call("GetDifficultyActive", "revengeance");
				death = (bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush") || (bool)CalValEX.Calamity.Call("GetDifficultyActive", "death");
				malice = (bool)CalValEX.Calamity.Call("GetDifficultyActive", "bossrush");
			}
			if (!malice)
            {
				if (!Main.raining)
                {
					if (Main.netMode != NetmodeID.MultiplayerClient)
					Main.StartRain();
					Main.SyncRain();
                }
            }
			CalValEXGlobalNPC.meldodon = NPC.whoAmI;
			//Die if it isnt april
			if (!CalValEX.AprilFoolMonth && !Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().Blok)
			{
				NPC.active = false;
			}	
			if (malice && NPC.ai[0] != 5 && NPC.ai[0] != 6) //This is fine
            {
				NPC.ai[1]++;
            }
			if (malice)
            {
				NPC.ai[2]++;
            }
			//Under 5% health become desperate
			if (NPC.life <= NPC.lifeMax * 0.05f)
            {
				NPC.ai[0] = 10;
            }
			//It begins
			if (NPC.ai[0] == 0)
            {
				NPC.ai[1]++;
				if (NPC.ai[1] >= 60)
				{
					if (CalValEXWorld.amogus && !Main.LocalPlayer.GetModPlayer<CalValEXPlayer>().amogus)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<Amogus>(), 0, 0, Main.myPlayer);
					}
					NPC.velocity.Y = -4f;
					NewPhase(1);
                }
            }
			//Hop while vomitting 
			if (NPC.ai[0] == 1)
            {
				NPC.rotation = 0;
				NPC.noGravity = false;
				NPC.noTileCollide = false;
				NPC.ai[1]++;
				NPC.ai[2]++;
				if (NPC.velocity.Y >= 19)
                {
					NPC.ai[3]++;
				}
				if (NPC.velocity.Y == 0)
                {
					NPC.velocity.Y = -12;
				}
				if (NPC.ai[3] >= 10)
                {
					NPC.velocity.Y = 0;
					NPC.ai[3] = 0;
                }
				//If very slow, switch directions
				else if (NPC.ai[2] >= 30)
				{
					Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
					for (int x = 0; x < 12; x++)
                    {
						if (Main.netMode != NetmodeID.MultiplayerClient)
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position.X, NPC.position.Y, Main.rand.Next(-30, 30), Main.rand.Next(-20, -12), ModContent.ProjectileType<EntropicVomit>(), Main.expertMode ? 25 : 30, 0f, Main.myPlayer, Main.rand.Next(1,4));
                    }
					NPC.ai[2] = 0;
                }
				if (NPC.ai[1] >= 300)
                {
					NewPhase(2);
                }
            }
			//Speen
			if (NPC.ai[0] == 2)
            {
				NPC.ai[1]++;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.rotation += Main.rand.Next((int)0.20f, (int)0.30f);
				NPC.velocity.Y = 0;
				NPC.velocity.X = 0;
				if (NPC.ai[1] >= 60)
                {
					NewPhase(3);
                }
            }
			//God Belows jet 
			if (NPC.ai[0] == 3)
			{
				ischarginng = false;
				NPC.TargetClosest();
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.ai[1]++;
				NPC.ai[2]++;
				int direcX = NPC.direction *-1;
				int direcY = NPC.directionY *-1;
				int proj = ProjectileID.CursedFlameHostile;
				if (CalValEX.CalamityActive)
				{
					proj = Mod.Find<ModProjectile>("GodsFire").Type;
				}
				if (NPC.ai[2] >= 5)
				{
					Terraria.Audio.SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position.X, NPC.position.Y, NPC.velocity.X * -0.1f, NPC.velocity.Y * -0.1f, proj, Main.expertMode ? 25 : 30, 0f);
					NPC.ai[2] = 0;
				}
				if (NPC.ai[1] <= 90)
				{
					NPC.velocity.X = direcX * 8;
					NPC.velocity.Y = direcY * 8;
				}
				else if (NPC.ai[1] == 31 | NPC.ai[1] == 91 || NPC.ai[1] == 131 || NPC.ai[1] == 181 || NPC.ai[1] == 231 || NPC.ai[1] == 281 || NPC.ai[1] == 331)
				{
					int chargespeed = revenge ? 22 : 18;
					NPC.rotation = NPC.velocity.ToRotation();
					Terraria.Audio.SoundEngine.PlaySound(SoundID.Item111, NPC.Center);
					Vector2 position = NPC.Center;
					position.X = NPC.Center.X + (30f * NPC.direction);
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					NPC.velocity = direction * chargespeed;
					int dmg = Main.expertMode ? 25 : 30;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 10, 10, ModContent.ProjectileType<EntropicVomit>(), dmg, 0, Main.myPlayer, Main.rand.Next(1, 4));
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 10, -10, ModContent.ProjectileType<EntropicVomit>(), dmg, 0, Main.myPlayer, Main.rand.Next(1, 4));
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, -10, 10, ModContent.ProjectileType<EntropicVomit>(), dmg, 0, Main.myPlayer, Main.rand.Next(1, 4));
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, -10, -10, ModContent.ProjectileType<EntropicVomit>(), dmg, 0, Main.myPlayer, Main.rand.Next(1, 4));
					}
				}
				if (NPC.ai[1] >= 381)
                {
					if (!fate)
						NewPhase(4);
					else
					{
						if (Main.rand.NextBool(2))
							NewPhase(5);
						else
							NewPhase(6);
					}

				}
            }
			//Tome of fates. Only happens once in the fight
			if (NPC.ai[0] == 4)
			{

				ischarginng = false;
				fate = true;
				NPC.rotation = 0;
				NPC.TargetClosest();
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0;
				if (NPC.ai[1] == 0)
				{
					for (int x = 0; x < 20; x++)
					{
						Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Shadowflame, 0, 0);
					}
				}
				NPC.ai[1]++;
				NPC.ai[3]++;
				if (NPC.ai[1] == 1)
                {
					for (int x = 0; x < 20; x++)
                    {
						Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Shadowflame, 0, 0);
                    }
					NPC.position.X = Main.player[NPC.target].Center.X + 400;
					NPC.position.Y = Main.player[NPC.target].Center.Y - 280;
				}
				if (NPC.ai[1] == 30)
				{
					Terraria.Audio.SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("Terraria/Sounds/Zombie_5"), NPC.Center);
					Vector2 position = NPC.Center;
					position.X = NPC.Center.X + (10f * NPC.direction);
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					float speed = 10f;
					int type = ModContent.ProjectileType<TomeoFates>();
					int damage = Main.expertMode ? 5 : 10;
					if (Main.netMode != NetmodeID.MultiplayerClient)
					Projectile.NewProjectile(NPC.GetSource_FromAI(), position, direction * speed, type, damage, 0f, Main.myPlayer);
				}
				if (NPC.ai[1] >= 60)
                {
					var thisRect = NPC.getRect();

					for (int i = 0; i < Main.maxProjectiles; i++)
					{
						var proj = Main.projectile[i];

						if (proj != null && proj.active && proj.getRect().Intersects(thisRect) && proj.type == ModContent.ProjectileType<TomeoFates>())
						{
							framebuffer = 0;
							framecounter = 0;
							NPC.ai[3] = 2;
							NPC.HitInfo bruh = new();
							bruh.Damage = NPC.lifeMax / 20;
							NPC.StrikeNPC(bruh, noPlayerInteraction: true);
							proj.active = false;
						}
					}
				}
				if (NPC.ai[3] >= 5 && CalValEX.CalamityActive)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						int proj = Mod.Find<ModProjectile>("GodsFire").Type;
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X - 1400, NPC.Center.Y - 200, 0, 80, proj, Main.expertMode ? 25 : 30, 20, 0);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + 600, NPC.Center.Y - 200, 0, 80, proj, Main.expertMode ? 25 : 30, 20, 0);
					}
					NPC.ai[3] = 0;
                }
				if (NPC.ai[1] >= 140)
                {
					NewPhase(5);
                }
            }
			//Top charge
			if (NPC.ai[0] == 5)
			{
				NPC.TargetClosest();
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.ai[1]++;
				if (ischarginng)
				{
					NPC.rotation += 0.25f;
					NPC.ai[2]++;
				}
				NPC.position.Y = Main.player[NPC.target].Center.Y - 250;
				if (NPC.ai[1] < 60)
				{
					NPC.position.X = Main.player[NPC.target].Center.X - 600;
				}
				if (NPC.ai[1] == 60)
				{
					NPC.velocity.X = 20;
					ischarginng = true;
				}
				if (NPC.ai[2] >= 3)
				{
					Terraria.Audio.SoundEngine.PlaySound(SoundID.Item61, NPC.Center);

					if (Main.netMode != NetmodeID.MultiplayerClient)
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position.X, NPC.position.Y, 0, 0, ModContent.ProjectileType<ShardofAntumbra>(), Main.expertMode ? 30 : 40, 0f, NPC.target);
					NPC.ai[2] = 0;
                }
				if (NPC.ai[1] >= 100)
                {
					chargecount += 1;
					ischarginng = false;
					if (Main.expertMode && chargecount < 2)
						NewPhase(6);
					else
					NewPhase(7);
                }
			}
			//Bottom charge
			if (NPC.ai[0] == 6)
			{
				NPC.TargetClosest();
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.ai[1]++;
				if (ischarginng)
				{
					NPC.rotation -= 0.25f;
					NPC.ai[2]++;
				}
				NPC.position.Y = Main.player[NPC.target].Center.Y + 250;
				if (NPC.ai[1] < 60)
				{
					NPC.position.X = Main.player[NPC.target].Center.X + 600;
				}
				if (NPC.ai[1] == 60)
				{
					NPC.velocity.X = -20;
					ischarginng = true;
				}
				if (NPC.ai[2] >= 3)
				{
					Terraria.Audio.SoundEngine.PlaySound(SoundID.Item61, NPC.Center);
					if (Main.netMode != NetmodeID.MultiplayerClient)
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.position.X, NPC.position.Y, 0, 0, ModContent.ProjectileType<ShardofAntumbra>(), Main.expertMode ? 30 : 40, 0f, Main.myPlayer, NPC.target);
					NPC.ai[2] = 0;
				}
				if (NPC.ai[1] >= 100)
				{
					chargecount += 1;
					ischarginng = false;
					if (Main.expertMode && chargecount < 2)
						NewPhase(5);
					else
						NewPhase(7);
				}
			}
			//Weird tp phase
			if (NPC.ai[0] == 7)
            {
				chargecount = 0;
				//NPC.alpha = 255;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.ai[1]++;
				if (NPC.ai[1] >= 60)
				{
					Terraria.Audio.SoundEngine.PlaySound(new Terraria.Audio.SoundStyle("Terraria/Sounds/Zombie_5"), NPC.Center);
					Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
					//NPC.alpha = 0;
					NewPhase(8);
                }
            }
			//Clone ram
			if (NPC.ai[0] == 8)
			{
				bool spawnWorms = NPC.CountNPCS(ModContent.NPCType<MeldWyrmHead>()) < 1 && expert;
				if (!Main.expertMode)
                {
					NPC.alpha = 80;
                }
				int distance = 300;
				int dmg = Main.expertMode ? 33 : 55;
				if (NPC.ai[1] == 0)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(NPC.GetSource_FromAI(), Main.player[NPC.target].position.X - distance, Main.player[NPC.target].position.Y + distance, 0, 0, ModContent.ProjectileType<MeldosaurusClone>(), dmg, 0, Main.myPlayer, NPC.target, 1);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), Main.player[NPC.target].position.X + distance, Main.player[NPC.target].position.Y - distance, 0, 0, ModContent.ProjectileType<MeldosaurusClone>(), dmg, 0, Main.myPlayer, NPC.target, 2);
						Projectile.NewProjectile(NPC.GetSource_FromAI(), Main.player[NPC.target].position.X - distance, Main.player[NPC.target].position.Y - distance, 0, 0, ModContent.ProjectileType<MeldosaurusClone>(), dmg, 0, Main.myPlayer, NPC.target, 3);
					}
				}
				NPC.alpha = 0;
				NPC.ai[1]++;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				if (NPC.ai[1] <= 30)
				{
					NPC.rotation = NPC.AngleTo(Main.player[NPC.target].Center);
					NPC.position.X = Main.player[NPC.target].position.X + distance;
					NPC.position.Y = Main.player[NPC.target].position.Y + distance;
				}
				if (NPC.ai[1] == 31)
				{
					Terraria.Audio.SoundEngine.PlaySound(SoundID.Zombie5, NPC.Center);
					Vector2 position = NPC.Center;
					position.X = NPC.Center.X + (30f * NPC.direction);
					Vector2 targetPosition = Main.player[NPC.target].Center;
					Vector2 direction = targetPosition - position;
					direction.Normalize();
					NPC.velocity = direction * 16;
				}
				if (NPC.ai[1] >= 90)
                {
					NPC.alpha = 0;
					NPC.velocity.X = NPC.velocity.Y = 0;
					if (spawnWorms)
					{
						Terraria.Audio.SoundEngine.PlaySound(SoundID.Zombie5 with { Pitch = SoundID.Zombie5.Pitch - 0.5f }, Main.player[NPC.target].Center);
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MeldWyrmHead>(), 0, NPC.whoAmI, 1);
							NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MeldWyrmHead>(), 0, NPC.whoAmI, 2);
						}
					}
					NewPhase(1);
                }
			}
			//Obligatory final desperation
			if (NPC.ai[0] == 10)
            {
				NPC.scale += 0.001f;
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0;
				NPC.rotation += 1.2f;
				NPC.noGravity = true;
				NPC.noTileCollide = true;
				NPC.ai[1]++;
				if (NPC.ai[1] >= 10)
				{
					Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.Center);
					for (int x = 0; x <= Main.rand.Next(4,6); x++)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
							Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, Main.rand.Next(-30, 30), Main.rand.Next(-40, 30), ModContent.ProjectileType<EntropicVomit>(), 30, 0f, Main.myPlayer, Main.rand.Next(1, 4));
                    }
					NPC.ai[1] = 0;
                }
            }			
		}

		public void NewPhase(int phase)
		{
			if (phase % 2 == 0)
			Main.NewLightning();
			NPC.alpha = 0;
			NPC.ai[1] = 0;
			NPC.ai[2] = 0;
			NPC.ai[3] = 0;
			NPC.ai[0] = phase;
		}

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y += frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y > frameHeight * 6)
			{
				NPC.frame.Y = 0;
			}
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			if (NPC.ai[0] == 1)
			{
				framebuffer++;
				if (framebuffer >= 6)
				{
					framecounter++;
					framebuffer = 0;
				}
				if (framecounter > 3)
				{
					framecounter = 0;
				}

				Texture2D deusheadsprite = (ModContent.Request<Texture2D>("CalValEX/AprilFools/Meldosaurus/MeldosaurusVomit").Value);

				int deusheadheight = framecounter * (deusheadsprite.Height / 4);

				Rectangle deusheadsquare = new(0, deusheadheight, deusheadsprite.Width, deusheadsprite.Height / 4);
				Color deusheadalpha = NPC.GetAlpha(drawColor);
				spriteBatch.Draw(deusheadsprite, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), deusheadsquare, deusheadalpha, NPC.rotation, Utils.Size(deusheadsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
				return false;
			}
			else if (NPC.ai[0] == 4)
			{
				if (framecounter <= 12)
				framebuffer++;
				if (framebuffer >= 6 && framecounter <= 12)
				{
					framecounter++;
					framebuffer = 0;
				}
				if (framecounter >= 12)
                {
					framecounter = 12;
                }

				Texture2D deusheadsprite = (ModContent.Request<Texture2D>("CalValEX/AprilFools/Meldosaurus/MeldosaurusThrow").Value);

				int deusheadheight = framecounter * (deusheadsprite.Height / 13);

				Rectangle deusheadsquare = new(0, deusheadheight, deusheadsprite.Width, deusheadsprite.Height / 13);
				Color deusheadalpha = NPC.GetAlpha(drawColor);
				spriteBatch.Draw(deusheadsprite, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), deusheadsquare, deusheadalpha, NPC.rotation, Utils.Size(deusheadsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
				return false;
			}
			else if (NPC.ai[0] == 4 && NPC.ai[3] == 2)
			{
				framebuffer++;
				if (framebuffer >= 6 && framecounter <= 8)
				{
					framecounter++;
					framebuffer = 0;
				}

				Texture2D deusheadsprite = (ModContent.Request<Texture2D>("CalValEX/AprilFools/Meldosaurus/MeldosaurusHit").Value);

				int deusheadheight = framecounter * (deusheadsprite.Height / 9);

				Rectangle deusheadsquare = new(0, deusheadheight, deusheadsprite.Width, deusheadsprite.Height / 9);
				Color deusheadalpha = NPC.GetAlpha(drawColor);
				spriteBatch.Draw(deusheadsprite, NPC.Center - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), deusheadsquare, deusheadalpha, NPC.rotation, Utils.Size(deusheadsquare) / 2f, NPC.scale, SpriteEffects.None, 0f);
				return false;
			}
			else

            {
				return true;
            }
		}

        public override void OnKill()
        {
            CalValEXWorld.downedMeldosaurus = true;
			CalValEXWorld.UpdateWorldBool();
        }

        /*public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (NPC.ai[0] == 8 && CalamityMod.World.CalamityWorld.death)
            {
				return false;
            }
			else
            {
				return true;
            }
        }

        public override bool PreNPCLoot()
		{
			DropHelper.DropItemCondition(npc, ModContent.ItemType<KnowledgeMeldosaurus>(), !CalValEXWorld.downedMeldosaurus);
			if (!NPC.AnyNPCs(ModContent.NPCType<AprilFools.Jharim.Jharim>()))
			{
				NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AprilFools.Jharim.Jharim>());
			}
			return true;
		}

        public override void NPCLoot()
		{
			DropHelper.DropBags(npc);
			DropHelper.DropItemChance(npc, ModContent.ItemType<MeldosaurusTrophy>(), 10);
			if (!Main.expertMode)
			{
				DropHelper.DropItemChance(npc, ModContent.ItemType<MeldosaurusMask>(), 7);
				DropHelper.DropItemSpray(npc, ModContent.ItemType<CalamityMod.Items.Materials.MeldBlob>(), 1, 2);
				float dropChance = DropHelper.NormalWeaponDropRateFloat;
				DropHelper.DropItemChance(npc, ModContent.ItemType<ShadesBane>(), dropChance);
				DropHelper.DropItemChance(npc, ModContent.ItemType<Nyanthrop>(), dropChance);
			}
			CalValEXWorld.downedMeldosaurus = true;
		}*/

		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * balance);
			NPC.damage = (int)(NPC.damage * 0.7f);
		}

		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(ischarginng);
			writer.Write(fate);
			writer.Write(chargecount);
			writer.Write(framebuffer);
			writer.Write(framecounter);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			chargecount = reader.ReadInt32();
			framebuffer = reader.ReadInt32();
			framecounter = reader.ReadInt32();
			ischarginng = reader.ReadBoolean();
			fate = reader.ReadBoolean();
		}

	}
}