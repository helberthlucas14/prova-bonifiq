using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Interfaces;
using System.Text.Json;

namespace ProvaPub.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Parte2Controller : ControllerBase
    {
        /// <summary>
        /// Precisamos fazer algumas alterações:
        /// 1 - Não importa qual page é informada, sempre são retornados os mesmos resultados. Faça a correção.
        /// 2 - Altere os códigos abaixo para evitar o uso de "new", como em "new ProductService()". Utilize a Injeção de Dependência para resolver esse problema
        /// 3 - Dê uma olhada nos arquivos /Models/CustomerList e /Models/ProductList. Veja que há uma estrutura que se repete. 
        /// Como você faria pra criar uma estrutura melhor, com menos repetição de código? E quanto ao CustomerService/ProductService. Você acha que seria possível evitar a repetição de código?
        /// 
        /// </summary>
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public Parte2Controller(IProductService productService, ICustomerService customerService)
        {
            this.productService = productService;
            this.customerService = customerService;
        }

        [HttpGet("products")]
        public async Task<IActionResult> ListProducts(int page, CancellationToken cancellationToken)
        {
            var response = await productService.PaginedListAsync(page, cancellationToken: cancellationToken);
            return Ok(response);
        }

        [HttpGet("customers")]
        public async Task<IActionResult> ListCustomers(int page, CancellationToken cancellationToken)
        {
            var response = await customerService.PaginedListAsync(page, cancellationToken: cancellationToken);
            return Ok(response);
        }
    }
}
