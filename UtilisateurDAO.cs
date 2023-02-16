using Microsoft.AspNetCore.Identity;

// IdentityUser<Guid> = classe définie dans IdentityFramework
// Qui contient déjà toutes les propréiétés essentielles pour stocker un utilisateir
public class UtilisateurDAO : IdentityUser<Guid>{
    public string CouleurPreferee { get; set; }
}

