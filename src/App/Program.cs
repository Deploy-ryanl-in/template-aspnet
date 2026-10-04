var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/healthz", () => Results.Json(new { status = "ok" }));
app.MapGet("/", () => Results.Content("""
    <!doctype html><html lang="zh-Hant"><meta charset="utf-8"><title>Personal PaaS</title>
    <style>body{font:18px system-ui;background:#101820;color:#e8edf4;max-width:760px;margin:12vh auto;padding:32px}a{color:#6ae0bc}p{line-height:1.7}</style>
    <h1>你的 ASP.NET 项目，已经上线。</h1><p>修改代码、提交并推送，平台会自动部署新版本。</p>
    <p><a href="/api/hello?name=Ryan">示例 API</a> · <a href="/healthz">健康检查</a></p></html>
    """, "text/html; charset=utf-8"));
app.MapGet("/api/hello", (string? name) => Results.Json(Greeting.Create(name ?? "world")));
app.Run();
public partial class Program;
public static class Greeting
{
    public static GreetingResult Create(string name) => new($"Hello, {name[..Math.Min(name.Length, 80)]}!");
}
public sealed record GreetingResult(string Message);
