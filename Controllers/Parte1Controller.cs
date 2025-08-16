using Microsoft.AspNetCore.Mvc;
using ProvaPub.Dtos;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers
{
	/// <summary>
	/// Ao rodar o código abaixo o serviço deveria sempre retornar um número diferente, mas ele fica retornando sempre o mesmo número.
	/// 1 - Faça as alterações para que o retorno seja sempre diferente
	/// 2 - Tome cuidado 
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte1Controller :  ControllerBase
	{
		private readonly IRandomNumberService _randomService;

		public Parte1Controller(IRandomNumberService randomService)
		{
			_randomService = randomService;
		}

		[HttpGet]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<int> Index(CancellationToken cancellationToken)
		{
			return await _randomService.GetRandomAsync(cancellationToken);
		}
	}
}
