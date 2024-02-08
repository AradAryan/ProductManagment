namespace Domain.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public bool IsAvailable { get; set; }
    }
}