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
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public class AdWordsAppConfig : AppConfigBase {
    /// <summary>
    /// Key name for enableGzipCompression.
    /// </summary>
    private const string ENABLE_GZIP_COMPRESSION = "EnableGzipCompression";

    /// <summary>
    /// Key name for authToken.
    /// </summary>
    private const string AUTHTOKEN = "AuthToken";

    /// <summary>
    /// Key name for email.
    /// </summary>
    private const string EMAIL = "Email";

    /// <summary>
    /// Key name for password.
    /// </summary>
    private const string PASSWORD = "Password";

    /// <summary>
    /// Key name for clientEmail.
    /// </summary>
    private const string CLIENT_EMAIL = "ClientEmail";

    /// <summary>
    /// Key name for clientCustomerId.
    /// </summary>
    private const string CLIENT_CUSTOMER_ID = "ClientCustomerId";

    /// <summary>
    /// Key name for developerToken.
    /// </summary>
    private const string DEVELOPER_TOKEN = "DeveloperToken";

    /// <summary>
    /// Key name for applicationToken.
    /// </summary>
    private const string APPLICATION_TOKEN = "ApplicationToken";

    /// <summary>
    /// Key name for userAgent.
    /// </summary>
    private const string USER_AGENT = "UserAgent";

    /// <summary>
    /// Key name for Legacy AdWords API URL.
    /// </summary>
    private const string LEGACY_ADWORDSAPI_SERVER = "LegacyAdWordsApi.Server";

    /// <summary>
    /// Key name for AdWords API URL.
    /// </summary>
    private const string ADWORDSAPI_SERVER = "AdWordsApi.Server";

    /// <summary>
    /// Default value for Legacy AdWords API URL.
    /// </summary>
    private const string DEFAULT_LEGACY_ADWORDSAPI_SERVER = "https://adwords.google.com";

    /// <summary>
    /// Default value for AdWords API URL.
    /// </summary>
    private const string DEFAULT_ADWORDSAPI_SERVER = "https://adwords.google.com";

    /// <summary>
    /// Authtoken to be used in making API calls.
    /// </summary>
    private string authToken;

    /// <summary>
    /// Email to be used in getting AuthToken.
    /// </summary>
    private string email;

    /// <summary>
    /// Password to be used in getting AuthToken.
    /// </summary>
    private string password;

    /// <summary>
    /// ClientEmail to be used in SOAP headers.
    /// </summary>
    private string clientEmail;

    /// <summary>
    /// ClientCustomerId to be used in SOAP headers.
    /// </summary>
    private string clientCustomerId;

    /// <summary>
    /// DeveloperToken to be used in the SOAP header.
    /// </summary>
    private string developerToken;

    /// <summary>
    /// ApplicationToken to be used in the SOAP header.
    /// </summary>
    private string applicationToken;

    /// <summary>
    /// Useragent to be used in the SOAP header.
    /// </summary>
    private string userAgent;

    /// <summary>
    /// Url for Legacy AdWords API.
    /// </summary>
    private string legacyAdWordsApiServer;

    /// <summary>
    /// Url for AdWords API.
    /// </summary>
    private string adWordsApiServer;

    /// <summary>
    /// True, if gzip compression should be turned on for SOAP requests and
    /// responses.
    /// </summary>
    private bool enableGzipCompression;

    /// <summary>
    /// Gets or sets the auth token to be used in SOAP headers.
    /// </summary>
    public string AuthToken {
      get {
        return authToken;
      }
      set {
        authToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the email to be used in getting AuthToken.
    /// </summary>
    public string Email {
      get {
        return email;
      }
      set {
        email = value;
      }
    }

    /// <summary>
    /// Gets or sets the password to be used in getting AuthToken.
    /// </summary>
    public string Password {
      get {
        return password;
      }
      set {
        password = value;
      }
    }

    /// <summary>
    /// Gets or sets the client email to be used in SOAP headers.
    /// </summary>
    public string ClientEmail {
      get {
        return clientEmail;
      }
      set {
        clientEmail = value;
      }
    }

    /// <summary>
    /// Gets or sets the client customerId to be used in SOAP headers.
    /// </summary>
    public string ClientCustomerId {
      get {
        return clientCustomerId;
      }
      set {
        clientCustomerId = value;
      }
    }

    /// <summary>
    /// Gets or sets the developer token to be used in SOAP headers.
    /// </summary>
    public string DeveloperToken {
      get {
        return developerToken;
      }
      set {
        developerToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the application token to be used in SOAP headers.
    /// </summary>
    public string ApplicationToken {
      get {
        return applicationToken;
      }
      set {
        applicationToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the useragent to be used in SOAP headers.
    /// </summary>
    public string UserAgent {
      get {
        return userAgent;
      }
      set {
        userAgent = value;
      }
    }

    /// <summary>
    /// Gets or sets the url for Legacy AdWords API.
    /// </summary>
    public string LegacyAdWordsApiServer {
      get {
        return legacyAdWordsApiServer;
      }
      set {
        legacyAdWordsApiServer = value;
      }
    }

    /// <summary>
    /// Gets or sets the URL for AdWords API.
    /// </summary>
    public string AdWordsApiServer {
      get {
        return adWordsApiServer;
      }
      set {
        adWordsApiServer = value;
      }
    }

    /// <summary>
    /// Gets or sets whether gzip compression should be turned on for SOAP
    /// requests and responses.
    /// </summary>
    public bool EnableGzipCompression {
      get {
        return enableGzipCompression;
      }
      set {
        enableGzipCompression = value;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public AdWordsAppConfig() : base() {
      authToken = "";
      email = "";
      password = "";
      clientEmail = "";
      clientCustomerId = "";
      developerToken = "";
      applicationToken = "";
      userAgent = "";
      enableGzipCompression = true;
      shortNameField = "AwApi-DotNet";
      legacyAdWordsApiServer = DEFAULT_LEGACY_ADWORDSAPI_SERVER;
      adWordsApiServer = DEFAULT_ADWORDSAPI_SERVER;

      ReadSettings((Hashtable) ConfigurationManager.GetSection("AdWordsApi"));
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed App.config settings.</param>
    protected override void ReadSettings(Hashtable settings) {
      base.ReadSettings(settings);

      email = ReadSetting(settings, EMAIL, email);
      password = ReadSetting(settings, PASSWORD, password);
      clientEmail = ReadSetting(settings, CLIENT_EMAIL, clientEmail);
      clientCustomerId = ReadSetting(settings, CLIENT_CUSTOMER_ID, clientCustomerId);
      developerToken = ReadSetting(settings, DEVELOPER_TOKEN, developerToken);
      applicationToken = ReadSetting(settings, APPLICATION_TOKEN, applicationToken);
      authToken = ReadSetting(settings, AUTHTOKEN, authToken);
      userAgent = ReadSetting(settings, USER_AGENT, userAgent);
      bool.TryParse(ReadSetting(settings, ENABLE_GZIP_COMPRESSION,
          enableGzipCompression.ToString()), out enableGzipCompression);
      legacyAdWordsApiServer = ReadSetting(settings, LEGACY_ADWORDSAPI_SERVER,
          legacyAdWordsApiServer);
      adWordsApiServer = ReadSetting(settings, ADWORDSAPI_SERVER, adWordsApiServer);
    }
  }
}
