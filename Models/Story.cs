using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProMgr.Models;
public class Story {
        public int StoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Responisble { get; set; }
        public string? Misc { get; set; }
        
        [JsonIgnore]
        public Epic Epic {get; set;} 
}