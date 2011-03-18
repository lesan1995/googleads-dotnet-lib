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
  ''' This code example retrieves all the disapproved ads in a given campaign.
  '''
  ''' Tags: AdGroupAdService.get
  ''' </summary>
  Class GetAllDisapprovedAds
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example retrieves all the disapproved ads in a given campaign."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllDisapprovedAds
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the AdGroupAdService.
      Dim service As AdGroupAdService = user.GetService(AdWordsService.v201101.AdGroupAdService)

      Dim campaignId As Long = Long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"))

      ' Create a selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "CreativeApprovalStatus", "DisapprovalReasons"}

      ' Set the filters.
      Dim predicate As New Predicate
      predicate.operator = PredicateOperator.EQUALS
      predicate.field = "CampaignId"
      predicate.values = New String() {campaignId.ToString}

      selector.predicates = New Predicate() {predicate}

      Try
        Dim page As AdGroupAdPage = service.get(selector)

        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          For Each tempAdGroupAd As AdGroupAd In page.entries
            If (tempAdGroupAd.ad.approvalStatus = AdApprovalStatus.DISAPPROVED) Then
              Console.WriteLine("Ad id {0} has been disapproved for the following reason(s):", _
                  tempAdGroupAd.ad.id)
              For Each reason As String In tempAdGroupAd.ad.disapprovalReasons
                Console.WriteLine("    {0}", reason)
              Next
            End If
          Next
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to get Ad(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
