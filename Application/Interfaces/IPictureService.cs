using Application.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPictureService
    {
        Task<IEnumerable<PictureDto>> GetPicturesByPostIdAsync(int postId);
        Task<PictureDto> GetPictureByIdAsync(int id);
        Task<PictureDto> AddPictureToPostAsync(int postId, IFormFile file);
        Task SetMainPicture(int postId, int id);
        Task DeletePictureAync(int id);
    }
}
