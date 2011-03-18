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
Imports Google.Api.Ads.AdWords.v201008

Imports System

Namespace Google.Api.Ads.AdWords.Examples.VB.v201008
  ''' <summary>
  ''' This code example retrieves all negative campaign criteria in an account.
  ''' To add a negative campaign criterion, run AddNegativeCampaignCriterion.vb.
  '''
  ''' Tags: CampaignCriterionService.get
  ''' </summary>
  Class GetAllNegativeCampaignCriteria
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example retrieves all negative campaign criteria in an account. " & _
            "To add a negative campaign criterion, run AddNegativeCampaignCriterion.vb."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetAllNegativeCampaignCriteria
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the CampaignCriterionService.
      Dim campaignCriterionService As CampaignCriterionService = user.GetService( _
          AdWordsService.v201008.CampaignCriterionService)

      Dim selector As New CampaignCriterionSelector

      Try
        Dim page As CampaignCriterionPage = campaignCriterionService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          For Each campaignCriterion As CampaignCriterion In page.entries
            If TypeOf campaignCriterion.criterion Is Keyword Then
              Dim keyword As Keyword = campaignCriterion.criterion
              Console.WriteLine("Negative keyword campaign criterion with campaign ID = '{0}', " & _
                  "criterion ID = '{1}', and text = '{2}' was found.", _
                  campaignCriterion.campaignId, keyword.id, keyword.text)
            ElseIf TypeOf campaignCriterion.criterion Is Placement Then
              Dim placement As Placement = campaignCriterion.criterion
              Console.WriteLine("Negative placement campaign criterion with campaign ID = " & _
                  "'{0}', criterion ID = '{1}' and url = '{2}' was found.", _
                  campaignCriterion.campaignId, placement.id, placement.url)
            Else
              Console.WriteLine("Negative campaign criterion with campaign ID = '{0}', " & _
                  "criterion ID = '{1}' and type = '{2}' was found.", _
                  campaignCriterion.campaignId, campaignCriterion.criterion.id, _
                  campaignCriterion.criterion.CriterionType)
            End If
          Next
        Else
          Console.WriteLine("No negative campaign criteria were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve negative campaign criteria. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
