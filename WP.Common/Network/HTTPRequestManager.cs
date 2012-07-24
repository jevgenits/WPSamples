using System;
using System.IO;
using System.Net;
using System.Text;

namespace WP.Common.Network
{
    public class HTTPRequestManager
    {
        /// <summary>
        /// Method to make a POST call to a HTTPS url
        /// </summary>
        /// <param name="url">HTTPS url</param>
        /// <param name="requestBody">content for a request</param>
        private void MakeHTTPSPostCall(string url, string requestBody)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = "POST";
            webRequest.BeginGetRequestStream(
                requestResult =>
                {
                    // call this line even if requestBody doesn't contain anything 
                    // otherwise you will most likely get a HTTP 404 NotFound exception
                    using (Stream postStream = webRequest.EndGetRequestStream(requestResult))
                    {
                        if (requestBody != null)
                        {
                            byte[] byteArray = Encoding.UTF8.GetBytes(requestBody);
                            postStream.Write(byteArray, 0, requestBody.Length);
                        }
                    }
                    webRequest.BeginGetResponse(
                        responseResult =>
                        {
                            try
                            {
                                using (var response =
                                    (HttpWebResponse)webRequest.EndGetResponse(responseResult))
                                using (var streamResponse = response.GetResponseStream())
                                using (var streamRead = new StreamReader(streamResponse))
                                {
                                    var responseString = streamRead.ReadToEnd();
                                    var success = response.StatusCode == HttpStatusCode.OK;

                                    // do something here with response...
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        },
                        null);
                },
                null);
        }
    }
}
