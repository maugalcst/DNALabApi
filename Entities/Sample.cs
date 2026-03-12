
using DnaLabApi.Enums;

namespace DnaLabApi.Entities
{
    public class Sample
    {
        public Guid Id { get; init; }
        public string DonorName { get; init; }
        public int DonorAge { get; init; }
        public DonorSex DonorSex { get; init; }
        public SampleType Type { get; init; }
        public SampleStatus Status { get; init; }
        public DateTimeOffset CollectedDate { get; init; }
    }
}