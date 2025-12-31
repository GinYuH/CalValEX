using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CalValEX.Tiles.Paintings
{

    public class PaintingLoader : ModSystem
    {
        public static Dictionary<string, int> paintingItems = new Dictionary<string, int>();

        public override void Load()
        {
            LoadPainting("TheGreatWaveofAcid", 6, 4, ItemRarityID.Green);
            LoadPainting("Abyssus", 5, 10, ItemUtils.BossRarity("SG"));

            LoadPainting("MissingPoster", 3, 3, ItemUtils.BossRarity("DS"), fables: true);
            LoadPainting("TheGreatSandworm", 2, 3, ItemUtils.BossRarity("DS"), fables: true);
            LoadPainting("NotLikeHome", 2, 2, ItemUtils.BossRarity("Crabulon"), fables: true);
            LoadPainting("SwearingShroom", 2, 3, ItemUtils.BossRarity("Crabulon"), fables: true);
            LoadPainting("DivineGore", 2, 3, ItemUtils.BossRarity("Perf"));
            LoadPainting("RottingLands", 2, 3, ItemUtils.BossRarity("Hive"));
            LoadPainting("TheYu", 4, 4, ItemUtils.BossRarity("Hive"));
            LoadPainting("Duality", 2, 2, ItemUtils.BossRarity("SG"));

            LoadPainting("Frozen2", 3, 2, ItemUtils.BossRarity("Cryogen"), true);
            LoadPainting("Scourgy", 3, 3, ItemUtils.BossRarity("AS"));
            LoadPainting("Scourgie", 3, 3, ItemUtils.BossRarity("AS"));
            LoadPainting("FalloftheFirstCity", 2, 4, ItemUtils.BossRarity("Brimmy"));
            LoadPainting("TheClone", 2, 3, ItemUtils.BossRarity("Cal"));
            LoadPainting("TheRogueExtractor", 4, 3, ItemUtils.BossRarity("Aureus"));
            LoadPainting("RecluseoftheDeep", 3, 3, ItemUtils.BossRarity("Levi"));
            LoadPainting("SleepingGiant", 4, 3, ItemUtils.BossRarity("Levi"));
            LoadPainting("CommonPlague", 3, 3, ItemUtils.BossRarity("PBG"));
            LoadPainting("RAVAGE", 3, 3, ItemUtils.BossRarity("Ravager"));
            LoadPainting("WormHeaven", 3, 5, ItemUtils.BossRarity("AD"));

            LoadPainting("Strength", 2, 2, 11);
            LoadPainting("Wisdom", 2, 2, 11);
            LoadPainting("Courage", 2, 2, 11);
            LoadPainting("AccidentalAbominationn", 2, 3, 11);
            LoadPainting("LighttheWorldAnew", 4, 3, 12);
            LoadPainting("Gateway", 2, 4, 12);
            LoadPainting("Envoy", 2, 4, 12);
            LoadPainting("Serpent", 2, 4, 12, true);
            LoadPainting("LostSouls", 5, 3, 13);
            LoadPainting("GhostLady", 3, 3, 13);
            LoadPainting("Snouts", 2, 3, 13);
            LoadPainting("NamelessSerpent", 6, 4, 14, true);
            LoadPainting("CosmicTerror", 6, 4, 14, true);
            LoadPainting("Rebirth", 3, 3, 15);
            LoadPainting("CalamitasPortrait", 2, 3, 15);
            LoadPainting("CataclysmPortrait", 2, 3, 15);
            LoadPainting("CatastrophePortrait", 2, 3, 15);
            LoadPainting("Exhume", 3, 3, 15);
            LoadPainting("Constructions", 6, 4, 15);
            LoadPainting("TheMechsMK1", 6, 4, 15);
            LoadPainting("TheMechanicalGamer", 4, 4, 15);
            LoadPainting("Goozling", 2, 2, 15);
            LoadPainting("CalamiteaTime", 6, 4, 16);

            LoadPainting("Plague22", 3, 3, ItemUtils.BossRarity("PBG"));
            LoadPainting("UnholyTrip", 6, 4, 11, noPad: true);
            LoadPainting("BlazingConflict", 7, 5, 12);
            LoadPainting("Signut", 6, 4, 12);
            LoadPainting("GallusYharus", 9, 8, 15);
            LoadPainting("WilliamPainting", 5, 5, 16);
            LoadPainting("VVanities", 5, 5, 16);
            LoadPainting("ModIconPainting", 5, 5, 16);
            LoadPainting("OldModIconPainting", 5, 5, 16);
            LoadPainting("CalamityPaint", 5, 5, 16);
            LoadPainting("CalamityPaintRetold", 5, 5, 16);
            LoadPainting("CalamityFriends", 5, 5, 16);
            LoadPainting("Yharlamitas", 4, 3, 16);
            LoadPainting("EyeofXeroc", 7, 5, 16);
            LoadPainting("TheYhar", 3, 3, 16);
            LoadPainting("OldUCMM", 5, 5, 16, noPad: true);
            LoadPainting("UCMM", 5, 5, 16, noPad: true);
            LoadPainting("SundayAfternoonCal", 10, 6, 16, noPad: true);

            LoadPainting("CreationoftheShadow", 6, 4, ItemUtils.BossRarity("Cal"));
            LoadPainting("RestingPlace", 3, 3, ItemUtils.BossRarity("AD"));

            LoadPainting("PARRY", 7, 5, CVUtils.MarsRarity);
            LoadPainting("Mar", 3, 4, CVUtils.MarsRarity);
            LoadPainting("Reclaimed", 10, 8, CVUtils.AvatarRarity);
            LoadPainting("GardenoftheDead", 9, 4, CVUtils.NamelessRarity);
            LoadPainting("GodlyVisions", 10, 8, CVUtils.NamelessRarity);

            LoadPainting("OurFounder", 4, 6, ItemRarityID.Blue);
            LoadPainting("LightningStruck", 2, 3, ItemRarityID.Blue);
            LoadPainting("Patula", 2, 2, ItemRarityID.Green);
            LoadPainting("Kumo", 2, 3, ItemRarityID.Green);
            LoadPainting("TheMarionette", 4, 3, ItemUtils.BossRarity("Crabulon"));
        }

        // Loads a painting
        public static void LoadPainting(string name, int width, int height, int rarity, bool infernum = false, bool fables = false, bool noPad = false)
        {
            BasePainting tile = new BasePainting(name, width, height, infernum, fables, noPad);
            ModContent.GetInstance<CalValEX>().AddContent(tile);
            BasePaintingItem item = new BasePaintingItem(name, tile.Type, rarity, infernum: infernum, fables: fables);
            ModContent.GetInstance<CalValEX>().AddContent(item);
            paintingItems.Add(name, item.Type);
        }

        // Loads a painting from an addon
        public static void LoadPainting(string name, int width, int height, string moddedRarity, bool infernum = false, bool fables = false, bool noPad = false)
        {
            BasePainting tile = new BasePainting(name, width, height, infernum, fables, noPad);
            ModContent.GetInstance<CalValEX>().AddContent(tile);
            BasePaintingItem item = new BasePaintingItem(name, tile.Type, moddedRarity: moddedRarity, infernum: infernum, fables: fables);
            ModContent.GetInstance<CalValEX>().AddContent(item);
            paintingItems.Add(name, item.Type);
        }
    }

    [Autoload(false)]
    public class BasePaintingItem : ModItem
    {
        public override string Name => PaintingName;
        public override string Texture => "CalValEX/Items/Tiles/Paintings/" + addon + Name;
        public BasePaintingItem(string name, int tile, int rarity = -1, string moddedRarity = "", bool infernum = false, bool fables = false)
        {
            this.PaintingName = name;
            this.Rarity = rarity;
            this.Tile = tile;
            ModdedRarity = moddedRarity;
            if (infernum && ModLoader.HasMod("InfernumMode"))
                addon = "Infernum";
            else if (fables && ModLoader.HasMod("CalamityFables"))
                addon = "Fables";
        }

        public string PaintingName;

        public string ModdedRarity = "";

        public int Rarity = 0;

        public int Tile = -1;

        public string addon = "";
        protected override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = Tile;
            Item.width = 12;
            Item.height = 12;
            Item.rare = CVUtils.SetRarity(Rarity, ModdedRarity);
        }
    }

    [Autoload(false)]
    public class BasePainting : ModTile
    {
        public override string Name => PaintingName + "Placed";
        public override string Texture => "CalValEX/Tiles/Paintings/" + addon + Name;
        public string PaintingName;
        public int Width = 1;
        public int Height = 1;
        public string addon = "";
        public bool noPad = false;
        public BasePainting(string name, int width, int height, bool infernum = false, bool fables = false, bool noPad = false)
        {
            this.PaintingName = name;
            this.Width = width;
            this.Height = height;
            this.noPad = noPad;
            if (infernum && ModLoader.HasMod("InfernumMode"))
                addon = "Infernum";
            else if (fables && ModLoader.HasMod("CalamityFables"))
                addon = "Fables";
        }

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileObjectData.newTile.Width = Width;
            TileObjectData.newTile.Height = Height;
            if (noPad)
            {
                TileObjectData.newTile.CoordinatePadding = 0;
            }
            List<int> height = new List<int>();
            for (int i = 0; i < Height; i++)
            {
                height.Add(16);
            }
            TileObjectData.newTile.CoordinateHeights = height.ToArray();
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(139, 0, 0), name);
        }
    }
}