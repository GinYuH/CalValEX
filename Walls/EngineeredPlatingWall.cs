using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class EngineeredPlatingWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            //ItemDrop = ModContent.ItemType<WulfrumPanelWall>();
            AddMapEntry(new Color(50, 92, 61));
        }
    }
}