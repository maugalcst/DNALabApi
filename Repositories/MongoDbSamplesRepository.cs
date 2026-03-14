using DnaLabApi.Entities;
using MongoDB.Driver;

namespace DnaLabApi.Repositories
{
    public class MongoDbSamplesRepository : ISampleRepository
    {
        private const string DatabaseName = "dnalab";
        private const string CollectionName = "samples";
        private readonly IMongoCollection<Sample> _collection;


        public MongoDbSamplesRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(DatabaseName);
            _collection = database.GetCollection<Sample>(CollectionName);
        }

        public async Task CreateAsync(Sample sample)
        {
            await _collection.InsertOneAsync(sample);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Sample>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Sample> GetByIdAsync(Guid id)
        {
            return await _collection.Find(sample => sample.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Sample sample)
        {
            await _collection.ReplaceOneAsync(x => x.Id == sample.Id, sample);
        }
    }
}