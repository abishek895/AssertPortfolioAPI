namespace AssetPortfolio.Core.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? ContactNumber { get; set; }

    public int? IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<UserAsset> UserAssets { get; set; } = new List<UserAsset>();
}
