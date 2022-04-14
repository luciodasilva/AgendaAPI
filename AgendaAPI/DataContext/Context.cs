using AgendaAPI.Models;

namespace AgendaAPI.DataContext
{
    public class Context
    {
        public DbSet <Agenda> Agenda { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

    }
}
