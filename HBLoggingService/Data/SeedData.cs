namespace HBLoggingService.Data;

using System.IO;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

// public static class SeedData
// {
//     public static async Task SeedDXCCAsync(IServiceProvider services)
//     {
//         var context = services.GetRequiredService<LoggingDbContext>();
//         if (!context.Dxcc.Any())
//         {
//             var json = await File.ReadAllTextAsync("SeedData/dxcc.json");
//             var dxccList = JsonSerializer.Deserialize<List<Dxcc>>(json);
//             context.Dxcc.AddRange(dxccList);
//             await context.SaveChangesAsync();
//         }
//     }
// }
// // goes in Program.cs
// using (var scope = app.Services.CreateScope())
// {
//     await SeedData.SeedDXCCAsync(scope.ServiceProvider);
// }
