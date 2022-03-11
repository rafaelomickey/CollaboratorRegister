using CollaboratorRegisterApi.Interfaces.Services;
using CollaboratorRegisterApi.Models.Requests;
using CollaboratorRegisterApi.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CollaboratorRegisterApi.Controllers
{
    [Route("v{version:apiVersion}/collaborator")]
    [ApiVersion("1")]
    [ApiController]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService _collaboratorService;
        private readonly ILogger<CollaboratorController> _logger;
        public CollaboratorController(ILogger<CollaboratorController> logger, ICollaboratorService collaboratorService)
        {
            _logger = logger;
            _collaboratorService = collaboratorService;
        }

        /// <summary>
        /// Método que adiciona um colaborador
        /// </summary>
        /// <param name="request">CollaboratorAddRequest model para criar um novo colaborador</param>
        /// <returns>retorna o id do novo usuario criado</returns>
        /// <response code="201">Retorna quando o colaborador é criado com sucesso</response>  
        /// <response code="409">Retorna quando a validação falha</response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Add([FromBody] CollaboratorAddRequest request)
        {
            try
            {
                var response = await _collaboratorService.Add(request);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ObjectResult(new BaseResponse { StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        /// <summary>
        /// Método que atualiza um colaborador
        /// </summary>
        /// <param name="request">CollaboratorUpdateRequest model para atualizar um colaborador existente</param>
        /// <response code="200">Retorna quando o colaborador é atualizado com sucesso</response>  
        /// <response code="409">Retorna quando a validação falha</response>
        [HttpPatch]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromBody] CollaboratorUpdateRequest request)
        {
            try
            {
                var response = await _collaboratorService.Update(request);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ObjectResult(new BaseResponse { StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        /// <summary>
        /// Método que exclui um colaborador
        /// </summary>
        /// <param name="collaboratorId">Id do colaborador que deseja excluir</param>
        /// <response code="200">Retorna quando o colaborador é excluido com sucesso</response>  
        /// <response code="409">Retorna quando a validação falha</response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromQuery] int collaboratorId)
        {
            try
            {
                var response = await _collaboratorService.Delete(collaboratorId);
                return new ObjectResult(response) { StatusCode = response.StatusCode };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ObjectResult(new BaseResponse { StatusCode = StatusCodes.Status500InternalServerError });
            }
        }

        /// <summary>
        /// Método que busca colaboradores
        /// </summary>
        /// <param name="request">CollaboratorGetRequest model para criar um novo colaborador</param>
        /// <returns>CollaboratorResponse retorna uma lista de colaboradores</returns>
        /// <response code="200">Retorna quando a busca é feita com sucesso</response>  
        /// <response code="204">Retorna quando a busca não possui dados para retorno</response>
        [HttpGet]
        [ProducesResponseType(typeof(CollaboratorGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CollaboratorGetResponse), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] CollaboratorGetRequest request)
        {
            var response = await _collaboratorService.Get(request);
            return new ObjectResult(response);
        }
    }
}
