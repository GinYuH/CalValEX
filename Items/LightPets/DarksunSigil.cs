using CalValEX.Buffs.LightPets;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using CalValEX.CalamityID;
using CalValEX.Projectiles.Pets.LightPets;

namespace CalValEX.Items.LightPets
{
    public class DarksunSigil : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 62;
            Item.damage = 0;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item117;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.shoot = ModContent.ProjectileType<DarksunSpirit_Fish>();
            Item.buffType = ModContent.BuffType<DarksunSpiritBuff>();
            Item.rare = CalRarityID.Auric;
        }

        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return false;
        }
    }
}