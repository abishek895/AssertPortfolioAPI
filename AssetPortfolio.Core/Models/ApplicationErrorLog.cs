namespace AssetPortfolio.Core.Models;

public partial class ApplicationErrorLog
{
    public int Id { get; set; }

    public string? ErrorMessage { get; set; }

    public string? Method { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }
}
