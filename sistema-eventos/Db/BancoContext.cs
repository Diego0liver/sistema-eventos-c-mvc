using Microsoft.EntityFrameworkCore;
using sistema_eventos.Models;

namespace sistema_eventos.Db
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        public DbSet<EventosModel> Eventos { get; set; }
        public DbSet<CadeiraModel> Cadeiras { get; set; }
        public DbSet<CadeiraEventoModel> CadeiraEvento { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TicketsModel> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CadeiraEventoModel>()
                .HasOne(ec => ec.cadeiras)
                .WithMany(c => c.CadeiraEvento)
                .HasForeignKey(ec => ec.cadeira_id);

            modelBuilder.Entity<CadeiraEventoModel>()
                .HasOne(ec => ec.eventos)
                .WithMany(e => e.CadeiraEvento)
                .HasForeignKey(ec => ec.evento_id);

            modelBuilder.Entity<TicketsModel>()
                .HasOne(t => t.CadeiraEvento)
                .WithMany(ce => ce.Tickets)
                .HasForeignKey(t => t.cadeiras_eventos_id);

            modelBuilder.Entity<TicketsModel>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.user_id);
        }
    }
}
