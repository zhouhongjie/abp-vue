﻿namespace AbpVueCli.Models
{
    public class ProjectInfo
    {
        /// <summary>
        ///     接口地址
        /// </summary>
        public string OpenApiAddr { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string TemplateFileDirectory { get; set; }
        public string[] ListQueryIgnoreParams { get; set; }
        public string ListPropertySchemaPath { get; set; }
    }
}