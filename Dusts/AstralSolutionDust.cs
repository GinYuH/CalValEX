using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Dusts
{
	public class AstralSolutionDust : ModDust
	{
		public override void SetStaticDefaults() {
			UpdateType = DustID.PureSpray;
		}
	}
}