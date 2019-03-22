using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

// This code was found and transfered from ASP.NET Framework to ASP.NET Core by Eric Fueng
// The original code by Garth Egbert can be found at https://bitbucket.org/inet_garth/canvas-oauth2-workflow/src
// This code is largely unchanged from the code found in the link above.

namespace Georest.Web.Controllers
{
    public class LtiLaunchParameters
    {
        public string context_id = string.Empty;
        public string context_label = string.Empty;
        public string context_title = string.Empty;
        public string custom_canvas_api_domain = string.Empty;
        public string custom_canvas_course_id = string.Empty;
        public string custom_canvas_enrollment_state = string.Empty;
        public string custom_canvas_user_id = string.Empty;
        public string custom_canvas_user_login_id = string.Empty;
        public string ext_roles = string.Empty;
        public string launch_presentation_document_target = string.Empty;
        public string launch_presentation_height = string.Empty;
        public string launch_presentation_locale = string.Empty;
        public string launch_presentation_return_url = string.Empty;
        public string launch_presentation_width = string.Empty;
        public string lis_course_offering_sourcedid = string.Empty;
        public string lis_person_contact_email_primary = string.Empty;
        public string lis_person_name_family = string.Empty;
        public string lis_person_name_full = string.Empty;
        public string lis_person_name_given = string.Empty;
        public string lis_outcome_service_url = string.Empty;
        public string lis_result_sourcedid = string.Empty;
        public string lti_message_type = string.Empty;
        public string lti_version = string.Empty;
        public string oauth_callback = string.Empty;
        public string oauth_consumer_key = string.Empty;
        public string oauth_nonce = string.Empty;
        public string oauth_signature = string.Empty;
        public string oauth_signature_method = string.Empty;
        public string oauth_timestamp = string.Empty;
        public string oauth_version = string.Empty;
        public string resource_link_id = string.Empty;
        public string resource_link_title = string.Empty;
        public string roles = string.Empty;
        public string tool_consumer_info_product_family_code = string.Empty;
        public string tool_consumer_info_version = string.Empty;
        public string tool_consumer_instance_contact_email = string.Empty;
        public string tool_consumer_instance_guid = string.Empty;
        public string tool_consumer_instance_name = string.Empty;
        public string user_id = string.Empty;
        public string user_image = string.Empty;

        //canvas vars
        public string ext_content_return_types = string.Empty;
        public string ext_content_file_extensions = string.Empty;
        public string selection_directive = string.Empty;

        public LtiLaunchParameters()
        {
        }

        public LtiLaunchParameters(IFormCollection formParams)
        {
            if (formParams != null)
            {
                parseFormData(formParams);
            }
        }

        private void parseFormData(IFormCollection formParams)
        {
            this.context_id = (formParams["context_id"].Count != 0) ? formParams["context_id"] : (StringValues)string.Empty;
            this.context_label = (formParams["context_label"].Count != 0) ? formParams["context_label"] : (StringValues)string.Empty;
            this.context_title = (formParams["context_title"].Count != 0) ? formParams["context_title"] : (StringValues)string.Empty;
            this.custom_canvas_api_domain = (formParams["custom_canvas_api_domain"].Count != 0) ? formParams["custom_canvas_api_domain"] : (StringValues)string.Empty;
            this.custom_canvas_course_id = (formParams["custom_canvas_course_id"].Count != 0) ? formParams["custom_canvas_course_id"] : (StringValues)string.Empty;
            this.custom_canvas_enrollment_state = (formParams["custom_canvas_enrollment_state"].Count != 0) ? formParams["custom_canvas_enrollment_state"] : (StringValues)string.Empty;
            this.custom_canvas_user_id = (formParams["custom_canvas_user_id"].Count != 0) ? formParams["custom_canvas_user_id"] : (StringValues)string.Empty;
            this.custom_canvas_user_login_id = (formParams["custom_canvas_user_login_id"].Count != 0) ? formParams["custom_canvas_user_login_id"] : (StringValues)string.Empty;
            this.ext_roles = (formParams["ext_roles"].Count != 0) ? formParams["ext_roles"] : (StringValues)string.Empty;
            this.launch_presentation_document_target = (formParams["launch_presentation_document_target"].Count != 0) ? formParams["launch_presentation_document_target"] : (StringValues)string.Empty;
            this.launch_presentation_height = (formParams["launch_presentation_height"].Count != 0) ? formParams["launch_presentation_height"] : (StringValues)string.Empty;
            this.launch_presentation_locale = (formParams["launch_presentation_locale"].Count != 0) ? formParams["launch_presentation_locale"] : (StringValues)string.Empty;
            this.launch_presentation_return_url = (formParams["launch_presentation_return_url"].Count != 0) ? formParams["launch_presentation_return_url"] : (StringValues)string.Empty;
            this.launch_presentation_width = (formParams["launch_presentation_width"].Count != 0) ? formParams["launch_presentation_width"] : (StringValues)string.Empty;
            this.lis_course_offering_sourcedid = (formParams["lis_course_offering_sourcedid"].Count != 0) ? formParams["lis_course_offering_sourcedid"] : (StringValues)string.Empty;
            this.lis_person_contact_email_primary = (formParams["lis_person_contact_email_primary"].Count != 0) ? formParams["lis_person_contact_email_primary"] : (StringValues)string.Empty;
            this.lis_person_name_family = (formParams["lis_person_name_family"].Count != 0) ? formParams["lis_person_name_family"] : (StringValues)string.Empty;
            this.lis_person_name_full = (formParams["lis_person_name_full"].Count != 0) ? formParams["lis_person_name_full"] : (StringValues)string.Empty;
            this.lis_person_name_given = (formParams["lis_person_name_given"].Count != 0) ? formParams["lis_person_name_given"] : (StringValues)string.Empty;
            this.lis_outcome_service_url = (formParams["lis_outcome_service_url"].Count != 0) ? formParams["lis_outcome_service_url"] : (StringValues)string.Empty;
            this.lis_result_sourcedid = (formParams["lis_result_sourcedid"].Count != 0) ? formParams["lis_result_sourcedid"] : (StringValues)string.Empty;

            this.lti_message_type = (formParams["lti_message_type"].Count != 0) ? formParams["lti_message_type"] : (StringValues)string.Empty;
            this.lti_version = (formParams["lti_version"].Count != 0) ? formParams["lti_version"] : (StringValues)string.Empty;

            this.oauth_callback = (formParams["oauth_callback"].Count != 0) ? formParams["oauth_callback"] : (StringValues)string.Empty;
            this.oauth_consumer_key = (formParams["oauth_consumer_key"].Count != 0) ? formParams["oauth_consumer_key"] : (StringValues)string.Empty;
            this.oauth_nonce = (formParams["oauth_nonce"].Count != 0) ? formParams["oauth_nonce"] : (StringValues)string.Empty;
            this.oauth_signature = (formParams["oauth_signature"].Count != 0) ? formParams["oauth_signature"] : (StringValues)string.Empty;
            this.oauth_signature_method = (formParams["oauth_signature_method"].Count != 0) ? formParams["oauth_signature_method"] : (StringValues)string.Empty;
            this.oauth_timestamp = (formParams["oauth_timestamp"].Count != 0) ? formParams["oauth_timestamp"] : (StringValues)string.Empty;
            this.oauth_version = (formParams["oauth_version"].Count != 0) ? formParams["oauth_version"] : (StringValues)string.Empty;
            this.resource_link_id = (formParams["resource_link_id"].Count != 0) ? formParams["resource_link_id"] : (StringValues)string.Empty;
            this.resource_link_title = (formParams["resource_link_title"].Count != 0) ? formParams["resource_link_title"] : (StringValues)string.Empty;
            this.roles = (formParams["roles"].Count != 0) ? formParams["roles"] : (StringValues)string.Empty;
            this.tool_consumer_info_product_family_code = (formParams["tool_consumer_info_product_family_code"].Count != 0) ? formParams["tool_consumer_info_product_family_code"] : (StringValues)string.Empty;
            this.tool_consumer_info_version = (formParams["tool_consumer_info_version"].Count != 0) ? formParams["tool_consumer_info_version"] : (StringValues)string.Empty;
            this.tool_consumer_instance_contact_email = (formParams["tool_consumer_instance_contact_email"].Count != 0) ? formParams["tool_consumer_instance_contact_email"] : (StringValues)string.Empty;
            this.tool_consumer_instance_guid = (formParams["tool_consumer_instance_guid"].Count != 0) ? formParams["tool_consumer_instance_guid"] : (StringValues)string.Empty;
            this.tool_consumer_instance_name = (formParams["tool_consumer_instance_name"].Count != 0) ? formParams["tool_consumer_instance_name"] : (StringValues)string.Empty;
            this.user_id = (formParams["user_id"].Count != 0) ? formParams["user_id"] : (StringValues)string.Empty;
            this.user_image = (formParams["user_image"].Count != 0) ? formParams["user_image"] : (StringValues)string.Empty;
            this.ext_content_return_types = (formParams["ext_content_return_types"].Count != 0) ? formParams["ext_content_return_types"] : (StringValues)string.Empty;
            this.selection_directive = (formParams["selection_directive"].Count != 0) ? formParams["selection_directive"] : (StringValues)string.Empty;
            this.ext_content_file_extensions = (formParams["ext_content_file_extensions"].Count != 0) ? formParams["ext_content_file_extensions"] : (StringValues)string.Empty;
        }
    }
}