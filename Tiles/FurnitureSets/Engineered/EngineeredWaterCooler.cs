using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
    public class EngineeredWaterCooler : ModTile
    {
        public override void SetStaticDefaults()
        {   
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileID.Sets.CountsAsWaterSource[Type] = true;

            //Size rules
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] {16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            //Placement - Anchor must be above alternates
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 2);
            //Placement - Flipping
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);

            TileObjectData.addTile(Type); 
            
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(110, 52, 52), name);
            DustType = DustID.Clay;
            AdjTiles = new int[] { TileID.Sinks };
        }
    }
}