using EntityLayer1;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLayer1
{
    public class DbHelper
    {

        MySqlConnection conn;
        MySqlCommand cmd;
        DbConnect dbc = new DbConnect();

        //adding country, state, city name from govt api to database
        public bool addPlace(List<RecordsModel> objRecordsModel)
        {
            bool allRecordsInserted = false;



            try
            {

                foreach (var records in objRecordsModel)
                {


                    conn = dbc.openConnection();
                    cmd = new MySqlCommand("sp_addPlace", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("val_country", records.country);
                    cmd.Parameters.AddWithValue("val_state", records.state);
                    cmd.Parameters.AddWithValue("val_city", records.city);

                    //if exist place check, fetching id 
                    int recordInserted = cmd.ExecuteNonQuery();

                    //fetch station id and send that station id with pollutant value and name to proc that added pollutants

                }

                allRecordsInserted = true;

                return allRecordsInserted;

            }

            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                dbc.closeConnection();
            }
            return allRecordsInserted;
        }

        //fetching city details from database
        public List<RecordsModel> fetchCity()
        {

            List<RecordsModel> objCityList = new List<RecordsModel>();

            try
            {



                conn = dbc.openConnection();
                cmd = new MySqlCommand("sp_fetchCity", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader sdr = cmd.ExecuteReader();


                while (sdr.Read())
                {
                    RecordsModel objCity = new RecordsModel();
                    objCity.id = sdr["id"].ToString();
                    objCity.city = sdr["city"].ToString();

                    objCityList.Add(objCity);


                }




                return objCityList;

            }

            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                dbc.closeConnection();
            }
            return objCityList;
        }


        //add station name,aqi, lat long 
        public void addStation(List<StationModel> objStationList, string cityId)
        {




            try
            {

                foreach (var records in objStationList)
                {
                    double latitude = records.station.geo[0];
                    double longitude = records.station.geo[1];

                    conn = dbc.openConnection();
                    cmd = new MySqlCommand("sp_addStation", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("val_stationName", records.station.name);
                    //  cmd.Parameters.AddWithValue("val_aqi", records.aqi);
                    cmd.Parameters.AddWithValue("val_lat", latitude);
                    cmd.Parameters.AddWithValue("val_long", longitude);
                    cmd.Parameters.AddWithValue("val_cityid", cityId);


                    int recordInserted = cmd.ExecuteNonQuery();

                }


            }

            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                dbc.closeConnection();
            }

        }

        //add pollutant records from pvt api on the basis of lat long 
        public void AddPollutants(PollutantListModel oneStation)
        {

            try
            {


                double latitude = oneStation.StationLatitude;
                double longitude = oneStation.StationLongitude;


                foreach (var pollutant in oneStation.pollutantModelList)
                {
                    conn = dbc.openConnection();
                    cmd = new MySqlCommand("sp_addPollutants", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("val_PollutantName", pollutant.PollutantName);
                    cmd.Parameters.AddWithValue("val_PollutantValue", pollutant.PollutantValue);
                    cmd.Parameters.AddWithValue("val_lat", latitude);
                    cmd.Parameters.AddWithValue("val_long", longitude);
                    int recordInserted = cmd.ExecuteNonQuery();

                }


            }

            catch (Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                dbc.closeConnection();
            }

        }

    }
}
