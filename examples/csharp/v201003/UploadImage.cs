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
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.AdWords.v201003;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201003 {
  /// <summary>
  /// This code example uploads an image. To get images, run GetAllImages.cs.
  ///
  /// Tags: MediaService.upload
  /// </summary>
  class UploadImage : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example uploads an image. To get images, run GetAllImages.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UploadImage();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the MediaService.
      MediaService mediaService = (MediaService) user.GetService(
          AdWordsService.v201003.MediaService);

      // Create image.
      Image image = new Image();
      image.data = MediaUtilities.GetAssetDataFromUrl(
          "https://sandbox.google.com/sandboximages/image.jpg");
      image.mediaTypeDb = MediaMediaType.IMAGE;
      image.name = "Sample Image";

      try {
        // Upload image.
        Media[] result = mediaService.upload(new Media[] {image});

        // Display image details.
        if (result != null && result.Length > 0) {
          foreach (Media temp in result) {
            Dictionary<MediaSize, Dimensions> dimensions = CreateMediaDimensionMap(temp.dimensions);
            Console.WriteLine("Image with id '{0}', dimensions '{1}x{2}', and MIME type '{3}'" +
                " was uploaded.", temp.mediaId, dimensions[MediaSize.FULL].width,
                dimensions[MediaSize.FULL].height, temp.mimeType);
          }
        } else {
          Console.WriteLine("No images were uploaded.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to upload images. Exception says \"{0}\"", ex.Message);
      }
    }

    /// <summary>
    /// Converts an array of Media_Size_DimensionsMapEntry into a dictionary.
    /// </summary>
    /// <param name="dimensions">The array of Media_Size_DimensionsMapEntry to be
    /// converted into a dictionary.</param>
    /// <returns>A dictionary with key as MediaSize, and value as Dimensions.
    /// </returns>
    private Dictionary<MediaSize, Dimensions> CreateMediaDimensionMap(
        Media_Size_DimensionsMapEntry[] dimensions) {
      Dictionary<MediaSize, Dimensions> mediaMap = new Dictionary<MediaSize, Dimensions>();
      foreach (Media_Size_DimensionsMapEntry dimension in dimensions) {
        mediaMap.Add(dimension.key, dimension.value);
      }
      return mediaMap;
    }
  }
}
