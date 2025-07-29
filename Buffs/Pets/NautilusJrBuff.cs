using CalValEX.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Buffs.Pets
{
    public class NautilusJrBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            CVUtils.PetBuff(player, buffIndex, ModContent.ProjectileType<NautilusJr>());
        }
    }
}