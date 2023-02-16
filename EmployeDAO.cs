public class EmployeDAO{
    public Guid Id { get; set; }=Guid.NewGuid();

    public string NomPrenom { get; set; }

    public Decimal Salaire { get; set; }

    public int CreditRepas { get; set; }
    public DateTime DateEntree { get; set; } = DateTime.Now;
    public bool Allergies { get; set; } = false;

    public Guid? IdChef { get; set; }

    public EmployeDAO? Chef { get; set; }
    public ICollection<EmployeDAO> SousFifres { get; set; }

}