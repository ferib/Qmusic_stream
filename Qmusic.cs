using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace Qmusic
{
    class Qmusic
    {
        private RestClient web { get; set; }
        private string uuid = "idfa:9FBA1A63-9417-47D8-9C8D-BD1D56EE7C9D";
        private string appname = "rp_qmusic_app";
        private string dist = "dpg";
        private string sessionid = "34F117AB-A1C9-4D32-81EE-75CF87B58C01";
        public Qmusic()
        {
            this.web = new RestClient("https://playerservices.streamtheworld.com/");
            this.web.UserAgent = "AppleCoreMedia/1.0.0.16G77 (iPhone; U; CPU OS 12_4 like Mac OS X; nl_be)";
            this.web.FollowRedirects = false;
        }

        public string GetStreamURL()
        {
            var request = new RestRequest($"api/livestream-redirect/QMUSICAAC.aac?uuid={uuid}&pname={appname}&dist={dist}", Method.GET);
            confHeader(ref request);

            var response = web.Execute(request);
            if(response.StatusCode == System.Net.HttpStatusCode.Found)
                return response.Headers.ToList().Find(x => x.Name == "Location").Value.ToString();
            return null;
        }


        private void confHeader(ref RestRequest request)
        {
            request.AddHeader("Host", "playerservices.streamtheworld.com");
            request.AddHeader("X-Playback-Session-Id", sessionid);
            request.AddHeader("icy-metadata","1");
            request.AddHeader("Accept-Encoding", "identity");
            request.AddHeader("Accept-Language", "nl-be");
            request.AddHeader("connection", "keep-alive");
            request.AddHeader("Accept", "*/*");
        }
    }
}
