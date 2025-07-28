using CalValEX.Buffs.LightPets;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.LightPets
{
    [LegacyName("ProShard")]
    public class ProfanedHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(9, 11));
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.UseSound = SoundID.Item20;
            Item.shoot = ModContent.ProjectileType<Projectiles.Pets.LightPets.ProGuard1>();
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = CalamityID.CalRarityID.Turquoise;
            Item.buffType = ModContent.BuffType<ProBuff>();
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return false;
        }
    }
}