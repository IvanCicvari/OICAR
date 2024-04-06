using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly StarplexContext _context;

        public SubscriptionsController(StarplexContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var subscriptions = _context.Subscriptions.ToList();
                if (subscriptions.Count == 0)
                {
                    return NotFound("Subscriptions are not available.");
                }
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var subscription = _context.Subscriptions.Find(id);
                if (subscription == null)
                {
                    return NotFound($"Subscription with id {id} is not found.");
                }
                return Ok(subscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Post(Subscription subscription)
        {
            try
            {
                _context.Add(subscription);
                _context.SaveChanges();
                return Ok("Subscription created.");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Subscription subscription)
        {
            if (subscription == null || subscription.SubscriptionId == 0)
            {
                if (subscription == null)
                {
                    return BadRequest("Subscription data is invalid");
                }
                else if (subscription.SubscriptionId == 0)
                {
                    return BadRequest($"Subscription id {subscription.SubscriptionId} is invalid.");
                }
            }

            try
            {
                var subscriptionModel = _context.Subscriptions.Find(subscription.SubscriptionId);
                if (subscriptionModel == null)
                {
                    return NotFound($"Subscription with id {subscription.SubscriptionId} not found.");
                }

                subscriptionModel.SubscriberId = subscription.SubscriberId;
                subscriptionModel.ChannelId = subscription.ChannelId;

                _context.SaveChanges();
                return Ok(subscriptionModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var subscription = _context.Subscriptions.Find(id);
                if (subscription == null)
                {
                    return NotFound($"Subscription with id {id} not found.");
                }
                _context.Subscriptions.Remove(subscription);
                _context.SaveChanges();
                return Ok($"Subscription with id {id} deleted.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
