using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalValEX.AprilFools
{
    [ExtendsFromMod("CalamityMod")]
    public class PaintedGoggles : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.consumable = false;
            Item.maxStack = 1;
            Item.value = 0;
        }

        public override bool CanUseItem(Player player)
        {
            return Main.netMode == NetmodeID.SinglePlayer;
        }

        public override bool? UseItem(Player player)
        {
            CalValEXWorld.paintEnabled = CalValEXWorld.paintEnabled == 1 ? 2 : 1;
            bool firstLoad = CalPaintOverride.originalFC.Count <= 0;
            if (CalValEXWorld.paintEnabled != 1)
            {
                CalPaintOverride.ReplaceWithPaint(firstLoad, false);
                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.NewText(Language.GetTextValue("Mods.CalValEX.Items.PaintedGoggles.Enabled"), Color.Lime);
                else
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.CalValEX.Items.PaintedGoggles.Enabled")), Color.Lime);

            }
            else
            {
                CalPaintOverride.ReplaceWithPaint(firstLoad, true);
                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.NewText(Language.GetTextValue("Mods.CalValEX.Items.PaintedGoggles.Disabled"), Color.Red);
                else
                    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(Language.GetTextValue("Mods.CalValEX.Items.PaintedGoggles.Disabled")), Color.Red);
            }
            return true;
        }
    }
}