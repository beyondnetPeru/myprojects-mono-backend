﻿namespace MyProjects.Projects.Api.Models
{
    public class Project
    {
        public string Id { get; set; } = string.Empty;  
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Status { get; set; } = 1;
    }
}
