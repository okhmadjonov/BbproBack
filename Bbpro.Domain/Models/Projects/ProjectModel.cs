﻿using Bbpro.Domain.Entities.Multilanguage;
using Bbpro.Domain.Entities.Projects;

namespace Bbpro.Domain.Models.Projects;

public class ProjectModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public Language Title { get; set; }
    public Language Description { get; set; }

    public string DownloadLink { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ProjectModel MapFromEntity(Project entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl,
            Title = entity.Title,
            Description = entity.Description,
            DownloadLink = entity.DownloadLink,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
