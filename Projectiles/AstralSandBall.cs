using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.AstralBlocks;

namespace CalValEX.Projectiles
{
    public class AstralSandBall : ModProjectile
	{
		protected bool falling = true;
		protected int tileType;

		public override void SetStaticDefaults() {
			// DisplayName.SetDefault("Blighted Atral Sand Ball");
			ProjectileID.Sets.ForcePlateDetection[Projectile.type] = true;
		}

		public override void SetDefaults() {
			Projectile.knockBack = 6f;
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			tileType = ModContent.TileType<AstralSandPlaced>();
		}

		public override void AI() {

			Projectile.tileCollide = true;
			Projectile.localAI[1] = 0f;

			if (Projectile.ai[0] == 1f) {
				if (!falling) {
					Projectile.ai[1] += 1f;

					if (Projectile.ai[1] >= 60f) {
						Projectile.ai[1] = 60f;
						Projectile.velocity.Y += 0.2f;
					}
				}
				else
					Projectile.velocity.Y += 0.41f;
			}
			else if (Projectile.ai[0] == 2f) {
				Projectile.velocity.Y += 0.2f;

				if (Projectile.velocity.X < -0.04f)
					Projectile.velocity.X += 0.04f;
				else if (Projectile.velocity.X > 0.04f)
					Projectile.velocity.X -= 0.04f;
				else
					Projectile.velocity.X = 0f;
			}

			Projectile.rotation += 0.1f;

			if (Projectile.velocity.Y > 10f)
				Projectile.velocity.Y = 10f;
		}

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
			if (falling)
				Projectile.velocity = Collision.AnyCollision(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height, true);
			else
				Projectile.velocity = Collision.TileCollision(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height, fallThrough, fallThrough, 1);

			return false;
		}

        public override void OnKill(int timeLeft) {
			if (Projectile.owner == Main.myPlayer && !Projectile.noDropItem) {
				int tileX = (int)(Projectile.position.X + Projectile.width / 2) / 16;
				int tileY = (int)(Projectile.position.Y + Projectile.width / 2) / 16;

				Tile tile = Main.tile[tileX, tileY];
				Tile tileBelow = Main.tile[tileX, tileY + 1];

				if (tile.IsHalfBlock && Projectile.velocity.Y > 0f && System.Math.Abs(Projectile.velocity.Y) > System.Math.Abs(Projectile.velocity.X))
					tileY--;

				if (tile.TileType == TileID.Dirt) {
					bool onMinecartTrack = tileY < Main.maxTilesY - 2 && tileBelow != null && tileBelow.TileType != TileID.Dirt && tileBelow.TileType == TileID.MinecartTrack;

					if (!onMinecartTrack)
						WorldGen.PlaceTile(tileX, tileY, tileType, false, true);

					if (!onMinecartTrack && tile.TileType != TileID.Dirt && tile.TileType == tileType) {
						if (tileBelow.IsHalfBlock || tileBelow.Slope != 0) {
							WorldGen.SlopeTile(tileX, tileY + 1, 0);

							if (Main.netMode == NetmodeID.Server)
								NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 14, tileX, tileY + 1);
						}

						if (Main.netMode != NetmodeID.SinglePlayer)
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, tileX, tileY, tileType);
					}
				}
			}
		}


		//public override bool CanDamage() => Projectile.localAI[1] != -1f;
	}
}