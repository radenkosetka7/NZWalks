using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;
using System.Xml.Linq;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;
        private readonly IMemoryCache cache;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, 
            ILogger<RegionsController> logger, IMemoryCache cache)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.cache = cache;
        }

        [HttpGet]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();
            logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
            return Ok(mapper.Map<List<RegionDTO>>(regionsDomain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            string cacheKey = $"Region-{id}";
            if(!cache.TryGetValue(cacheKey, out var region))
            {
                region = await regionRepository.GetByIdAsync(id);
                if (region == null)
                {
                    return NotFound();
                }
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                cache.Set(cacheKey, region, cacheOptions);
            }
            logger.LogInformation($"Finished GetById request with data: {JsonSerializer.Serialize(region)}");
            return Ok(mapper.Map<RegionDTO>(region));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO requestDTO)
        {

             var regionDomainModel = mapper.Map<Region>(requestDTO);
             regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
             var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            logger.LogInformation($"Finished Create request with data: {JsonSerializer.Serialize(regionDomainModel)}");
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO requestDTO)
        {
             var regionDomainModel = mapper.Map<Region>(requestDTO);
             regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
             if (regionDomainModel == null)
             {
                return NotFound();
             }
            logger.LogInformation($"Finished Update request with data: {JsonSerializer.Serialize(regionDomainModel)}");
            return Ok(mapper.Map<RegionDTO>(regionDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            logger.LogInformation($"Finished Delete request with data: {JsonSerializer.Serialize(regionDomainModel)}");
            return Ok();

        }
    }
}
