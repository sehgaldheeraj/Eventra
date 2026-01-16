namespace Domain.Common
{
    public abstract class SoftDeletableEntity : Entity
    {
        public DateTime? DeletedAt { get; private set; }

        public bool IsDeleted => DeletedAt.HasValue;

        protected void MarkDeleted()
        {
            if (IsDeleted)
                return;

            DeletedAt = DateTime.UtcNow;
        }
    }
}
