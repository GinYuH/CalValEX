using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Mounts.LimitedFlight
{
    [LegacyName("HauntedPebble")]
    public class PhantoSprout : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.LightRed;
            //Item.UseSound = SoundID.Item23;
            Item.noMelee = true;
            Item.mountType = ModContent.MountType<PhantomDebris>();
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            // kick the pebble out of the mount slot
            if (Main.LocalPlayer.miscEquips[0].type == Type)
            {
                Item.NewItem(Main.LocalPlayer.GetSource_FromThis(), Main.LocalPlayer.getRect(), Item.type);
                Main.LocalPlayer.miscEquips[0].SetDefaults();
            }
            return true;
        }
    }
}