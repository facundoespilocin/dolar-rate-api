using AutoMapper;
using Ecommerce.DataAccessLayer.Entities.Product;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.EmailService;
using Ecommerce.Utils.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using static Ecommerce.Utils.Enums;

namespace Ecommerce.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ProductsService(IProductsRepository productsRepository, IOptions<AppSettings> appSettings, IMapper mapper, IEmailService emailService)
        {
            _productsRepository = productsRepository;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll(SearchRequest request)
        {
            var products = await _productsRepository.GetAll(request);

            return products;
        }
        
        public async Task<ProductDTO> GetById(long productId)
        {
            var products = await _productsRepository.GetById(productId);

            return products;
        }

        public async Task<ServiceResponse> PostCreateProduct(ProductDTO request)
        {
            ServiceResponse response = new();

            try
            {
                response = await _productsRepository.InsertProduct(request);
            }
            catch (Exception e)
            {
                response.Metadata.Status = System.Net.HttpStatusCode.BadRequest;
                response.Metadata.Message = e.Message;
            }

            return response;
        }

        public async Task<ServiceResponse> PostLoadProducts(LoadProductsDTO request)
        {
            ServiceResponse response = new();

            try
            {
                response = await _productsRepository.InsertProducts(request.Products);
            }
            catch (Exception e)
            {
                //throw new Exception("Error while inserting Products. Message: " + e.Message);
                response.Metadata.Status = System.Net.HttpStatusCode.BadRequest;
                response.Metadata.Message = e.Message;
            }

            return response;
        }

        public async Task<IEnumerable<Product>> PostReadProducts(IFormFile file, DataSourceTypes dataSource)
        {
            List<Product> products = new();

            try
            {
                if (dataSource == DataSourceTypes.Excel)
                {
                    products = await ProcessExcel(file);
                }
                else if (dataSource == DataSourceTypes.Connector)
                {
                }

                return products;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error al cargar el archivo.");
                throw new Exception(ex.Message);
            }
        }

        private async static Task<List<Product>> ProcessExcel(IFormFile file)
        {
            List<Product> products = new();

            using var stream = new MemoryStream();

            Dictionary<string, Type> propertyTypes = new()
                    {
                        { "Id", typeof(long) },
                        { "Price", typeof(double) },
                        { "Quantity", typeof(long) },
                        { "IsActive", typeof(bool) },
                        { "ProductCategoryId", typeof(long) },
                        { "BrandId", typeof(long) },
                    };

            await file.CopyToAsync(stream);

            stream.Position = 0;

            IWorkbook workbook = new XSSFWorkbook(stream);

            ISheet sheet = workbook.GetSheetAt(0);

            for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                Product product = new();

                IRow row = sheet.GetRow(rowIndex);

                if (row != null)
                {
                    for (int colIndex = 0; colIndex < row.LastCellNum; colIndex++)
                    {
                        ICell cell = row.GetCell(colIndex);

                        if (cell != null)
                        {
                            string property = sheet.GetRow(0).GetCell(colIndex).ToString();

                            if (propertyTypes.TryGetValue(property, out Type propertyType))
                            {
                                product.GetType().GetProperty(property)?.SetValue(product, Convert.ChangeType(cell.ToString(), propertyType));
                            }
                            else
                            {
                                product.GetType().GetProperty(property)?.SetValue(product, cell.ToString());
                            }
                        }
                    }

                    products.Add(product);
                }
            }

            return products;
        }
    }
}