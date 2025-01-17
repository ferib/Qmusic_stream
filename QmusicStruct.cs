﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Qmusic
{
    public class Color
    {
        public string background { get; set; }
        public string extra { get; set; }
        public string foreground { get; set; }
    }

    public class Logo
    {
        public string active_android_url { get; set; }
        public string active_iphone_url { get; set; }
        public string app_card { get; set; }
        public string app_messages_avatar { get; set; }
        public string app_player_bg_phone { get; set; }
        public string app_player_bg_tablet { get; set; }
        public string app_player_header { get; set; }
        public string app_player_icon { get; set; }
        public string app_player_small { get; set; }
        public string app_player_thumbnail { get; set; }
        public string app_square { get; set; }
        public string appletv_background { get; set; }
        public string homepage_banner { get; set; }
        public string inactive_android_url { get; set; }
        public string inactive_iphone_url { get; set; }
        public string radioplayer_banner { get; set; }
        public string radioplayer_cover { get; set; }
        public string site_background { get; set; }
        public string site_logo { get; set; }
    }

    public class Aac
    {
        public object extra { get; set; }
        public string source { get; set; }
    }

    public class Android
    {
        public string high { get; set; }
        public string low { get; set; }
        public string video { get; set; }
    }

    public class Extra
    {
        public string pbid { get; set; }
        public string title { get; set; }
    }

    public class Hl
    {
        public Extra extra { get; set; }
        public string source { get; set; }
    }

    public class Iphone
    {
        public string live { get; set; }
        public string video { get; set; }
    }

    public class Mobile
    {
        public string audio { get; set; }
        public string live { get; set; }
        public string video { get; set; }
    }

    public class Mp3
    {
        public object extra { get; set; }
        public string source { get; set; }
    }

    public class RadioplayerId
    {
        public object extra { get; set; }
        public string source { get; set; }
    }

    public class Extra2
    {
        public string pbid { get; set; }
        public string title { get; set; }
    }

    public class Video
    {
        public Extra2 extra { get; set; }
        public string source { get; set; }
    }

    public class Streams
    {
        public List<Aac> aac { get; set; }
        public Android android { get; set; }
        public List<Hl> hls { get; set; }
        public Iphone iphone { get; set; }
        public Mobile mobile { get; set; }
        public List<Mp3> mp3 { get; set; }
        public List<RadioplayerId> radioplayerId { get; set; }
        public List<Video> video { get; set; }
    }

    public class Data
    {
        public string api_url { get; set; }
        public string background_image { get; set; }
        public string id { get; set; }
        public Logo logo { get; set; }
        public string name { get; set; }
        public List<object> search_terms { get; set; }
        public string station_id { get; set; }
        public Streams streams { get; set; }
    }

    public class Datum
    {
        public Color color { get; set; }
        public Data data { get; set; }
        public int id { get; set; }
        public List<object> locations { get; set; }
        public int published_start { get; set; }
        public int published_stop { get; set; }
        public string type { get; set; }
    }

    public class channelsResponse
    {
        public List<Datum> data { get; set; }
    }
}
