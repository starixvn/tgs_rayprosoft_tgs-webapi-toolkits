# Tgs.WebApi.Toolkits

## 說明
用以輔助TGS專案 WebApi 開發套件

## Api Response 格式處理
TGS的api response 回傳格式依循以下格式

Success 時回傳格式：
```json
{
    "err": 0,
    "errmsg": "",
    "data": 回傳資料
}
```

Fail 時回傳格式：
```json
{
    "err": 錯誤碼
    "errmsg": "錯誤訊息",
    "errmsgid": "錯誤訊息多國語系Key"
}
```

這個套件要套用此效果需要在 MvcOptions 呼叫擴充方法 AddWrapResponseFilters，並且在 SwaggerGenOptions 呼叫擴充方法 ApplyTgsWebApiSupport

```csharp
builder.Services
	.AddControllers()
	.AddMvcOptions(mvcOptions => mvcOptions.AddWrapResponseFilters());

builder.Services
	.AddSwaggerGen(c =>
	{
		c.ApplyTgsWebApiSupport();
	});
```

## Http Status Code 調整
如果需要客製化Api的回傳Status Code，可以使用 SuccessStatusCodeAttribute 來更改 Api成功的回傳，當失敗時，使用ControllerBase的擴充方法 Fail 的多載函式來更改。

SuccessStatusCodeAttribute、FailStatusCodeAttribute 可以用來提供 swagger 產生額外的說明。

若要使用這些機制需要在 MvcOptions 呼叫擴充方法 AddWebApiToolkitFilters，並且在 SwaggerGenOptions 呼叫擴充方法 AddWebApiToolkitFilters

```csharp
builder.Services
	.AddControllers()
	.AddMvcOptions(mvcOptions => mvcOptions.AddWebApiToolkitFilters());

builder.Services
	.AddSwaggerGen(c =>
	{
		c.ApplyWebApiToolkitSupport();
	});
```
## HttpClient Logging
如果需要對Api呼叫 log 要求及回應時可以使用此擴充，使用方法有兩種：

在 HttpClientFactory 註冊使用：
```csharp
builder.Services
	.AddHttpClient<ServiceClient>()
	.AddLoggingHandler<ServiceClient>();
```

建立 HttpClient 時使用：
```csharp
new HttpClient(sp.ResolveLoggingHandler<TServiceClient>())
```

## HttpLogging
目前確認 Asp.Net Core 的 HttpLogging 如果在 api 本身沒有讀取 Request body 時會沒有記錄其內容，要解決的話需要強制讀取內容。
```csharp
app.Use(async (context, next) =>
{
	await next().ConfigureAwait(false);

	var logger = context.RequestServices.GetService<ILoggerFactory>()
		?.CreateLogger("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware");

	if (logger?.IsEnabled(LogLevel.Information) == true)
	{
		using var stream = new StreamReader(context.Request.Body);

		_ = await stream.ReadToEndAsync().ConfigureAwait(false);
	}
});
```
WebApi Toolkits提供簡便的方法：

```csharp
app.UseFlushRequestContent();
```
