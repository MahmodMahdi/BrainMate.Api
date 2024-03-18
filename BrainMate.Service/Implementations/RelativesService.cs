using BrainMate.Data.Entities;
using BrainMate.Infrastructure.Interfaces;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Implementations
{
	public class RelativesService : IRelativesService
	{
		#region Fields
		private readonly IRelativesRepository _relativesRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _webHost;
		#endregion
		#region Constructors
		public RelativesService(IRelativesRepository relativesRepository,
			IHttpContextAccessor httpContextAccessor,
			IWebHostEnvironment webHost)
		{
			_relativesRepository = relativesRepository;
			_httpContextAccessor = httpContextAccessor;
			_webHost = webHost;
		}
		#endregion
		#region Handle Functions
		public async Task<List<Relatives>> GetRelativesListAsync()
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var relatives = await _relativesRepository.GetInstructorsAsync();
			foreach (var relative in relatives)
			{
				if (relative.Image != null)
				{
					relative.Image = baseUrl + relative.Image;
				}
			}
			return relatives;
		}
		#endregion
	}
}
