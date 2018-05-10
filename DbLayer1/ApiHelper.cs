using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EntityLayer1;
using System.Collections;

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
            string urlParameters1 = "?token=9890a36bde6c6f1aa7d923ef95c8b26dca940d49&keyword=" + city + ", India";


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


        int count = 0;


        //fetch pollutant records from pvt api on the basis of lat long 
        public List<PollutantModel> fetchPollutants(double latitude, double longitude)

        {
            List<PollutantModel> objPollutantList = new List<PollutantModel>();

            try
            {

                
                string URL1 = "https://api.waqi.info/feed/geo:" + latitude + ";" + longitude + "/";
                string urlParameters1 = "?token=9890a36bde6c6f1aa7d923ef95c8b26dca940d49";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL1);


                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));




                //for (int i=1;i<25;i++)
                //{
                //    HttpResponseMessage respons = client.GetAsync(urlParameters1).Result;
                //    if (respons != null || respons.IsSuccessStatusCode)
                //    {

                //        var result = respons.Content.ReadAsStringAsync().Result;
                //    }

                // }










                HttpResponseMessage response = client.GetAsync(urlParameters1).Result;

                if (response != null || response.IsSuccessStatusCode)
                {
                   
                    var result = response.Content.ReadAsStringAsync().Result;

                   

                    JObject obj = JObject.Parse(result);

                    if (!obj["data"].HasValues)
                    {
                        
                       
                        if (count < 5)
                        {
                            count = count + 1;
                            fetchPollutants(latitude, longitude);
                           
                        }
                        return objPollutantList;
                    }
                    else
                    {


                        JObject objResult = (JObject)obj["data"];



                        JObject obj1 = (JObject)obj["data"]["iaqi"];
                        JArray obj2 = (JArray)obj["data"]["city"]["geo"];
                        JValue obj3 = (JValue)obj["data"]["aqi"];
                       
                        string aqi = (string)obj3.ToObject(typeof(string));
                        // IList<string> keys = obj1.Properties().Select(p => p.Name).ToList();

                        double[] arr = (double[])obj2.ToObject(typeof(double[]));

                        if (latitude == arr[0]||latitude==arr[1] && longitude == arr[1]||longitude==arr[0])
                        {
                            Dictionary<string, JObject> dictObj = obj1.ToObject<Dictionary<string, JObject>>();




                            //  JArray pollutantList = (JArray)obj["data"];
                            // var xmlfile = dictObj["co"];
                            //objStationList = (List<StationModel>)pollutantList.ToObject(typeof(List<StationModel>));


                            foreach (var value in dictObj)
                            {
                                PollutantModel pm = new PollutantModel();
                                string pollutant = value.Key;
                                JObject pollutantValue = (JObject)value.Value;
                                JValue val = (JValue)pollutantValue["v"];
                                int pvalue = (int)val.ToObject(typeof(int));
                                pm.PollutantName = pollutant;
                                pm.PollutantValue = pvalue.ToString();
                                objPollutantList.Add(pm);

                            }
                            PollutantModel pm1 = new PollutantModel();
                            pm1.PollutantName = "Aqi";

                            if (aqi == "" || aqi == "-")
                            {
                                pm1.PollutantValue = "-1";
                            }
                            else
                            {
                                pm1.PollutantValue = aqi;
                            }
                            
                            objPollutantList.Add(pm1);
                                

                        }
                    }
                }

            }

            catch (Exception ex)
            {
                string message = ex.ToString();
            }

            return objPollutantList;



        }





    }
}

