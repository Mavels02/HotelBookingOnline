﻿using API_HotelBooking.Models;
using API_HotelBooking.DTOs;

namespace API_HotelBooking.Service
{
	public interface IDatPhongService
	{
		Task<DatPhong> DatPhongAsync(DatPhongDTO dto);
		Task<IEnumerable<DatPhong>> GetAllAsync();
		
		Task<bool> HuyDatPhongAsync(int id);
		Task<DatPhongDTO> GetThongTinDatPhongAsync(int MaPMaPhong);
	}
}
