namespace P1.Models
{
    public class Films:Model
    {
        public short id { get; set; }
        public string? url { get; set; }
        public string? title { get; set; }
        public string? producer { get; set; }
        public string? director { get; set; }
        public List<string>? characters { get; set; }
    }
}
