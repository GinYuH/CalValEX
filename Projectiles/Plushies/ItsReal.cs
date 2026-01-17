using CalValEX.Items.Plushies;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Projectiles.Plushies
{
    public class ItsReal : ModProjectile
    {
        bool invincibility = false;
        int counter;
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 84;
            Projectile.height = 154;
            Projectile.aiStyle = ProjAIStyleID.BeachBall;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            counter++;
            if (counter == 60)
            {
                Projectile.tileCollide = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), PlushManager.PlushItems["Calamitas"]);
        }
    }
}