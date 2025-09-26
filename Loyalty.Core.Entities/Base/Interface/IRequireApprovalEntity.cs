namespace Loyalty.Core.Entities.Base.Interface
{
    public interface IRequireApprovalEntity
    {
        bool IsApproved { get; set; }
    }
}
