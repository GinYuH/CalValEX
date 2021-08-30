﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using CalValEX.Items.Tiles;
using Terraria.ObjectData;

namespace CalValEX.Tiles.MiscFurniture
{
	public class TerminusShrineLevel2Placed : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.addTile(Type);
			animationFrameHeight = 54;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Terminus Shrine");
			AddMapEntry(new Color(113, 142, 162), name);
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 16, 16, ModLoader.GetMod("CalamityMod").ItemType("BossRush"), 1);
			CalValEXWorld.Rockshrine = false;
			Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<TerminusShrine>());
		}

		private int count;
		private int rotation;
		private float stonepos;
		public override void PlaceInWorld(int i, int j, Item item)
		{
			stonepos = -1.00001f;
			CalValEXWorld.Rockshrine = false;
			CalValEXWorld.RockshrinEX = false;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			rotation++;
			if (stonepos <= -1)
			{
				stonepos = stonepos * 0.01f;
			}
			else if (stonepos >= 1)
			{
				stonepos = stonepos * -0.01f;
			}
			Texture2D auraTexture = mod.GetTexture("Tiles/MiscFurniture/TerminusShrineStone_Aura");
			Texture2D stoneTexture = mod.GetTexture("Tiles/MiscFurniture/TerminusShrineStone");
			Rectangle sourceRectangle = new Rectangle(0, 0, auraTexture.Width, auraTexture.Height);
			Rectangle stoneRectangle = new Rectangle(0, 0, stoneTexture.Width, stoneTexture.Height);
			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
			Vector2 position = new Vector2((i * 16) - 16 - Main.screenPosition.X, (j * 16) - 36 - Main.screenPosition.Y) + zero;
			Vector2 stoneposition = new Vector2((i * 16) - 4 - Main.screenPosition.X + stonepos, (j * 16) - 29 - Main.screenPosition.Y) + zero;
			Color color = Color.White;
			Tile tile = Main.tile[i, j];
			Vector2 origin = new Vector2(auraTexture.Width, auraTexture.Height);

			if (!tile.halfBrick() && tile.slope() == 0)
			{
					if (count == 11)
					{
						spriteBatch.End();
						spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
						spriteBatch.Draw(auraTexture, position, sourceRectangle, color, rotation * 0.01f, origin / 2f, 1f, SpriteEffects.None, 0f);
						spriteBatch.Draw(stoneTexture, stoneposition, stoneRectangle, color, 0f, origin / 2f, 1f, SpriteEffects.None, 0f);
					}
			}
			count++;
			if (count >= 12)
				count = 0;
			//spriteBatch.Draw(texture, this.Center - Main.screenPosition, sourceRectangle, lightColor, rotation, origin / 2f, 1f, SpriteEffects.None, 0);
		}

		public override bool HasSmartInteract()
		{
			return true;
		}

		public override void AnimateTile(ref int frame, ref int frameCounter)
		{
			frame = 1;
		}

		public override void MouseOver(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			localPlayer.noThrow = 2;
			localPlayer.showItemIcon = true;
			localPlayer.showItemIcon2 = ModLoader.GetMod("CalamityMod").ItemType("Rock");
		}

		public override bool NewRightClick(int i, int j)
		{
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer.HasItem(ModLoader.GetMod("CalamityMod").ItemType("Rock")))
			{
				CalValEXWorld.RockshrinEX = true;
				localPlayer.ConsumeItem(ModLoader.GetMod("CalamityMod").ItemType("Rock"));
				Main.PlaySound(SoundID.Item119);
				Main.tile[i, j].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i, j - 1].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i, j - 2].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();

				Main.tile[i - 1, j].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i - 1, j - 1].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i - 1, j - 2].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();

				Main.tile[i - 2, j].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i - 2, j - 1].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i - 2, j - 2].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();

				Main.tile[i - 2, j].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i - 2, j - 1].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				Main.tile[i - 2, j - 2].type = (ushort)ModContent.TileType<TerminusShrineLevel3Placed>();
				WorldGen.SquareTileFrame(i, j, true);
				NetMessage.SendTileSquare(-1, i, j, 1);
			}
			else 
			{
				CalValEXWorld.Rockshrine = false;
				Item.NewItem(i * 16, j * 16, 16, 16, ModLoader.GetMod("CalamityMod").ItemType("BossRush"), 1);
				Main.tile[i, j].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i, j - 1].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i, j - 2].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();

				Main.tile[i - 1, j].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i - 1, j - 1].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i - 1, j - 2].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();

				Main.tile[i - 2, j].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i - 2, j - 1].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i - 2, j - 2].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();

				Main.tile[i - 2, j].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i - 2, j - 1].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				Main.tile[i - 2, j - 2].type = (ushort)ModContent.TileType<TerminusShrinePlaced>();
				WorldGen.SquareTileFrame(i, j, true);
				NetMessage.SendTileSquare(-1, i, j, 1);
			}
			return true;
		}
	}
}