﻿using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProjManagement.Api.Swagger
{
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "Project Management Api",
                Version = description.ApiVersion.ToString(),
                Description = "An Api to track the achievements of your developers",
                Contact = new OpenApiContact() { Name = "Simeon Chuks", Email = "simeonchuksapi@gmail.com"}
            };

            if (description.IsDeprecated){info.Description += " | Note: This Api version has been deprecated";}

            return info;
        }
    }
}