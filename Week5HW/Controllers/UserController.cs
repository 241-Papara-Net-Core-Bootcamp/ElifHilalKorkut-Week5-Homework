using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Week5HW.Core.Dto;
using Week5HW.Core.Entities;
using Week5HW.Core.Enums;
using Week5HW.Core.Interfaces;

namespace Week5HW.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly Func<CacheTech, ICacheService> _cacheService;
        private IMapper mapper;

        public UserController(IUserRepository repository, Func<CacheTech, ICacheService> cacheService, IMapper mapper)
        {
            _repository = repository;
            _cacheService = cacheService;
            this.mapper = mapper;
        }


        [HttpGet("Get")]
       // [Route("RetrieveFromApi")]

        public async Task DataFromApi()

        {
            HttpWebRequest _httpReq = (HttpWebRequest)WebRequest.Create("https://jsonplaceholder.typicode.com/posts ");
           
            _httpReq.Method = "GET";

            HttpWebResponse _httpRes = (HttpWebResponse)_httpReq.GetResponse();
          
            Console.WriteLine(_httpRes.StatusCode);
            Console.WriteLine(_httpRes.Server);

            string _stringJson;
            using (Stream stream = _httpRes.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                _stringJson = reader.ReadToEnd();
            }
          
            
            List<DtoUser> items = (List<DtoUser>)JsonConvert.DeserializeObject(_stringJson, typeof(List<DtoUser>));
           
            var mapping = mapper.Map<List<User>>(items);
         
            foreach (var item in mapping)
            {
                await _repository.AddAsync(item);
            }

            
           
            Console.WriteLine("Data has been retrieved from external API"+ items );

        }



        [HttpPost]
        [Route("Retrieve")]
       public IActionResult RetrieveFromApi()
        {
            RecurringJob.AddOrUpdate(() => DataFromApi(), "*/5 * * * * *");
            return Ok($"Recurring Job Scheduled. It will be repeated every 5 minutes.");
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var user = _repository.GetByIdAsync(id).Result;
            return Ok(user);
        }


        [HttpGet("GetAll")]
        public IActionResult GetAllUsers()
        {
            var users = _repository.GetAllAsync();
            return Ok(users);
        }

        
    }

}

