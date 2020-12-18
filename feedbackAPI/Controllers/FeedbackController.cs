using feedbackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace feedbackAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedbackController : Controller
    {
        DBContext _ctx;

        public FeedbackController(DBContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<IList<Message>> GetMessages()
        {
            return await _ctx.Messages.ToListAsync();
        }

        [HttpGet]
        [Route("SaveMessage")]
        public async Task<IActionResult> SaveMessage(string title, string text)
        {
            try
            {
                var message = new Message
                {
                    Title = title,
                    Text = text
                };
                _ctx.Messages.Add(message);
                await _ctx.SaveChangesAsync();
                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetMessageById")]
        public async Task<MessageViewModel> GetMessageById(int messageId)
        {
            if (messageId == 0)
                throw new Exception("MessageId is null");
            var message = await _ctx.Messages.Where(x => x.Id == messageId).FirstOrDefaultAsync();
            if (message == null)
                throw new Exception("Message not found");
            return new MessageViewModel
            {
                Id = message.Id,
                Title = message.Title,
                Text = message.Text
            };
        }
    }
}
