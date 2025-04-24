using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) 
        {
          
        }

        

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimStatus> ClaimStatuses { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<ClaimAssignment> ClaimAssignments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e=> e.LastName).IsRequired();
                entity.Property(e => e.DateOfBirth).IsRequired();
            });

            // Policy
            modelBuilder.Entity<Policy>(entity =>
            {
                entity.ToTable("Policies");
                entity.HasKey(e => e.PolicyId);
                entity.Property(e => e.PolicyNumber).IsRequired();
                entity.HasOne(e => e.Customer)
                      .WithMany(c => c.Policies)
                      .HasForeignKey(e => e.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Claim
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("Claims");
                entity.HasKey(e => e.ClaimId);
                entity.Property(e => e.ClaimNumber).IsRequired();
                entity.HasOne(e => e.Policy)
                      .WithMany(p => p.Claims)
                      .HasForeignKey(e => e.PolicyId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.ClaimStatus)
                      .WithMany(s => s.Claims)
                      .HasForeignKey(e => e.ClaimStatusId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ClaimStatus
            modelBuilder.Entity<ClaimStatus>(entity =>
            {
                entity.ToTable("ClaimStatuses");
                entity.HasKey(e => e.ClaimStatusId);
                entity.Property(e => e.StatusName).IsRequired();
            });

            // Agent
            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agents");
                entity.HasKey(e => e.AgentId);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
            });

            // ClaimAssignment
            modelBuilder.Entity<ClaimAssignment>(entity =>
            {
                entity.ToTable("ClaimAssignments");
                entity.HasKey(e => e.ClaimAssignmentId);
                entity.HasOne(e => e.Claim)
                      .WithMany(c => c.ClaimAssignments)
                      .HasForeignKey(e => e.ClaimId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Agent)
                      .WithMany(a => a.ClaimAssignments)
                      .HasForeignKey(e => e.AgentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payments");
                entity.HasKey(e => e.PaymentId);
                entity.HasOne(e => e.Claim)
                      .WithMany(c => c.Payments)
                      .HasForeignKey(e => e.ClaimId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
