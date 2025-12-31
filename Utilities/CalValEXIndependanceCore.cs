using Terraria.ModLoader;
using Terraria;

namespace CalValEX
{
    public partial class CalValEX : Mod
    {
        private Mod calamity;
        private bool calamityActive;
        public static bool CalamityActive => instance?.calamityActive ?? false;
        public static Mod Calamity => CalamityActive ? instance.calamity : null;
        public static bool FablesActive => instance?.fables != null;
        public static Mod Fables => FablesActive ? instance.fables : null;

        public static bool CalamityContent<T>(string name, out T content) where T : IModType
        {
            content = default(T);
            if (!CalamityActive)
                return false;
            return Calamity.TryFind(name, out content);
        }

        public static int CalamityProjectile(string name)
        {
            if (CalamityContent(name, out ModProjectile content))
                return content.Type;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return -1;
        }

        public static int CalamityItem(string name)
        {
            if (CalamityContent(name, out ModItem content))
                return content.Type;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return -1;
        }

        public static int CalamityNPC(string name)
        {
            if (CalamityContent(name, out ModNPC content))
                return content.Type;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return -1;
        }

        public static int CalamityTile(string name)
        {
            if (CalamityContent(name, out ModTile content))
                return content.Type;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return -1;
        }

        public static int CalamityWall(string name)
        {
            if (CalamityContent(name, out ModWall content))
                return content.Type;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return -1;
        }

        public static int CalamityBuff(string name)
        {
            if (CalamityContent(name, out ModBuff content))
                return content.Type;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return -1;
        }

        public static ModBiome CalamityBiome(string name)
        {
            if (CalamityContent(name, out ModBiome content))
                return content;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return null;
        }

        public static bool InCalamityBiome(Player player, string biome)
        {
            if (player.InModBiome(CalamityBiome(biome)))
                return true;
            return false;
        }

        public static int CalamityRarity(string name)
        {
            if (CalamityContent(name, out ModRarity content))
                return content.Type;
            CalValEX.instance.Logger.Error(name + " not found. Report this to the Calamity's Vanities developers if you see this.");
            return -1;
        }
    }
}