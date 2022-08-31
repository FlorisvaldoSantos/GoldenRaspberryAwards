using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Services.Interface;
using Service.Validators;
using System;
using System.Threading.Tasks;

namespace GoldenRaspberryAwards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoldenRaspberryAwardsController : ControllerBase
    {
        private readonly ILogger<GoldenRaspberryAwardsController> _logger;
        private readonly IProducerBaseService<ProducerDTO> producerBaseService;

        public GoldenRaspberryAwardsController(ILogger<GoldenRaspberryAwardsController> logger, IProducerBaseService<ProducerDTO> _producerBaseService)
        {
            this._logger = logger;
            this.producerBaseService = _producerBaseService;
        }

        /// <summary>
        /// Insere novo produtor na Base de Dados  
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateProducer([FromBody] ProducerDTO producerDTO)
        {
            try
            {
                if (producerDTO == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

                var result = await producerBaseService.CreateProducer<ProducerDTO, ProducerModel, ProducerValidator>(producerDTO);

                return Created($"/api/GoldenRaspberryAwards/CreateProducer/{result.Id}", result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao processar: {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Lista todos os Produtores
        /// </summary>
        [HttpGet]
        [Route("GetAllProducer")]
        public async Task<IActionResult> GetAllProducer()
        {
            try
            {
                if (!await producerBaseService.HasProducer())
                    return NoContent();

                return Ok(await producerBaseService.GetAllProducers<ProducerModel>());
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao processar: {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Busca produtor por Id 
        /// </summary>
        [HttpGet]
        [Route("GetProducerById/{Id}")]
        //[Route("{Id}", Name ="GetProducerById")]
        public async Task<IActionResult> GetProducer(int Id)
        {
            try
            {
                return Ok(await producerBaseService.GetProducersById<ProducerModel>(Id));
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao processar: {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Atualiza dados Produtor
        /// </summary>
        [HttpPut]
        [Route("UpdateProducer")]
        public async Task<IActionResult> UpdateProducer([FromBody] ProducerDTO producerDTO)
        {
            try
            {
                return Ok(await producerBaseService.UpdateProducer<ProducerDTO, ProducerModel, ProducerValidator>(producerDTO));
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao processar: {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Remove Produtor da Base por Id
        /// </summary>
        [HttpDelete("DeleteProducerById/{id}")]
        public async Task<IActionResult> DeleteProducer(int Id)
        {
            try
            {
                return Ok(await producerBaseService.DeleteProducer(Id));
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao processar: {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Remove Produtor da Base por Nome
        /// </summary>
        [HttpDelete("DeleteProducerByName/{name}")]
        public async Task<IActionResult> DeleteProducer(string name)
        {
            try
            {

                if (string.IsNullOrEmpty(name))
                    return BadRequest();
                
                if (!await producerBaseService.HasProducer(new ProducerDTO() { Producer = name }))
                    return NotFound();

              
                return Ok(await producerBaseService.DeleteProducer(name));
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao processar: {e.Message}");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Lista todos os Produtores
        /// </summary>
        [HttpGet]
        [Route("GetAwardProducer")]
        public async Task<IActionResult> GetAwardProducer()
        {
            try
            {
                if (!await producerBaseService.HasProducer())
                    return NotFound();

                return Ok(await producerBaseService.GetAwardProducer());
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao processar: {e.Message}");
                return BadRequest(e.Message);
            }
        }


     
    }
}
