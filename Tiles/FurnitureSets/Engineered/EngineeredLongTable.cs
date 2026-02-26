using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
    public class EngineeredLongTable : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileTable[Type] = true;
            
            //Size rules
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 }; //
            TileObjectData.newTile.CoordinatePadding = 2;

            TileObjectData.addTile(Type);

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(110, 52, 52), name);
            DustType = DustID.Clay;
            AdjTiles = new int[] { TileID.Tables };
        }
    }
}