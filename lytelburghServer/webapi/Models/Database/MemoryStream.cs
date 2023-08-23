using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

public class ForthbroughtFadungFramework : DbContext
{
    public DbSet<Begetter> Begetters { get; set; }
    public DbSet<Thinkback> Thinkbacks { get; set; }


    public string DbPath { get; }

    public ForthbroughtFadungFramework()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "lytelburgh.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Begetter
{
    public int Id { get; set; }
    public string GivenName { get; set; }
    public string SurName { get; set; }
    public string NickName { get; set; }
    public float Age { get; set; }
    public List<Thinkback> Thinkbacks { get; } = new();
}

public class Thinkback
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Content { get; set; }

    
}

