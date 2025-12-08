using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Pets
{
    public class AuricBottle : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.UseSound = SoundID.NPCHit13;
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.AbyssalTitan>();
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = CalamityID.CalRarityID.Violet;
            Item.buffType = ModContent.BuffType<Buffs.Pets.YharimSquidBuff>();
        }
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return false;
        }
    }
}