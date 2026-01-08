namespace AssetPortfolio.Core.Models;

public partial class UserAsset
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? AssetId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Asset? Asset { get; set; }

    public virtual User? User { get; set; }
}
