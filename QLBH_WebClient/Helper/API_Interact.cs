using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBH_WebClient.Helper
{
    public class API_Interact
    {
        public static RestResponse Auth(string url, string url_authen, string data)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url_authen, Method.Post);
            var body = data;
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);
            return response;
        }


        public static string GetData(string url, string url_authen, string token)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url_authen, Method.Get);
            string stringToken = "Bearer " + token;
            request.AddHeader("Authorization", stringToken);
            var response = client.Execute(request);
            return response.Content;
        }

        public static RestResponse DeleteData(string url, string url_authen, string id, string idName, string token)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var url_req = url_authen + "?" + idName + "=" + id;
            var request = new RestRequest(url_req, Method.Delete);
            string stringToken = "Bearer " + token;
            request.AddHeader("Authorization", stringToken);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse GetDataById(string url, string url_authen, string id, string idName, string token)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var url_req = url_authen + "?" + idName + "=" + id;
            var request = new RestRequest(url_req, Method.Get);
            string stringToken = "Bearer " + token;
            request.AddHeader("Authorization", stringToken);
            var response = client.Execute(request);
            return response;
        }


        public static RestResponse SearchDataByName(string url, string url_authen, string name, string idName, string token)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var url_req = url_authen + "?" + idName + "=" + name;
            var request = new RestRequest(url_req, Method.Get);
            string stringToken = "Bearer " + token;
            request.AddHeader("Authorization", stringToken);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse InsertData(string url, string url_authen, string data, string token)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url_authen, Method.Post);
            string stringToken = "Bearer " + token;
            request.AddHeader("Authorization", stringToken);
            var body = data;
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse PullData(string url, string url_authen, string data, string token)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url_authen, Method.Post);
            string stringToken = "Bearer " + token;
            request.AddHeader("Authorization", stringToken);
            var body = data;
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);
            return response;
        }

        public static RestResponse UpdateData(string url, string url_authen, string data, string token)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url_authen, Method.Put);
            string stringToken = "Bearer " + token;
            request.AddHeader("Authorization", stringToken);
            var body = data;
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);
            return response;
        }

        public static string SaveImage(string url, string url_authen, string data)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(url_authen, Method.Post);
            var body = data;
            request.AddStringBody(body, DataFormat.Json);
            var response = client.Execute(request);
            return response.Content;
        }
    }
}