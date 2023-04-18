using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Model;

using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;

using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyApp.Controllers
{
    [Route("api/MyThing")]
    [ApiController]
    public class MyAppController : ControllerBase
    {
        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "N7bpoamywNPXH0Wf7QDEewrMu2eylTLwOrhTLeCc",
            BasePath = "https://mycsharpapp-b261e-default-rtdb.firebaseio.com/",
        };
        IFirebaseClient client;

        // GET: Student
        //public ActionResult Index()
        //{
        //    client = new FireSharp.FirebaseClient(config);
        //    FirebaseResponse response = client.Get("Students");
        //    dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
        //    var list = new List<Student>();
        //    foreach (var item in data)
        //    {
        //        list.Add(JsonConvert.DeserializeObject<Student>(((JProperty)item).Value.ToString()));
        //    }
        //    return View(list);
        //}

        private void AddStudentToFirebase(VillaDTO villaDto)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = villaDto;
            PushResponse response = client.Push("Students/", data);
            SetResponse setResponse = client.Set("Students/" + data.Id, data);
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("id", Name ="GetVilla")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            if(id == null)
            {
                return NotFound();
            }
            return Ok(VillaStore.villaList.FirstOrDefault((x) => x.Id==id));
        }

        [HttpPost]
        public ActionResult<VillaDTO> Create([FromBody] VillaDTO villaDTO)
        {
            //client = new FirebaseClient(ifc);

            //if (villaDTO == null)
            //{
            //    return BadRequest();
            //}
            //if(villaDTO.Id < 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            //var setter = client.Set("Villa", villaDTO);
            ////villaDTO.Id = VillaStore.villaList.OrderByDescending((u) => u.Id).FirstOrDefault().Id + 1;

            ////VillaStore.villaList.Add(villaDTO);

            ////return CreatedAtRoute("GetVilla", new {id= villaDTO.Id }, villaDTO);
            //return Ok(setter);

            try
            {
                AddStudentToFirebase(villaDTO);
                ModelState.AddModelError(string.Empty, "Added Successfully!");
            }
            catch
            {

            }
            return Ok();
        }

    }
}
