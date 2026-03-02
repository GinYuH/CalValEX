using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class EngineeredPanelWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(72, 26, 26));
        }
    }
}