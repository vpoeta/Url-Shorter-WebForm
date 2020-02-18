using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    private readonly string URL_SHORTER_SERVICE_ENDPOINT = "http://localhost:5000/api/v1/UrlShorter";
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = RouteData.Values["symbol"] as string;
        Regex r = new Regex("^[a-zA-Z0-9]*$");
        if (!string.IsNullOrWhiteSpace(code) && code.Length < 11 && r.IsMatch(code))
        {
            try
            {
                string[] response = UrlShorterService_Get(code);
                Response.Redirect(response[0]);
            }
            catch (Exception)
            {
                result.Text = "Error in communication with Url shorter Service!";
                result.ForeColor = Color.Red;
            }
        }
    }
    public void Submit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (!string.IsNullOrEmpty(txtLink.Text))
            {
                try
                {
                    string[] response = UrlShorterService_Post(txtLink.Text);
                    linkresult.Text = response[0];
                    linkresult.NavigateUrl = response[0];
                }
                catch (Exception)
                {
                    result.Text = "Error in communication with Url shorter Service!";
                    result.ForeColor = Color.Red;
                }
            }
        }
    }

    protected void CustomLinkValidate(object source, ServerValidateEventArgs args)
    {
        string url = args.Value;
        args.IsValid = false;
        Uri validatedUri;

        if (Uri.TryCreate(url, UriKind.Absolute, out validatedUri))
        {
            if (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps)
                args.IsValid = true;
        }
        return;

    }

    private string[] UrlShorterService_Post(string longUrl)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL_SHORTER_SERVICE_ENDPOINT);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = new JavaScriptSerializer().Serialize(new { LongUrl = longUrl });
            streamWriter.Write(json);
        }

        string result;
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }
        string[] s = result.Split(',');

        for (int i = 0; i < s.Count(); ++i)
            s[i] = s[i].Substring(s[i].IndexOf(":") + 2, s[i].LastIndexOf('"') - s[i].IndexOf(":") - 2);

        httpResponse.Close();
        return s;
    }

    private string[] UrlShorterService_Get(string code)
    {
        var endpoint = Path.Combine(URL_SHORTER_SERVICE_ENDPOINT, code);
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(endpoint);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";

        string result;
        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }
        string[] s = result.Split(',');

        for (int i = 0; i < s.Count(); ++i)
            s[i] = s[i].Substring(s[i].IndexOf(":") + 2, s[i].LastIndexOf('"') - s[i].IndexOf(":") - 2);

        httpResponse.Close();
        return s;
    }
}