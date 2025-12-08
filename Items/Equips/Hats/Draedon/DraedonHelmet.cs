using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Equips.Hats.Draedon
{
    [AutoloadEquip(EquipType.Head)]
    public class DraedonHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
            if (Main.netMode != NetmodeID.Server)
            {
                SetupDrawing();
            }
        }
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                //EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Hats/Draedon/DraedonHelmet_Head", EquipType.Head, this, name: Name);
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Hats/Draedon/DraedonHelmet_Head_Melee", EquipType.Head, this, name: Name + "Melee");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Hats/Draedon/DraedonHelmet_Head_Ranged", EquipType.Head, this, name: Name + "Ranged");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Hats/Draedon/DraedonHelmet_Head_Magic", EquipType.Head, this, name: Name + "Magic");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Hats/Draedon/DraedonHelmet_Head_Summoner", EquipType.Head, this, name: Name + "Summoner");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Hats/Draedon/DraedonHelmet_Head_Rogue", EquipType.Head, this, name: Name + "Rogue");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Hats/Draedon/DraedonHelmet_Head_Typeless", EquipType.Head, this, name: Name + "Typeless");
            }
        }
        private void SetupDrawing()
        {
            int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
            int equipSlotHead2 = EquipLoader.GetEquipSlot(Mod, Name + "Melee", EquipType.Head);
            int equipSlotHead3 = EquipLoader.GetEquipSlot(Mod, Name + "Ranged", EquipType.Head);
            int equipSlotHead4 = EquipLoader.GetEquipSlot(Mod, Name + "Magic", EquipType.Head);
            int equipSlotHead5 = EquipLoader.GetEquipSlot(Mod, Name + "Summoner", EquipType.Head);
            int equipSlotHead6 = EquipLoader.GetEquipSlot(Mod, Name + "Rogue", EquipType.Head);
            int equipSlotHead7 = EquipLoader.GetEquipSlot(Mod, Name + "Typeless", EquipType.Head);
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead2] = false;
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead3] = false;
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead4] = false;
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead5] = false;
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead6] = false;
            ArmorIDs.Head.Sets.DrawHead[equipSlotHead7] = false;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
        }

        public override void UpdateEquip(Player player)
        {
            var p = player.GetModPlayer<CalValEXPlayer>();
            p.arsenalHelmet = true;
        }

        public override void UpdateVanity(Player player)
        {
            var p = player.GetModPlayer<CalValEXPlayer>();
            p.arsenalHelmet = true;
        }
    }
}