namespace ProMgr.Models;
public class Monument {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ProjectManager { get; set; }
        public string? Misc { get; set; }
        public ICollection<Epic>? Epics { get; set; }

}