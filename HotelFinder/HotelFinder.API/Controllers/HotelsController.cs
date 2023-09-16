using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        //[HttpGet]
        //public List<Hotel> Get()
        //{
        //    return _hotelService.GetAllHotels();
        //}

        //IActionResult Kullanımı:
        [HttpGet]        
        public async Task<IActionResult> Get()
        {
            var hotels =await _hotelService.GetAllHotels();
            return Ok(hotels);//200 ok döndür ve responsa data ekle
        }

        //[HttpGet("{id}")]
        //public Hotel Get(int id)
        //{
        //    return _hotelService.GetHotelById(id);
        //}

        //IActionResult Kullanımı:
        [HttpGet]
        //[Route("GetHotelById/{id}")] //api/hotels/gethotelbyid/2
        //Bu şekilde yazmak yerine şu şekilde de yazabilirsin:
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel =await _hotelService.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel);// 200 ok+ data
            }
            return NotFound();//404
        }

        [HttpGet]
        //[Route("GetHotelByName/{name}")] //api/hotels/gethotelbyname/hilton
        [Route("[action]/{name}")]
        public async Task<IActionResult> GetHotelByName(string name)
        {
            var hotel =await _hotelService.GetHotelByName(name);
            if (hotel!=null)
            {
                return Ok(hotel);//200+ data
            }
                return NotFound();//404
        }

        //İki parametre alsaydı?
        //[HttpGet]
        //[Route("[action]/{id}/{name}")]
        ////[Route("[action]")=>bu şekilde de yazabilirsin ama o zaman querystringe=>api/Hotels/gethotelbyIdAndName?id=2&name=titanic şeklinde yazmalısın.
        //public async Task<IActionResult> GetHotelByIdAndName(int id, string name)
        //{
        //        return Ok();//200+ data
        //}

        //[HttpPost]
        //public Hotel Post([FromBody]Hotel hotel)
        //{
        //    return _hotelService.CreateHotel(hotel);
        //}

        //IActionResult Kullanımı:
        [HttpPost]
        [Route("CreateHotel")]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            //Entitiesde istediğimiz property özelliklerini taşıyorsa yani modele uygunsa kaydedeceğiz.
            //Sayfa başında bunun olması validation ın otomatik yapılması demek. [ApiController] bunu yazarsan ifle kontrol etmene gerek yok!
            //if (ModelState.IsValid)
            //{
            var createdHotel = await _hotelService.CreateHotel(hotel);
                return CreatedAtAction("Get", new { id = createdHotel.Id }, createdHotel);//201+ data
            //}
            //return BadRequest(ModelState);//400 +validation errors
        }

        //[HttpPut]
        //public Hotel Put([FromBody] Hotel hotel)
        //{
        //    return _hotelService.UpdateHotel(hotel);
        //}

        //IActionResult Kullanımı:
        [HttpPut]
        [Route("UpdateHotel")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            //Böyle bi otel datası var mı kontrol et
            if (await _hotelService.GetHotelById(hotel.Id)!=null)
            {
                return Ok(await _hotelService.UpdateHotel(hotel));//200+ data
            }
            return NotFound();
        }

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _hotelService.DeleteHotel(id);
        //}

        //IActionResult Kullanımı:
        //[HttpDelete("{id}")]

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (await _hotelService.GetHotelById(id) != null)
            {
                await _hotelService.DeleteHotel(id);
                return Ok();//200
            }
            return NotFound();
        }
    }
}
