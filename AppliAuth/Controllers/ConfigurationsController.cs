using AppliAuth.Classes;
using AppliAuth.Extensions;
using AppliAuth.Interfaces;
using AppliAuth.Models;
using AppliAuth.Models.Produits;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliAuth.Controllers
{
    [ApiController]
    [Route("configurations")]
    public class ConfigurationsController : ControllerBase
    {

        private Director Director;

        public ConfigurationsController(Director d)
        {
            Director = d;
        }


    


        [Authorize]
        [HttpPost]
        public IActionResult PostConfiguration([FromBody] PostModel model)
        {

            try
            {
                IProductDirector director = Director.GetDirector(model.type);
                director.Insert(director.Parse(model.record));
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles.ADMIN)]
        [HttpPost]
        [Route("calcul")]
        public IActionResult PostCalculConfiguration([FromBody] GetModel model)
        {
            try {
                
                IProductDirector director = Director.GetDirector(model.type);
                var product = director.Parse(model.data);

               

              
                return Ok(director.Get(product));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
          
        }

    }
}
