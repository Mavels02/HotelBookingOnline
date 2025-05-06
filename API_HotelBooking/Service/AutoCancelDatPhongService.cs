using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using API_HotelBooking.Data;
using API_HotelBooking.Models;

public class AutoCancelDatPhongService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(30); // Kiểm tra mỗi 30 giây

    public AutoCancelDatPhongService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var now = DateTime.Now;
                var waitingDatPhongs = await context.DatPhongs
                    .Where(dp => dp.TrangThai == "Đang chờ thanh toán" &&
                                 EF.Functions.DateDiffMinute(dp.ThoiGianDat, now) >= 2)
                    .ToListAsync();

                foreach (var dp in waitingDatPhongs)
                {
                    context.DatPhongs.Remove(dp);

                    var phong = await context.Phongs.FindAsync(dp.MaP);
                    if (phong != null)
                        phong.TrangThai = "Còn trống";
                }

                await context.SaveChangesAsync();
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
