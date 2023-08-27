using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string GivenName { get; set; }
    public string SurName { get; set; }
    public string NickName { get; set; }
    public float Age { get; set; }
    public GenderEnum Gender { get; set; } = GenderEnum.Custom;
    public string? CustomGender { get; set; }

    public ICollection<Thinkback> Thinkbacks { get; set; } = new List<Thinkback>();
    
    public Begetter(string givenName, string surName, string nickName, float age, GenderEnum gender, ICollection<Thinkback>? thinkbacks = null)
    {
        GivenName = givenName;
        SurName = surName;
        NickName = nickName;
        Age = age;
        Thinkbacks = thinkbacks ?? Thinkbacks;
        Gender = gender;
    }

    public Begetter(string givenName, string surName, string nickName, float age, string customGender, ICollection<Thinkback>? thinkbacks = null)
    {
        GivenName = givenName;
        SurName = surName;
        NickName = nickName;
        Age = age;
        Thinkbacks = thinkbacks ?? Thinkbacks;
        CustomGender = customGender;
    }

}

public enum GenderEnum
{
    Male,
    Female,
    NonBinary,
    Custom
}


public class Thinkback
{
    public Thinkback(DateTime dateTime, string narrative, Begetter begetter)
    {
        DateTime = dateTime;
        Narrative = narrative;
        Begetter = begetter;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    // Time steps should probably be per second
    public DateTime DateTime { get; set; }
    public string Narrative { get; set; }

    public Begetter Begetter { get; set; } // Navigation property for the agent


}

