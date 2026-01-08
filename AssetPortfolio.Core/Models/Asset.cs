namespace AssetPortfolio.Core.Models;

public partial class Asset
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

    public virtual AssetType? AssetType { get; set; }

    public virtual ICollection<UserAsset> UserAssets { get; set; } = new List<UserAsset>();
}
