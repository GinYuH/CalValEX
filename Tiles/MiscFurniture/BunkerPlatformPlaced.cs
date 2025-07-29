using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.MiscFurniture
{
    public class BunkerPlatformPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.Platforms[Type] = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleMultiplier = 27;
            TileObjectData.newTile.StyleWrapLimit = 27;
            TileObjectData.newTile.UsesCustomCanPlace = false;
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
            AddMapEntry(new Color(139, 0, 0));
            AdjTiles = new int[] { TileID.Platforms };
        }

        public override void PostSetDefaults()
        {
            Main.tileNoSunLight[Type] = false;
        }
    }
}