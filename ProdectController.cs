using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingmachineCore.Helper;
using VendingmachineCore.Interface;
using VendingmachineCore.Model;
using VendingmachineInfastruction.Entity;

namespace Vending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdectController : ControllerBase
    {
        #region Prop

        private readonly IProduct prodect;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        #endregion

        #region Ctor

        public ProdectController(IProduct prodect, IMapper mapper, IConfiguration configuration)
        {
            this.prodect = prodect;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        #endregion

        [HttpGet, Route("~/api/GetProdect")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var emps = await prodect.GetAsync();
                var data = mapper.Map<IEnumerable<ProductVm>>(emps);
                return Ok(new ApiResponse<IEnumerable<ProductVm>>()
                {
                    Code = configuration["Response:Success:Code"],
                    Status = configuration["Response:Success:Status"],
                    Success = data
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Status"],
                    Error = ex.Message
                });
            }

        }

        //[HttpGet, Route("~/api/GetEmployeeById")]  // Query String
        [HttpGet, Route("~/api/GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var emp = await prodect.GetByIdAsync(id);
                var data = mapper.Map<ProductVm>(emp);

                return Ok(new ApiResponse<ProductVm>()
                {
                    Code = configuration["Response:Success:Code"],
                    Status = configuration["Response:Success:Status"],
                    Success = data
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Status"],
                    Error = ex.Message
                });
            }

        }


        [DisableCors]
        //[EnableCors("")]
        [HttpPost, Route("~/api/PostProduct")]
        public async Task<IActionResult> PostEmployee(ProductVm model)
        {
            try
            {

                if (!ModelState.IsValid)
                    return NotFound("Validation Error");

                var data = mapper.Map<Product>(model);
                await prodect.CreateAsync(data);


                return Ok(new ApiResponse<ProductVm>()
                {
                    Code = configuration["Response:Created:Code"],
                    Status = configuration["Response:Created:Status"]
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Status"],
                    Error = ex.Message
                });
            }

        }


        [HttpPut, Route("~/api/PutProduct")]
        public async Task<IActionResult> PutEmployee(ProductVm model)
        {
            try
            {

                if (!ModelState.IsValid)
                    return NotFound("Validation Error");

                var data = mapper.Map<Product>(model);
                await prodect.UpdateAsync(data);


                return Ok(new ApiResponse<ProductVm>()
                {
                    Code = configuration["Response:Updated:Code"],
                    Status = configuration["Response:Updated:Status"]
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Status"],
                    Error = ex.Message
                });
            }

        }



        [HttpDelete, Route("~/api/DeleteProduct")]
        public async Task<IActionResult> DeleteEmployee(ProductVm model)
        {
            try
            {

                var data = mapper.Map<Product>(model);
                await prodect.DeleteAsync(data);


                return Ok(new ApiResponse<string>()
                {
                    Code = configuration["Response:Deleted:Code"],
                    Status = configuration["Response:Deleted:Status"],
                    Success = "Done Deleted"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {
                    Code = configuration["Response:Error:Code"],
                    Status = configuration["Response:Error:Status"],
                    Error = ex.Message
                });
            }

        }



    }
}
