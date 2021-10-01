using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Converters;
using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Dtos;
using MyFinances.WebApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;


        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public DataResponse<IEnumerable<OperationDto>> Get()
        {
            var response = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                response.Data = _unitOfWork.Operation.Get().ToDtos();
            }
            catch (Exception e)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(e.Source, e.Message));
            }           

            return response;
        }
        [HttpGet("{id}")]
        public DataResponse<OperationDto> Get(int id)
        {
            var response = new DataResponse<OperationDto>();

            try
            {
                response.Data = _unitOfWork.Operation.Get(id)?.ToDto();
            }
            catch (Exception e)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(e.Source, e.Message));
            }

            return response;
        }
        [HttpPost]
        public DataResponse<int> Add(OperationDto operationDto)
        {
            var response = new DataResponse<int>();

            try
            {
                var operation = operationDto.ToDao();
                _unitOfWork.Operation.Add(operation);
                _unitOfWork.Complete();
                response.Data = operation.Id;
            }
            catch (Exception e)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(e.Source, e.Message));
            }

            return response;
        }
        [HttpPut]
        public Response Update(OperationDto operation)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Update(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(e.Source, e.Message));
            }

            return response;
        }
        [HttpDelete]
        public Response Delete(int id)
        {
            var response = new Response();

            try
            {
                _unitOfWork.Operation.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                //logowanie do pliku
                response.Errors.Add(new Error(e.Source, e.Message));
            }

            return response;
        }

    }
}

