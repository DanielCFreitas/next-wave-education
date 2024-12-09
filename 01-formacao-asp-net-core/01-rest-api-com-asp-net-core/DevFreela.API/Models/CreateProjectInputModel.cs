namespace DevFreela.API.Models;

public class CreateProjectInputModel
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int IdCliente { get; set; }
    public int IdFreelancer { get; set; }
    public decimal TotalCost { get; set; }
}