﻿using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class ShadowBrickWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            //ItemDrop = ModContent.ItemType<ShadowBrickWall>();
            AddMapEntry(new Color(15, 1, 14));
        }
    }
}