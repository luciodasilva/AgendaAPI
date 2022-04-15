using AgendaAPI.Models;

namespace AgendaAPI.DataContext
{
    public class Context
    {
        public DbSet <Horario> Horarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

    }
}
