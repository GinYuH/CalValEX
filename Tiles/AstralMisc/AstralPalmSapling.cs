﻿using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using CalValEX.Tiles.AstralBlocks;
using Terraria.ObjectData;
using Terraria.GameContent.Metadata;

namespace CalValEX.Tiles.AstralMisc
{
    public class AstralPalmSapling : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;

			TileObjectData.newTile.Width = 1;
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.AnchorValidTiles = new[] { ModContent.TileType<AstralSandPlaced>() };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.DrawFlipHorizontal = true;
			TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
			TileObjectData.newTile.LavaDeath = true;
			TileObjectData.newTile.RandomStyleRange = 3;
			TileID.Sets.CommonSapling[Type] = true;
			TileID.Sets.TreeSapling[Type] = true;
			TileObjectData.addTile(Type);
            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

            LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Blighted Astral Palm Sapling");
			AddMapEntry(new Color(200, 200, 200), name);

			AdjTiles = new int[] { TileID.Saplings };
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

		public override void RandomUpdate(int i, int j)
		{
			if (WorldGen.genRand.NextBool(20))
			{
				Tile tile = Framing.GetTileSafely(i, j);
				bool growSucess;
				growSucess = WorldGen.GrowPalmTree(i, j);
				bool isPlayerNear = WorldGen.PlayerLOS(i, j);
				if (growSucess && isPlayerNear)
					WorldGen.TreeGrowFXCheck(i, j);
			}
		}

		public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
		{
			if (i % 2 == 1)
				effects = SpriteEffects.FlipHorizontally;
		}
	}
}