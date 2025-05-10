﻿using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class HallowedBrickWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            //ItemDrop = ModContent.ItemType<HallowedBrickWall>();
            AddMapEntry(new Color(78, 105, 77));
        }
    }
}