using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Tiles.Blocks
{
    public class PlagueHivePlaced : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            AddMapEntry(new Color(0, 76, 82));
            RegisterItemDrop(Terraria.ID.ItemID.Hive); // fuck you you're useless and don't work god f
            DustType = DustID.t_BorealWood;
        }
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ItemID.Hive);
        }
    }
}