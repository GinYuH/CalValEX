﻿using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class AstralPlatingWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            //ItemDrop = ModContent.ItemType<AstralPlatingWall>();
            AddMapEntry(new Color(59, 77, 75));
        }
    }
}