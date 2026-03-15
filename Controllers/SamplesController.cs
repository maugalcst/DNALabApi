using DnaLabApi.Dtos;
using DnaLabApi.Entities;
using DnaLabApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DnaLabApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SamplesController : ControllerBase
    {
        private readonly ISampleRepository _sampleRepository;

        public SamplesController(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<SampleDto>> GetAllAsync()
        {
            return (await _sampleRepository.GetAllAsync()).Select(sample => sample.AsDto());
        }

        [ActionName("GetByIdAsync")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SampleDto>> GetByIdAsync(Guid id)
        {
            var sampleToReturn = await _sampleRepository.GetByIdAsync(id);
            if (sampleToReturn == null)
            {
                return NotFound();
            }
            return Ok(sampleToReturn.AsDto());
        }

        [HttpPost]
        public async Task<ActionResult> CreateSampleAsync(CreateSampleDto sample)
        {
            
            var createdSample = new Sample
            {
                Id = Guid.NewGuid(),
                DonorName = sample.DonorName,
                DonorAge = sample.DonorAge,
                DonorSex = sample.DonorSex,
                Type = sample.SampleType,
                Status = Enums.SampleStatus.Collected,
                CollectedDate = DateTimeOffset.UtcNow
            };

            await _sampleRepository.CreateAsync(createdSample);
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdSample.Id },
                createdSample.AsDto()
            );
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateSampleDto sample)
        {
            var oldSample = await _sampleRepository.GetByIdAsync(id);
            if (oldSample is null)
                return NotFound();
            
            var oldCollectedDate = oldSample.CollectedDate;
            
            var updatedSample = new Sample
            {
                Id = id,
                DonorName = sample.DonorName,
                DonorAge = sample.DonorAge,
                DonorSex = sample.DonorSex,
                Type = sample.SampleType,
                Status = sample.Status,
                CollectedDate = oldCollectedDate
            };

            await _sampleRepository.UpdateAsync(updatedSample);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var toDelete = await _sampleRepository.GetByIdAsync(id);
            if (toDelete == null)
                return NotFound();
            
            await _sampleRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}