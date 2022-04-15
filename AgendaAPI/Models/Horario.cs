namespace AgendaAPI.Models
{
    public class Horario
    {
        public DateTime DataHora { get; set; }
        public int ClienteId { get; set; }
        public Cliente cliente { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
