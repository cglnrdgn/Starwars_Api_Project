using Newtonsoft.Json;
using P1.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
namespace P1.Models
{
    public class Mains
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private static readonly string apiUrl = "https://swapi.dev/api/";

        private readonly string _proxyName;

        public Mains(string proxyName)
        {
            _proxyName = proxyName;
        }

        public Mains()
        {
        }

        private static string Request(string endpoint, HttpMethod httpMethod)
        {
            return Request(endpoint, httpMethod, null);
        }

        private static string Request(string endpoint, HttpMethod httpMethod, string data)
        {
            string result = string.Empty;
            string url = apiUrl + endpoint;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = httpMethod.ToString();

            if (httpMethod == HttpMethod.POST && !string.IsNullOrEmpty(data))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                httpWebRequest.ContentLength = bytes.Length;
                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            try
            {
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("HTTP isteğinde bir hata oluştu: " + ex.Message);
            }

            return result;
        }

        private static string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            StringBuilder parameters = new StringBuilder();

            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }

            return parameters.ToString().TrimEnd('&');
        }

        public static T GetSingle<T>(string endpoint, Dictionary<string, string>? parameters = null) where T : Model
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                
                serializedParameters = "?" + SerializeDictionary(parameters);
            }
            return GetSingleByUrl<T>(endpoint + serializedParameters);
        }


        public ModelResult<T> GetMultiple<T>(string endpoint) where T : Model
        {
            return GetMultiple<T>(endpoint, null);
        }

        public ModelResult<T> GetMultiple<T>(string endpoint, Dictionary<string, string> parameters) where T : Model
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }

            string json = Request(endpoint + serializedParameters, HttpMethod.GET);
            ModelResult<T> swapiResponse = JsonConvert.DeserializeObject<ModelResult<T>>(json);
            return swapiResponse;
        }

        public NameValueCollection GetQueryParameters(string dataWithQuery)
        {
            NameValueCollection result = new NameValueCollection();

            string[] parts = dataWithQuery.Split('?');

            if (parts.Length > 1)
            {
                string QueryParameter = parts[1];
                if (!string.IsNullOrEmpty(QueryParameter))
                {
                    string[] p = QueryParameter.Split('&');

                    foreach (string s in p)
                    {
                        if (s.Contains('='))
                        {
                            string[] temp = s.Split('=');
                            result.Add(temp[0], temp[1]);
                        }
                        else
                        {
                            result.Add(s, string.Empty);
                        }
                    }
                }
            }
            return result;
        }

        public ModelResult<T> GetAllPaginated<T>(string entityName, string pageNumber = "1") where T : Model
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("page", pageNumber);

            ModelResult<T> result = GetMultiple<T>(entityName, parameters);

            result.next_page_no = String.IsNullOrEmpty(result.next) ? null : GetQueryParameters(result.next)["page"];
            result.previous_page_no = String.IsNullOrEmpty(result.previous) ? null : GetQueryParameters(result.previous)["page"];

            return result;
        }

        public static T GetSingleByUrl<T>(string url) where T : Model
        {
            string json = Request(url, HttpMethod.GET);

            T swapiResponse = JsonConvert.DeserializeObject<T>(json);

            return swapiResponse;
        }
    }
}
