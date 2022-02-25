using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ProMgr.Models;
public class Epic {
        public int EpicId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Misc { get; set; }
        
        [JsonIgnore]
        public Monument Monument {get; set;} 
        public ICollection<Story>? Stories { get; set; }
}