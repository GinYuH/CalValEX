using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class EngineeredPanelWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            //ItemDrop = ModContent.ItemType<WulfrumPanelWall>();
            AddMapEntry(new Color(86, 34, 34));
        }
    }
}