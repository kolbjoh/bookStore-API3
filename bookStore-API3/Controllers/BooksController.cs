using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bookStore_API3.Contracts;
using bookStore_API3.Data;
using bookStore_API3.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookStore_API3.Controllers
{
    /// <summary>
    /// Interacts with the Books Table
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;


        public BooksController(IBookRepository bookRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _logger = logger;
            _mapper = mapper;

        }
        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>A list of books</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks()
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: Attempted Call");
                var books = await _bookRepository.FindAll();
                var response = _mapper.Map<IList<BooksDTO>>(books);
                _logger.LogInfo($"{location}: Successful");
                return Ok(response);
            }
            catch (Exception e)
            {

                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }
        /// <summary>
        /// Gets a Book by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Books record</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: Attempted Call for id: {id}");
                var book = await _bookRepository.FindById(id);
                if(book == null)
                {
                    _logger.LogWarn($"{location}: failed to retrive record with id: {id}");
                    return NotFound();
                }
                var response = _mapper.Map<IList<BooksDTO>>(book);
                _logger.LogInfo($"{location}: Successfully got record with id: {id}");
                return Ok(response);
            }
            catch (Exception e)
            {

                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }
        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns>Book object</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Create([FromBody] BooksCreateDTO bookDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: Create Attempted");
                if(bookDTO == null)
                {
                    _logger.LogWarn($"{location}: fEmpty Request was subbmitted");
                    return BadRequest(ModelState);
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Data was Incomplete");
                    return BadRequest(ModelState);
                }

                //after validation, input data
                var book = _mapper.Map<Book>(bookDTO);
                var isSuccess = await _bookRepository.Create(book);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Creation failed");
                }
                _logger.LogInfo($"{location}: Creation was sucessful");
                _logger.LogInfo($"{location}: {book}");
                return Created("Create", new {book});
            }
            catch (Exception e)
            {

                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }



        }

        /// <summary>
        /// Updates Book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Update(int id, [FromBody] BooksUpdateDTO bookDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: Author update attemted - id:{id} ");
                if (id < 1 || bookDTO == null || id != bookDTO.id)
                {
                    _logger.LogWarn($"{location}: Author Update failed with bad data");
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Author data was incomplete");
                    return BadRequest(ModelState);
                }

                var isExists = await _bookRepository.isExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"{location}: Author with id: {id} was not found");
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"{location}: Data was Incomplete");
                    return BadRequest(ModelState);
                }

                var book = _mapper.Map<Book>(bookDTO);
                var isSuccess = await _bookRepository.Update(book);
                if (!isSuccess)
                {
                    return InternalError($"{location}: Update operation failed");
                }
                _logger.LogInfo($"{location}: Record with id: {id} successfully updated");
                return NoContent();
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Removes an book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            var location = GetControllerActionNames();
            try
            {
                _logger.LogInfo($"{location}: book delete attemted - id:{id} ");
                if (id < 1)
                {
                    _logger.LogWarn($"{location}: book Delete failed with bad data");
                    return BadRequest();
                }
                var isExists = await _bookRepository.isExists(id);
                if (!isExists)
                {
                    _logger.LogWarn($"{location}: book with id: {id} was not found");
                    return NotFound();
                }
                var book = await _bookRepository.FindById(id);
                var isSuccess = await _bookRepository.Delete(book);
                if (!isSuccess)
                {
                    return InternalError($"{location}: book delete failed");
                }
                _logger.LogWarn($"{location}: book with id: {id} successfully deleted");
                return NoContent();
            }
            catch (Exception e)
            {

                return InternalError($"{e.Message} - {e.InnerException}");
            }
        }


        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }
        
        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "something went wrong. Please contact the Administrator");
        }

    }
}
