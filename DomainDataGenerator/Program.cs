using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DomainDataGenerator
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Seeding insurance domain data...");

            var configuration = new ConfigurationBuilder()
                 .AddUserSecrets<Program>() // Use User Secrets for development
                .AddJsonFile("appsettings.json", optional: true) 
                .Build();

            var testDbConnectionString = configuration["aitest"];
            if (string.IsNullOrEmpty(testDbConnectionString))
            {
                Console.WriteLine("Connection string not found in appsettings.json. Exiting.");
                return;
            }

            var optionsBuilder = new DbContextOptionsBuilder<InsuranceDbContext>();
            optionsBuilder.UseSqlServer(
                configuration["aitest"]); // TODO: Replace with your actual connection string

            using var context = new InsuranceDbContext(optionsBuilder.Options);
            var random = new Random();

            var statuses = GenerateClaimStatuses(context);
            var agents = GenerateAgents(context, random);
            var (customers, policies, claims, assignments, payments) = GenerateCustomersAndClaims(
                context, statuses, agents, random);

            context.Customers.AddRange(customers);
            context.Policies.AddRange(policies);
            context.Claims.AddRange(claims);
            context.ClaimAssignments.AddRange(assignments);
            context.Payments.AddRange(payments);
            context.SaveChanges();

            Console.WriteLine(
                "Seeded 4000 customers, 40 agents, 5 claim statuses, and claims/payments/assignments as specified.");
        }

        // --- Modular Methods ---

        static List<ClaimStatus> GenerateClaimStatuses(InsuranceDbContext context)
        {
            var statusNames = new[] { "notified", "pending", "approved", "rejected", "closed" };
            var statuses = statusNames
                .Select((name, i) => new ClaimStatus { ClaimStatusId = i + 1, StatusName = name }).ToList();
            context.ClaimStatuses.AddRange(statuses);
            context.SaveChanges();
            return statuses;
        }

        static List<Agent> GenerateAgents(InsuranceDbContext context, Random random)
        {
            var agents = new List<Agent>();
            for (int i = 0; i < 40; i++)
            {
                var firstName = NameGenerator.GenerateFirstName();
                var lastName = NameGenerator.GenerateLastName();
                var email = EmailAddressGenerator.GenerateCompanyEmail(firstName, lastName, "progressiveedge");
                agents.Add(new Agent
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Phone = $"555-02{random.Next(1000, 9999)}"
                });
            }

            context.Agents.AddRange(agents);
            context.SaveChanges();
            return agents;
        }

        static (List<Customer>, List<Policy>, List<Claim>, List<ClaimAssignment>, List<Payment>)
            GenerateCustomersAndClaims(
                InsuranceDbContext context,
                List<ClaimStatus> statuses,
                List<Agent> agents,
                Random random)
        {
            DateTime now = DateTime.UtcNow;
            DateTime earliest = now.AddMonths(-18);
            var customers = new List<Customer>();
            var policies = new List<Policy>();
            var claims = new List<Claim>();
            var assignments = new List<ClaimAssignment>();
            var payments = new List<Payment>();
            int agentIndex = 0;

            for (int i = 0; i < 4000; i++)
            {
                var firstName = NameGenerator.GenerateFirstName();
                var lastName = NameGenerator.GenerateLastName();
                var email = EmailAddressGenerator.GeneratePersonalEmail(firstName, lastName, false);
                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = now.AddYears(-random.Next(18, 80)),
                    Email = email,
                    Phone = $"555-01{random.Next(1000, 9999)}",
                    Address = $"{random.Next(100, 999)} Main St",
                    Policies = new List<Policy>()
                };
                int claimCount = random.Next(1, 5);
                for (int j = 0; j < claimCount; j++)
                {
                    var claimDate = earliest.AddDays(random.Next((now - earliest).Days));
                    var policy = new Policy
                    {
                        PolicyNumber = $"POL-{i:D4}-{j:D2}",
                        PolicyType = "Auto",
                        StartDate = claimDate.AddMonths(-random.Next(1, 12)),
                        EndDate = claimDate.AddMonths(random.Next(1, 12)),
                        PremiumAmount = random.Next(500, 2000),
                        Claims = new List<Claim>()
                    };
                    var (status, statusName) = PickClaimStatus(statuses, claimDate, now, random);
                    var claimAmount = random.Next(1000, 10000);
                    decimal? approvedAmount = (statusName == "approved" || statusName == "closed")
                        ? (decimal?)(claimAmount * (decimal)(0.7 + random.NextDouble() * 0.3))
                        : null;
                    var claim = new Claim
                    {
                        ClaimNumber = $"CLM-{i:D4}-{j:D2}",
                        DateFiled = claimDate,
                        ClaimAmount = claimAmount,
                        ApprovedAmount = approvedAmount,
                        Description = "Auto accident claim",
                        ClaimStatusId = status.ClaimStatusId,
                        Payments = new List<Payment>(),
                        ClaimAssignments = new List<ClaimAssignment>()
                    };
                    // Agent assignment logic
                    if (ShouldAssignAgent(statusName, claimDate, now, random))
                    {
                        var agent = agents[agentIndex % agents.Count];
                        agentIndex++;
                        var assignment = new ClaimAssignment
                        {
                            Agent = agent,
                            Claim = claim,
                            AssignedDate = claimDate.AddDays(random.Next(0, 5))
                        };
                        claim.ClaimAssignments.Add(assignment);
                        assignments.Add(assignment);
                    }

                    // Payments for approved/closed
                    if ((statusName == "approved" || statusName == "closed") && approvedAmount.HasValue)
                    {
                        GeneratePaymentsForClaim(claim, payments, claimDate, approvedAmount.Value, random);
                    }

                    policy.Claims.Add(claim);
                    claims.Add(claim);
                    policies.Add(policy);
                    customer.Policies.Add(policy);
                }

                customers.Add(customer);
            }

            return (customers, policies, claims, assignments, payments);
        }

        static (ClaimStatus, string) PickClaimStatus(List<ClaimStatus> statuses, DateTime claimDate, DateTime now,
            Random random)
        {
            string[] statusNames = statuses.Select(s => s.StatusName).ToArray();
            string statusName;
            if ((now - claimDate).TotalDays > 60 && random.NextDouble() < 0.9)
            {
                statusName = random.NextDouble() < 0.5 ? "approved" : "rejected";
            }
            else
            {
                statusName = statusNames[random.Next(statusNames.Length)];
            }

            var status = statuses.First(s => s.StatusName == statusName);
            return (status, statusName);
        }

        static bool ShouldAssignAgent(string statusName, DateTime claimDate, DateTime now, Random random)
        {
            if (statusName != "pending")
                return true;
            if ((now - claimDate).TotalDays <= 30)
                return random.NextDouble() > 0.5; // 50% of recent pending claims have no agent
            return true;
        }

        static void GeneratePaymentsForClaim(Claim claim, List<Payment> payments, DateTime claimDate,
            decimal approvedAmount, Random random)
        {
            int paymentCount = random.Next(1, 3);
            decimal totalPaid = 0;
            for (int p = 0; p < paymentCount; p++)
            {
                decimal maxPay = approvedAmount - totalPaid;
                if (p == paymentCount - 1 || maxPay < 1) break;
                decimal pay = Math.Round((decimal)(random.NextDouble() * (double)maxPay * (p == 0 ? 0.7 : 1)), 2);
                if (pay < 1) pay = maxPay;
                var payment = new Payment
                {
                    PaymentDate = claimDate.AddDays(random.Next(1, 60)),
                    Amount = pay,
                    PaymentStatus = "Completed",
                    Claim = claim
                };
                claim.Payments.Add(payment);
                payments.Add(payment);
                totalPaid += pay;
            }
        }
    }
}