using AutoMapper;
using LinkBL.Ecxeptions;
using LinkBL.ModelBL.TableWithUrlCrud;
using LinkUI.Models.TableWithUrlUI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LinkUI.Controllers
{
    [Route("[controller]")]
    public class TableWithUrlController : Controller
    {
        private readonly ITableWithUrlCrud crud;
        private readonly IMapper mapper;

        public TableWithUrlController(
            ITableWithUrlCrud crud,
            IMapper mapper)
        {
            this.crud = crud;
            this.mapper = mapper;
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            var items = await crud.GetAllAsync(token);

            if (items is null)
                return View(new List<OutTableWithUrlDtoUI>());

            return View("~/Views/TableWithUrl/Index.cshtml", items.Select(x => mapper.Map<OutTableWithUrlDtoUI>(x)).ToList());
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RedirectByShortUrl([FromBody] InputRedirectByShortUrlDtoUI input, CancellationToken token = default)
        {
            var items = await crud.MagnifyCountOfTransitionsAsync(input.ShortUrl, token);

            if (items is null)
                throw new NullReferenceException("Cannot recieve data from BL");

            return Json(mapper.Map<OutMagnifyCountOfTransitionsDtoUI>(items));
        }

        [Route("[action]/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id, CancellationToken token = default)
        {
            var item = await crud.GetAsync(id, token);

            return Json(mapper.Map<OutTableWithUrlDtoUI>(item));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InputCreateTableUrlDtoUI input, CancellationToken token = default)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }

            try
            {
                var item = await crud.InsertAsync(input.OriginalUrl, token);
                return Json(mapper.Map<OutTableWithUrlDtoUI>(item));
            }
            catch (DtoVereficationException ex)
            {
                return BadRequest(new { errors = new List<string>() { ex.Message } });
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] InputUpdateTableUrlDtoUI input, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }

            try
            {
                await crud.UpdateAsync(input.Id, input.OriginalUrl, token);
                return Ok();
            }
            catch (DtoVereficationException ex)
            {
                return BadRequest(new { errors = new List<string>() { ex.Message } });
            }
        }

        [Route("[action]/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id, CancellationToken token = default)
        {
            await crud.DeleteAsync(id, token);

            return Ok();
        }
    }
}
