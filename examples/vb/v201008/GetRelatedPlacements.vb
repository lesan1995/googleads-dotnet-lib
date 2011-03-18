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
  ''' This code example retrieves urls that have content keywords related to a
  ''' given website.
  '''
  ''' Tags: TargetingIdeaService.get
  ''' </summary>
  Class GetRelatedPlacements
    Inherits SampleBase
    ''' <summary>
    ''' Returns a description about the code example.
    ''' </summary>
    Public Overrides ReadOnly Property Description() As String
      Get
        Return "This code example retrieves urls that have content keywords related to a " & _
            "given website."
      End Get
    End Property

    ''' <summary>
    ''' Main method, to run this code example as a standalone application.
    ''' </summary>
    ''' <param name="args">The command line arguments.</param>
    Public Shared Sub Main(ByVal args As String())
      Dim codeExample As SampleBase = New GetRelatedPlacements
      Console.WriteLine(codeExample.Description)
      codeExample.Run(New AdWordsUser)
    End Sub

    ''' <summary>
    ''' Run the code example.
    ''' </summary>
    ''' <param name="user">AdWords user running the code example.</param>
    Public Overrides Sub Run(ByVal user As AdWordsUser)
      ' Get the TargetingIdeaService.
      Dim targetingIdeaService As TargetingIdeaService = user.GetService( _
          AdWordsService.v201008.TargetingIdeaService)

      Dim urlText As String = "mars.google.com"

      Dim searchParameter As New RelatedToUrlSearchParameter
      searchParameter.urls = New String() {urlText}
      searchParameter.includeSubUrls = False

      Dim selector As New TargetingIdeaSelector
      selector.searchParameters = New SearchParameter() {searchParameter}
      selector.ideaType = IdeaType.PLACEMENT
      selector.requestType = RequestType.IDEAS

      Dim paging As New Paging
      paging.startIndex = 0
      paging.numberResults = 10

      selector.paging = paging

      Try
        Dim page As TargetingIdeaPage = targetingIdeaService.get(selector)
        If ((Not page Is Nothing) AndAlso (Not page.entries Is Nothing)) Then
          Console.WriteLine("There are a total of {0} urls with content keywords related " & _
              "to '{1}'. The first {2} entries are displayed below: \n", page.totalNumEntries, _
              urlText, page.entries.Length)

          For Each idea As TargetingIdea In page.entries
            For Each entry As Type_AttributeMapEntry In idea.data
              If (entry.key = AttributeType.PLACEMENT) Then
                Dim placementAttribute As PlacementAttribute = entry.value
                Console.WriteLine("Related content keywords were found at '{0}'.", _
                    placementAttribute.value.url)
              End If
            Next
          Next
        Else
          Console.WriteLine("No urls with content keywords related to your url were found.")
        End If
      Catch ex As Exception
        Console.WriteLine("Failed to retrieve related placements. Exception says ""{0}""", _
            ex.Message)
      End Try
    End Sub
  End Class
End Namespace
