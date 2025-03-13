using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MongoDB.Driver;
using SpaceTrack.DAL.Model;
using SpaceTrack.DAL;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


///////////////////////////////////////////////////////////////////////////////////////////////for name  15000

namespace SpaceTrack.Services
{
    public class SpaceTrackTLE
    {
        private readonly HttpClient _httpClient;

        public SpaceTrackTLE()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer()
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.space-track.org")
            };
        }

        public async Task<(List<string> FirstLines, List<string> SecondLines)> GetAllTLEDataAsync(string queryUrl)
        {
            var credentials = new Dictionary<string, string>
            {
                 { "identity", "salahmohamedabdelhafez@gmail.com" },
                { "password", "track73track98nsst" }
                //{ "identity", "mohammedsalah888m@gmail.com" },
                //{ "password", "space45track73mohamed5677salah" }
            };

            // Log in to Space-Track
            var loginResponse = await _httpClient.PostAsync("/ajaxauth/login", new FormUrlEncodedContent(credentials));
            loginResponse.EnsureSuccessStatusCode();

            // Fetch the TLE data
            var response = await _httpClient.GetStringAsync(queryUrl);

            var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<string> firstLines = new List<string>();
            List<string> secondLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                // Ensure correct parsing of Line1 and Line2 based on TLE designators
                if (lines[i].StartsWith("1 ") && i + 1 < lines.Length && lines[i + 1].StartsWith("2 "))
                {
                    firstLines.Add(lines[i]);     // Line1 starts with "1"
                    secondLines.Add(lines[i + 1]); // Line2 starts with "2"
                    i++; // Skip the next line since it's already processed
                }
            }

            return (firstLines, secondLines);
        }

        public async Task<string> GetPayloadNameAsync(int noradId)
        {
            // Log in to Space-Track
            var credentials = new Dictionary<string, string>
                             {
                { "identity", "salahmohamedabdelhafez@gmail.com" },
                { "password", "track73track98nsst" }
            };
            //{
            //    { "identity", "mohammedsalah888m@gmail.com" },
            //    { "password", "space45track73mohamed5677salah" }
            //};

            var loginResponse = await _httpClient.PostAsync("/ajaxauth/login", new FormUrlEncodedContent(credentials));
            loginResponse.EnsureSuccessStatusCode();

            // Construct query URL to fetch object data using NORAD ID
            string queryUrl = $"/basicspacedata/query/class/satcat/NORAD_CAT_ID/{noradId}/orderby/LAUNCH asc/format/json";
            //https://www.space-track.org/basicspacedata/query/class/satcat/NORAD_CAT_ID/363/orderby/INTLDES%20asc/emptyresult/show
            // Send request to Space-Track
            var response = await _httpClient.GetStringAsync(queryUrl);

            // Parse JSON response
            var objects = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, object>>>(response);

            if (objects != null && objects.Count > 0 && objects[0].ContainsKey("OBJECT_NAME"))
            {
                return objects[0]["OBJECT_NAME"]?.ToString();
            }

            return null; // Return null if OBJECT_NAME is not found
        }
    }
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////comment for name 15000 end 




//88888***********************************************************************************************************************************work for first 300
/*
namespace SpaceTrack.Services
{
    public class SpaceTrackTLE
    {
        private readonly HttpClient _httpClient;

        public SpaceTrackTLE()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer()
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.space-track.org")
            };
        }

        public async Task<(List<string> FirstLines, List<string> SecondLines)> GetAllTLEDataAsync(string queryUrl)
        {
            var credentials = new Dictionary<string, string>
            {

             { "identity", "salahmohamedabdelhafez@gmail.com" },
                { "password", "track73track98nsst" }
               
            };

            // Log in to Space-Track
            var loginResponse = await _httpClient.PostAsync("/ajaxauth/login", new FormUrlEncodedContent(credentials));
            loginResponse.EnsureSuccessStatusCode();

            // Fetch the TLE data
            var response = await _httpClient.GetStringAsync(queryUrl);

            var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<string> firstLines = new List<string>();
            List<string> secondLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                // Ensure correct parsing of Line1 and Line2 based on TLE designators
                if (lines[i].StartsWith("1 ") && i + 1 < lines.Length && lines[i + 1].StartsWith("2 "))
                {
                    firstLines.Add(lines[i]);     // Line1 starts with "1"
                    secondLines.Add(lines[i + 1]); // Line2 starts with "2"
                    i++; // Skip the next line since it's already processed
                }
            }

            return (firstLines, secondLines);
        }
    }
}
*/
//88888***********************************************************************************************************************************work for first 300 end









/*
namespace SpaceTrack.Services
{
    public class SpaceTrackTLE
    {
        private readonly HttpClient _httpClient;
        private string _authToken;
        private DateTime _tokenExpiry;

        public SpaceTrackTLE()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer()
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.space-track.org")
            };
        }

        // Property to check if the session is authenticated
        public bool IsAuthenticated => !string.IsNullOrEmpty(_authToken) && DateTime.UtcNow < _tokenExpiry;

        // Method to authenticate and set token
        public async Task AuthenticateAsync()
        {
            if (IsAuthenticated) return;

            var credentials = new Dictionary<string, string>
            {
                { "identity", "salahmohamedabdelhafez@gmail.com" },
                { "password", "track73track98nsst" }
                //{ "identity", "mohammedsalah888m@gmail.com" },
                //{ "password", "space45track73mohamed5677salah" }
            };

            // Log in to Space-Track
            var loginResponse = await _httpClient.PostAsync("/ajaxauth/login", new FormUrlEncodedContent(credentials));
            loginResponse.EnsureSuccessStatusCode();

            // Set token and expiry (assume 1-hour session validity as an example)
            _authToken = "Authenticated"; // Placeholder since Space-Track doesn't provide a token explicitly
            _tokenExpiry = DateTime.UtcNow.AddHours(1);

            Console.WriteLine("Authenticated successfully.");
        }

        // Method to fetch all TLE data
        public async Task<(List<string> FirstLines, List<string> SecondLines)> GetAllTLEDataAsync(string queryUrl)
        {
            // Ensure authentication
            await AuthenticateAsync();

            // Fetch the TLE data
            var response = await _httpClient.GetStringAsync(queryUrl);

            var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<string> firstLines = new List<string>();
            List<string> secondLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                // Ensure correct parsing of Line1 and Line2 based on TLE designators
                if (lines[i].StartsWith("1 ") && i + 1 < lines.Length && lines[i + 1].StartsWith("2 "))
                {
                    firstLines.Add(lines[i]);     // Line1 starts with "1"
                    secondLines.Add(lines[i + 1]); // Line2 starts with "2"
                    i++; // Skip the next line since it's already processed
                }
            }

            return (firstLines, secondLines);
        }

        // Method to fetch payload name using NORAD ID
        public async Task<string> GetPayloadNameAsync(int noradId)
        {
            // Ensure authentication
            await AuthenticateAsync();

            // Construct query URL to fetch object data using NORAD ID
            string queryUrl = $"/basicspacedata/query/class/satcat/NORAD_CAT_ID/{noradId}/orderby/LAUNCH asc/format/json";

            // Send request to Space-Track
            var response = await _httpClient.GetStringAsync(queryUrl);

            // Parse JSON response
            //var objects = Newtonsoft.Json.JsonSerializer.Deserialize<List<Dictionary<string, object>>>(response);
            var objects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(response);

            if (objects != null && objects.Count > 0 && objects[0].ContainsKey("OBJECT_NAME"))
            {
                return objects[0]["OBJECT_NAME"]?.ToString();
            }

            return null; // Return null if OBJECT_NAME is not found
        }
      
    }
}
*/

//namespace SpaceTrack.Services
//{
//    public class SpaceTrackTLE
//    {
//        private readonly HttpClient _httpClient;

//        public SpaceTrackTLE()
//        {
//            var handler = new HttpClientHandler
//            {
//                CookieContainer = new CookieContainer()
//            };

//            _httpClient = new HttpClient(handler)
//            {
//                BaseAddress = new Uri("https://www.space-track.org")
//            };
//        }

//        public async Task<(List<string> FirstLines, List<string> SecondLines)> GetAllTLEDataAsync(string queryUrl)
//        {
//            var credentials = new Dictionary<string, string>
//            {
//                 { "identity", "salahmohamedabdelhafez@gmail.com" },
//                { "password", "track73track98nsst" }
//                //{ "identity", "mohammedsalah888m@gmail.com" },
//                //{ "password", "space45track73mohamed5677salah" }
//            };

//            // Log in to Space-Track
//            var loginResponse = await _httpClient.PostAsync("/ajaxauth/login", new FormUrlEncodedContent(credentials));
//            loginResponse.EnsureSuccessStatusCode();

//            // Fetch the TLE data
//            var response = await _httpClient.GetStringAsync(queryUrl);

//            var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

//            List<string> firstLines = new List<string>();
//            List<string> secondLines = new List<string>();

//            for (int i = 0; i < lines.Length; i++)
//            {
//                // Ensure correct parsing of Line1 and Line2 based on TLE designators
//                if (lines[i].StartsWith("1 ") && i + 1 < lines.Length && lines[i + 1].StartsWith("2 "))
//                {
//                    firstLines.Add(lines[i]);     // Line1 starts with "1"
//                    secondLines.Add(lines[i + 1]); // Line2 starts with "2"
//                    i++; // Skip the next line since it's already processed
//                }
//            }

//            return (firstLines, secondLines);
//        }

//        public async Task<string> GetPayloadNameAsync(int noradId)
//        {
//            // Log in to Space-Track
//            var credentials = new Dictionary<string, string>
//                             {
//                { "identity", "salahmohamedabdelhafez@gmail.com" },
//                { "password", "track73track98nsst" }
//            };
//            //{
//            //    { "identity", "mohammedsalah888m@gmail.com" },
//            //    { "password", "space45track73mohamed5677salah" }
//            //};

//            var loginResponse = await _httpClient.PostAsync("/ajaxauth/login", new FormUrlEncodedContent(credentials));
//            loginResponse.EnsureSuccessStatusCode();

//            // Construct query URL to fetch object data using NORAD ID
//            string queryUrl = $"/basicspacedata/query/class/satcat/NORAD_CAT_ID/{noradId}/orderby/LAUNCH asc/format/json";
//     //https://www.space-track.org/basicspacedata/query/class/satcat/NORAD_CAT_ID/363/orderby/INTLDES%20asc/emptyresult/show
//            // Send request to Space-Track
//            var response = await _httpClient.GetStringAsync(queryUrl);

//            // Parse JSON response
//            var objects = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, object>>>(response);

//            if (objects != null && objects.Count > 0 && objects[0].ContainsKey("OBJECT_NAME"))
//            {
//                return objects[0]["OBJECT_NAME"]?.ToString();
//            }

//            return null; // Return null if OBJECT_NAME is not found
//        }
//    }
//}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////comment for name 15000






//public async Task<string> GetPayloadNameAsync(int noradId)
//{
//    // Query Space-Track API for the payload name using NORAD ID
//    string queryUrl = $"https://www.space-track.org/basicspacedata/query/class/satcat/NORAD_CAT_ID/{noradId}/orderby/INTLDES%20asc/format/json";

//    try
//    {
//        // Fetch the response from Space-Track API
//        var response = await _httpClient.GetStringAsync(queryUrl);

//        // Deserialize the JSON response
//        var payloadData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(response);

//        // Extract the OBJECT_NAME field if present, else return "Unknown"
//        return payloadData?.FirstOrDefault()?["OBJECT_NAME"].GetString() ?? "Unknown";
//    }
//    catch (Exception ex)
//    {
//        // Handle exceptions (e.g., network issues, API errors)
//        Console.WriteLine($"Error fetching payload name: {ex.Message}");
//        return "Unknown";
//    }
//}


//public async Task<string> GetPayloadNameAsync(int noradId)
//{
//    // Query Space-Track API for the payload name using NoradId
//    string queryUrl = $"https://www.space-track.org/basicspacedata/query/class/satcat/NORAD_CAT_ID/{noradId}/orderby/INTLDES%20asc/format/json";

//    // Fetch the payload name
//    var response = await _httpClient.GetStringAsync(queryUrl);
//    var payloadData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(response);

//    // Extract the name if present, else return "Unknown"
//    return payloadData?.FirstOrDefault()?["OBJECT_NAME"] ?? "Unknown";
//}
//    }
//}

/*//////////////////////////////////////////////Comment to update name
namespace SpaceTrack.Services
{
    public class SpaceTrackTLE
    {
        private readonly HttpClient _httpClient;

        public SpaceTrackTLE()
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer()
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://www.space-track.org")
            };
        }

        public async Task<(List<string> FirstLines, List<string> SecondLines)> GetAllTLEDataAsync(string queryUrl)
        {
            var credentials = new Dictionary<string, string>
            {
                { "identity", "mohammedsalah888m@gmail.com" },
                { "password", "space45track73mohamed5677salah" }
            };

            // Log in to Space-Track
            var loginResponse = await _httpClient.PostAsync("/ajaxauth/login", new FormUrlEncodedContent(credentials));
            loginResponse.EnsureSuccessStatusCode();

            // Fetch the TLE data
            var response = await _httpClient.GetStringAsync(queryUrl);

            var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<string> firstLines = new List<string>();
            List<string> secondLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                // Ensure correct parsing of Line1 and Line2 based on TLE designators
                if (lines[i].StartsWith("1 ") && i + 1 < lines.Length && lines[i + 1].StartsWith("2 "))
                {
                    firstLines.Add(lines[i]);     // Line1 starts with "1"
                    secondLines.Add(lines[i + 1]); // Line2 starts with "2"
                    i++; // Skip the next line since it's already processed
                }
            }

            return (firstLines, secondLines);

            //var lines = response.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            //List<string> firstLines = new List<string>();
            //List<string> secondLines = new List<string>();

            //for (int i = 0; i < lines.Length; i += 3) // TLE format is 3 lines (Name, Line1, Line2)
            //{
            //    if (i + 1 < lines.Length && i + 2 < lines.Length)
            //    {
            //        firstLines.Add(lines[i + 1]); // Line1
            //        secondLines.Add(lines[i + 2]); // Line2
            //    }
            //}

            //return (firstLines, secondLines);
        }
    }

}
  */

/*
namespace SpaceTrack.Services
{
    public class SpaceTrackTLE
    {
        private class WebClientEx : WebClient
        {
            private readonly CookieContainer _cookieContainer = new CookieContainer();
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest request = base.GetWebRequest(address);
                if (request is HttpWebRequest httpRequest)
                    httpRequest.CookieContainer = _cookieContainer;
                return request;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////Excellent

        public (List<string> FirstLines, List<string> SecondLines) GetAllTLEData(string queryUrl)
        {
            string baseUrl = "https://www.space-track.org";

            using (var client = new WebClientEx())
            {
                var credentials = new NameValueCollection
    {
        { "identity", "mohammedsalah888m@gmail.com" },
        { "password", "space45track73mohamed5677salah" }
    };

                // Log in to Space-Track
                client.UploadValues($"{baseUrl}/ajaxauth/login", credentials);

                // Fetch the TLE data
                var response = client.DownloadData(queryUrl);
                string data = Encoding.UTF8.GetString(response);

                // Parse the response into TLE Lines
                var lines = data.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //

                List<string> firstLines = new List<string>();
                List<string> secondLines = new List<string>();

                for (int i = 0; i < lines.Length; i += 3) // TLE format is 3 lines (Name, Line1, Line2)
                {
                    if (i + 1 < lines.Length && i + 2 < lines.Length)
                    {
                        firstLines.Add(lines[i + 1]); // Line1
                        secondLines.Add(lines[i + 2]); // Line2
                    }
                }

                return (firstLines, secondLines);
            }
        }

    }
}
*/

///////////////////////////////////////////////////////////////////////////////////////////////////////Excellent



//public (List<string> FirstLines, List<string> SecondLines) GetAllTLEData(string queryUrl)
//{
//    string baseUrl = "https://www.space-track.org";

//    using (var client = new WebClientEx())
//    {
//        var credentials = new NameValueCollection
//{
//    { "identity", "mohammedsalah888m@gmail.com" },
//    { "password", "space45track73mohamed5677salah" }
//};

//        // Log in to Space-Track
//        client.UploadValues($"{baseUrl}/ajaxauth/login", credentials);

//        // Fetch the TLE data
//        var response = client.DownloadData(queryUrl);
//        string data = Encoding.UTF8.GetString(response);

//        // Parse the response into TLE Lines
//        var lines = data.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

//        List<string> firstLines = new List<string>();
//        List<string> secondLines = new List<string>();

//        for (int i = 0; i < lines.Length; i += 3) // TLE format is 3 lines (Name, Line1, Line2)
//        {
//            if (i + 1 < lines.Length && i + 2 < lines.Length)
//            {
//                firstLines.Add(lines[i + 1]); // Line1
//                secondLines.Add(lines[i + 2]); // Line2
//            }
//            else
//            {
//                Console.WriteLine($"Malformed TLE set at line {i}. Skipping...");
//            }
//        }

//        Console.WriteLine($"Fetched TLEs: {firstLines.Count}");
//        return (firstLines, secondLines);
//    }
//}
///////////////////////////////////////////////////////////////////////////////////////////////////////Excellent


//////////////////////////////////////////////////////////////////////////////////////////Work for only 24000 debri
//public (List<string> FirstLines, List<string> SecondLines) GetAllDebrisTLEData()
//{
//    string baseUrl = "https://www.space-track.org";
//    string requestUrl = $"{baseUrl}/basicspacedata/query/class/tle_latest/OBJECT_TYPE/DEBRIS/orderby/ORDINAL%20asc/limit/40000/format/tle/emptyresult/show";

//    using (var client = new WebClientEx())
//    {
//        var credentials = new NameValueCollection
//{
//    { "identity", "mohammedsalah888m@gmail.com" },
//    { "password", "space45track73mohamed5677salah" }
//};

//        // Log in to Space-Track
//        client.UploadValues($"{baseUrl}/ajaxauth/login", credentials);

//        // Fetch the TLE data
//        var response = client.DownloadData(requestUrl);
//        string data = Encoding.UTF8.GetString(response);

//        // Parse the response into TLE Lines
//        var lines = data.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

//        List<string> firstLines = new List<string>();
//        List<string> secondLines = new List<string>();

//        for (int i = 0; i < lines.Length; i += 3) // TLE format is 3 lines (Name, Line1, Line2)
//        {
//            if (i + 1 < lines.Length && i + 2 < lines.Length)
//            {
//                firstLines.Add(lines[i + 1]); // Line1
//                secondLines.Add(lines[i + 2]); // Line2
//            }
//        }

//        return (firstLines, secondLines);
//    }
//}
//////////////////////////////////////////////////////////////////////////////////////////Work for only 24000 debri

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Excellent
//public (string FirstLine, string SecondLine) GetTLEData(string noradId, DateTime startDate, DateTime endDate)
//{
//    string baseUrl = "https://www.space-track.org";
//    string requestUrl = $"{baseUrl}/basicspacedata/query/class/tle_latest/ORDINAL/1/NORAD_CAT_ID/{noradId}/orderby/EPOCH%20desc/format/tle";
//    using (var client = new WebClientEx())
//    {
//        var credentials = new NameValueCollection
//{
//    { "identity", "mohammedsalah888m@gmail.com" },
//    { "password", "space45track73mohamed5677salah" }
//};
//        client.UploadValues($"{baseUrl}/ajaxauth/login", credentials);
//        var response = client.DownloadData(requestUrl);
//        string data = Encoding.UTF8.GetString(response);
//        // Parse the response into TLE lines
//        var lines = data.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
//        if (lines.Length >= 2)
//        {
//            return (lines[0], lines[1]);
//        }
//        throw new Exception("Failed to retrieve complete TLE data");
//    }
//}
///////////////////////////////////////////////////////////////////////////////////////////////////////////Excellent
//public class SpaceTrack
//{
//    private class WebClientEx : WebClient
//    {
//        private readonly CookieContainer _cookieContainer = new CookieContainer();
//      protected override WebRequest GetWebRequest(Uri address)
//        {
//            WebRequest request = base.GetWebRequest(address);
//            if (request is HttpWebRequest httpRequest)
//                httpRequest.CookieContainer = _cookieContainer;
//      return request;
//        }
//    }
//    public string GetTLEData(string noradId, DateTime startDate, DateTime endDate)
//    {
//        string baseUrl = "https://www.space-track.org";
//        string requestUrl = $"{baseUrl}/basicspacedata/query/class/tle_latest/ORDINAL/1/NORAD_CAT_ID/{noradId}/orderby/EPOCH%20desc/format/tle";

//        using (var client = new WebClientEx())
//        {
//            var credentials = new NameValueCollection
//            {
//                { "identity", "mohammedsalah888m@gmail.com" },
//                { "password", "space45track73mohamed5677salah" }
//            };

//            client.UploadValues($"{baseUrl}/ajaxauth/login", credentials);

//            var response = client.DownloadData(requestUrl);
//            return Encoding.UTF8.GetString(response);
//        }
//    }
//}
//}
//}

//https://www.space-track.org/basicspacedata/query/class/tle_latest/ORDINAL/1/format/tle to fetch all
