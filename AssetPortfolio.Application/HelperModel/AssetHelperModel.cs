namespace AssetPortfolio.Application.HelperModel
{
	public class AssetHelperModel
	{
		public int Id { get; set; }

		public int? AssetTypeId { get; set; }

		public string? AssetName { get; set; }

		public decimal? PriceUsd { get; set; }

		public DateTime? CreatedOn { get; set; }

		public int? Createby { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public int? UpdatedBy { get; set; }
		public int? Quantity { get; set; }
		public virtual ICollection<AssetTypeHelperModel>? AssetTypeHelperModels { get; set; }
	}
}
