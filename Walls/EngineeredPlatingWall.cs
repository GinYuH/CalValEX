using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Walls
{
    public class EngineeredPlatingWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;
            AddMapEntry(new Color(38, 42, 50));
        }
    }
}