using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EntityLayer1;

namespace DbLayer1
{
    public class ApiHelper
    {


        private const string URL = "https://api.data.gov.in/resource/3b01bcb8-0b14-4abf-b6f2-c1bfd384ba69";
        private string urlParameters = "?api-key=579b464db66ec23bdd000001cdd3946e44ce4aad7209ff7b23ac571b&format=json&offset=0&limit=all";



        //fetching country, state, city name from govt api
        public List<RecordsModel> fetchPlace()

        {
           // List<FieldsModel> objFieldModel = new List<FieldsModel>();
            List<RecordsModel> objRecordsModel = new List<RecordsModel>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
           
                if (response != null || response.IsSuccessStatusCode)
                {

                   
                var result = response.Content.ReadAsStringAsync().Result;
                JObject obj = JObject.Parse(result);
              //  JArray addJsonList = (JArray)obj["fields"];
                JArray totalCount = (JArray)obj["records"];

             //   objFieldModel = (List<FieldsModel>)addJsonList.ToObject(typeof(List<FieldsModel>));
                objRecordsModel = (List<RecordsModel>)totalCount.ToObject(typeof(List<RecordsModel>));


                //distinct value if required
                //var country= objRecordsModel.Select(o => o.country).Distinct();
                //var state=objRecordsModel.Select(o => o.state).Distinct();
                //var city=objRecordsModel.Select(o => o.city).Distinct();

                return objRecordsModel;

                }

            return objRecordsModel;



        }
        

        //fetching station detail from pvt api
        public List<StationModel> fetchStation(string city)

        {
            string URL1 = "https://api.waqi.info/search/";
            string urlParameters1 = "?token=9890a36bde6c6f1aa7d923ef95c8b26dca940d49&keyword="+city+ ", India";
           

            List<StationModel> objStationList = new List<StationModel>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL1);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters1).Result;  // Blocking call!

            if (response != null || response.IsSuccessStatusCode)
            {


                var result = response.Content.ReadAsStringAsync().Result;
                JObject obj = JObject.Parse(result);
                //  JArray addJsonList = (JArray)obj["fields"];
                JArray totalCount = (JArray)obj["data"];

                //   objFieldModel = (List<FieldsModel>)addJsonList.ToObject(typeof(List<FieldsModel>));
                objStationList = (List<StationModel>)totalCount.ToObject(typeof(List<StationModel>));


                //distinct value if required
                //var country= objRecordsModel.Select(o => o.country).Distinct();
                //var state=objRecordsModel.Select(o => o.state).Distinct();
                //var city=objRecordsModel.Select(o => o.city).Distinct();

                return objStationList;

            }

            return objStationList;



        }




        //fetch pollutant records from pvt api on the basis of lat long 
        //public List<StationModel> fetchPollutants()

        //{
        //    string URL1 = "https://api.waqi.info/search/";
        //    string urlParameters1 = "?token=9890a36bde6c6f1aa7d923ef95c8b26dca940d49&keyword=" + city + ", India";


        //    List<StationModel> objStationList = new List<StationModel>();


        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(URL1);

        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));

        //    // List data response.
        //    HttpResponseMessage response = client.GetAsync(urlParameters1).Result;  // Blocking call!

        //    if (response != null || response.IsSuccessStatusCode)
        //    {


        //        var result = response.Content.ReadAsStringAsync().Result;
        //        JObject obj = JObject.Parse(result);
        //        //  JArray addJsonList = (JArray)obj["fields"];
        //        JArray totalCount = (JArray)obj["data"];

        //        //   objFieldModel = (List<FieldsModel>)addJsonList.ToObject(typeof(List<FieldsModel>));
        //        objStationList = (List<StationModel>)totalCount.ToObject(typeof(List<StationModel>));


        //        //distinct value if required
        //        //var country= objRecordsModel.Select(o => o.country).Distinct();
        //        //var state=objRecordsModel.Select(o => o.state).Distinct();
        //        //var city=objRecordsModel.Select(o => o.city).Distinct();

        //        return objStationList;

        //    }

        //    return objStationList;



        //}





    }
}

