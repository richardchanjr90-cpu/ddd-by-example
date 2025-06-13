namespace Loyalty.Data.Entities.Base
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public bool IsNew => Id <= 0;
    }
}