using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using LtiLibrary.Core.OAuth;
using LtiLibrary.AspNetCore;
using LtiLibrary.NetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Specialized;
using Microsoft.Extensions.Logging;

// This code was found and transfered from ASP.NET Framework to ASP.NET Core by Eric Fueng.
// The original code by Garth Egbert can be found at https://bitbucket.org/inet_garth/canvas-oauth2-workflow/src
// Several changes by Tyelor Klein, commented by Tyelor Klein.

namespace Georest.Web.Controllers
{
    public class OAuthHelper : LtiLaunchParameters
    {
        protected readonly ILogger _logger = new Logger<Exception>(new LoggerFactory());

        public string ClientUrl { get; set; } = string.Empty;
        public string ConsumerSecret { get; set; } = string.Empty;
        public bool IsSignatureVerified { get; set; } = false;

        public string UniqueSessionKey => this.user_id + "." + this.custom_canvas_api_domain + "." + this.custom_canvas_course_id;

        /***********************************************************/
        //  This property determines if you have a valid role to
        //  launch Geolabs.
        /***********************************************************/
        public bool AllowLtiLaunch
        {
            get
            {
                bool rval = false;
                if (!string.IsNullOrEmpty(this.roles))
                {
                    string myRoles = this.roles.ToUpper();
                    rval = myRoles.Contains("FACULTY") || myRoles.Contains("INSTRUCTOR") || myRoles.Contains("ADMINISTRATOR") || myRoles.Contains("TEACHINGASSISTANT") || myRoles.Contains("LEARNER") || myRoles.Contains("STUDENT");
                }
                return rval;
                //return true;
            }
        }

        public OAuthHelper() : base()
        {

        }

        /***********************************************************/
        //  This creates the OAuthHelper using the input HttpRequest.
        /***********************************************************/
        public OAuthHelper(HttpRequest request) : base(request.Form)
        {
            if (request != null)
            {
                ClientUrl = request.Host.ToString();

                /***********************************************************/
                //  This is supposed to verify that this is a valid token
                //  from canvas but it doesn't seem to work properly and 
                //  it could be fixed or scrapped in the future.
                /***********************************************************/
                ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"] != null ? ConfigurationManager.AppSettings["consumerSecret"] : Guid.NewGuid().ToString();

                var requestFormDictionary = ToDictionary(request.Form);
                IsSignatureVerified = (OAuthUtility.GenerateSignature(request.Method, new Uri(request.GetEncodedUrl()), requestFormDictionary, ConsumerSecret) == this.oauth_signature);
            }
        }

        public bool verifyNonce()
        {
            bool rval = false;

            if (string.IsNullOrEmpty(oauth_nonce))
                rval = false;
            else
            {
                /***********************************************************/
                //  It is up to you how you want to verify the nonce is 
                //  unique. The code below does not work but may provide a
                //  good starting point for verification.
                /***********************************************************/
                //use memcached to verify the nonce is unique
                //try
                //{
                //	if (_bypassElastiCache) return true;

                //	clsMemCached mem = new clsMemCached();
                //	string key = this.oauth_consumer_key + ":" + this.oauth_nonce;
                //	_logger.Debug("memCached Key: " + key);
                //	if (mem.getKeyValue(key) == null)
                //	{
                //		mem.setKeyValue(key, "1");
                //		rval = true;
                //	}
                //	else
                //	{
                //		rval = false;
                //	}
                //}
                //catch (Exception err)
                //{
                //	_logger.Error(err, "[EXCEPTION] verifyNonce() failed");
                //}

                /***********************************************************/
                //  For the purpose of demo and testing we will always 
                //  return true
                /***********************************************************/
                rval = true;
            }

            return rval;
        }

        /***********************************************************/
        //  This method will verify if the OAuth2 token from Canvas
        //  has been generated within the last 5 minutes.
        /***********************************************************/
        public bool VerifyTimestamp()
        {
            bool rval = false;

            if (this.oauth_timestamp != string.Empty)
            {
                try
                {
                    DateTime tstamp = new DateTime(1970, 1, 1, 0, 0, 0);
                    tstamp = tstamp.AddSeconds(int.Parse(this.oauth_timestamp)).ToLocalTime();
                    rval = ((DateTime.Now - tstamp).TotalMinutes < 5);
                }
                catch (Exception err)
                {
                    _logger.LogError(err, "[EXCEPTION] verifyTimeStamp(): ");
                }
            }

            return rval;
        }

        /***********************************************************/
        //  This helper method will convert a given IFormCollection
        //  into a corresponding NameValueCollection.
        /***********************************************************/
        private NameValueCollection ToDictionary(IFormCollection formCollection)
        {
            var nvc = new NameValueCollection();
            foreach (var key in formCollection.Keys)
            {
                nvc.Add(key, formCollection[key]);
            }

            return nvc;
        }
    }
}