using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RCE_Providers.EscapeRoomProviders.DTOs;
using RCE_Providers.EscapeRoomProviders.Entities;
using RCE_Providers.EscapeRoomProviders.Repositories;
using RCE_Providers.Common;

namespace RCE_Providers.EscapeRoomProviders.Services;

public class EscapeRoomProviderService : IEscapeRoomProviderService
{
    private readonly IEscapeRoomProviderRepository _repository;
    private readonly IMapper _mapper;

    public EscapeRoomProviderService(IEscapeRoomProviderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EscapeRoomProviderResponseDTO> CreateProviderAsync(EscapeRoomProviderRequestDTO dto)
    {
        var provider = _mapper.Map<EscapeRoomProvider>(dto);
        var baseSlug = Slug.Generate(dto.Name);
        if (string.IsNullOrWhiteSpace(baseSlug)) baseSlug = "provider";
        var candidate = baseSlug;
        var suffix = 2;
        while (await _repository.GetBySlugAsync(candidate) != null)
        {
            candidate = $"{baseSlug}-{suffix}";
            suffix++;
        }
        provider.Slug = candidate;
        await _repository.AddAsync(provider);
        await _repository.SaveChangesAsync();

        return _mapper.Map<EscapeRoomProviderResponseDTO>(provider);
    }

    public async Task<bool> DeleteProviderBySlugAsync(string providerSlug)
    {
        var provider = await _repository.GetBySlugAsync(providerSlug);
        if (provider == null) return false;
        try
        {
            // Soft delete
            provider.IsDeleted = true;
            provider.DeletedAt = DateTime.UtcNow;
            _repository.Update(provider);
            await _repository.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> UpdateProviderBySlugAsync(string providerSlug, EscapeRoomProviderRequestDTO dto)
    {
        var provider = await _repository.GetBySlugAsync(providerSlug);
        if (provider == null) return false;
        try
        {
            _repository.Update(_mapper.Map(dto, provider));
            await _repository.SaveChangesAsync();

            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<EscapeRoomProviderResponseDTO>> GetAllProviders()
    {
        var providers = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<EscapeRoomProviderResponseDTO>>(providers);
    }

    public async Task<EscapeRoomProviderResponseDTO?> GetProviderBySlugAsync(string slug)
    {
        var provider = await _repository.GetBySlugAsync(slug);
        return _mapper.Map<EscapeRoomProviderResponseDTO>(provider);
    }
}