using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
    public class EngineeredWorkBench : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            Main.tileTable[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new int[] { 18 }; //
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(110, 52, 52), name);
            AdjTiles = new int[] { TileID.WorkBenches };
        }
    }
}