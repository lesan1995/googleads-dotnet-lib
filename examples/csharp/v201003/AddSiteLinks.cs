// Copyright 2011, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201003;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201003 {
  /// <summary>
  /// This code example shows how to add site links to an existing
  /// campaign. To create a campaign, run AddCampaign.cs.
  ///
  /// Tags: CampaignAdExtensionService.mutate
  /// </summary>
  class AddSiteLinks : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to add site links to an existing campaign. To " +
            "create a campaign, run AddCampaign.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddSiteLinks();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService)user.GetService(AdWordsService.v201003.
          CampaignAdExtensionService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      SitelinksExtension siteLinkExtension = new SitelinksExtension();

      Sitelink siteLink1 = new Sitelink();
      siteLink1.displayText = "Music";
      siteLink1.destinationUrl = "http://www.example.com/music";

      Sitelink siteLink2 = new Sitelink();
      siteLink2.displayText = "DVDs";
      siteLink2.destinationUrl = "http://www.example.com/dvds";

      Sitelink siteLink3 = new Sitelink();
      siteLink3.displayText = "New albums";
      siteLink3.destinationUrl = "http://www.example.com/albums/new";

      siteLinkExtension.sitelinks = new Sitelink[] {siteLink1, siteLink2, siteLink3};

      CampaignAdExtension campaignAdExtension = new CampaignAdExtension();
      campaignAdExtension.adExtension = siteLinkExtension;
      campaignAdExtension.campaignId = campaignId;

      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaignAdExtension;

      try {
        CampaignAdExtensionReturnValue retVal =
            campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (CampaignAdExtension campaignExtension in retVal.value) {
            Console.WriteLine("Created a campaign ad extension with id = \"{0}\" and " +
                "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
            foreach (Sitelink siteLink in
                (campaignExtension.adExtension as SitelinksExtension).sitelinks) {
              Console.WriteLine("-- Site link text is \"{0}\" and destination url is {1}",
                  siteLink.displayText, siteLink.destinationUrl);
            }
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add site links. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
