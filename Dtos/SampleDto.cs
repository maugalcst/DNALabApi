using DnaLabApi.Entities;
using DnaLabApi.Enums;

namespace DnaLabApi.Dtos
{
    public record SampleDto(
        Guid Id,
        string DonorName,
        int DonorAge,
        DonorSex DonorSex,
        SampleType Type,
        DateTimeOffset CollectedDate
    );

    public record CreateSampleDto(
        string DonorName,
        int DonorAge,
        DonorSex DonorSex,
        SampleType SampleType
    );

    public record UpdateSampleDto(
        string DonorName,
        int DonorAge,
        DonorSex DonorSex,
        SampleType SampleType,
        SampleStatus Status
    );

    public static class SampleExtensions
    {
        public static SampleDto AsDto(this Sample sample)
        {
            return new SampleDto(sample.Id, sample.DonorName, sample.DonorAge, sample.DonorSex, sample.Type, sample.CollectedDate);
        }
    }
}