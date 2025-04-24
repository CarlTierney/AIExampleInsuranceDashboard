using System;
using System.Collections.Concurrent;

namespace DomainDataGenerator
{
    public static class EmailAddressGenerator
    {
        private static readonly ConcurrentDictionary<string, string> EmailCache = new();
        private static readonly string[] PersonalDomains = new[] { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "icloud.com", "aol.com", "mail.com", "protonmail.com" };
        private static readonly ConcurrentDictionary<string, int> CompanyEmailCounters = new();
        private static readonly ConcurrentDictionary<string, string> CompanyEmailCache = new();
        private static int _uniqueCounter = 1;
        private static readonly object CounterLock = new();

        public static string GenerateEmail(string firstName, string lastName, bool unique)
        {
            var key = $"{firstName.ToLower()}_{lastName.ToLower()}";
            if (!unique)
            {
                // Return cached email if exists, otherwise generate and cache
                return EmailCache.GetOrAdd(key, _ => $"{firstName.ToLower()}.{lastName.ToLower()}@example.com");
            }
            // Always generate a unique email
            int count;
            lock (CounterLock)
            {
                count = _uniqueCounter++;
            }
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}{count}@example.com";
            EmailCache[key] = email; // Update cache to latest unique
            return email;
        }

        public static string GeneratePersonalEmail(string firstName, string lastName, bool unique)
        {
            var key = $"{firstName.ToLower()}_{lastName.ToLower()}";
            if (!unique)
            {
                return EmailCache.GetOrAdd(key, _ =>
                {
                    var domain = PersonalDomains[Random.Shared.Next(PersonalDomains.Length)];
                    return $"{firstName.ToLower()}.{lastName.ToLower()}@{domain}";
                });
            }
            int count;
            lock (CounterLock)
            {
                count = _uniqueCounter++;
            }
            var domainUnique = PersonalDomains[Random.Shared.Next(PersonalDomains.Length)];
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}{count}@{domainUnique}";
            EmailCache[key] = email;
            return email;
        }

        public static string GenerateCompanyEmail(string firstName, string lastName, string companyName)
        {
            var baseEmail = $"{firstName.ToLower()}.{lastName.ToLower()}@{companyName.ToLower()}.com";
            var key = baseEmail;
            if (!CompanyEmailCache.ContainsKey(key))
            {
                CompanyEmailCache[key] = baseEmail;
                CompanyEmailCounters[key] = 1;
                return baseEmail;
            }
            // Email already used, append a unique number after last name
            int counter = CompanyEmailCounters.AddOrUpdate(key, 2, (_, old) => old + 1);
            var uniqueEmail = $"{firstName.ToLower()}.{lastName.ToLower()}{counter}@{companyName.ToLower()}.com";
            CompanyEmailCache[uniqueEmail] = uniqueEmail;
            return uniqueEmail;
        }
    }
}
