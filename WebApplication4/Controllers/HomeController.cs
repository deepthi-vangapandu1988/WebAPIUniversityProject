﻿using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Data.Repository;
using WebApplication4.Dto;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/Home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUniversityRepository<Student> _studentRepository;
        private readonly IMapper _mapper;
        private APIResponse _response;
        public HomeController(ILogger<HomeController> logger, IUniversityRepository<Student> studentRepository, IMapper mapper)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetStudentsAsync()
        {
            try
            {
                _logger.LogInformation("Get All Students");
                //_logger.LogDebug(JsonConvert.SerializeObject(await _studentRepository.GetAllAsync()));

                //List<StudentDTO> result = await _db.Students.Select(n => new StudentDTO
                //{
                //    Id = n.Id,
                //    Name = n.Name,
                //    Email = n.Email,
                //    Address = n.Address
                //}).ToListAsync();


                List<StudentDTO> result = _mapper.Map<List<StudentDTO>>(await _studentRepository.GetAllAsync());
                _response.Data = result;
                //Async always returns Task<Result>
                //Async will return Result when you use await

                //var r = _db.Students.ToList();
                //var list = new List<StudentDTO>();
                //foreach (var item in r)
                //{
                //    var i = new StudentDTO
                //    {
                //        Id = item.Id,
                //        Name = item.Name,
                //        Email = item.Email,
                //        Address = item.Address
                //    };
                //    list.Add(i);
                //}
            }
            catch (Exception ex)
            {
                _response = new APIResponse(System.Net.HttpStatusCode.InternalServerError, false, ex.Message);
            }
            return _response;
        }
        [HttpGet("{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetStudentByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new APIResponse(System.Net.HttpStatusCode.BadRequest, false, "Id must be non zero");
                }

                var student = (await _studentRepository.GetByFilterAsync(n => n.Id == id)).FirstOrDefault();
                if (student == null)
                {
                    return new APIResponse(System.Net.HttpStatusCode.NotFound, false, "Record not found");
                }

                //StudentDTO result = new StudentDTO()
                //{
                //    Id = student.Id,
                //    Name = student.Name,
                //    Email = student.Email,
                //    Address = student.Address,
                //};
                StudentDTO result = _mapper.Map<StudentDTO>(student);
            }
            catch (Exception ex)
            {
                _response = new APIResponse(System.Net.HttpStatusCode.InternalServerError, false, ex.Message);
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody] StudentCreateDTO model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            if ((await _studentRepository.GetByFilterAsync(n => n.Name == model.Name)).FirstOrDefault() != null)
            {
                ModelState.AddModelError("Input error", "Student name must be unique");
                return BadRequest(ModelState);
            }

            if (model == null)
                return BadRequest();

            //Student student = new Student()
            //{
            //    Name = model.Name,
            //    Email = model.Email,
            //    Address = model.Address
            //};
            Student student = _mapper.Map<Student>(model);
            await _studentRepository.CreateAsync(student);

            //StudentDTO result = new StudentDTO()
            //{
            //    Id = student.Id,
            //    Name = student.Name,
            //    Email = student.Email,
            //    Address = student.Address,
            //};
            StudentDTO result = _mapper.Map<StudentDTO>(student);

            return CreatedAtRoute("GetStudentById", new { id = result.Id }, result);
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> UpdateStudentAsync(int id, [FromBody] StudentUpdateDTO model)
        {
            if (id <= 0 || model == null || id != model.Id)
                return BadRequest();

            var student = (await _studentRepository.GetByFilterAsync(n => n.Id == id)).FirstOrDefault();
            if (student == null)
                return NotFound();

            //student.Name = model.Name;
            //student.Email = model.Email;
            //student.Address = model.Address;
            _mapper.Map(model, student);

            await _studentRepository.UpdateAsync(student);

            //StudentDTO result = new StudentDTO()
            //{
            //    Id = student.Id,
            //    Name = student.Name,
            //    Email = student.Email,
            //    Address = student.Address,
            //};
            StudentDTO result = _mapper.Map<StudentDTO>(student);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = (await _studentRepository.GetByFilterAsync(n => n.Id == id)).FirstOrDefault();
            if (student == null)
                return NotFound();
            await _studentRepository.DeleteAsync(student);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdatePartialStudent(int id, JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (id == 0 || patchDocument == null)
                return BadRequest();
            var student = UniversityStore.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound();
            patchDocument.ApplyTo(student, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return NoContent();
        }
    }
}
