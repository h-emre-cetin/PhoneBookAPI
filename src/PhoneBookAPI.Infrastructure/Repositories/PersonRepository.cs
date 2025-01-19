using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using PhoneBookAPI.Core.Entities;
using PhoneBookAPI.Core.Interfaces;
using PhoneBookAPI.Infrastructure.Data;
using System.Linq;
using PhoneBookAPI.Core.Entities.Enums;

namespace PhoneBookAPI.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Person> _persons;

        public PersonRepository(MongoDbContext context)
        {
            _context = context;
            _persons = context.Persons;
        }

        public async Task<Person> AddAsync(Person entity)
        {
            await _persons.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _persons.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _persons.Find(_ => true).ToListAsync();
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _persons.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetByLocationAsync(string location)
        {
            return await _persons
                .Find(p => p.ContactInformation.Any(c => 
                    c.Type == ContactType.Location && 
                    c.Value == location && 
                    c.IsActive))
                .ToListAsync();
        }

        public async Task<Dictionary<string, LocationStatistics>> GetLocationStatisticsAsync()
        {
            var persons = await _persons.Find(FilterDefinition<Person>.Empty).ToListAsync();

            var locationStats = new Dictionary<string, LocationStatistics>();

            foreach (var person in persons)
            {
                foreach (var contact in person.ContactInformation.Where(c => c.IsActive))
                {
                    if (!locationStats.ContainsKey(contact.Value))
                    {
                        locationStats[contact.Value] = new LocationStatistics
                        {
                            PersonCount = 0,
                            PhoneNumberCount = 0
                        };
                    }

                    locationStats[contact.Value].PersonCount++;

                    if (contact.Type == ContactType.PhoneNumber)
                    {
                        locationStats[contact.Value].PhoneNumberCount++;
                    }
                }
            }

            return locationStats;
        }

        public async Task UpdateAsync(Person entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            await _persons.ReplaceOneAsync(p => p.Id == entity.Id, entity);
        }
    }
}