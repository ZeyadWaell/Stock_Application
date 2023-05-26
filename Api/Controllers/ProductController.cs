using Api.Dto;
using Api.Helpers;
using Api.ResponseModule;
using AutoMapper;
using Core.Entites;
using Core.Interface;
using Core.Specfication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductController : BaseController
    {
        //private readonly IProductRepostory _productRepostory;
        private readonly IGenericRepostory<Product> _productRepostory;
        private readonly IGenericRepostory<ProductBrand> _productbrandRepostory;
        private readonly IGenericRepostory<ProductType> _productTypeRepostory;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper,IGenericRepostory<Product> productRepostory, IGenericRepostory<ProductBrand> productbrandRepostory, IGenericRepostory<ProductType> productTypeRepostory/*IProductRepostory productRepostory*/)
        {
            _productbrandRepostory = productbrandRepostory;
            _productRepostory = productRepostory;
            _productTypeRepostory = productTypeRepostory;
            _mapper = mapper;
            //_productRepostory = productRepostory;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts([FromQuery] ProductSpecParms productspecfication)
        {
           var Specfication = new ProductWithTypeAndBrandSpecfications(productspecfication);
            var countSpec = new ProductwithFilterForCountSpecfication(productspecfication);
            var totalItems = await _productRepostory.CountAsync(countSpec);

            var product = await _productRepostory.ListAsync(Specfication);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(product);
            var paginatedData = new Pagination<ProductDto>(productspecfication.PageIndex,productspecfication.PageSize, totalItems,mappedProducts);
            return Ok(paginatedData);
        }

        [HttpGet("GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int Id)
        {
            var Specfication = new ProductWithTypeAndBrandSpecfications(Id);
            var product = await _productRepostory.GetEntityWithSpecfictions(Specfication);
            var mappedProducts = _mapper.Map<ProductDto>(product);

            if (product == null)
                return NotFound(new ApiResponse(400));


            return Ok(mappedProducts);
        }
        [HttpGet("GetProductstype")]
        public async Task<IReadOnlyList<ProductType>> GetProductType()
        {
            var product = await _productTypeRepostory.GetAllAsync();
            return product;
        }
        [HttpGet("GetProductsBrand")]
        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrand()
        {
            var product = await _productbrandRepostory.GetAllAsync();
            return product;
        }
    }
}
