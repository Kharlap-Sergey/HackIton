namespace Api.Domain
{
    public class Entity<TEntityId>
        where TEntityId : struct
    {
        public TEntityId? Id { get; set; }
    }
}
