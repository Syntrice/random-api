﻿namespace RandomAPI.Options
{
    public sealed class DatabaseOptions
    {
        public const string SectionName = "DatabaseOptions";
        public string DatabasePath { get; set; } = null!;
    }
}