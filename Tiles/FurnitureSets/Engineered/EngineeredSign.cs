using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
	public class EngineeredSign : ModTile
	{
		public static LocalizedText DefaultSignText { get; private set; }

		public override void SetStaticDefaults() {
			// These are all used by TileID.Signs, hover over them to read their documentation and see the purpose of each
			Main.tileSign[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.FramesOnKillWall[Type] = true;
			TileID.Sets.AvoidedByNPCs[Type] = true;
			TileID.Sets.TileInteractRead[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true;

			VanillaFallbackOnModDeletion = TileID.Signs;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16};
			TileObjectData.newTile.StyleHorizontal = false;
			TileObjectData.newTile.StyleMultiplier = 2;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			
			TileObjectData.addTile(Type);

			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(110, 52, 52), name);
            DustType = DustID.Meteorite;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            int xFrameOffset = Main.tile[i, j].TileFrameX;
            int yFrameOffset = Main.tile[i, j].TileFrameY;
            Texture2D glowmask = Request<Texture2D>("CalValEX/Tiles/FurnitureSets/Engineered/EngineeredSignGlow").Value;
            Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
            Color drawColour = Color.White;
            Tile trackTile = Main.tile[i, j];
            if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
                spriteBatch.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 18, 18), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
            else if (trackTile.IsHalfBlock)
                spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
        }

		public override void PlaceInWorld(int i, int j, Item item) 
		{
			int signId = Sign.ReadSign(i, j, true);
			if (signId != -1) {
				Sign.TextSign(signId, DefaultSignText.Value);
			}
		}

		public override bool RightClick(int i, int j) 
		{
			// Normal sign right click behavior happens automatically because of Main.tileSign, this code just shows how to retrieve the text of the sign and should be removed from normal sign tiles.
			int signId = Sign.ReadSign(i, j);
			if (signId != -1) {
				string signText = Main.sign[signId].text;
				ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(signText), Color.White);
			}
			return true;
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY) {
			// Destroy the associated Sign data.
			Sign.KillSign(i, j);
		}

		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) {
			return true;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            float pulse = 0.75f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f) * 0.25f;
            pulse = MathHelper.Clamp(pulse, 0.8f, 1f);

            float noise = Main.rand.NextFloat(0.85f, 1f);

            r = ((3f / 175f) * pulse) * (noise * 0.4f);
            g = ((67f / 175f) * pulse) * (noise * 0.4f);
            b = ((146f / 175f) * pulse) * (noise * 0.4f);
        }
	}
}
