#region License

//
// Copyright (c) 2015 Strategik Pty Ltd,
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion License

using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Social;
using Microsoft.SharePoint.Client.UserProfiles;
using Strategik.CoreFramework.Configuration;
using System;
using System.Collections.Generic;

namespace Strategik.CoreFramework.Helpers
{
    /// <summary>
    /// Extension and helper methods for working with O365 and SP2013 social features
    /// </summary>
    public static class STKSocialHelper
    {
        #region Debug Methods

        public static void DumpProfilePropertiesForCurrentUser(ClientContext context)
        {
            PeopleManager peopleManager = new PeopleManager(context);
            PersonProperties personProperties = peopleManager.GetMyProperties();
            context.Load(personProperties,
                p => p.AccountName,
                p => p.DisplayName,
                p => p.Email,
                p => p.PersonalUrl,
                p => p.PictureUrl,
                p => p.Title,
                p => p.UserProfileProperties,
                p => p.UserUrl);

            context.ExecuteQuery();

            Console.WriteLine("");
            Console.WriteLine("Accessed user profile properties for user " + personProperties.DisplayName);
            Console.WriteLine("Account Name: " + personProperties.AccountName);
            Console.WriteLine("Email address: " + personProperties.Email);
            Console.WriteLine("Title: " + personProperties.Title);
            Console.WriteLine("Picture URL: " + personProperties.PictureUrl);
            Console.WriteLine("Personal Url:" + personProperties.PersonalUrl);

            foreach (var prop in personProperties.UserProfileProperties)
            {
                if (prop.Value != null)
                {
                    //        Console.WriteLine("Found user profile property " + prop.Key + " with value " + prop.Value.ToString());
                }
            }

            Console.WriteLine("");

            PersonProperties anotherPersonProperties = peopleManager.GetPropertiesFor("i:0#.f|membership|adrian@strategik365.onmicrosoft.com");
            context.Load(anotherPersonProperties,
               p => p.AccountName,
               p => p.DisplayName,
               p => p.Email,
               p => p.PersonalUrl,
               p => p.PictureUrl,
               p => p.Title,
               p => p.UserUrl);

            context.ExecuteQuery();

            Console.WriteLine("");
            Console.WriteLine("Accessed user profile properties for user " + personProperties.DisplayName);
            Console.WriteLine("Account Name: " + anotherPersonProperties.AccountName);
            Console.WriteLine("Email address: " + anotherPersonProperties.Email);
            Console.WriteLine("Title: " + anotherPersonProperties.Title);
            Console.WriteLine("Picture URL: " + anotherPersonProperties.PictureUrl);
            Console.WriteLine("Personal Url:" + anotherPersonProperties.PersonalUrl);
            Console.WriteLine("");

            //peopleManager.SetMyProfilePicture(StreaM)

            String[] profileProperties = new String[] { "DisplayName", "Email" };
            UserProfilePropertiesForUser userProfileProps = new UserProfilePropertiesForUser(context, "i:0#.f|membership|adrian@strategik365.onmicrosoft.com", profileProperties);

            IEnumerable<String> profilePropertyValues = peopleManager.GetUserProfilePropertiesFor(userProfileProps);

            context.Load(userProfileProps);
            context.ExecuteQuery();

            foreach (var value in profilePropertyValues)
            {
                Console.WriteLine(value + "\n");
            }
        }

        //https://msdn.microsoft.com/en-us/library/jj163818.aspx
        public static void TestFollowingInO365(ClientContext context, STConfiguration config)
        {
            SocialFollowingManager followingManager = new SocialFollowingManager(context);

            // A document
            SocialActorInfo socialActorInfo = new SocialActorInfo();
            socialActorInfo.ContentUri = config.ServiceUri.ToString() + "/documents/M2L2 - Authentication - Kerberos.pptx"; // any old file that we know exists
            socialActorInfo.ActorType = SocialActorType.Document;

            ClientResult<bool> isFollowed = followingManager.IsFollowed(socialActorInfo);
        }

        #endregion Debug Methods
    }
}