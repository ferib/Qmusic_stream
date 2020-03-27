using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Qmusic
{
    class Qmusic
    {
        private RestClient web { get; set; }
        private RestClient webq { get; set; }
        private string uuid = "idfa:9FBA1A63-9417-47D8-9C8D-BD1D56EE7C9D";
        private string appname = "rp_qmusic_app";
        private string dist = "dpg";
        private string sessionid = "EF2AE925-44E7-4FA7-A823-23317156E8DE";

        public string[] channelName = { "QMUSICAAC", "QMAXIMUMHITSAAC", "QFOUTERADIOAAC", "Q00SAAC", "Q90SAAC", "Q00SAAC", "QTHEMEAAC", "QWORKALICIOUSAAC", "QHOTNOW.mp3", "QRUNAAC", "QSHUTUP_DANCEAAC" };

        public bool isPlaying { get; set; }
        public Qmusic()
        {
            this.web = new RestClient("https://playerservices.streamtheworld.com/");
            this.webq = new RestClient("https://api.qmusic.be/");
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

        public void SaveMusicStream(string filepath, long durationSecond)
        {
            ReadMusicStream(this.GetStreamURL(), filepath, durationSecond);
        }

        public void ReadMusicStream(string url, string path, long durationSecond = 10)
        {
            this.isPlaying = true;

            List<byte> BufferFile = new List<byte>();

            
                
            string hosteDNS = url.Replace("https://", "").Split('/')[0];
            string requestString = $"GET /QMUSICAAC.aac HTTP/1.1\r\n";

            requestString += $"Host: {hosteDNS}:443\r\n";
            requestString += $"X-Playback-Session-Id: {sessionid}\r\n";
            //requestString += "Range: bytes=0-1\r\n"; //LMAO don't use this, will only give 256kb, some kind of special buffer??
            requestString += "icy-metadata:	1\r\n";
            requestString += "Accept: */*\r\n";
            requestString += "User-Agent: AppleCoreMedia/1.0.0.16G77 (iPhone; U; CPU OS 12_4 like Mac OS X; nl_be)\r\n";
            requestString += "Accept-Language: nl-be\r\n";
            requestString += "Accept-Encoding: identity\r\n";
            requestString += "Connection: keep-alive\r\n";
            requestString += "\r\n";

            TcpClient client = new TcpClient();
            client.Connect($"{hosteDNS}", 80);

            NetworkStream stream = client.GetStream();

            StreamWriter writer = new StreamWriter(stream);

            long fileSize = 0;

            // Send the request.
            writer.Write(requestString);
            writer.Flush();

            DateTime BDStart;
            DateTime BDEnd;

            while (this.isPlaying && fileSize < 8192 * durationSecond)
            {
                Thread.Sleep(2000);
                byte[] buffer = new byte[8192]; //7820 b/second?

                while (this.isPlaying && stream.DataAvailable && fileSize < 8192 * durationSecond)
                {
                   
                    BDStart = DateTime.Now;
                    stream.Read(buffer, 0, buffer.Length);

                    BufferFile.AddRange(buffer);
                    fileSize += buffer.Length;

                    //send data around
                    //foreach (var b in buffer)
                    //{
                    //    Console.Write(b.ToString("X2"));
                    //}
                    //Console.WriteLine();

                    BDEnd = DateTime.Now;
                    Thread.Sleep(BDStart.AddMilliseconds(1000) - BDEnd);
                    Console.WriteLine($"[{DateTime.UtcNow.ToString("dd/MM/yyyy_HH:mm:ss.fff")}]: 1sec downloaded in: {(BDEnd - BDStart).ToString("fff")}");

                }
                Console.WriteLine("buffered");
                //if(BufferFile.Count > 0)
                //{
                //    File.WriteAllBytes($"test_{DateTime.UtcNow.ToString("dd_MM_yyyy_HH-mm")}.mp4", BufferFile.ToArray());
                //    BufferFile.Clear();
                //    stream.Flush();
                //}
            }
            File.WriteAllBytes($"test_{DateTime.UtcNow.ToString("dd_MM_yyyy_HH-mm")}.mp4", BufferFile.ToArray());
            stream.Close();
            writer.Close();
            client.Close();

            
            return;
        }

        private List<Aac> GetStreamsList()
        {
            var request = new RestRequest($"2.4/app/channels", Method.GET);
            confHeader(ref request);

            var response = this.web.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return null;
            var result = JsonConvert.DeserializeObject<channelsResponse>(response.Content);

            List<Aac> aacStreams = new List<Aac>();
            foreach(var d in result.data)
            {
                if(d.data != null && d.data.streams != null && d.data.streams.aac != null)
                {
                    aacStreams.Add(d.data.streams.aac.FirstOrDefault());
                }
            }
            return aacStreams;

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
