using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using RVTR.Lodging.WebApi;
using Xunit;

namespace IntegrationTests
{
  public class IntegrationTester : IClassFixture<CustomWebApplicationFactoryInMemDB<Startup>>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactoryInMemDB<Startup> _factory;
    [Theory]
    [MemberData(nameof(StaticTestingData.PostRequests), MemberType = typeof(StaticTestingData))]
    public async void CheckPostResponse(string url, object data)
      {
      var httpContent = new StringContent(data.ToString());
      httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
      var r = await _client.PostAsync(url, httpContent);
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.Created || r.StatusCode == System.Net.HttpStatusCode.OK);
    }
      [Theory]
      [MemberData(nameof(StaticTestingData.GetRequests), MemberType = typeof(StaticTestingData))]
      public async void CheckGetResponse(string url)
      {
      var r = await _client.GetAsync(url);
      Console.WriteLine(await r.Content.ReadAsStringAsync());
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.OK);
      }
      public bool CheckPutResponse(HttpResponseMessage r)
      {
        throw new NotImplementedException();
      }
      [Theory]
      [MemberData(nameof(StaticTestingData.DeleteRequests), MemberType = typeof(StaticTestingData))]
      public async void CheckDeleteResponse(string url)
      {
      var r = await _client.DeleteAsync(url);
      //All deletes return OK, not proper but acceptable?
      Assert.True(r.StatusCode == System.Net.HttpStatusCode.Accepted || r.StatusCode == System.Net.HttpStatusCode.NoContent || r.StatusCode == System.Net.HttpStatusCode.OK);
      }

      public IntegrationTester(CustomWebApplicationFactoryInMemDB<Startup> factory)
      {
        _factory = factory;
        var c = _factory.CreateClient();
        this._client = c;
      }

    }
  }