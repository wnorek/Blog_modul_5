using Application.DTO;
using Application.DTO.Attachments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAttachemntService
    {
        Task<IEnumerable<AttachmentDto>> GetAttachmentByPostIdAsync(int postId);
        Task<DownloadAttachmentDto> DownloadAttachmentByIdAsync(int id);
        Task<AttachmentDto> AddAttachmentToPostAsync(int postId, IFormFile file);
        Task DeleteAttachmentAsync(int id);
    }
}
