' Copyright 2011, Google Inc. All Rights Reserved.
'
' Licensed under the Apache License, Version 2.0 (the "License");
' you may not use this file except in compliance with the License.
' You may obtain a copy of the License at
'
'     http://www.apache.org/licenses/LICENSE-2.0
'
' Unless required by applicable law or agreed to in writing, software
' distributed under the License is distributed on an "AS IS" BASIS,
' WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
' See the License for the specific language governing permissions and
' limitations under the License.

' Author: api.anash@gmail.com (Anash P. Oommen)

Imports Google.Api.Ads.AdWords.Lib
Imports Google.Api.Ads.AdWords.v201101

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201101
  ''' <summary>
  ''' This code example gets bid landscapes for an ad group. To get adgroups,
  ''' run GetAllAdGroups.vb.
  '''
  ''' Tags: DataService.getAdGroupBidLandscape
  ''' </summary>
  Class GetAdGroupBidLandScape
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets bid landscapes for an ad group. To get adgroups, run " & _
            "GetAllAdGroups.vb"
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New AddAdExtensionOverride
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the DataService.
      Dim dataService As DataService = user.GetService(AdWordsService.v201101.DataService)

      Dim adGroupId As Long = Long.Parse(_T("INSERT_ADGROUP_ID_HERE"))

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"AdGroupId", "LandscapeType", "LandscapeCurrent", _
          "StartDate", "EndDate", "Bid", "LocalClicks", "LocalCost", "MarginalCpc", _
          "LocalImpressions"}

      ' Set the filters.
      Dim adGroupPredicate As New Predicate
      adGroupPredicate.field = "AdGroupId"
      adGroupPredicate.operator = PredicateOperator.IN
      adGroupPredicate.values = New String() {adGroupId.ToString}

      selector.predicates = New Predicate() {adGroupPredicate}

      Try
        ' Get bid landscape for ad group.
        Dim page As AdGroupBidLandscapePage = dataService.getAdGroupBidLandscape(selector)
        If (((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) AndAlso _
            (page.entries.Length > 0)) Then
          For Each bidLandscape As AdGroupBidLandscape In page.entries
            Console.WriteLine("Found ad group bid landscape with ad group id '{0}', " & _
                "type '{1}', current: '{2}', start date '{3}', end date '{4}', and " & _
                "landscape points", bidLandscape.adGroupId, bidLandscape.type, _
                bidLandscape.landscapeCurrent, bidLandscape.startDate, bidLandscape.endDate)
            Dim point As BidLandscapeLandscapePoint
            For Each point In bidLandscape.landscapePoints
              Console.WriteLine("- bid: {0} => clicks: {1}, cost: {2}, marginalCpc: {3}, " & _
                  "impressions: {4}", point.bid.microAmount, point.bid.microAmount, _
                  point.clicks, point.cost.microAmount, point.marginalCpc.microAmount, _
                  point.impressions)
            Next
          Next
        Else
          Console.WriteLine("No ad group bid landscapes were found.\n")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get ad group bid landscapes. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
