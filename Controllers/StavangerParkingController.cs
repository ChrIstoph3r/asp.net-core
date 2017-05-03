using System.Threading.Tasks;
using InterviewTest.Data;
using InterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using InterviewTest.interfaces;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;

namespace InterviewTest.Controllers
{
    public class StavangerParkingController : Controller
    {
        private readonly Context _context;

        public StavangerParkingController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var parkingList = new List<ParkingPlace>(await GetStavangerParkingModelList());

            RemoveUnwantedCharsFromName(parkingList);
            SetCapacityToList(parkingList);
            return View(parkingList);
        }

        private List<Capacity> CreateCapacityList()
        {
            var Capacity = new List<Capacity>()
            {   
                new Capacity(){ Sted = "Parketten", Kapasitet = 290},
                new Capacity(){ Sted = "Valberget", Kapasitet = 170},
                new Capacity(){ Sted = "Jorenholmen", Kapasitet = 500},
                new Capacity(){ Sted = "Posten", Kapasitet = 150},
                new Capacity(){ Sted = "Kyrre", Kapasitet = 310},
                new Capacity(){ Sted = "StOlav", Kapasitet = 480},
                new Capacity(){ Sted = "Jernbanen", Kapasitet = 290},
                new Capacity(){ Sted = "Siddis", Kapasitet = 360},
                new Capacity(){ Sted = "Forum", Kapasitet = 289},
            };

            return Capacity;
        }

        private void SetCapacityToList(List<ParkingPlace> parkingPlacelist)
        {
            var CapacityList = CreateCapacityList();

            foreach(var capacity in CapacityList)
            {
                foreach(var parkingPlace in parkingPlacelist)
                {
                    if(capacity.Sted == parkingPlace.Sted)
                    {
                        parkingPlace.Kapasitet = capacity.Kapasitet;
                        break;
                    }
                }
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            return View(await GetStavangerParkingModel(id));
        }

        private void RemoveUnwantedCharsFromName(List<ParkingPlace> parkingPlaceModelList)
        {
            foreach(var pp in parkingPlaceModelList)
            {
                pp.Sted = pp.Sted.Replace(" ", "");
            }
        }

        private async Task<IEnumerable<ParkingPlace>> GetStavangerParkingModelList()
        {
            string baseUrl = "https://open.stavanger.kommune.no";
            string fileName = "/dataset/36ceda99-bbc3-4909-bc52-b05a6d634b3f/resource/d1bdc6eb-9b49-4f24-89c2-ab9f5ce2acce/download/parking.json";
            
            var jsonStringRes = await GetJsonStringResponse(baseUrl, fileName);

            var jsonStringToModelList = JsonConvert.DeserializeObject<IEnumerable<ParkingPlace>>(jsonStringRes);

            return jsonStringToModelList;
        }

        private async Task<ParkingPlace> GetStavangerParkingModel(string parkingName)
        {
            var parkingPlaceModelList = await GetStavangerParkingModelList();

            var parkingPlaceModel = parkingPlaceModelList.FirstOrDefault(parking => parking.Sted == parkingName);

            return parkingPlaceModel;
        }

        private async Task<string> GetJsonStringResponse(string baseUrl, string fileName)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var response = await client.GetAsync(fileName);
                    response.EnsureSuccessStatusCode(); // Throw in not success
                    
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    return stringResponse;
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request exception: {e.Message}");
                }
            }
            return null;
        }
    }
}