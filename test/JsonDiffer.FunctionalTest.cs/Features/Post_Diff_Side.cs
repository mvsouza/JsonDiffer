using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.XUnit2;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDiffer.FunctionalTest.Features
{
    [FeatureDescription(
@"In order to compare a JSON document to another posted
As service consumer  
I want to post it.")]
    [Label("Story-1")]
    public partial class Post_Diff_Side
    {
        [Scenario]
        [MultiAssert]
        public async void Post_valid_left_side_json()
        {
            await Runner.RunScenarioActionsAsync(
                        _ => Given_a_Json_document_on_base64("{\"key\":\"value\"}"),
                        _ => Given_a_diff_id("IDTEST"),
                        _ => When_the_document_is_posted_as_left_side_diff(),
                        _ => Then_should_receive_ok_message()
                    );
        }
        [Scenario]
        [MultiAssert]
        public async void Post_valid_right_side_json()
        {
            await Runner.RunScenarioActionsAsync(
                        _ => Given_a_Json_document_on_base64("{\"key\":\"value\"}"),
                        _ => Given_a_diff_id("IDTEST"),
                        _ => When_the_document_is_posted_as_right_side_diff(),
                        _ => Then_should_receive_ok_message()
                    );
        }
        [Scenario]
        [MultiAssert]
        public async void Try_to_post_invalid_left_side_json()
        {
            await Runner.RunScenarioActionsAsync(
                        _ => Given_a_Json_document_not_on_base64("%"),
                        _ => Given_a_diff_id("IDTEST"),
                        _ => When_the_document_is_posted_as_left_side_diff(),
                        _ => Then_should_receive_bad_request_message("Validation exception")
                    );
        }
        [Scenario]
        [MultiAssert]
        public async void Try_to_post_invalid_right_side_json()
        {
            await Runner.RunScenarioActionsAsync(
                        _ => Given_a_Json_document_not_on_base64("%"),
                        _ => Given_a_diff_id("IDTEST"),
                        _ => When_the_document_is_posted_as_right_side_diff(),
                        _ => Then_should_receive_bad_request_message("Validation exception")
                    );
        }
    }
}
