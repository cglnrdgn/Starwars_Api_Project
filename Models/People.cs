﻿namespace P1.Models
{
    public class People:Model
    {
        public string? gender { get; set; }
        public string? url { get; set; }
        public string? height { get; set; }
        public string? hair_color { get; set; }
        public string? skin_color { get; set; }
        public string? name { get; set; }
        public List<string>? films { get; set; }
        public string? birth_year { get; set; }
        public string? homeworld { get; set; }
        public string? eye_color { get; set; }
        public string? mass { get; set; }
    }
}
