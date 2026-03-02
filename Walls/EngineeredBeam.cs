using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class EngineeredBeam : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(107, 43, 43));
        }
    }
}