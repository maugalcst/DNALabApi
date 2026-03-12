using DnaLabApi.Entities;
using DnaLabApi.Enums;

namespace DnaLabApi.Repositories
{
    public class InMemorySamplesRepository : ISampleRepository
    {
        private readonly List<Sample> samples = [
            new Sample { Id = Guid.NewGuid(), DonorName = "John Stuart", DonorAge = 35, DonorSex = DonorSex.Male, Status = SampleStatus.InProcess, Type = SampleType.Blood, CollectedDate = DateTimeOffset.UtcNow },
            new Sample { Id = Guid.NewGuid(), DonorName = "Kyle Smith", DonorAge = 70, DonorSex = DonorSex.Male, Status = SampleStatus.Collected, Type = SampleType.Saliva, CollectedDate = DateTimeOffset.UtcNow },
            new Sample { Id = Guid.NewGuid(), DonorName = "Lena Hart", DonorAge = 25, DonorSex = DonorSex.Female, Status = SampleStatus.Collected, Type = SampleType.Hair, CollectedDate = DateTimeOffset.UtcNow },
            new Sample { Id = Guid.NewGuid(), DonorName = "Novak Djokovic", DonorAge = 38, DonorSex = DonorSex.Male, Status = SampleStatus.Analyzed, Type = SampleType.Blood, CollectedDate = DateTimeOffset.UtcNow },
            new Sample { Id = Guid.NewGuid(), DonorName = "Carlos Alcaraz", DonorAge = 22, DonorSex = DonorSex.Male, Status = SampleStatus.Archived, Type = SampleType.Urine, CollectedDate = DateTimeOffset.UtcNow },
        ];
        public async Task CreateAsync(Sample sample)
        {
            samples.Add(sample);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sampleToDelete = samples.FirstOrDefault(x => x.Id == id);
            if (sampleToDelete != null)
            {
                samples.Remove(sampleToDelete);
            }
        }

        public async Task<IEnumerable<Sample>> GetAllAsync()
        {
            return samples;
        }

        public async Task<Sample> GetByIdAsync(Guid id)
        {
            var sample = samples.FirstOrDefault(x => x.Id == id);
            return sample;
        }

        public async Task UpdateAsync(Sample sample)
        {
            var oldSample = samples.Find(x => x.Id == sample.Id);
            if (oldSample != null)
            {
                samples.Remove(oldSample);
                samples.Add(sample);
            }
        }
    }
}