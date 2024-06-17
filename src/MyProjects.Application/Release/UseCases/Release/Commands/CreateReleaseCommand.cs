﻿using MediatR;

namespace MyProjects.Application.Release.UseCases.Release.Commands
{
    public class CreateReleaseCommand : IRequest
    {
        public string Title { get; set; } 
        public string Description { get; set; }

        public CreateReleaseCommand(string title, string description)
        {
            Title = title;
            Description = description;           
        }
    }
}

