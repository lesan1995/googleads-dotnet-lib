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
  ''' This code example gets all paused campaigns. To add a campaign, run
  ''' AddCampaign.vb.
  '''
  ''' Tags: CampaignService.get
  ''' </summary>
  Class GetAllPausedCampaigns
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example gets all paused campaigns. To add a campaign, run " & _
            "AddCampaign.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllPausedCampaigns
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignService.
      Dim campaignService As CampaignService = user.GetService( _
          AdWordsService.v201101.CampaignService)

      ' Create the selector.
      Dim selector As New Selector
      selector.fields = New String() {"Id", "Name", "Status"}

      ' Set the filters.
      Dim predicate As New Predicate
      predicate.operator = PredicateOperator.EQUALS
      predicate.field = "Status"
      predicate.values = New String() {CampaignStatus.PAUSED.ToString}

      selector.predicates = New Predicate() {predicate}

      Try
        ' Get all campaigns.
        Dim page As CampaignPage = campaignService.get(selector)

        ' Display campaigns.
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          If (page.entries.Length > 0) Then
            For Each campaign As Campaign In page.entries
              Console.WriteLine("Paused campaign with id = '{0}', name = '{1}' was found.", _
                  campaign.id, campaign.name)
            Next
          Else
            Console.WriteLine("No paused campaigns were found.")
          End If
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve Campaign(s). Exception says ""{0}""", ex.Message)
      End Try
    End Sub
  End Class
End Namespace
