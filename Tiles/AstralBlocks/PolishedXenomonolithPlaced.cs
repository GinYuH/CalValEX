﻿using Terraria;
using Terraria.ModLoader;
using CalValEX.Dusts;
using Terraria.GameContent.Metadata;

namespace CalValEX.Tiles.AstralBlocks
{
    public class PolishedXenomonolithPlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            //ItemDrop = ModContent.ItemType<PolishedXenomonolith>();
            DustType = ModContent.DustType<AstralDust>();
            AddMapEntry(new Color(271, 49, 42));
            Main.tileBlendAll[this.Type] = true;
            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Wood"]);
        }

       /* public override void ChangeWaterfallStyle(ref int style)
        {
            style = mod.GetWaterfallStyleSlot("AstralWaterfallStyle");
        }*/
    }
}