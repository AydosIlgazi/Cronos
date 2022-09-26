using Cronos.Application.Data;
using Cronos.Application.Features.Activity;
using Cronos.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Cronos.Application.Features.Activity.GetActivities;

namespace Cronos.Web.Controllers
{

      public class ActivityController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ApplicationContext _context;
        public ActivityController(IMediator mediator, ApplicationContext context)
        {
            _mediator = mediator;   
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           ActivityViewModel activityViewModel = new ActivityViewModel();
           activityViewModel = await _mediator.Send(new GetActivitiesQuery());
           return View(activityViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> AddActivity()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddActivity(AddActivityCommand command)
        {
            var activityViewModel = await _mediator.Send(command);
            return View(activityViewModel);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetActivityById(int id)
        //{
        //    var activity = await _mediator.Send(new GetActivityByIdQuery { Id = id });
        //    return View(activity);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, UpdateActivityCommand command)
        //{
        //    command.Id = id;
        //    var activity = await _mediator.Send(command);
        //    return RedirectToAction("Index", "Activity");
        //}
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var activity = await _mediator.Send(new GetActivityByIdQuery { Id = id });
            return View(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateActivityCommand command)
        {
            command.Id = id;
            var activity = await _mediator.Send(command);
            return RedirectToAction("Index", "Activity");
        }

        public async Task<IActionResult> Delete(int id, DeleteActivityCommand command)
        {
            command.Id = id;
            var activity = await _mediator.Send(command);
            return RedirectToAction("Index", "Activity");
        }
    }
}
