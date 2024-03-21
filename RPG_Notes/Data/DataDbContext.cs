using Microsoft.EntityFrameworkCore;
using RPG_Notes.Models;

namespace RPG_Notes.Data;

public class DataDbContext : DbContext
{
    public static DataDbContext Instance { get; } = new();

    private static readonly string _connectionString = "server=localhost;database=rpg_notes;user=root;password=987412365";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }

    public DbSet<Note> Notes { get; set; }
}
