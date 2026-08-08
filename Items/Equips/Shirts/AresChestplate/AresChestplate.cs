using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Items.Equips.Shirts.AresChestplate
{
    [AutoloadEquip(EquipType.Body)]
    public class AresChestplate : ModItem {
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 1;

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.rare = CalamityID.CalRarityID.Exo;
            Item.vanity = true;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            ArmorIDs.Body.Sets.HidesArms[Item.bodySlot] = true;
        }

        public override void UpdateEquip(Player player)
        {
            bool gausspawned = player.ownedProjectileCounts[ModContent.ProjectileType<GaussArm>()] <= 0;
            bool laserpawned = player.ownedProjectileCounts[ModContent.ProjectileType<LaserArm>()] <= 0;
            bool teslaspawned = player.ownedProjectileCounts[ModContent.ProjectileType<TeslaArm>()] <= 0;
            bool plasmaspawned = player.ownedProjectileCounts[ModContent.ProjectileType<PlasmaArm>()] <= 0;
            if (gausspawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0, 0, ModContent.ProjectileType<GaussArm>(), 0, 0f, player.whoAmI);
            }
            if (laserpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0, 0, ModContent.ProjectileType<LaserArm>(), 0, 0f, player.whoAmI);
            }
            if (teslaspawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                   0, 0, ModContent.ProjectileType<TeslaArm>(), 0, 0f, player.whoAmI);
            }
            if (plasmaspawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0, 0, ModContent.ProjectileType<PlasmaArm>(), 0, 0f, player.whoAmI);
            }
            player.GetModPlayer<CalValEXPlayer>().aresarms = true;
        }

        /*public override void UpdateVanity(Player player)
        {
            bool gausspawned = player.ownedProjectileCounts[ModContent.ProjectileType<GaussArm>()] <= 0;
            bool laserpawned = player.ownedProjectileCounts[ModContent.ProjectileType<LaserArm>()] <= 0;
            bool teslaspawned = player.ownedProjectileCounts[ModContent.ProjectileType<TeslaArm>()] <= 0;
            bool plasmaspawned = player.ownedProjectileCounts[ModContent.ProjectileType<PlasmaArm>()] <= 0;
            if (gausspawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                    0, 0, ModContent.ProjectileType<GaussArm>(), 0, 0f, player.whoAmI);
            }
            if (laserpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                   0, 0, ModContent.ProjectileType<LaserArm>(), 0, 0f, player.whoAmI);
            }
            if (teslaspawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                   0, 0, ModContent.ProjectileType<TeslaArm>(), 0, 0f, player.whoAmI);
            }
            if (plasmaspawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position.X + player.width / 2, player.position.Y + player.height / 2,
                   0, 0, ModContent.ProjectileType<PlasmaArm>(), 0, 0f, player.whoAmI);
            }
            player.GetModPlayer<CalValEXPlayer>().aresarms = true;
        }*/
    }
}