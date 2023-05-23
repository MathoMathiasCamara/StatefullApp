using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatefullAPI.Stores;

namespace StatefullAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly ProductStore _productStore;
        private readonly SaleStore _saleStore;
        public OperationController(ProductStore productStore, SaleStore saleStore)
        {
            _productStore = productStore;
            _saleStore = saleStore;
        }

        [HttpGet("GetProducts")]
        public Task<IEnumerable<Product>> Products()
        {
            return Task.FromResult(_productStore.GetProducts().AsEnumerable());
        }

        [HttpGet("GetSales")]
        public Task<IEnumerable<Sale>> Sales()
        {
            return Task.FromResult(_saleStore.GetSales().AsEnumerable());
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            if (product == null) return NotFound();

            _productStore.AddProduct(product);

            return Ok();
        }

        [HttpPost("AddSale")]
        public IActionResult AddSale(Sale sale)
        {
            if (sale == null) return NotFound();

            _saleStore.AddSale(sale);

            return Ok();
        }

        [HttpPost("RollbackToDate")]
        public IActionResult RollbackToDate(DateTime rollbackToDate)
        {
            return Ok(new
            {
                Products = _productStore.GetProducts(rollbackToDate),
                Sales = _saleStore.GetSales(rollbackToDate)
            });
        }

        [HttpGet("GetDatabases")]
        public IActionResult GetDatabase()
        {
            return Ok(new
            {
                Products = _productStore.GetDatabase(),
                Sales = _saleStore.GetDatabase(),
            });
        }
    }
}
