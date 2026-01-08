namespace AssetPortfolio.Core.Models;

public partial class AssetType
{
    public int Id { get; set; }

    public string? AssetTypeName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
