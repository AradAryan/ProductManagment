using System.Text.Json.Serialization;

namespace Application.Dtos
{
    public class BaseDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public string? Creator { get; set; }

        [JsonIgnore]
        public bool IsAvailable { get; set; }
    }
}
