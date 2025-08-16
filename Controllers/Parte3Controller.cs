using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Models.Enum;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class Parte3Controller : ControllerBase
    {
        private readonly IOrderService _orderService;

        public Parte3Controller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("orders")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<OrderReponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequestDto orderRequest, CancellationToken cancellationToken)
        {
            var response = await _orderService.PayOrderAsync(orderRequest, cancellationToken);
            return Ok(new ApiResponse<OrderReponseDto>(response));
        }
    }
}
