using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using CalValEX.Tiles.AstralBlocks;
using CalValEX.Walls.AstralUnsafe;
using CalValEX.Tiles.AstralMisc;
using CalValEX.Walls.AstralSafe;
using CalValEX.CalamityID;
using ReLogic.Content;
using CalValEX.Items.Plushies;

namespace CalValEX
{
    public class CalValEXGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public bool isCalValPet;
        public static Texture2D FlinstoneGangster;
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                FlinstoneGangster = ModContent.Request<Texture2D>("CalValEX/Projectiles/BrimstoneMonster", AssetRequestMode.ImmediateLoad).Value;
            }
        }
        public override bool PreDraw(Projectile projectile, ref Color drawColor)
        {
            if (CalValEXWorld.RockshrinEX && CalValEX.CalamityActive)
            {
                if (projectile.type == CalProjectileID.BrimstoneMonster)
                {
                    Main.EntitySpriteDraw(FlinstoneGangster, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), null, projectile.GetAlpha(drawColor), projectile.rotation, FlinstoneGangster.Size() / 2f, projectile.scale, SpriteEffects.None, 0);
                    return false;
                }
            }
            return true;
        }
        public override bool PreAI(Projectile proj)
        {
            if (proj.owner == Main.myPlayer)
            {
                if (CalValEX.CalamityActive && proj.type == CalProjectileID.AstralSpray)
                    InfectionConvert((int)(proj.position.X + proj.width / 2) / 16, (int)(proj.position.Y + proj.height / 2) / 16, 2);
            }
            return true;
        }

        public override void AI(Projectile proj)
        {
			if (isCalValPet)
            {
                for (int k = 0; k < Main.maxProjectiles; k++)
                {
                    Projectile otherProj = Main.projectile[k];
                    if (!otherProj.active || otherProj.owner != proj.owner || k == proj.whoAmI)
                        continue;

                    bool bothPets = otherProj.GetGlobalProjectile<CalValEXGlobalProjectile>().isCalValPet;
                    float dist = Math.Abs(proj.position.X - otherProj.position.X) + Math.Abs(proj.position.Y - otherProj.position.Y);
                    if (bothPets && dist < proj.width)
                    {
                        if (proj.position.X < otherProj.position.X)
                            proj.velocity.X -= 0.4f;
                        else
                            proj.velocity.X += 0.4f;

                        if (proj.position.Y < otherProj.position.Y)
                            proj.velocity.Y -= 0.4f;
                        else
                            proj.velocity.Y += 0.4f;
                    }
                }
            }
            if (proj.owner == Main.myPlayer)
            {
                switch (proj.type)
                {
                    case ProjectileID.PureSpray:
                        PureConvert((int)(proj.position.X + proj.width / 2) / 16, (int)(proj.position.Y + proj.height / 2) / 16, 2);
                        break;
                    case ProjectileID.HallowSpray:
                    case ProjectileID.CrimsonSpray:
                    case ProjectileID.CorruptSpray:
                        VoidConvert((int)(proj.position.X + proj.width / 2) / 16, (int)(proj.position.Y + proj.height / 2) / 16, 2);
                        break;
                }
            }
		}

		public void PureConvert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
					{
                        ConvertTile(ModContent.TileType<AstralHardenedSandPlaced>(), TileID.HardenedSand, k, l);
                        ConvertTile(ModContent.TileType<AstralSandstonePlaced>(), TileID.Sandstone, k, l);
                        ConvertTile(ModContent.TileType<AstralTreeWoodPlaced>(), TileID.LivingWood, k, l);
                        ConvertTile(ModContent.TileType<AstralDirtPlaced>(), TileID.Dirt, k, l);
                        ConvertTile(ModContent.TileType<AstralSandPlaced>(), TileID.Sand, k, l);
                        ConvertTile(ModContent.TileType<AstralClayPlaced>(), TileID.ClayBlock, k, l);
                        ConvertTile(ModContent.TileType<XenostonePlaced>(), TileID.Stone, k, l);
                        ConvertTile(ModContent.TileType<AstralIcePlaced>(), TileID.IceBlock, k, l);
                        ConvertTile(ModContent.TileType<AstralSnowPlaced>(), TileID.SnowBlock, k, l);
                        ConvertTile(ModContent.TileType<AstralShortGrass>(), TileID.Plants, k, l);
                        ConvertTile(ModContent.TileType<AstralTallGrass>(), TileID.Plants2, k, l);
                        ConvertTile(ModContent.WallType<AstralDirtWallPlaced>(), WallID.DirtUnsafe, k, l, true);
                        ConvertTile(ModContent.WallType<XenostoneWallPlaced>(), WallID.Stone, k, l, true);
                        ConvertTile(ModContent.WallType<AstralHardenedSandWallPlaced>(), WallID.HardenedSand, k, l, true);
                        ConvertTile(ModContent.WallType<AstralSandstoneWallPlaced>(), WallID.Sandstone, k, l, true);
                        ConvertTile(ModContent.WallType<AstralIceWallPlaced>(), WallID.IceUnsafe, k, l, true);
                        ConvertTile(ModContent.WallType<AstralGrassWallPlaced>(), WallID.Grass, k, l, true);
					}
				}
			}
		}

		public static void InfectionConvert(int i, int j, int size = 4)
        {
            if (CalValEX.CalamityActive)
			{
				for (int k = i - size; k <= i + size; k++)
				{
					for (int l = j - size; l <= j + size; l++)
					{
						if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                        {
                            ConvertTile(ModContent.TileType<AstralTreeWoodPlaced>(), CalTileID.AstralMonolith, k, l);
                            ConvertTile(ModContent.TileType<AstralDirtPlaced>(), CalTileID.AstralDirt, k, l);
                            ConvertTile(ModContent.TileType<AstralClayPlaced>(), CalTileID.AstralClay, k, l);
                            ConvertTile(ModContent.TileType<AstralSnowPlaced>(), CalTileID.AstralSnow, k, l);
                            ConvertTile(ModContent.WallType<AstralDirtWallPlaced>(), CalWallID.AstralDirtWall, k, l, true);
						}
					}
				}
			}
		}
		public static void VoidConvert(int i, int j, int size = 4)
		{
			for (int k = i - size; k <= i + size; k++)
			{
				for (int l = j - size; l <= j + size; l++)
				{
					if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
					{
						ConvertTile(ModContent.TileType<AstralTreeWoodPlaced>(), TileID.LivingWood, k, l);
                        ConvertTile(ModContent.TileType<AstralDirtPlaced>(), TileID.Dirt, k, l);
                        ConvertTile(ModContent.TileType<AstralClayPlaced>(), TileID.ClayBlock, k, l);
                        ConvertTile(ModContent.TileType<AstralSnowPlaced>(), TileID.SnowBlock, k, l);
                        ConvertTile(ModContent.WallType<AstralDirtWallPlaced>(), WallID.DirtUnsafe, k, l, true);
					}
				}
			}
		}

		public static void ConvertTile(int initialtype, int finaltype, int k, int l, bool wall = false)
        {
			Tile t = Main.tile[k, l];
			if (!wall)
			{
				if (t.TileType == initialtype)
				{
					Main.tile[k, l].TileType = (ushort)finaltype;
					WorldGen.SquareTileFrame(k, l, true);
					NetMessage.SendTileSquare(-1, k, l, 1);
				}
			}
			else
            {
                if (t.WallType == initialtype)
                {
                    Main.tile[k, l].WallType = (ushort)finaltype;
                    WorldGen.SquareWallFrame(k, l, true);
                    NetMessage.SendTileSquare(-1, k, l, 1);
                }
            }
        }

		public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
			if (projectile.type == CalamityID.CalProjectileID.MeldFlames && target.type == ModContent.NPCType<AprilFools.Jharim.Jharim>())
			{
				return true;
			}
			else
			{
				return null;
			}
        }
    }
}
