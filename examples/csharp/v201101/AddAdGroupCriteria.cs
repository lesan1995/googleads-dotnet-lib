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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example adds a keyword and a placement to an ad group. To get
  /// ad groups, run GetAllAdGroups.cs.
  ///
  /// Tags: AdGroupCriterionService.mutate
  /// </summary>
  class AddAdGroupCriteria : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a keyword and a placement to an ad group. To get " +
            "ad groups, run GetAllAdGroups.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddAdGroupCriteria();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201101.AdGroupCriterionService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));

      // Create keyword.
      Keyword keyword = new Keyword();
      keyword.text = "mars cruise";
      keyword.matchType = KeywordMatchType.BROAD;

      // Create biddable ad group criterion.
      AdGroupCriterion keywordCriterion = new BiddableAdGroupCriterion();
      keywordCriterion.adGroupId = adGroupId;
      keywordCriterion.criterion = keyword;

      // Create placement.
      Placement placement = new Placement();
      placement.url = "http://mars.google.com";

      // Create biddable ad group criterion.
      AdGroupCriterion placementCriterion = new BiddableAdGroupCriterion();
      placementCriterion.adGroupId = adGroupId;
      placementCriterion.criterion = placement;

      // Create operations.
      AdGroupCriterionOperation keywordOperation = new AdGroupCriterionOperation();
      keywordOperation.@operator = Operator.ADD;
      keywordOperation.operand = keywordCriterion;

      AdGroupCriterionOperation placementOperation = new AdGroupCriterionOperation();
      placementOperation.@operator = Operator.ADD;
      placementOperation.operand = placementCriterion;

      try {
        AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
            new AdGroupCriterionOperation[] {keywordOperation, placementOperation});

        if (retVal != null && retVal.value != null) {
          foreach (AdGroupCriterion adGroupCriterion in retVal.value) {
            Console.WriteLine("Ad group criterion with ad group id = '{0}, criterion id = '{1} " +
                "and type = '{2}' was created.", adGroupCriterion.adGroupId,
                adGroupCriterion.criterion.id, adGroupCriterion.criterion.CriterionType);
          }
        } else {
          Console.WriteLine("No ad group criteria were added.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad group criteria. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
