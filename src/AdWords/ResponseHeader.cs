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

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdWords {
  /// <summary>
  /// SOAP Response header for AdWords API services.
  /// </summary>
  public class ResponseHeader : SoapHeaderBase {
    /// <summary>
    /// Request ID for this API call.
    /// </summary>
    private string requestIdField;

    /// <summary>
    /// Number of operations for this API call.
    /// </summary>
    private long? operationsField;

    /// <summary>
    /// Response time for this API call.
    /// </summary>
    private long? responseTimeField;

    /// <summary>
    /// Units consumed for this API call.
    /// </summary>
    private long? unitsField;

    /// <summary>
    /// Gets or sets the request id for this API call.
    /// </summary>
    public string requestId {
      get {
        return this.requestIdField;
      }
      set {
        this.requestIdField = value;
      }
    }

    /// <summary>
    /// Gets or sets the number of operations for this API call.
    /// </summary>
    public long? operations {
      get {
        return this.operationsField;
      }
      set {
        this.operationsField = value;
      }
    }

    /// <summary>
    /// Gets or sets the response time for this API call.
    /// </summary>
    public long? responseTime {
      get {
        return this.responseTimeField;
      }
      set {
        this.responseTimeField = value;
      }
    }

    /// <summary>
    /// Gets or sets the number of units consumed for this API call.
    /// </summary>
    public long? units {
      get {
        return this.unitsField;
      }
      set {
        this.unitsField = value;
      }
    }
  }
}
