using Peers.DataContracts;
using Peers.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Peers.Helpers
{
    public class LoginHelper
    {

        public static async Task<Response> UserLoginProcess(String email, String firstName, String lastName)
        {
            var loginTrackResult = await LoginHelper.UserLoginTrack(App.user.Email, App.user.FirstName, App.user.LastName);
            var userModifyResult = await LoginHelper.CreateUser(App.user.Email, App.user.FirstName, App.user.LastName);

            return userModifyResult;
        }

        public static async Task<Response> CreateUser(String email, String firstName, String lastName)
        {
            var resp = await UserGet(email);
            if (resp.IsSuccess && resp.Count == 0)
            {
                var modResp = await UserModify(0, email, firstName, lastName);
                if (modResp.IsSuccess)
                {
                    return new Response()
                    {
                        IsSuccess = true,
                        Count=1,
                        Message = "user created"
                    };
                }
                else
                    return new Response()
                    {
                        IsSuccess = false,
                        Count=0,
                        Message = "error creating user"
                    };
                
            }
            else
                return new Response()
                {
                    IsSuccess = false,
                    Count = 0,
                    Message = "error getting user"
                };
        }


        public static async Task<UserResponse> UserGet(String email)
        {
            SetHttpsCertificateCallback();

            var uri = new Uri(Config.ServiceEndpoint + "user\\" + email );

            try
            {
                var parsed = await RequestHelper.RequestData<UserResponse>(uri);
                return parsed;

            }
            catch (WebException)
            {                
                return new UserResponse()
                {
                    IsSuccess = false,
                    Message = UserMessage.ServiceConnectionError
                };
            }
            catch (Exception e)
            {

                return new UserResponse()
                {
                    IsSuccess = false,
                    Message = "exception"
                };

            }


        }

        public static async Task<Response> UserModify(int id, String email, String firstName, String lastName)
        {
            SetHttpsCertificateCallback();

            var uri = new Uri(Config.ServiceEndpoint + "user");
            String postData = "{\"Id\":\"" + id + "\", \"Email\":\"" + email + "\", \"FirstName\":\"" + firstName + "\",\"LastName\":\"" + lastName + "\"}";

            try
            {
                var parsed = await RequestHelper.RequestData<Response>(uri, postData);
                return parsed;

            }
            catch (WebException)
            {

                return new Response()
                {
                    IsSuccess = false,
                    Message = UserMessage.ServiceConnectionError
                };
            }
            catch (Exception e)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "exception"
                };
            }
        }

        public static async Task<Response> UserLoginTrack(String email, String firstName, String lastName)
        {
            SetHttpsCertificateCallback();

            var uri = new Uri(Config.ServiceEndpoint + "logintrack");

//            String postData = "{'email':'" + email + "','firstName':'" + firstName + "','lastName':'"+ lastName +"'}";
           // String postData = "{\"loginTrackRequest\":{\"Email\":\"" + email + "\", \"FirstName\":\"" + firstName + "\",\"LastName\":\"" + lastName + "\"}}";
            // {"Email":"shudecek@gmail.com", "FirstName":"Stan","LastName":"hudecek"}
            String postData = "{\"Email\":\"" + email + "\", \"FirstName\":\"" + firstName + "\",\"LastName\":\"" + lastName + "\"}";

            try
            {
                var parsed = await RequestHelper.RequestData<Response>(uri, postData);
                return parsed;

            }
            catch (WebException)
            {

                return new Response()
                {
                    IsSuccess = false,
                    Message = UserMessage.ServiceConnectionError
                };
            }
            catch (Exception e)
            {
                return new Response()
                {
                    IsSuccess = false,
                    Message = "exception"
                };
            }
        }

        private static void SetHttpsCertificateCallback()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                                        new System.Net.Security.RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) =>
                                        {
                                            if (policyErrors == System.Net.Security.SslPolicyErrors.None)
                                                return true;
                                            else if (policyErrors == System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch || Config.ServiceBase.Contains("//dev"))
                                            {
                                                // return true for dev only
                                                return true;
                                            }
                                            return false;
                                        });
        }

    }
}
