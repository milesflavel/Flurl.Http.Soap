![Icon](https://raw.githubusercontent.com/milesflavel/Flurl.Http.Soap/master/assets/noun_soap_3664421.png?1)
# Flurl.Http.Soap
Very basic extension for [Flurl](https://github.com/tmenier/Flurl) to support the SOAP protocol.

## Important
This is very much a half-baked, early implementation. It currently only implements a `ReceiveSoap` method, with a simple `SoapEnvelope` wrapper class.

## Usage
```
string soapXmlString = 
    @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
        <soap:Body>
            <RequestBody>
                <ApiField>Test</ApiField>
            </RequestBody>
        </soap:Body>
    </soap:Envelope>";

ResponseBody responseBodyContent = await "https://api.url/endpoint"
    .PostXmlAsync(soapXmlString)
    .ReceiveSoap<ResponseBody>();
```

## Thanks
* [Luk Vermeulen](https://github.com/lvermeulen) whose work on [Flurl.Http.Xml](https://github.com/lvermeulen/Flurl.Http.Xml) was the direct inspiration for this
* [soap](https://thenounproject.com/search/?q=soap&i=3664421) icon by [Giuditta Valentina Gentile](https://thenounproject.com/giuditta.gentile/) from [The Noun Project](https://thenounproject.com)
