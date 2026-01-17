using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Dusts;
using CalValEX.Tiles.AstralMisc;
using Terraria.GameContent.Metadata;
using CalValEX.Items.Tiles.Blocks.Astral;

namespace CalValEX.Tiles.AstralBlocks
{
    public class AstralGrassPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileBrick[Type] = true;
            //ItemDrop = ModContent.ItemType<AstralDirt>();
            DustType = ModContent.DustType<AstralSolutionDust>();
            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Grass"]);
            AddMapEntry(new Color(252, 88, 252));
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.NeedsGrassFramingDirt[Type] = ModContent.TileType<AstralDirtPlaced>();
            Main.tileMerge[Type][ModContent.TileType<AstralDirtPlaced>()] = true;
            RegisterItemDrop(ModContent.ItemType<AstralDirt>());

        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail && !effectOnly)
            {
                Main.tile[i, j].TileType= (ushort)ModContent.TileType<AstralDirtPlaced>();
            }
        }

        public override void RandomUpdate(int i, int j)
        {
            if (Main.rand.NextBool(7))
            {
                int grassspawned;
                int pos;
                int choice = Main.rand.Next(2);
                if (choice == 0)
                {
                    grassspawned = ModContent.TileType<AstralTallGrass>();
                    pos = 2;
                }
                else
                {
                    grassspawned = ModContent.TileType<AstralShortGrass>();
                    pos = 1;
                }

                if (Main.tile[i, j - 1].TileType == TileID.Dirt && Main.tile[i, j].Slope == 0 && !Main.tile[i, j].IsHalfBlock)
                {
                    WorldGen.PlaceTile(i, j - pos, grassspawned, true, false, -1, Main.rand.Next(18));
                }

                int LocationX = i;
                int LocationY = j;
                for (int x = LocationX - 4; x <= LocationX + 4; x++)
                {
                    for (int y = LocationY - 4; y <= LocationY + 4; y++)
                    {
                        if (Vector2.Distance(new Vector2(LocationX, LocationY), new Vector2(x, y)) <= 4)
                        {
                            if (Main.tile[x, y].TileType == ModContent.TileType<AstralDirtPlaced>())
                            {
                                if (Main.tile[x + 1, y].TileType != TileID.Dirt && Main.tile[x, y - 1].TileType != TileID.Dirt && Main.tile[x - 1, y].TileType != TileID.Dirt && Main.tile[x, y + 1].TileType != TileID.Dirt && Main.tile[x + 1, y + 1].TileType != TileID.Dirt && Main.tile[x - 1, y - 1].TileType != TileID.Dirt && Main.tile[x - 1, y + 1].TileType != TileID.Dirt && Main.tile[x + 1, y - 1].TileType != TileID.Dirt)
                                {
                                }
                                else
                                {
                                    if (Main.rand.NextBool(1))
                                    {
                                        Main.tile[x, y].TileType = (ushort)ModContent.TileType<AstralGrassPlaced>();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
