﻿using BrainMate.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Core.Features.Foods.Commands.Models
{
	public class AddFoodCommand : IRequest<Response<string>>
	{
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public string? Type { get; set; }
		public TimeOnly? Time { get; set; }
		public IFormFile? Image { get; set; }
		public int? PatientId { get; set; }
	}
}
