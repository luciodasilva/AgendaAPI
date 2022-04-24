namespace AgendaAPI.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int RegistroFuncionario { get; set; }
        public string? Cargo { get; set; }
        public string? Telefone { get; set; }
        public Cliente? Cliente { get; set; }

    }
}
