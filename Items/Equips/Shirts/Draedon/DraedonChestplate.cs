using CalValEX.CalamityID;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Equips.Shirts.Draedon {
    [AutoloadEquip(EquipType.Body)]
    public class DraedonChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                SetupDrawing();
            }
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.rare = CalRarityID.DarkOrange;
            Item.vanity = true;
        }
        public override void Load()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                //EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Shirts/Draedon/DraedonChestplate_Body", EquipType.Body, this, name: Name);
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Shirts/Draedon/DraedonChestplate_Body_Melee", EquipType.Body, this, name: Name + "Melee");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Shirts/Draedon/DraedonChestplate_Body_Ranged", EquipType.Body, this, name: Name + "Ranged");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Shirts/Draedon/DraedonChestplate_Body_Magic", EquipType.Body, this, name: Name + "Magic");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Shirts/Draedon/DraedonChestplate_Body_Summoner", EquipType.Body, this, name: Name + "Summoner");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Shirts/Draedon/DraedonChestplate_Body_Rogue", EquipType.Body, this, name: Name + "Rogue");
                EquipLoader.AddEquipTexture(Mod, "CalValEX/Items/Equips/Shirts/Draedon/DraedonChestplate_Body_Typeless", EquipType.Body, this, name: Name + "Typeless");
            }
        }
        private void SetupDrawing()
        {
            int equipSlotBody = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
            int equipSlotBody2 = EquipLoader.GetEquipSlot(Mod, Name + "Melee", EquipType.Body);
            int equipSlotBody3 = EquipLoader.GetEquipSlot(Mod, Name + "Ranged", EquipType.Body);
            int equipSlotBody4 = EquipLoader.GetEquipSlot(Mod, Name + "Magic", EquipType.Body);
            int equipSlotBody5 = EquipLoader.GetEquipSlot(Mod, Name + "Summoner", EquipType.Body);
            int equipSlotBody6 = EquipLoader.GetEquipSlot(Mod, Name + "Rogue", EquipType.Body);
            int equipSlotBody7 = EquipLoader.GetEquipSlot(Mod, Name + "Typeless", EquipType.Body);
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = false;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody2] = false;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody3] = false;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody4] = false;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody5] = false;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody6] = false;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody7] = false;
        }

        public override void UpdateEquip(Player player)
        {
            var p = player.GetModPlayer<CalValEXPlayer>();
            p.arsenalChestplate = true;
        }

        public override void UpdateVanity(Player player)
        {
            var p = player.GetModPlayer<CalValEXPlayer>();
            p.arsenalChestplate = true;
        }
    }
}