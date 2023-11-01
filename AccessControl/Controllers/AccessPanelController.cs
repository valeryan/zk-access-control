using Microsoft.AspNetCore.Mvc;
using PullSDK_core;


namespace AccessControl.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AccessPanelController : ControllerBase
    {


        private readonly ILogger<AccessPanelController> _logger;

        public AccessPanelController(ILogger<AccessPanelController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            AccessPanel ACDevice = new AccessPanel();

            // connect
            if (!ACDevice.Connect("192.168.1.100", 4370, 0, 5000))
            {
                // return 500 response
                return StatusCode(500, new { error = "Could not connect to the device" });
            }

            // read users
            List<User>? users = ACDevice.ReadUsers();
            if (users == null)
            {
                return StatusCode(500, new { error = "Could not read users from the device" });
            }

            return Ok(users);
        }

        [HttpGet("Events")]
        public IActionResult GetEvents()
        {
            AccessPanel ACDevice = new AccessPanel();

            // connect
            if (!ACDevice.Connect("192.168.1.100", 4370, 0, 5000))
            {
                return StatusCode(500, new { error = "Could not connect to the device" });
            }

            // read events
            AccessPanelEvent? events = ACDevice.GetEventLog();
            if (events == null)
            {
                return StatusCode(500, new { error = "Could not read events from the device" });
            }

            return Ok(events);
        }

        [HttpGet("Transactions")]
        public IActionResult GetTransactions()
        {
            AccessPanel ACDevice = new AccessPanel();

            // connect
            if (!ACDevice.Connect("192.168.1.100", 4370, 0, 5000))
            {
                return StatusCode(500, new { error = "Could not connect to the device" });
            }

            List<Transaction>? transactions = ACDevice.ReadTransactionLog(1503536969);
            if (transactions == null)
            {
                return StatusCode(500, new { error = "Could not read transactions from the device" });
            }

            return Ok(transactions);
        }
    }
}
