using Microsoft.AspNetCore.Mvc;
using HiTech.Service.NotificationAPI.Services.IService;
using HiTech.Service.NotificationAPI.DTOs.Response;
using HiTech.Shared.Controllers;

namespace HiTech.Service.NotificationAPI.Controllers
{
    [Route("api/hitech/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/hitech/notifications/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<NotificationResponse>>>> GetNotifications(int id)
        {
            var notis = await _notificationService.GetAllByUserIDAsync(id);
            return Ok(HiTechApi.ResponseOk(notis));
        }

        // GET: api/hitech/notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<NotificationResponse>>> GetNotification(int id)
        {
            var noti = await _notificationService.GetByIDAsync(id);

            var response = noti != null
                ? HiTechApi.ResponseOk(noti)
                : HiTechApi.ResponseNotFound();

            return noti != null ? Ok(response) : NotFound(response);
        }

        // PUT: api/hitech/notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutNotification(int id)
        {
            if (!await _notificationService.NotificationExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _notificationService.ReadNotification(id);

            if (success)
            {
                return NoContent();
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }
    }
}
