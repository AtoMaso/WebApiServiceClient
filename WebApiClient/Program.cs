using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebApiClient
{
  class Program
  {
    // You will need to substitute your own host Url here:
    static string host = "http://localhost:5700/";

    //static void Main(string[] args)
    //{
    //      Console.WriteLine("Attempting to Log in with default admin user");

    //      // Get hold of a Dictionary representing the JSON in the response Body:
    //      var responseDictionary = GetResponseAsDictionary("admin@example.com", "Admin@123456");
    //      foreach (var kvp in responseDictionary)
    //      {
    //        Console.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
    //      }
    //      Console.Read();
    //}


    //static Dictionary<string, string> GetResponseAsDictionary(string userName, string password)
    //{
    //      HttpClient client = new HttpClient();
    //      var pairs = new List<KeyValuePair<string, string>>
    //                {
    //                    new KeyValuePair<string, string>( "grant_type", "password" ),
    //                    new KeyValuePair<string, string>( "username", userName ),
    //                    new KeyValuePair<string, string> ( "Password", password )
    //                };
    //      var content = new FormUrlEncodedContent(pairs);

    //      // Attempt to get a token from the token endpoint of the Web Api host:
    //      HttpResponseMessage response = client.PostAsync(host + "Token", content).Result;
    //      var result = response.Content.ReadAsStringAsync().Result;

    //      // De-Serialize into a dictionary and return:
    //      Dictionary<string, string> tokenDictionary  = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
    //      return tokenDictionary;
    //}

    static void Main(string[] args)
    {
      // Use the User Names/Emails and Passwords we set up in IdentityConfig:
      string adminUserName = "srbinovskim@optusnet.com.au";
      string adminUserPassword = "Admin1";

      string authorUserName = "srbinovskad@optusnet.com.au";
      string authorUserPassword = "Trader1";

       string userUserName = "srbinovskin@optusnet.com.au";
       string userUserPassword = "Trader2";

      // Use the new GetToken method to get a token for each user:
      string adminUserToken = GetToken(adminUserName, adminUserPassword);
      string authorUserToken = GetToken(authorUserName, authorUserPassword);
      string userUserToken = GetToken(userUserName, userUserPassword);

    // Try to get some data as an Admin:   
    Console.WriteLine(adminUserToken);
    Console.WriteLine("");

      Console.WriteLine("Attempting to get Userinfo as Admin User");
      string adminUserInfoResult = GetUserInfo(adminUserToken);
      Console.WriteLine("Admin User Info Result: {0}", adminUserInfoResult);
      Console.WriteLine("");

      Console.WriteLine("Attempting to get Values as Admin User");
      string adminValuesInfoResult = GetValues(adminUserToken);
      Console.WriteLine("Values Info Result: {0}", adminValuesInfoResult);
      Console.WriteLine("");

      // Try to get some data as a Author user:
      Console.WriteLine("Attempting to get Userinfo as Author User");
      string authorUserInfoResult = GetUserInfo(authorUserToken);
      Console.WriteLine("Author User Info Result: {0}", authorUserInfoResult);
      Console.WriteLine("");

      Console.WriteLine("Attempting to get Values as Author User");
      string authorValuesInfoResult = GetValues(authorUserToken);
      Console.WriteLine("Values Info Result: {0}", authorValuesInfoResult);
      Console.WriteLine("");

      // Try to get some data as a User user:
      Console.WriteLine("Attempting to get Userinfo as User");
      string userUserInfoResult = GetUserInfo(userUserToken);
      Console.WriteLine("User Info Result: {0}", userUserInfoResult);
      Console.WriteLine("");

      Console.WriteLine("Attempting to get Values as User");
      string userValuesInfoResult = GetValues(userUserToken);
      Console.WriteLine("Values Info Result: {0}", userValuesInfoResult);
      Console.WriteLine("");

       Console.Read();
    }


    static string GetToken(string userName, string password)
    {
          HttpClient client = new HttpClient();
          var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "username", userName ),
                            new KeyValuePair<string, string> ( "Password", password )
                        };
          var content = new FormUrlEncodedContent(pairs);

            //try
            //{

                // Attempt to get a token from the token endpoint of the Web Api host:
                HttpResponseMessage response = client.PostAsync(host + "Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                // De-Serialize into a dictionary and return:
                Dictionary<string, string> tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                return tokenDictionary["access_token"];
            //}
            //catch (Exception exc)
            //{
            //    string mess = exc.InnerException.Message;
            //} 
            //return string.Empty;
         
    }


    static string GetUserInfo(string token)
    {
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = client.GetAsync(host + "api/Account/UserInfo").Result;
        return response.Content.ReadAsStringAsync().Result;
      }
    }


    static string GetValues(string token)
    {
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = client.GetAsync(host + "api/Values").Result;
        return response.Content.ReadAsStringAsync().Result;
      }
    }



  }
}
