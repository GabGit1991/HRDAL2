using Microsoft.AspNetCore.Identity;

public class RoleDAO : IdentityRole<Guid>{
    public RoleDAO()
    {
              this.Id=Guid.NewGuid();
    }
    public string EnglishName { get; set; }
}

