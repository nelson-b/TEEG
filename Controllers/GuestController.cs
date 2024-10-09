using FluentValidation;
using Guest.BAL.Commands;
using Guest.BAL.Interface;
using Guest.BAL.Query;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI_TEE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController : ControllerBase
    {
        private const string errorMessage = "An error occured";
        private readonly ILogger<GuestController> _logger;
        private readonly AppDbContext _appDbContext;
        private readonly IGuestRepository _guestRepository;
        private readonly IValidator<AddGuestCommand> _addGuestValidator;
        private readonly IValidator<UpdateGuestCommand> _updateGuestValidator;
        private readonly IValidator<GetGuestQuery> _getGuestValidator;
        private readonly IValidator<GetAllGuestQuery> _getAllGuestValidator;
        private readonly IMediator _mediator;

        public GuestController(ILogger<GuestController> logger, AppDbContext appDbContext, IMediator mediator, 
            IValidator<AddGuestCommand> addGuestValidator,
            IValidator<UpdateGuestCommand> updateGuestValidator,
            IValidator<GetGuestQuery> getGuestValidator,
            IValidator<GetAllGuestQuery> getAllGuestValidator)
        {
            _addGuestValidator = addGuestValidator;
            _updateGuestValidator = updateGuestValidator;
            _getGuestValidator = getGuestValidator;
            _getAllGuestValidator = getAllGuestValidator;
            _logger = logger;
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        [HttpPost("Add")]
        public ActionResult AddGuest([FromBody]AddGuestCommand guestEntity) 
        {
            try
            {
                if (guestEntity is null)
                {
                    throw new ArgumentNullException(nameof(guestEntity));
                }

                var modalValidate = _addGuestValidator.Validate(guestEntity);
                if (!modalValidate.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _mediator.Send(guestEntity);

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(errorMessage, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpPut("Update")]
        public ActionResult UpdateGuestPhoneNo(Guid id, UpdateGuestCommand guestEntity)
        {
            try
            {
                if (guestEntity is null)
                {
                    throw new ArgumentNullException(nameof(guestEntity));
                }

                var modalValidate = _updateGuestValidator.Validate(guestEntity);
                if (!modalValidate.IsValid)
                {
                    return BadRequest(ModelState);
                }

                guestEntity.Id = id;
                var result = _mediator.Send(guestEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(errorMessage, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetById")]
        public ActionResult GetGuestById(Guid id)
        {
            try
            {
                if (id==Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(id));
                }
                var getGuestQueryDet = new GetGuestQuery()
                {
                    Id = id
                };
                var modalValidate = _getGuestValidator.Validate(getGuestQueryDet);
                if (!modalValidate.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _mediator.Send(getGuestQueryDet);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(errorMessage, ex.Message);
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult GetAllGuests()
        {
            try
            {
                var result = _mediator.Send(new GetAllGuestQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(errorMessage, ex.Message);
                return BadRequest(ex);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is GuestController controller &&
                   EqualityComparer<ILogger<GuestController>>.Default.Equals(_logger, controller._logger);
        }
    }
}