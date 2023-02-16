using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


// Type function => Delegate
// Destiné à ajouter des options au modelBuilder
public delegate void CustomizeModelDelegate(ModelBuilder modelBuilder);
public delegate void SeedDataDelegate(ModelBuilder modelBuilder);

// Hériter de IdentityDbContext<UtilisateurDAO,RoleDAO, Guid>
// ajoute à ma DAL des DbSets => Users, Roles, Claims..
public class HRContext : IdentityDbContext<UtilisateurDAO,RoleDAO, Guid>
{

    private readonly CustomizeModelDelegate customizeModel;
    private readonly SeedDataDelegate? seedData;

    public HRContext(
        DbContextOptions<HRContext> options,
        // Paramètre de construction
        // On demande une fonction pour customizer le modele
        CustomizeModelDelegate? customizeModel = null,
        SeedDataDelegate? seedData = null
    

        ) : base(options)
    {
        this.customizeModel = customizeModel;
        this.seedData = seedData;
        this.Database.EnsureCreated();
    }
    public DbSet<EmployeDAO> Employes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Spécifications de base de ma DAL
        modelBuilder.Entity<EmployeDAO>(entity =>
        {
            entity.HasKey(c => c.Id);
            // Optimisation des recherches par la propriété NomPrenom
            entity.HasIndex(c => c.NomPrenom);
            entity.HasMany(c => c.SousFifres).WithOne(c => c.Chef).HasForeignKey(c => c.IdChef);
            entity.Property(c => c.NomPrenom).IsRequired().HasMaxLength(50);
        });
        // L'application qui utilise cette DAL va choisir des options supplémentaires

        if (this.customizeModel != null)
        {
            // Cette fonction est fournie par l'injection de dépendance
            this.customizeModel(modelBuilder);
        }

        if (this.seedData != null)
        {
            // Cette fonction est fournie par l'injection de dépendance
            this.seedData(modelBuilder);
        }
    }
}